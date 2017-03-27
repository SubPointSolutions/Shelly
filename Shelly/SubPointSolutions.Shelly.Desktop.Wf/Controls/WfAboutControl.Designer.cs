namespace SubPointSolutions.Shelly.Desktop.Wf.Controls
{
    partial class WfAboutControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pTop = new System.Windows.Forms.Panel();
            this.lAppTitle = new System.Windows.Forms.Label();
            this.pContent = new System.Windows.Forms.Panel();
            this.gbSystem = new System.Windows.Forms.GroupBox();
            this.lAppVersion = new System.Windows.Forms.Label();
            this.pTop.SuspendLayout();
            this.pContent.SuspendLayout();
            this.gbSystem.SuspendLayout();
            this.SuspendLayout();
            // 
            // pTop
            // 
            this.pTop.Controls.Add(this.lAppTitle);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(350, 50);
            this.pTop.TabIndex = 0;
            // 
            // lAppTitle
            // 
            this.lAppTitle.AutoSize = true;
            this.lAppTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lAppTitle.Location = new System.Drawing.Point(3, 9);
            this.lAppTitle.Name = "lAppTitle";
            this.lAppTitle.Size = new System.Drawing.Size(110, 29);
            this.lAppTitle.TabIndex = 0;
            this.lAppTitle.Text = "App Title";
            // 
            // pContent
            // 
            this.pContent.Controls.Add(this.gbSystem);
            this.pContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pContent.Location = new System.Drawing.Point(0, 50);
            this.pContent.Name = "pContent";
            this.pContent.Size = new System.Drawing.Size(350, 100);
            this.pContent.TabIndex = 1;
            // 
            // gbSystem
            // 
            this.gbSystem.Controls.Add(this.lAppVersion);
            this.gbSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSystem.Location = new System.Drawing.Point(0, 0);
            this.gbSystem.Name = "gbSystem";
            this.gbSystem.Size = new System.Drawing.Size(350, 100);
            this.gbSystem.TabIndex = 1;
            this.gbSystem.TabStop = false;
            this.gbSystem.Text = "System Info";
            // 
            // lAppVersion
            // 
            this.lAppVersion.AutoSize = true;
            this.lAppVersion.Location = new System.Drawing.Point(6, 16);
            this.lAppVersion.Name = "lAppVersion";
            this.lAppVersion.Size = new System.Drawing.Size(45, 13);
            this.lAppVersion.TabIndex = 0;
            this.lAppVersion.Text = "Version:";
            // 
            // WfAboutControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pContent);
            this.Controls.Add(this.pTop);
            this.Name = "WfAboutControl";
            this.Size = new System.Drawing.Size(350, 150);
            this.pTop.ResumeLayout(false);
            this.pTop.PerformLayout();
            this.pContent.ResumeLayout(false);
            this.gbSystem.ResumeLayout(false);
            this.gbSystem.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pTop;
        private System.Windows.Forms.Panel pContent;
        private System.Windows.Forms.Label lAppTitle;
        private System.Windows.Forms.Label lAppVersion;
        private System.Windows.Forms.GroupBox gbSystem;
    }
}
