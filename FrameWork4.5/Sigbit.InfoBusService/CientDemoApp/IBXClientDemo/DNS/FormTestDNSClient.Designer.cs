namespace IBXClientDemo.DNS
{
    partial class FormTestDNSClient
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
            this.edtTransCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGetServiceUrl = new System.Windows.Forms.Button();
            this.edtServiceUrl = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "TransCode：";
            // 
            // edtTransCode
            // 
            this.edtTransCode.Location = new System.Drawing.Point(102, 20);
            this.edtTransCode.Name = "edtTransCode";
            this.edtTransCode.Size = new System.Drawing.Size(188, 21);
            this.edtTransCode.TabIndex = 1;
            this.edtTransCode.Text = "verify_image_break";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "ServiceUrl：";
            // 
            // btnGetServiceUrl
            // 
            this.btnGetServiceUrl.Location = new System.Drawing.Point(175, 131);
            this.btnGetServiceUrl.Name = "btnGetServiceUrl";
            this.btnGetServiceUrl.Size = new System.Drawing.Size(222, 32);
            this.btnGetServiceUrl.TabIndex = 4;
            this.btnGetServiceUrl.Text = "Get Service Url via DNS";
            this.btnGetServiceUrl.UseVisualStyleBackColor = true;
            this.btnGetServiceUrl.Click += new System.EventHandler(this.btnGetServiceUrl_Click);
            // 
            // edtServiceUrl
            // 
            this.edtServiceUrl.Location = new System.Drawing.Point(102, 56);
            this.edtServiceUrl.Name = "edtServiceUrl";
            this.edtServiceUrl.Size = new System.Drawing.Size(527, 21);
            this.edtServiceUrl.TabIndex = 5;
            // 
            // FormTestDNSClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 262);
            this.Controls.Add(this.edtServiceUrl);
            this.Controls.Add(this.btnGetServiceUrl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.edtTransCode);
            this.Controls.Add(this.label1);
            this.Name = "FormTestDNSClient";
            this.Text = "FormTestDNSClient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edtTransCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGetServiceUrl;
        private System.Windows.Forms.TextBox edtServiceUrl;
    }
}