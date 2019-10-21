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
            this.button_Choise = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_filePath = new System.Windows.Forms.TextBox();
            this.comboBox_codeType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_record = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(256, 36);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(145, 71);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button_Choise
            // 
            this.button_Choise.Location = new System.Drawing.Point(407, 9);
            this.button_Choise.Name = "button_Choise";
            this.button_Choise.Size = new System.Drawing.Size(40, 23);
            this.button_Choise.TabIndex = 4;
            this.button_Choise.Text = "打开";
            this.button_Choise.UseVisualStyleBackColor = true;
            this.button_Choise.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "验证码文件：";
            // 
            // textBox_filePath
            // 
            this.textBox_filePath.Location = new System.Drawing.Point(73, 9);
            this.textBox_filePath.Name = "textBox_filePath";
            this.textBox_filePath.Size = new System.Drawing.Size(328, 21);
            this.textBox_filePath.TabIndex = 6;
            // 
            // comboBox_codeType
            // 
            this.comboBox_codeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_codeType.FormattingEnabled = true;
            this.comboBox_codeType.Location = new System.Drawing.Point(73, 36);
            this.comboBox_codeType.Name = "comboBox_codeType";
            this.comboBox_codeType.Size = new System.Drawing.Size(177, 20);
            this.comboBox_codeType.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "CodeType：";
            // 
            // textBox_record
            // 
            this.textBox_record.BackColor = System.Drawing.Color.White;
            this.textBox_record.Location = new System.Drawing.Point(2, 113);
            this.textBox_record.Multiline = true;
            this.textBox_record.Name = "textBox_record";
            this.textBox_record.ReadOnly = true;
            this.textBox_record.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_record.Size = new System.Drawing.Size(445, 254);
            this.textBox_record.TabIndex = 9;
            // 
            // FormTestCodeBreak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 369);
            this.Controls.Add(this.textBox_record);
            this.Controls.Add(this.comboBox_codeType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_filePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_Choise);
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
        private System.Windows.Forms.Button button_Choise;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_filePath;
        private System.Windows.Forms.ComboBox comboBox_codeType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_record;
    }
}