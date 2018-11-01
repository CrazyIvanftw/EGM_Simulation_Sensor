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
    public partial class PathCorrectionForm : Form
    {
        private EGM_Monitor m;

        public PathCorrectionForm()
        {
            InitializeComponent();
        }

        public PathCorrectionForm(EGM_Monitor m)
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
