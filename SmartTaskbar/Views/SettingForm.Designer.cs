namespace SmartTaskbar.Views
{
    partial class SettingForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox_AutoMode = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.radioButtonDisableMode = new System.Windows.Forms.RadioButton();
            this.radioButtonForegroundMode = new System.Windows.Forms.RadioButton();
            this.radioButtonBlacklistMode = new System.Windows.Forms.RadioButton();
            this.radioButtonWhitelistMode = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox_AutoMode.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox_AutoMode, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 31.68747F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 68.31253F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(960, 537);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox_AutoMode
            // 
            this.groupBox_AutoMode.Controls.Add(this.tableLayoutPanel2);
            this.groupBox_AutoMode.Location = new System.Drawing.Point(3, 3);
            this.groupBox_AutoMode.Name = "groupBox_AutoMode";
            this.groupBox_AutoMode.Size = new System.Drawing.Size(234, 164);
            this.groupBox_AutoMode.TabIndex = 0;
            this.groupBox_AutoMode.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.Controls.Add(this.radioButtonDisableMode, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.radioButtonForegroundMode, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.radioButtonBlacklistMode, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.radioButtonWhitelistMode, 0, 3);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 20);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(222, 138);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // radioButtonDisableMode
            // 
            this.radioButtonDisableMode.AutoSize = true;
            this.radioButtonDisableMode.Location = new System.Drawing.Point(20, 10);
            this.radioButtonDisableMode.Margin = new System.Windows.Forms.Padding(20, 10, 0, 0);
            this.radioButtonDisableMode.Name = "radioButtonDisableMode";
            this.radioButtonDisableMode.Size = new System.Drawing.Size(14, 13);
            this.radioButtonDisableMode.TabIndex = 0;
            this.radioButtonDisableMode.TabStop = true;
            this.radioButtonDisableMode.UseVisualStyleBackColor = true;
            // 
            // radioButtonForegroundMode
            // 
            this.radioButtonForegroundMode.AutoSize = true;
            this.radioButtonForegroundMode.Location = new System.Drawing.Point(20, 44);
            this.radioButtonForegroundMode.Margin = new System.Windows.Forms.Padding(20, 10, 0, 0);
            this.radioButtonForegroundMode.Name = "radioButtonForegroundMode";
            this.radioButtonForegroundMode.Size = new System.Drawing.Size(14, 13);
            this.radioButtonForegroundMode.TabIndex = 1;
            this.radioButtonForegroundMode.TabStop = true;
            this.radioButtonForegroundMode.UseVisualStyleBackColor = true;
            // 
            // radioButtonBlacklistMode
            // 
            this.radioButtonBlacklistMode.AutoSize = true;
            this.radioButtonBlacklistMode.Location = new System.Drawing.Point(20, 78);
            this.radioButtonBlacklistMode.Margin = new System.Windows.Forms.Padding(20, 10, 0, 0);
            this.radioButtonBlacklistMode.Name = "radioButtonBlacklistMode";
            this.radioButtonBlacklistMode.Size = new System.Drawing.Size(14, 13);
            this.radioButtonBlacklistMode.TabIndex = 2;
            this.radioButtonBlacklistMode.TabStop = true;
            this.radioButtonBlacklistMode.UseVisualStyleBackColor = true;
            // 
            // radioButtonWhitelistMode
            // 
            this.radioButtonWhitelistMode.AutoSize = true;
            this.radioButtonWhitelistMode.Location = new System.Drawing.Point(20, 112);
            this.radioButtonWhitelistMode.Margin = new System.Windows.Forms.Padding(20, 10, 0, 0);
            this.radioButtonWhitelistMode.Name = "radioButtonWhitelistMode";
            this.radioButtonWhitelistMode.Size = new System.Drawing.Size(14, 13);
            this.radioButtonWhitelistMode.TabIndex = 3;
            this.radioButtonWhitelistMode.TabStop = true;
            this.radioButtonWhitelistMode.UseVisualStyleBackColor = true;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartTaskbar";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox_AutoMode.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox_AutoMode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.RadioButton radioButtonDisableMode;
        private System.Windows.Forms.RadioButton radioButtonForegroundMode;
        private System.Windows.Forms.RadioButton radioButtonBlacklistMode;
        private System.Windows.Forms.RadioButton radioButtonWhitelistMode;
    }
}