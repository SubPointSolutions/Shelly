namespace SubPointSolutions.Shelly.Desktop.Wf.Controls
{
    partial class WfSettingsEditorControl
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
            this.pOptions = new System.Windows.Forms.Panel();
            this.pProperties = new System.Windows.Forms.Panel();
            this.tvOptions = new System.Windows.Forms.TreeView();
            this.pOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pOptions
            // 
            this.pOptions.Controls.Add(this.tvOptions);
            this.pOptions.Dock = System.Windows.Forms.DockStyle.Left;
            this.pOptions.Location = new System.Drawing.Point(5, 5);
            this.pOptions.Name = "pOptions";
            this.pOptions.Size = new System.Drawing.Size(230, 347);
            this.pOptions.TabIndex = 0;
            // 
            // pProperties
            // 
            this.pProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pProperties.Location = new System.Drawing.Point(235, 5);
            this.pProperties.Name = "pProperties";
            this.pProperties.Size = new System.Drawing.Size(471, 347);
            this.pProperties.TabIndex = 1;
            // 
            // tvOptions
            // 
            this.tvOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvOptions.Location = new System.Drawing.Point(0, 0);
            this.tvOptions.Name = "tvOptions";
            this.tvOptions.Size = new System.Drawing.Size(230, 347);
            this.tvOptions.TabIndex = 0;
            // 
            // WfSettingsEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pProperties);
            this.Controls.Add(this.pOptions);
            this.Name = "WfSettingsEditorControl";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(711, 357);
            this.pOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pOptions;
        private System.Windows.Forms.TreeView tvOptions;
        private System.Windows.Forms.Panel pProperties;
    }
}
