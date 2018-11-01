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
    public partial class PositionStreamForm : Form
    {
        private EGM_Monitor m;

        public PositionStreamForm()
        {
            InitializeComponent();
        }

        public PositionStreamForm(EGM_Monitor m)
        {
            InitializeComponent();
            this.m = m;
        }

        private void end_button_Click(object sender, EventArgs e)
        {
            m.Mode = EGM_Server.INACTIVE;
            this.Invoke((MethodInvoker)delegate
            {
                this.Close();
            });
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.feedback_pos.Text = $"({m.X}, {m.Y}, {m.Z})";
            this.planned_pos.Text = $"({m.Xp}, {m.Yp}, {m.Zp})";

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
