namespace IBXClientDemo.VCodeBreak
{
    partial class FormTestVCodeBreakClient
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
            this.label1 = new System.Windows.Forms.Label();
            this.edtFileName = new System.Windows.Forms.TextBox();
            this.btnBrowseFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnUploadImage = new System.Windows.Forms.Button();
            this.btnUploadAndBreak = new System.Windows.Forms.Button();
            this.btnFetchBreakResult = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "验证码图片文件：";
            // 
            // edtFileName
            // 
            this.edtFileName.Location = new System.Drawing.Point(138, 43);
            this.edtFileName.Name = "edtFileName";
            this.edtFileName.Size = new System.Drawing.Size(422, 21);
            this.edtFileName.TabIndex = 2;
            // 
            // btnBrowseFile
            // 
            this.btnBrowseFile.Location = new System.Drawing.Point(426, 70);
            this.btnBrowseFile.Name = "btnBrowseFile";
            this.btnBrowseFile.Size = new System.Drawing.Size(134, 23);
            this.btnBrowseFile.TabIndex = 3;
            this.btnBrowseFile.Text = "Browse File...";
            this.btnBrowseFile.UseVisualStyleBackColor = true;
            this.btnBrowseFile.Click += new System.EventHandler(this.btnBrowseFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "*.jpg";
            this.openFileDialog1.Filter = "图像文件|*.jpg;*.png;*.gif|JPG文件|*.jpg|所有文件|*.*";
            // 
            // btnUploadImage
            // 
            this.btnUploadImage.Location = new System.Drawing.Point(43, 69);
            this.btnUploadImage.Name = "btnUploadImage";
            this.btnUploadImage.Size = new System.Drawing.Size(175, 35);
            this.btnUploadImage.TabIndex = 4;
            this.btnUploadImage.Text = "上传图片得到回执";
            this.btnUploadImage.UseVisualStyleBackColor = true;
            this.btnUploadImage.Click += new System.EventHandler(this.btnUploadImage_Click);
            // 
            // btnUploadAndBreak
            // 
            this.btnUploadAndBreak.Location = new System.Drawing.Point(110, 147);
            this.btnUploadAndBreak.Name = "btnUploadAndBreak";
            this.btnUploadAndBreak.Size = new System.Drawing.Size(374, 49);
            this.btnUploadAndBreak.TabIndex = 5;
            this.btnUploadAndBreak.Text = "上传并发送破解情求";
            this.btnUploadAndBreak.UseVisualStyleBackColor = true;
            this.btnUploadAndBreak.Click += new System.EventHandler(this.btnUploadAndBreak_Click);
            // 
            // btnFetchBreakResult
            // 
            this.btnFetchBreakResult.Location = new System.Drawing.Point(110, 219);
            this.btnFetchBreakResult.Name = "btnFetchBreakResult";
            this.btnFetchBreakResult.Size = new System.Drawing.Size(374, 45);
            this.btnFetchBreakResult.TabIndex = 13;
            this.btnFetchBreakResult.Text = "获取验证码破解的结果";
            this.btnFetchBreakResult.UseVisualStyleBackColor = true;
            this.btnFetchBreakResult.Click += new System.EventHandler(this.btnFetchBreakResult_Click);
            // 
            // FormTestVCodeBreakClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 380);
            this.Controls.Add(this.btnFetchBreakResult);
            this.Controls.Add(this.btnUploadAndBreak);
            this.Controls.Add(this.btnUploadImage);
            this.Controls.Add(this.btnBrowseFile);
            this.Controls.Add(this.edtFileName);
            this.Controls.Add(this.label1);
            this.Name = "FormTestVCodeBreakClient";
            this.Text = "FormTestVCodeBreakClient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edtFileName;
        private System.Windows.Forms.Button btnBrowseFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnUploadImage;
        private System.Windows.Forms.Button btnUploadAndBreak;
        private System.Windows.Forms.Button btnFetchBreakResult;
    }
}