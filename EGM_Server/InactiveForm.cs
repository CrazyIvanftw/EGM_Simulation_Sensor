using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace EGM_Server
{
    public partial class InactiveForm : Form
    {

        private EGM_Monitor m;

        public InactiveForm()
        {
            InitializeComponent();
        }

        public InactiveForm(EGM_Monitor m)
        {
            InitializeComponent();
            this.m = m;
        }

        private void position_stream_button_Click(object sender, EventArgs e)
        {
            m.Mode = EGM_Server.POS_STREAM;
            this.Invoke((MethodInvoker)delegate
            {
                this.Close();
            });
        }

        private void position_guidance_button_Click(object sender, EventArgs e)
        {
            m.Mode = EGM_Server.POS_GUIDE;
            this.Invoke((MethodInvoker)delegate
            {
                this.Close();
            });
        }

        private void path_correction_button_Click(object sender, EventArgs e)
        {
            m.Mode = EGM_Server.PATH_CORR;
            this.Invoke((MethodInvoker)delegate
            {
                this.Close();
            });
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (m.StopServer)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.Close();
                });
            }
        }
    }
}
