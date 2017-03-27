namespace Quark.Desktop.Controls
{
    partial class ShModalDialogControlBase
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
            this.pMainContent = new System.Windows.Forms.Panel();
            this.pBottom = new System.Windows.Forms.Panel();
            this.bOk = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.pBody = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pBottom.SuspendLayout();
            this.pBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // pMainContent
            // 
            this.pMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMainContent.Location = new System.Drawing.Point(0, 0);
            this.pMainContent.Name = "pMainContent";
            this.pMainContent.Size = new System.Drawing.Size(625, 314);
            this.pMainContent.TabIndex = 1;
            // 
            // pBottom
            // 
            this.pBottom.Controls.Add(this.bOk);
            this.pBottom.Controls.Add(this.bCancel);
            this.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBottom.Location = new System.Drawing.Point(0, 314);
            this.pBottom.Name = "pBottom";
            this.pBottom.Size = new System.Drawing.Size(625, 57);
            this.pBottom.TabIndex = 6;
            // 
            // bOk
            // 
            this.bOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bOk.Location = new System.Drawing.Point(457, 15);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(75, 23);
            this.bOk.TabIndex = 0;
            this.bOk.Text = "OK";
            this.bOk.UseVisualStyleBackColor = true;
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.Location = new System.Drawing.Point(538, 15);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 0;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // pBody
            // 
            this.pBody.Controls.Add(this.pMainContent);
            this.pBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pBody.Location = new System.Drawing.Point(0, 0);
            this.pBody.Name = "pBody";
            this.pBody.Size = new System.Drawing.Size(625, 314);
            this.pBody.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 312);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(625, 2);
            this.label1.TabIndex = 1;
            // 
            // QModalDialogControlBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pBody);
            this.Controls.Add(this.pBottom);
            this.Name = "QModalDialogControlBase";
            this.Size = new System.Drawing.Size(625, 371);
            this.pBottom.ResumeLayout(false);
            this.pBody.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pMainContent;
        private System.Windows.Forms.Panel pBottom;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Panel pBody;
        private System.Windows.Forms.Label label1;
    }
}
