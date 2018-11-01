using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Stations;
using abb.egm;

namespace EGM_Server
{
    /// <summary>Code-behind class for the EGM_Server Smart Component.</summary>
    public class CodeBehind : SmartComponentCodeBehind
    {
        EGM_Monitor monitor;
        EGM_Server server;
        bool simulationRunning = false;

        /// <summary>Called when the value of a dynamic property value has changed.</summary>
        public override void OnPropertyValueChanged(SmartComponent component, DynamicProperty changedProperty, Object oldValue)
        {
        }

        ///<summary>Called when the value of an I/O signal value has changed.</summary>
        public override void OnIOSignalValueChanged(SmartComponent component, IOSignal changedSignal)
        {
            if(changedSignal.Name == "ServerStart" && (1 ==(int) component.IOSignals["ServerStart"].Value))
            {
                //monitor = new EGM_Monitor();
                //server = new EGM_Server(monitor);
                //server.Start();
            }else if (changedSignal.Name == "ServerStart" && (0 == (int)component.IOSignals["ServerStart"].Value) && server != null)
            {
                //server.Stop();
            }
            if(changedSignal.Name == "TrapFinished")
            {
                int state = (int)component.IOSignals["TrapFinished"].Value;
                if(state == 1)
                {
                    monitor.Mode = EGM_Server.INACTIVE;
                    component.IOSignals["TrapFinished"].Value = 0;
                }
            }
            
        }

        ///<summary>Called during simulation.</summary>
        public override void OnSimulationStep(SmartComponent component, double simulationTime, double previousTime)
        {
            updateOutputSignals(component);
        }

        public override void OnSimulationStart(SmartComponent component)
        {
            base.OnSimulationStart(component);
            // this will allow one monitor to persist from simulation to simulation. I don't know if that's a good idea or a 
            // bad idea, but right now, the monitor will persist.
            if (monitor == null)
            {
                monitor = new EGM_Monitor();
            }
            
            server = new EGM_Server(monitor);
            server.Start();
            
            simulationRunning = true;
        }

        public override void OnSimulationStop(SmartComponent component)
        {
            server.Stop();
            resetOutputSignals(component);
            simulationRunning = false;
            base.OnSimulationStop(component);
        }

        public void resetOutputSignals(SmartComponent component)
        {
            component.IOSignals["Stream"].Value = 0;
            component.IOSignals["Guideance"].Value = 0;
            component.IOSignals["Correction"].Value = 0;
            component.IOSignals["Inactive"].Value = 0;
        }

        public void updateOutputSignals(SmartComponent component)
        {
            int mode = monitor.Mode;
            if (simulationRunning)
            {
                switch (mode)
                {
                    case EGM_Server.INACTIVE:
                        if ((int)component.IOSignals["Inactive"].Value == 0)
                        {
                            component.IOSignals["Stream"].Value = 0;
                            component.IOSignals["Guideance"].Value = 0;
                            component.IOSignals["Correction"].Value = 0;
                            component.IOSignals["Inactive"].Value = 1;
                        }
                        break;

                    case EGM_Server.POS_STREAM:
                        if ((int)component.IOSignals["Stream"].Value == 0)
                        {
                            component.IOSignals["Inactive"].Value = 0;
                            component.IOSignals["Guideance"].Value = 0;
                            component.IOSignals["Correction"].Value = 0;
                            component.IOSignals["Stream"].Value = 1;
                        }
                        break;

                    case EGM_Server.POS_GUIDE:
                        if ((int)component.IOSignals["Guideance"].Value == 0)
                        {
                            component.IOSignals["Inactive"].Value = 0;
                            component.IOSignals["Stream"].Value = 0;
                            component.IOSignals["Correction"].Value = 0;
                            component.IOSignals["Guideance"].Value = 1;
                        }
                        break;

                    case EGM_Server.PATH_CORR:
                        if ((int)component.IOSignals["Correction"].Value == 0)
                        {
                            component.IOSignals["Inactive"].Value = 0;
                            component.IOSignals["Stream"].Value = 0;
                            component.IOSignals["Guideance"].Value = 0;
                            component.IOSignals["Correction"].Value = 1;

                        }
                        break;
                }
            }
        }
    }

    ///<summary>This is the server that will communicate with the robot controller and relay instructions from the sensors</summary>
    /// <remarks> 
    /// Right now the model is that one thread will be dedicated to the EGM connection (Udp server) and other threads will handle 
    /// processing information from the sensors and managing what mode the server is in. This is very vague... I should make sure I 
    /// update this as the design of the component gets more concrete. 
    /// </remarks>
    class EGM_Server
    {
        public const int INACTIVE = 0;
        public const int POS_STREAM = 1;
        public const int POS_GUIDE = 2;
        public const int PATH_CORR = 3;
        
        private Thread EGM_GUI_Thread = null;
        private UdpClient EGM_Server_Client = null;
        private Thread EGM_Udp_Server_Thread = null;
        private Thread EGM_Server_Mode_Thread = null;
        private uint seqNumber = 0;
        private int mode;
        private int lastMode;
        private int EGM_Port_Nbr = 6510;
        public EGM_Monitor monitor;

        // Constructor -> Pass reference to a univarsal monitor
        public EGM_Server(EGM_Monitor m)
        {
            monitor = m;
        }

        private void EGM_Udp_Server_Thread_Start()
        {
            EGM_Server_Client = new UdpClient(EGM_Port_Nbr);
            var remoteEP = new IPEndPoint(IPAddress.Any, EGM_Port_Nbr);
            while (!monitor.StopServer)
            {
                byte[] data = null;
                try
                {
                    // Don't want to block this thread waiting for data because 
                    // if it's blocked here the thread can only be stoped by calling Abort() 
                    // and that crashes RobotStudio. So if we want RobotStudio to keep 
                    // working even after the server is stopped, we're gonna have to use a 
                    // tryFetch() instead of a doFetch(). The performance of the server might 
                    // improve if there is a sleep() that only activates the thread at about 
                    // the same rate that the cobot controller will send messages. 
                    if(EGM_Server_Client.Available > 0)
                    {
                        data = EGM_Server_Client.Receive(ref remoteEP);
                    }
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                if (data != null)
                {
                    // old de-serialize inbound message from robot
                    EgmRobot robot = EgmRobot.CreateBuilder().MergeFrom(data).Build();
                    // update current position of the robot
                    monitor.X = Convert.ToInt32(robot.FeedBack.Cartesian.Pos.X);
                    monitor.Y = Convert.ToInt32(robot.FeedBack.Cartesian.Pos.Y);
                    monitor.Z = Convert.ToInt32(robot.FeedBack.Cartesian.Pos.Z);
                    // update the planned position of the robot
                    monitor.Xp = Convert.ToInt32(robot.Planned.Cartesian.Pos.X);
                    monitor.Yp = Convert.ToInt32(robot.Planned.Cartesian.Pos.Y);
                    monitor.Zp = Convert.ToInt32(robot.Planned.Cartesian.Pos.Z);

                    // if an initial coordinate is needed, update the initial coordinate
                    if (monitor.GetStartCoordinate)
                    {
                        monitor.X1 = Convert.ToInt32(robot.FeedBack.Cartesian.Pos.X);
                        monitor.Y1 = Convert.ToInt32(robot.FeedBack.Cartesian.Pos.Y);
                        monitor.Z1 = Convert.ToInt32(robot.FeedBack.Cartesian.Pos.Z);
                        monitor.GetStartCoordinate = false;
                    }

                    // send messages back to the robot (different messages for different modes)
                    switch (monitor.Mode)
                    {
                        case INACTIVE:
                            // Tell the robot to go to its current position (this message data will
                            // probably be ignored by the robot, but in the edge case of ending a 
                            // position guidence or path correction operation, there can be a period 
                            // of time where the server has switched to inactive, but the robot controller 
                            // has not stopped the EGM process yet. Durring this time, the controller 
                            // expects messages from the server and if it does not receive these messages, 
                            // the RAPID kernel throws a runtime error (41822: No data from the UdpUc device).
                            // If this error is unhandeled, it causes a second runtime error (40228: Execution error).
                            // These errors stop the execution of the RAPID program. It may be possible to add 
                            // Error handlers into RAPID that will allow the RAPID program to continue running, 
                            // but from a design perspective, it may be better to just send dummy messages until 
                            // the controller ends the EGM process for on its end. 
                            monitor.Xs = monitor.X;
                            monitor.Ys = monitor.Y;
                            monitor.Zs = monitor.Z;
                            SendMessage(remoteEP);
                            break;

                        case POS_STREAM:
                            // Tell the robot to go to its current position (message data is ignored
                            // by the robot durring steaming, but the message has to be sent as an 
                            // ack for maintaining a connection) 
                            monitor.Xs = monitor.X;
                            monitor.Ys = monitor.Y;
                            monitor.Zs = monitor.Z;
                            SendMessage(remoteEP);
                            break;

                        case POS_GUIDE:
                            // Tell the robot to go to the currently provided position from the sensor
                            SendMessage(remoteEP);
                            break;

                        case PATH_CORR:
                            // Tell the robot to follow its current path with the offset from the sensor
                            // TODO: make sure that there is some explanation somewhere in the code about
                            // the robot controller expects from this message and why it's different from 
                            // the others
                            SendPathCorrection(remoteEP);
                            break;
                    }
                    
                }
            }
            EGM_Server_Client.Close();
        }
        
        // Send a message to the given IP end point
        public void SendMessage(IPEndPoint remoteEP)
        {
            EgmSensor.Builder sensor = EgmSensor.CreateBuilder();
            CreateSensorMessage(sensor);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                EgmSensor sensorMessage = sensor.Build();
                sensorMessage.WriteTo(memoryStream);

                // send the udp message to the robot
                int bytesSent = EGM_Server_Client.Send(memoryStream.ToArray(),
                                               (int)memoryStream.Length, remoteEP);
                if (bytesSent < 0)
                {
                    Console.WriteLine("Error send to robot");
                }
            }
        }
        public void SendPathCorrection(IPEndPoint remoteEP)
        {
            EgmSensorPathCorr.Builder sensorPathCorr = EgmSensorPathCorr.CreateBuilder();
            CreatePathCorrection(sensorPathCorr);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                EgmSensorPathCorr sensorPathCorrMessage = sensorPathCorr.Build();
                sensorPathCorrMessage.WriteTo(memoryStream);

                // send the udp message to the robot
                int bytesSent = EGM_Server_Client.Send(memoryStream.ToArray(),
                                               (int)memoryStream.Length, remoteEP);
                if (bytesSent < 0)
                {
                    Console.WriteLine("Error send to robot");
                }
            }
        }
        //Create a message to send to the robot
        void CreateSensorMessage(EgmSensor.Builder sensor)
        {
            // create a header
            EgmHeader.Builder hdr = new EgmHeader.Builder();
            hdr.SetSeqno(seqNumber++)
                .SetTm((uint)DateTime.Now.Ticks)
                .SetMtype(EgmHeader.Types.MessageType.MSGTYPE_CORRECTION);

            sensor.SetHeader(hdr);

            EgmPlanned.Builder planned = new EgmPlanned.Builder();
            EgmPose.Builder pos = new EgmPose.Builder();
            EgmQuaternion.Builder pq = new EgmQuaternion.Builder();
            EgmCartesian.Builder pc = new EgmCartesian.Builder();

            pc.SetX(monitor.Xs)
                .SetY(monitor.Ys)
                .SetZ(monitor.Zs);

            pq.SetU0(0.0)
                .SetU1(0.0)
                .SetU2(0.0)
                .SetU3(0.0);

            pos.SetPos(pc)
                .SetOrient(pq);

            planned.SetCartesian(pos);  // bind pos object to planned
            sensor.SetPlanned(planned); // bind planned to sensor object
            return;
        }
        void CreatePathCorrection(EgmSensorPathCorr.Builder sensorPathCorr)
        {
            // header -> needs to be MSGTYPE_PATH_CORRECTION
            EgmHeader.Builder hdr = new EgmHeader.Builder();
            hdr.SetSeqno(seqNumber++)
                .SetTm((uint)DateTime.Now.Ticks)
                .SetMtype(EgmHeader.Types.MessageType.MSGTYPE_PATH_CORRECTION);

            sensorPathCorr.SetHeader(hdr);

            EgmPathCorr.Builder pathCorr = new EgmPathCorr.Builder();
            EgmCartesian.Builder pos = new EgmCartesian.Builder();


            pos.SetX(monitor.Xs)
                .SetY(monitor.Xs)
                .SetZ(monitor.Xs);


            pathCorr.SetPos(pos)
                .SetAge((uint)DateTime.Now.Ticks);
            sensorPathCorr.SetPathCorr(pathCorr);
            return;
        }

        private void EGM_GUI_Thread_Start()
        {
            lastMode = 255;
            mode = monitor.Mode;
            while (true)
            {
                if (lastMode != mode)
                {
                    switch (mode)
                    {
                        case INACTIVE:
                                EGM_Server_Mode_Thread = new Thread(() => EGM_Inactive(monitor));
                                EGM_Server_Mode_Thread.Start();
                            break;

                        case POS_STREAM:
                                EGM_Server_Mode_Thread = new Thread(() => EGM_Position_Stream(monitor));
                                EGM_Server_Mode_Thread.Start();
                            break;

                        case POS_GUIDE:
                                EGM_Server_Mode_Thread = new Thread(() => EGM_Position_Guidance(monitor));
                                EGM_Server_Mode_Thread.Start();
                            break;

                        case PATH_CORR:
                                EGM_Server_Mode_Thread = new Thread(() => EGM_Path_Correction(monitor));
                                EGM_Server_Mode_Thread.Start();
                            break;
                    }
                }
                lastMode = mode;
                mode = monitor.Mode;
            }
        }

        private void EGM_Inactive(EGM_Monitor m)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new InactiveForm(m));
        }

        private void EGM_Position_Stream(EGM_Monitor m)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PositionStreamForm(m));
        }

        private void EGM_Position_Guidance(EGM_Monitor m)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            monitor.GetStartCoordinate = true; // <- START COORDINATE FLAG
            Application.Run(new PositionGuidenceForm(m));
            
        }

        private void EGM_Path_Correction(EGM_Monitor m)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PathCorrectionForm(m));
        }

        //private void Get_Robot_Feedback(UdpClient EGM_Server_Client, IPEndPoint remoteEP)
        //{
        //    byte[] data = null;
        //    try
        //    {
        //        data = EGM_Server_Client.Receive(ref remoteEP);
        //    }
        //    catch (Exception e)
        //    {
        //    }
        //    if (data != null)
        //    {
        //        EgmRobot robot = EgmRobot.CreateBuilder().MergeFrom(data).Build();
        //        monitor.X = Convert.ToInt32(robot.FeedBack.Cartesian.Pos.X);
        //        monitor.Y = Convert.ToInt32(robot.FeedBack.Cartesian.Pos.Y);
        //        monitor.Z = Convert.ToInt32(robot.FeedBack.Cartesian.Pos.Z);
        //        monitor.Xp = Convert.ToInt32(robot.Planned.Cartesian.Pos.X);
        //        monitor.Yp = Convert.ToInt32(robot.Planned.Cartesian.Pos.Y);
        //        monitor.Zp = Convert.ToInt32(robot.Planned.Cartesian.Pos.Z);
        //    }
        //}

        //private void SendMessage(UdpClient EGM_Server_Client, IPEndPoint remoteEP)
        //{
        //    EgmSensor.Builder sensor;
        //    EgmSensorPathCorr.Builder sensorPathCorr;
        //    switch (monitor.Mode)
        //    {
        //        case INACTIVE:
        //            // Don't send any message
        //            break;

        //        case POS_STREAM:
        //            sensor = EgmSensor.CreateBuilder();
        //            //CreateStreamMessage(sensor);
        //            using (MemoryStream memoryStream = new MemoryStream())
        //            {
        //                EgmSensor sensorMessage = sensor.Build();
        //                sensorMessage.WriteTo(memoryStream);

        //                // send the udp message to the robot
        //                int bytesSent = EGM_Server_Client.Send(memoryStream.ToArray(),
        //                                               (int)memoryStream.Length, remoteEP);
        //                if (bytesSent < 0)
        //                {
        //                    Console.WriteLine("Error send to robot");
        //                }
        //            }
        //            break;

        //        case POS_GUIDE:
        //            // TODO -> implement
        //            sensor = EgmSensor.CreateBuilder();
        //            //CreateGuideanceMessage(sensor);
        //            using (MemoryStream memoryStream = new MemoryStream())
        //            {
        //                EgmSensor sensorMessage = sensor.Build();
        //                sensorMessage.WriteTo(memoryStream);

        //                // send the udp message to the robot
        //                int bytesSent = EGM_Server_Client.Send(memoryStream.ToArray(),
        //                                               (int)memoryStream.Length, remoteEP);
        //                if (bytesSent < 0)
        //                {
        //                    Console.WriteLine("Error send to robot");
        //                }
        //            }
        //            break;

        //        case PATH_CORR:
        //            // TODO -> implement
        //            sensorPathCorr = EgmSensorPathCorr.CreateBuilder();
        //            //CreateCorrectionMessage(sensorPathCorr);
        //            using (MemoryStream memoryStream = new MemoryStream())
        //            {
        //                EgmSensorPathCorr sensorPathCorrMessage = sensorPathCorr.Build();
        //                sensorPathCorrMessage.WriteTo(memoryStream);

        //                // send the udp message to the robot
        //                int bytesSent = EGM_Server_Client.Send(memoryStream.ToArray(),
        //                                               (int)memoryStream.Length, remoteEP);
        //                if (bytesSent < 0)
        //                {
        //                    Console.WriteLine("Error send to robot");
        //                }
        //            }
        //            break;
        //    }
        //}

        public void Start()
        {
            monitor.StopServer = false;
            EGM_GUI_Thread = new Thread(new ThreadStart(EGM_GUI_Thread_Start));
            EGM_Udp_Server_Thread = new Thread(new ThreadStart(EGM_Udp_Server_Thread_Start));
            EGM_GUI_Thread.Start();
            EGM_Udp_Server_Thread.Start();
        }

        public void Stop()
        {
            //EGM_GUI_Thread.Abort();
            monitor.StopServer = true;
            //writeToTextFile($"Server Thread -> Monitor StopServer = {monitor.StopServer}");
            //EGM_Udp_Server_Thread.Abort();
            EGM_GUI_Thread = null;
            EGM_Udp_Server_Thread = null;
        }
        
    }

    ///<summary>This isn't reallya monitor yet, but eventually this is going to need one... so why not start now?</summary>
    public class EGM_Monitor
    {
        // server mode and operational variables
        private int mode = EGM_Server.INACTIVE;
        private volatile bool stopServer = false;
        private volatile bool getStartCoordinate = false;

        //robot starting coordinates
        private int x1 = 0;
        private int y1 = 0;
        private int z1 = 0;

        //robot current coordiantes
        private int x = 0;
        private int y = 0;
        private int z = 0;

        //robot planned coordinates
        private int xp = 0;
        private int yp = 0;
        private int zp = 0;

        //sensor provided coordinates
        private int xs = 0;
        private int ys = 0;
        private int zs = 0;

        // the getters and setters... this is really ugly, you should change this when you get a chance
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Z { get => z; set => z = value; }
        public int Xp { get => xp; set => xp = value; }
        public int Yp { get => yp; set => yp = value; }
        public int Zp { get => zp; set => zp = value; }
        public int Xs { get => xs; set => xs = value; }
        public int Ys { get => ys; set => ys = value; }
        public int Zs { get => zs; set => zs = value; }
        public int Mode { get => mode; set => mode = value; }
        public bool StopServer { get => stopServer; set => stopServer = value; }
        public bool GetStartCoordinate { get => getStartCoordinate; set => getStartCoordinate = value; }
        public int X1 { get => x1; set => x1 = value; }
        public int Y1 { get => y1; set => y1 = value; }
        public int Z1 { get => z1; set => z1 = value; }
    }
}

