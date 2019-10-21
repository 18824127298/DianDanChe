namespace BUUBreak
{
    partial class FormTestCodeBreak
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
            this.label1 = new System.Windows.Forms.Label();
            this.edtFilePath = new System.Windows.Forms.TextBox();
            this.comboBox_codeType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.memoLog = new System.Windows.Forms.TextBox();
            this.btnBreakVCode = new System.Windows.Forms.Button();
            this.btnBrowseFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(353, 87);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(145, 71);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "验证码文件：";
            // 
            // edtFilePath
            // 
            this.edtFilePath.Location = new System.Drawing.Point(97, 9);
            this.edtFilePath.Name = "edtFilePath";
            this.edtFilePath.Size = new System.Drawing.Size(401, 21);
            this.edtFilePath.TabIndex = 6;
            // 
            // comboBox_codeType
            // 
            this.comboBox_codeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_codeType.FormattingEnabled = true;
            this.comboBox_codeType.Location = new System.Drawing.Point(97, 39);
            this.comboBox_codeType.Name = "comboBox_codeType";
            this.comboBox_codeType.Size = new System.Drawing.Size(177, 20);
            this.comboBox_codeType.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "CodeType：";
            // 
            // memoLog
            // 
            this.memoLog.BackColor = System.Drawing.Color.White;
            this.memoLog.Location = new System.Drawing.Point(16, 181);
            this.memoLog.Multiline = true;
            this.memoLog.Name = "memoLog";
            this.memoLog.ReadOnly = true;
            this.memoLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.memoLog.Size = new System.Drawing.Size(482, 254);
            this.memoLog.TabIndex = 9;
            // 
            // btnBreakVCode
            // 
            this.btnBreakVCode.Location = new System.Drawing.Point(16, 123);
            this.btnBreakVCode.Name = "btnBreakVCode";
            this.btnBreakVCode.Size = new System.Drawing.Size(229, 35);
            this.btnBreakVCode.TabIndex = 10;
            this.btnBreakVCode.Text = "破解验证码";
            this.btnBreakVCode.UseVisualStyleBackColor = true;
            this.btnBreakVCode.Click += new System.EventHandler(this.btnBreakVCode_Click);
            // 
            // btnBrowseFile
            // 
            this.btnBrowseFile.Location = new System.Drawing.Point(405, 36);
            this.btnBrowseFile.Name = "btnBrowseFile";
            this.btnBrowseFile.Size = new System.Drawing.Size(93, 23);
            this.btnBrowseFile.TabIndex = 11;
            this.btnBrowseFile.Text = "浏览...";
            this.btnBrowseFile.UseVisualStyleBackColor = true;
            this.btnBrowseFile.Click += new System.EventHandler(this.btnBrowseFile_Click);
            // 
            // FormTestCodeBreak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 461);
            this.Controls.Add(this.btnBrowseFile);
            this.Controls.Add(this.btnBreakVCode);
            this.Controls.Add(this.memoLog);
            this.Controls.Add(this.comboBox_codeType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.edtFilePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormTestCodeBreak";
            this.Text = "FormTestCodeBreak";
            this.Load += new System.EventHandler(this.FormTestCodeBreak_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edtFilePath;
        private System.Windows.Forms.ComboBox comboBox_codeType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox memoLog;
        private System.Windows.Forms.Button btnBreakVCode;
        private System.Windows.Forms.Button btnBrowseFile;
    }
}