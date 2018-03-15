namespace WindowsFormsApp1
{
    partial class FormMain  
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.labelFile = new System.Windows.Forms.Label();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.buttonFile = new System.Windows.Forms.Button();
            this.labelMD5 = new System.Windows.Forms.Label();
            this.textBoxMD5 = new System.Windows.Forms.TextBox();
            this.buttonInstall = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.labelInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelFile
            // 
            this.labelFile.AutoSize = true;
            this.labelFile.Location = new System.Drawing.Point(12, 9);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(32, 13);
            this.labelFile.TabIndex = 1;
            this.labelFile.Text = "&Path:";
            // 
            // textBoxFile
            // 
            this.textBoxFile.Location = new System.Drawing.Point(50, 6);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.ReadOnly = true;
            this.textBoxFile.Size = new System.Drawing.Size(294, 20);
            this.textBoxFile.TabIndex = 2;
            // 
            // buttonFile
            // 
            this.buttonFile.Location = new System.Drawing.Point(350, 6);
            this.buttonFile.Name = "buttonFile";
            this.buttonFile.Size = new System.Drawing.Size(100, 23);
            this.buttonFile.TabIndex = 3;
            this.buttonFile.Text = "&Browse";
            this.buttonFile.UseVisualStyleBackColor = true;
            this.buttonFile.Click += new System.EventHandler(this.buttonFile_Click_1);
            // 
            // labelMD5
            // 
            this.labelMD5.AutoSize = true;
            this.labelMD5.Location = new System.Drawing.Point(11, 40);
            this.labelMD5.Name = "labelMD5";
            this.labelMD5.Size = new System.Drawing.Size(33, 13);
            this.labelMD5.TabIndex = 4;
            this.labelMD5.Text = "&MD5:";
            // 
            // textBoxMD5
            // 
            this.textBoxMD5.Location = new System.Drawing.Point(50, 37);
            this.textBoxMD5.Name = "textBoxMD5";
            this.textBoxMD5.ReadOnly = true;
            this.textBoxMD5.Size = new System.Drawing.Size(294, 20);
            this.textBoxMD5.TabIndex = 5;
            // 
            // buttonInstall
            // 
            this.buttonInstall.Enabled = false;
            this.buttonInstall.Location = new System.Drawing.Point(350, 37);
            this.buttonInstall.Name = "buttonInstall";
            this.buttonInstall.Size = new System.Drawing.Size(100, 23);
            this.buttonInstall.TabIndex = 6;
            this.buttonInstall.Text = "&Run";
            this.buttonInstall.UseVisualStyleBackColor = true;
            this.buttonInstall.Click += new System.EventHandler(this.buttonInstall_Click);
            // 
            // labelInfo
            // 
            this.labelInfo.Location = new System.Drawing.Point(-1, 70);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(467, 25);
            this.labelInfo.TabIndex = 7;
            this.labelInfo.Text = "Developed by Micsoftvn";
            this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelInfo.Click += new System.EventHandler(this.labelInfo_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 104);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.buttonInstall);
            this.Controls.Add(this.textBoxMD5);
            this.Controls.Add(this.labelMD5);
            this.Controls.Add(this.buttonFile);
            this.Controls.Add(this.textBoxFile);
            this.Controls.Add(this.labelFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Security Tool - Micsoftvn";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelFile;
        private System.Windows.Forms.TextBox textBoxFile;
        private System.Windows.Forms.Button buttonFile;
        private System.Windows.Forms.Label labelMD5;
        private System.Windows.Forms.TextBox textBoxMD5;
        private System.Windows.Forms.Button buttonInstall;
        private System.Windows.Forms.Label labelInfo;
    }
}

