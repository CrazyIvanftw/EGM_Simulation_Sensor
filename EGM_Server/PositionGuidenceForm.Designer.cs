namespace EGM_Server
{
    partial class PositionGuidenceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.end_button = new System.Windows.Forms.Button();
            this.planned_pos = new System.Windows.Forms.Label();
            this.feedback_pos = new System.Windows.Forms.Label();
            this.plan_label = new System.Windows.Forms.Label();
            this.curr_label = new System.Windows.Forms.Label();
            this.Feedback_label = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.x_trackBar = new System.Windows.Forms.TrackBar();
            this.x_min_label = new System.Windows.Forms.Label();
            this.x_max_label = new System.Windows.Forms.Label();
            this.input_x = new System.Windows.Forms.Label();
            this.input_y = new System.Windows.Forms.Label();
            this.y_max_label = new System.Windows.Forms.Label();
            this.y_min_label = new System.Windows.Forms.Label();
            this.y_trackBar = new System.Windows.Forms.TrackBar();
            this.input_z = new System.Windows.Forms.Label();
            this.z_max_label = new System.Windows.Forms.Label();
            this.z_min_label = new System.Windows.Forms.Label();
            this.z_trackBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.x_trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.y_trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.z_trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // end_button
            // 
            this.end_button.Location = new System.Drawing.Point(4, 83);
            this.end_button.Name = "end_button";
            this.end_button.Size = new System.Drawing.Size(142, 23);
            this.end_button.TabIndex = 0;
            this.end_button.Text = "End Position Guidance";
            this.end_button.UseVisualStyleBackColor = true;
            this.end_button.Click += new System.EventHandler(this.end_button_Click);
            // 
            // planned_pos
            // 
            this.planned_pos.AutoSize = true;
            this.planned_pos.Location = new System.Drawing.Point(103, 52);
            this.planned_pos.Name = "planned_pos";
            this.planned_pos.Size = new System.Drawing.Size(43, 13);
            this.planned_pos.TabIndex = 10;
            this.planned_pos.Text = "(0, 0, 0)";
            // 
            // feedback_pos
            // 
            this.feedback_pos.AutoSize = true;
            this.feedback_pos.Location = new System.Drawing.Point(103, 30);
            this.feedback_pos.Name = "feedback_pos";
            this.feedback_pos.Size = new System.Drawing.Size(43, 13);
            this.feedback_pos.TabIndex = 9;
            this.feedback_pos.Text = "(0, 0, 0)";
            // 
            // plan_label
            // 
            this.plan_label.AutoSize = true;
            this.plan_label.Location = new System.Drawing.Point(10, 52);
            this.plan_label.Name = "plan_label";
            this.plan_label.Size = new System.Drawing.Size(79, 13);
            this.plan_label.TabIndex = 8;
            this.plan_label.Text = "Planned (x,y,z):";
            // 
            // curr_label
            // 
            this.curr_label.AutoSize = true;
            this.curr_label.Location = new System.Drawing.Point(15, 30);
            this.curr_label.Name = "curr_label";
            this.curr_label.Size = new System.Drawing.Size(74, 13);
            this.curr_label.TabIndex = 7;
            this.curr_label.Text = "Current (x,y,z):";
            // 
            // Feedback_label
            // 
            this.Feedback_label.AutoSize = true;
            this.Feedback_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Feedback_label.Location = new System.Drawing.Point(12, 9);
            this.Feedback_label.Name = "Feedback_label";
            this.Feedback_label.Size = new System.Drawing.Size(131, 17);
            this.Feedback_label.TabIndex = 6;
            this.Feedback_label.Text = "Robot Feedback:";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // x_trackBar
            // 
            this.x_trackBar.Location = new System.Drawing.Point(94, 153);
            this.x_trackBar.Maximum = 100;
            this.x_trackBar.Minimum = -100;
            this.x_trackBar.Name = "x_trackBar";
            this.x_trackBar.Size = new System.Drawing.Size(188, 45);
            this.x_trackBar.TabIndex = 11;
            this.x_trackBar.Scroll += new System.EventHandler(this.x_trackBar_Scroll);
            // 
            // x_min_label
            // 
            this.x_min_label.AutoSize = true;
            this.x_min_label.Location = new System.Drawing.Point(13, 153);
            this.x_min_label.Name = "x_min_label";
            this.x_min_label.Size = new System.Drawing.Size(75, 13);
            this.x_min_label.TabIndex = 12;
            this.x_min_label.Text = "x min: x1 - 100";
            // 
            // x_max_label
            // 
            this.x_max_label.AutoSize = true;
            this.x_max_label.Location = new System.Drawing.Point(292, 153);
            this.x_max_label.Name = "x_max_label";
            this.x_max_label.Size = new System.Drawing.Size(81, 13);
            this.x_max_label.TabIndex = 13;
            this.x_max_label.Text = "x max: x1 + 100";
            // 
            // input_x
            // 
            this.input_x.AutoSize = true;
            this.input_x.Location = new System.Drawing.Point(182, 137);
            this.input_x.Name = "input_x";
            this.input_x.Size = new System.Drawing.Size(13, 13);
            this.input_x.TabIndex = 14;
            this.input_x.Text = "0";
            // 
            // input_y
            // 
            this.input_y.AutoSize = true;
            this.input_y.Location = new System.Drawing.Point(182, 198);
            this.input_y.Name = "input_y";
            this.input_y.Size = new System.Drawing.Size(13, 13);
            this.input_y.TabIndex = 18;
            this.input_y.Text = "0";
            // 
            // y_max_label
            // 
            this.y_max_label.AutoSize = true;
            this.y_max_label.Location = new System.Drawing.Point(292, 214);
            this.y_max_label.Name = "y_max_label";
            this.y_max_label.Size = new System.Drawing.Size(81, 13);
            this.y_max_label.TabIndex = 17;
            this.y_max_label.Text = "x max: x1 + 100";
            // 
            // y_min_label
            // 
            this.y_min_label.AutoSize = true;
            this.y_min_label.Location = new System.Drawing.Point(13, 214);
            this.y_min_label.Name = "y_min_label";
            this.y_min_label.Size = new System.Drawing.Size(75, 13);
            this.y_min_label.TabIndex = 16;
            this.y_min_label.Text = "y min: y1 - 100";
            // 
            // y_trackBar
            // 
            this.y_trackBar.Location = new System.Drawing.Point(94, 214);
            this.y_trackBar.Maximum = 100;
            this.y_trackBar.Minimum = -100;
            this.y_trackBar.Name = "y_trackBar";
            this.y_trackBar.Size = new System.Drawing.Size(188, 45);
            this.y_trackBar.TabIndex = 15;
            this.y_trackBar.Scroll += new System.EventHandler(this.y_trackBar_Scroll);
            // 
            // input_z
            // 
            this.input_z.AutoSize = true;
            this.input_z.Location = new System.Drawing.Point(182, 258);
            this.input_z.Name = "input_z";
            this.input_z.Size = new System.Drawing.Size(13, 13);
            this.input_z.TabIndex = 22;
            this.input_z.Text = "0";
            // 
            // z_max_label
            // 
            this.z_max_label.AutoSize = true;
            this.z_max_label.Location = new System.Drawing.Point(292, 274);
            this.z_max_label.Name = "z_max_label";
            this.z_max_label.Size = new System.Drawing.Size(81, 13);
            this.z_max_label.TabIndex = 21;
            this.z_max_label.Text = "z max: z1 + 100";
            // 
            // z_min_label
            // 
            this.z_min_label.AutoSize = true;
            this.z_min_label.Location = new System.Drawing.Point(13, 274);
            this.z_min_label.Name = "z_min_label";
            this.z_min_label.Size = new System.Drawing.Size(75, 13);
            this.z_min_label.TabIndex = 20;
            this.z_min_label.Text = "z min: z1 - 100";
            // 
            // z_trackBar
            // 
            this.z_trackBar.Location = new System.Drawing.Point(94, 274);
            this.z_trackBar.Maximum = 100;
            this.z_trackBar.Minimum = -100;
            this.z_trackBar.Name = "z_trackBar";
            this.z_trackBar.Size = new System.Drawing.Size(188, 45);
            this.z_trackBar.TabIndex = 19;
            this.z_trackBar.Scroll += new System.EventHandler(this.z_trackBar_Scroll);
            // 
            // PositionGuidenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 329);
            this.Controls.Add(this.input_z);
            this.Controls.Add(this.z_max_label);
            this.Controls.Add(this.z_min_label);
            this.Controls.Add(this.z_trackBar);
            this.Controls.Add(this.input_y);
            this.Controls.Add(this.y_max_label);
            this.Controls.Add(this.y_min_label);
            this.Controls.Add(this.y_trackBar);
            this.Controls.Add(this.input_x);
            this.Controls.Add(this.x_max_label);
            this.Controls.Add(this.x_min_label);
            this.Controls.Add(this.x_trackBar);
            this.Controls.Add(this.planned_pos);
            this.Controls.Add(this.feedback_pos);
            this.Controls.Add(this.plan_label);
            this.Controls.Add(this.curr_label);
            this.Controls.Add(this.Feedback_label);
            this.Controls.Add(this.end_button);
            this.Name = "PositionGuidenceForm";
            this.Text = "PositionGuidenceForm";
            ((System.ComponentModel.ISupportInitialize)(this.x_trackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.y_trackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.z_trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button end_button;
        private System.Windows.Forms.Label planned_pos;
        private System.Windows.Forms.Label feedback_pos;
        private System.Windows.Forms.Label plan_label;
        private System.Windows.Forms.Label curr_label;
        private System.Windows.Forms.Label Feedback_label;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TrackBar x_trackBar;
        private System.Windows.Forms.Label x_min_label;
        private System.Windows.Forms.Label x_max_label;
        private System.Windows.Forms.Label input_x;
        private System.Windows.Forms.Label input_y;
        private System.Windows.Forms.Label y_max_label;
        private System.Windows.Forms.Label y_min_label;
        private System.Windows.Forms.TrackBar y_trackBar;
        private System.Windows.Forms.Label input_z;
        private System.Windows.Forms.Label z_max_label;
        private System.Windows.Forms.Label z_min_label;
        private System.Windows.Forms.TrackBar z_trackBar;
    }
}