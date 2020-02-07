namespace SmartTaskbar.Views
{
    partial class MainContextMenu
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
            this.exitMenuButton = new SmartTaskbar.Views.MenuButton();
            this.menuDelimiter1 = new SmartTaskbar.Views.MenuDelimiter();
            this.disableButton = new SmartTaskbar.Views.MenuButton();
            this.SuspendLayout();
            // 
            // exitMenuButton
            // 
            this.exitMenuButton.BackColor = System.Drawing.Color.Transparent;
            this.exitMenuButton.FlatAppearance.BorderSize = 0;
            this.exitMenuButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.exitMenuButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.exitMenuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitMenuButton.ForeColor = System.Drawing.Color.Black;
            this.exitMenuButton.Image = global::SmartTaskbar.Properties.Resources.Empty;
            this.exitMenuButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.exitMenuButton.Location = new System.Drawing.Point(5, 430);
            this.exitMenuButton.Margin = new System.Windows.Forms.Padding(0);
            this.exitMenuButton.Name = "exitMenuButton";
            this.exitMenuButton.Size = new System.Drawing.Size(230, 40);
            this.exitMenuButton.TabIndex = 0;
            this.exitMenuButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.exitMenuButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.exitMenuButton.UseVisualStyleBackColor = false;
            // 
            // menuDelimiter1
            // 
            this.menuDelimiter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.menuDelimiter1.Location = new System.Drawing.Point(10, 423);
            this.menuDelimiter1.Margin = new System.Windows.Forms.Padding(5);
            this.menuDelimiter1.Name = "menuDelimiter1";
            this.menuDelimiter1.Size = new System.Drawing.Size(220, 2);
            this.menuDelimiter1.TabIndex = 1;
            // 
            // disableButton
            // 
            this.disableButton.BackColor = System.Drawing.Color.Transparent;
            this.disableButton.FlatAppearance.BorderSize = 0;
            this.disableButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.disableButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.disableButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.disableButton.ForeColor = System.Drawing.Color.Black;
            this.disableButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.disableButton.Location = new System.Drawing.Point(5, 378);
            this.disableButton.Margin = new System.Windows.Forms.Padding(0);
            this.disableButton.Name = "disableButton";
            this.disableButton.Size = new System.Drawing.Size(230, 40);
            this.disableButton.TabIndex = 2;
            this.disableButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.disableButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.disableButton.UseVisualStyleBackColor = false;
            // 
            // MainContextMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 480);
            this.Controls.Add(this.disableButton);
            this.Controls.Add(this.menuDelimiter1);
            this.Controls.Add(this.exitMenuButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(240, 480);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(240, 480);
            this.Name = "MainContextMenu";
            this.Padding = new System.Windows.Forms.Padding(5, 10, 5, 10);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "S";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private MenuButton exitMenuButton;
        private MenuDelimiter menuDelimiter1;
        private MenuButton disableButton;
    }
}