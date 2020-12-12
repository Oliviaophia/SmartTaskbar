
namespace SmartTaskbar.UI.Views
{
    partial class MainSettingForm
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
            this.panelMenu = new System.Windows.Forms.Panel();
            this.settingMenuButton1 = new SmartTaskbar.UI.Views.SettingMenuButton();
            this.panelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.Controls.Add(this.settingMenuButton1);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(300, 635);
            this.panelMenu.TabIndex = 0;
            // 
            // settingMenuButton1
            // 
            this.settingMenuButton1.BackColor = System.Drawing.Color.Transparent;
            this.settingMenuButton1.FlatAppearance.BorderSize = 0;
            this.settingMenuButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(100)))), ((int)(((byte)(230)))));
            this.settingMenuButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(69)))), ((int)(((byte)(225)))));
            this.settingMenuButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingMenuButton1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.settingMenuButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.settingMenuButton1.Location = new System.Drawing.Point(9, 9);
            this.settingMenuButton1.Margin = new System.Windows.Forms.Padding(0);
            this.settingMenuButton1.Name = "settingMenuButton1";
            this.settingMenuButton1.Size = new System.Drawing.Size(270, 48);
            this.settingMenuButton1.TabIndex = 0;
            this.settingMenuButton1.Text = "settingMenuButton1";
            this.settingMenuButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.settingMenuButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.settingMenuButton1.UseVisualStyleBackColor = false;
            // 
            // MainSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 635);
            this.Controls.Add(this.panelMenu);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "MainSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartTaskbar";
            this.panelMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private SettingMenuButton settingMenuButton1;
    }
}