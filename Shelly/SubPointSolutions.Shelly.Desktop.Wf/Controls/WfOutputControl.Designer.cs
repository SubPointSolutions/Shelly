namespace SubPointSolutions.Shelly.Desktop.Wf.Controls
{
    partial class WfOutputControl
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
            this.lbLog = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lbLog
            // 
            this.lbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLog.FormattingEnabled = true;
            this.lbLog.Items.AddRange(new object[] {
            "[INFO] sdfsf sdfsd sdfd",
            "[INFO] sdfsf sdfsd sdfd",
            "[INFO] sdfsf sdfsd sdfd",
            "",
            "[INFO] sdfsf sdfsd sdfd",
            "[INFO] sdfsf sdfsd sdfd",
            "",
            "[INFO] sdfsf sdfsd sdfd",
            "[INFO] sdfsf sdfsd sdfd"});
            this.lbLog.Location = new System.Drawing.Point(0, 0);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(394, 359);
            this.lbLog.TabIndex = 0;
            // 
            // WfOutputControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbLog);
            this.Name = "WfOutputControl";
            this.Size = new System.Drawing.Size(394, 359);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbLog;
    }
}
