namespace IBXClientDemo.VoiceReg
{
    partial class FormTestVoiceRegClient
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
            this.btnUploadAndSendVoiceRegRequest = new System.Windows.Forms.Button();
            this.btnUploadImage = new System.Windows.Forms.Button();
            this.btnBrowseFile = new System.Windows.Forms.Button();
            this.edtFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnFetchRegResult = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUploadAndSendVoiceRegRequest
            // 
            this.btnUploadAndSendVoiceRegRequest.Location = new System.Drawing.Point(90, 128);
            this.btnUploadAndSendVoiceRegRequest.Name = "btnUploadAndSendVoiceRegRequest";
            this.btnUploadAndSendVoiceRegRequest.Size = new System.Drawing.Size(374, 49);
            this.btnUploadAndSendVoiceRegRequest.TabIndex = 11;
            this.btnUploadAndSendVoiceRegRequest.Text = "上传并发送语音识别请求";
            this.btnUploadAndSendVoiceRegRequest.UseVisualStyleBackColor = true;
            this.btnUploadAndSendVoiceRegRequest.Click += new System.EventHandler(this.btnUploadAndSendVoiceRegRequest_Click);
            // 
            // btnUploadImage
            // 
            this.btnUploadImage.Location = new System.Drawing.Point(22, 48);
            this.btnUploadImage.Name = "btnUploadImage";
            this.btnUploadImage.Size = new System.Drawing.Size(175, 35);
            this.btnUploadImage.TabIndex = 10;
            this.btnUploadImage.Text = "上传语音文件得到回执";
            this.btnUploadImage.UseVisualStyleBackColor = true;
            this.btnUploadImage.Click += new System.EventHandler(this.btnUploadImage_Click);
            // 
            // btnBrowseFile
            // 
            this.btnBrowseFile.Location = new System.Drawing.Point(405, 49);
            this.btnBrowseFile.Name = "btnBrowseFile";
            this.btnBrowseFile.Size = new System.Drawing.Size(134, 23);
            this.btnBrowseFile.TabIndex = 9;
            this.btnBrowseFile.Text = "Browse File...";
            this.btnBrowseFile.UseVisualStyleBackColor = true;
            this.btnBrowseFile.Click += new System.EventHandler(this.btnBrowseFile_Click);
            // 
            // edtFileName
            // 
            this.edtFileName.Location = new System.Drawing.Point(117, 22);
            this.edtFileName.Name = "edtFileName";
            this.edtFileName.Size = new System.Drawing.Size(422, 21);
            this.edtFileName.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "语音文件：";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "*.jpg";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "语音文件(*.wav)|*.wav|所有文件|*.*";
            // 
            // btnFetchRegResult
            // 
            this.btnFetchRegResult.Location = new System.Drawing.Point(90, 193);
            this.btnFetchRegResult.Name = "btnFetchRegResult";
            this.btnFetchRegResult.Size = new System.Drawing.Size(374, 45);
            this.btnFetchRegResult.TabIndex = 12;
            this.btnFetchRegResult.Text = "获取语音识别的结果";
            this.btnFetchRegResult.UseVisualStyleBackColor = true;
            this.btnFetchRegResult.Click += new System.EventHandler(this.btnFetchRegResult_Click);
            // 
            // FormTestVoiceRegClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 338);
            this.Controls.Add(this.btnFetchRegResult);
            this.Controls.Add(this.btnUploadAndSendVoiceRegRequest);
            this.Controls.Add(this.btnUploadImage);
            this.Controls.Add(this.btnBrowseFile);
            this.Controls.Add(this.edtFileName);
            this.Controls.Add(this.label1);
            this.Name = "FormTestVoiceRegClient";
            this.Text = "FormTestVoiceRegClient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUploadAndSendVoiceRegRequest;
        private System.Windows.Forms.Button btnUploadImage;
        private System.Windows.Forms.Button btnBrowseFile;
        private System.Windows.Forms.TextBox edtFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnFetchRegResult;
    }
}