namespace SubPointSolutions.Shelly.Desktop.Wf.Controls
{
    partial class WfStartPageControl
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
            this.pLeft = new System.Windows.Forms.Panel();
            this.pProjectActions = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lAppName = new System.Windows.Forms.Label();
            this.pContent = new System.Windows.Forms.Panel();
            this.pLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // pLeft
            // 
            this.pLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.pLeft.Controls.Add(this.pProjectActions);
            this.pLeft.Controls.Add(this.label1);
            this.pLeft.Controls.Add(this.lAppName);
            this.pLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pLeft.Location = new System.Drawing.Point(0, 0);
            this.pLeft.Name = "pLeft";
            this.pLeft.Padding = new System.Windows.Forms.Padding(10);
            this.pLeft.Size = new System.Drawing.Size(300, 496);
            this.pLeft.TabIndex = 0;
            // 
            // pProjectActions
            // 
            this.pProjectActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pProjectActions.Location = new System.Drawing.Point(13, 58);
            this.pProjectActions.Name = "pProjectActions";
            this.pProjectActions.Size = new System.Drawing.Size(274, 425);
            this.pProjectActions.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(13, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 2);
            this.label1.TabIndex = 1;
            // 
            // lAppName
            // 
            this.lAppName.AutoSize = true;
            this.lAppName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lAppName.ForeColor = System.Drawing.Color.White;
            this.lAppName.Location = new System.Drawing.Point(7, 10);
            this.lAppName.Name = "lAppName";
            this.lAppName.Size = new System.Drawing.Size(126, 31);
            this.lAppName.TabIndex = 0;
            this.lAppName.Text = "Welcome";
            // 
            // pContent
            // 
            this.pContent.BackColor = System.Drawing.Color.White;
            this.pContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pContent.Location = new System.Drawing.Point(300, 0);
            this.pContent.Name = "pContent";
            this.pContent.Size = new System.Drawing.Size(436, 496);
            this.pContent.TabIndex = 1;
            // 
            // WfStartPageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pContent);
            this.Controls.Add(this.pLeft);
            this.Name = "WfStartPageControl";
            this.Size = new System.Drawing.Size(736, 496);
            this.pLeft.ResumeLayout(false);
            this.pLeft.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pLeft;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lAppName;
        private System.Windows.Forms.Panel pContent;
        private System.Windows.Forms.FlowLayoutPanel pProjectActions;
    }
}
