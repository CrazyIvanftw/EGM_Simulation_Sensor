namespace EGM_Server
{
    partial class InactiveForm
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
            this.position_stream_button = new System.Windows.Forms.Button();
            this.position_guidance_button = new System.Windows.Forms.Button();
            this.path_correction_button = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // position_stream_button
            // 
            this.position_stream_button.Location = new System.Drawing.Point(38, 73);
            this.position_stream_button.Name = "position_stream_button";
            this.position_stream_button.Size = new System.Drawing.Size(146, 23);
            this.position_stream_button.TabIndex = 0;
            this.position_stream_button.Text = "Position Stream";
            this.position_stream_button.UseVisualStyleBackColor = true;
            this.position_stream_button.Click += new System.EventHandler(this.position_stream_button_Click);
            // 
            // position_guidance_button
            // 
            this.position_guidance_button.Location = new System.Drawing.Point(38, 134);
            this.position_guidance_button.Name = "position_guidance_button";
            this.position_guidance_button.Size = new System.Drawing.Size(146, 23);
            this.position_guidance_button.TabIndex = 1;
            this.position_guidance_button.Text = "Position Guidence";
            this.position_guidance_button.UseVisualStyleBackColor = true;
            this.position_guidance_button.Click += new System.EventHandler(this.position_guidance_button_Click);
            // 
            // path_correction_button
            // 
            this.path_correction_button.Location = new System.Drawing.Point(38, 194);
            this.path_correction_button.Name = "path_correction_button";
            this.path_correction_button.Size = new System.Drawing.Size(146, 23);
            this.path_correction_button.TabIndex = 2;
            this.path_correction_button.Text = "Path Correction";
            this.path_correction_button.UseVisualStyleBackColor = true;
            this.path_correction_button.Click += new System.EventHandler(this.path_correction_button_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // InactiveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 322);
            this.Controls.Add(this.path_correction_button);
            this.Controls.Add(this.position_guidance_button);
            this.Controls.Add(this.position_stream_button);
            this.Name = "InactiveForm";
            this.Text = "InactiveForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button position_stream_button;
        private System.Windows.Forms.Button position_guidance_button;
        private System.Windows.Forms.Button path_correction_button;
        private System.Windows.Forms.Timer timer1;
    }
}