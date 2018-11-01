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
    public partial class PositionGuidenceForm : Form
    {
        private EGM_Monitor m;
        private int x = 0, y = 0, z = 0, x1 = 0, y1 = 0, z1 = 0;

        public PositionGuidenceForm()
        {
            InitializeComponent();
        }

        public PositionGuidenceForm(EGM_Monitor m)
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

        private void x_trackBar_Scroll(object sender, EventArgs e)
        {
            x = x1 + this.x_trackBar.Value;
            m.Xs = x;
            this.input_x.Text = $"{x}";
        }

        private void y_trackBar_Scroll(object sender, EventArgs e)
        {
            y = y1 + this.y_trackBar.Value;
            m.Ys = y;
            this.input_y.Text = $"{y}";
        }

        private void z_trackBar_Scroll(object sender, EventArgs e)
        {
            z = z1 + this.z_trackBar.Value;
            m.Zs = z;
            this.input_x.Text = $"{z}";
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
            if(m.X1 != x1 || m.Y1 != y1 || m.Z1 != z1)
            {
                x1 = m.X1;
                y1 = m.Y1;
                z1 = m.Z1;
                x = x1;
                y = y1;
                z = z1;
                m.Xs = x;
                m.Ys = y;
                m.Zs = z;
                this.x_min_label.Text = $"x min: {x1 - 100}";
                this.y_min_label.Text = $"y min: {y1 - 100}";
                this.z_min_label.Text = $"z min: {z1 - 100}";
                this.x_max_label.Text = $"x max: {x1 + 100}";
                this.y_max_label.Text = $"y max: {y1 + 100}";
                this.z_max_label.Text = $"z max: {z1 + 100}";
            }
            this.input_x.Text = $"{x}";
            this.input_y.Text = $"{y}";
            this.input_z.Text = $"{z}";
        }
    }
}
