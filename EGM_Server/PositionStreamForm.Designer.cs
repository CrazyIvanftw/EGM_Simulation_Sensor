namespace EGM_Server
{
    partial class PositionStreamForm
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
            this.Feedback_label = new System.Windows.Forms.Label();
            this.curr_label = new System.Windows.Forms.Label();
            this.plan_label = new System.Windows.Forms.Label();
            this.feedback_pos = new System.Windows.Forms.Label();
            this.planned_pos = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // end_button
            // 
            this.end_button.Location = new System.Drawing.Point(27, 94);
            this.end_button.Name = "end_button";
            this.end_button.Size = new System.Drawing.Size(148, 23);
            this.end_button.TabIndex = 0;
            this.end_button.Text = "End Position Stream";
            this.end_button.UseVisualStyleBackColor = true;
            this.end_button.Click += new System.EventHandler(this.end_button_Click);
            // 
            // Feedback_label
            // 
            this.Feedback_label.AutoSize = true;
            this.Feedback_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Feedback_label.Location = new System.Drawing.Point(26, 13);
            this.Feedback_label.Name = "Feedback_label";
            this.Feedback_label.Size = new System.Drawing.Size(131, 17);
            this.Feedback_label.TabIndex = 1;
            this.Feedback_label.Text = "Robot Feedback:";
            // 
            // curr_label
            // 
            this.curr_label.AutoSize = true;
            this.curr_label.Location = new System.Drawing.Point(29, 34);
            this.curr_label.Name = "curr_label";
            this.curr_label.Size = new System.Drawing.Size(74, 13);
            this.curr_label.TabIndex = 2;
            this.curr_label.Text = "Current (x,y,z):";
            // 
            // plan_label
            // 
            this.plan_label.AutoSize = true;
            this.plan_label.Location = new System.Drawing.Point(24, 56);
            this.plan_label.Name = "plan_label";
            this.plan_label.Size = new System.Drawing.Size(79, 13);
            this.plan_label.TabIndex = 3;
            this.plan_label.Text = "Planned (x,y,z):";
            // 
            // feedback_pos
            // 
            this.feedback_pos.AutoSize = true;
            this.feedback_pos.Location = new System.Drawing.Point(117, 34);
            this.feedback_pos.Name = "feedback_pos";
            this.feedback_pos.Size = new System.Drawing.Size(43, 13);
            this.feedback_pos.TabIndex = 4;
            this.feedback_pos.Text = "(0, 0, 0)";
            // 
            // planned_pos
            // 
            this.planned_pos.AutoSize = true;
            this.planned_pos.Location = new System.Drawing.Point(117, 56);
            this.planned_pos.Name = "planned_pos";
            this.planned_pos.Size = new System.Drawing.Size(43, 13);
            this.planned_pos.TabIndex = 5;
            this.planned_pos.Text = "(0, 0, 0)";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // PositionStreamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 153);
            this.Controls.Add(this.planned_pos);
            this.Controls.Add(this.feedback_pos);
            this.Controls.Add(this.plan_label);
            this.Controls.Add(this.curr_label);
            this.Controls.Add(this.Feedback_label);
            this.Controls.Add(this.end_button);
            this.Name = "PositionStreamForm";
            this.Text = "PositionStreamForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button end_button;
        private System.Windows.Forms.Label Feedback_label;
        private System.Windows.Forms.Label curr_label;
        private System.Windows.Forms.Label plan_label;
        private System.Windows.Forms.Label feedback_pos;
        private System.Windows.Forms.Label planned_pos;
        private System.Windows.Forms.Timer timer1;
    }
}