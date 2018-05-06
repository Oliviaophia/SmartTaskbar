namespace SmartTaskbar
{
    partial class FormAbout
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.linkRelease = new System.Windows.Forms.LinkLabel();
            this.linkWeb = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SmartTaskbar.Properties.Resources.logo_blue;
            this.pictureBox1.Location = new System.Drawing.Point(19, 12);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.Location = new System.Drawing.Point(86, 14);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(164, 32);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "SmartTaskbar";
            // 
            // labelCopyright
            // 
            this.labelCopyright.AutoSize = true;
            this.labelCopyright.Location = new System.Drawing.Point(89, 49);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(223, 15);
            this.labelCopyright.TabIndex = 0;
            this.labelCopyright.Text = "Copyright 2018 Chanple Cai, MIT License";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(89, 68);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(72, 15);
            this.labelVersion.TabIndex = 0;
            this.labelVersion.Text = "version 1.1.4";
            // 
            // linkRelease
            // 
            this.linkRelease.AutoSize = true;
            this.linkRelease.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkRelease.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkRelease.LinkColor = System.Drawing.Color.RoyalBlue;
            this.linkRelease.Location = new System.Drawing.Point(88, 117);
            this.linkRelease.Name = "linkRelease";
            this.linkRelease.Size = new System.Drawing.Size(128, 19);
            this.linkRelease.TabIndex = 0;
            this.linkRelease.TabStop = true;
            this.linkRelease.Tag = "";
            this.linkRelease.Text = "Get Leatest Release";
            this.linkRelease.VisitedLinkColor = System.Drawing.Color.RoyalBlue;
            // 
            // linkWeb
            // 
            this.linkWeb.AutoSize = true;
            this.linkWeb.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkWeb.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkWeb.LinkColor = System.Drawing.Color.RoyalBlue;
            this.linkWeb.Location = new System.Drawing.Point(88, 95);
            this.linkWeb.Name = "linkWeb";
            this.linkWeb.Size = new System.Drawing.Size(144, 19);
            this.linkWeb.TabIndex = 0;
            this.linkWeb.TabStop = true;
            this.linkWeb.Tag = "";
            this.linkWeb.Text = "Visit the SmartTaskbar";
            this.linkWeb.VisitedLinkColor = System.Drawing.Color.RoyalBlue;
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 160);
            this.Controls.Add(this.linkWeb);
            this.Controls.Add(this.linkRelease);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.labelCopyright);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = global::SmartTaskbar.Properties.Resources.logo_32;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.LinkLabel linkRelease;
        private System.Windows.Forms.LinkLabel linkWeb;
    }
}