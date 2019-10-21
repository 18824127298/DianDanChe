namespace BC_YN
{
    partial class Form_Demo
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
            this.button_choise = new System.Windows.Forms.Button();
            this.pictureBox_Src = new System.Windows.Forms.PictureBox();
            this.textBox_result = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Src)).BeginInit();
            this.SuspendLayout();
            // 
            // button_choise
            // 
            this.button_choise.Location = new System.Drawing.Point(70, 81);
            this.button_choise.Name = "button_choise";
            this.button_choise.Size = new System.Drawing.Size(75, 23);
            this.button_choise.TabIndex = 0;
            this.button_choise.Text = "选择图片";
            this.button_choise.UseVisualStyleBackColor = true;
            this.button_choise.Click += new System.EventHandler(this.button_choise_Click);
            // 
            // pictureBox_Src
            // 
            this.pictureBox_Src.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_Src.Location = new System.Drawing.Point(4, 1);
            this.pictureBox_Src.Name = "pictureBox_Src";
            this.pictureBox_Src.Size = new System.Drawing.Size(100, 50);
            this.pictureBox_Src.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox_Src.TabIndex = 1;
            this.pictureBox_Src.TabStop = false;
            // 
            // textBox_result
            // 
            this.textBox_result.BackColor = System.Drawing.Color.White;
            this.textBox_result.Location = new System.Drawing.Point(111, 12);
            this.textBox_result.Name = "textBox_result";
            this.textBox_result.ReadOnly = true;
            this.textBox_result.Size = new System.Drawing.Size(100, 21);
            this.textBox_result.TabIndex = 2;
            this.textBox_result.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form_Demo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 113);
            this.Controls.Add(this.textBox_result);
            this.Controls.Add(this.pictureBox_Src);
            this.Controls.Add(this.button_choise);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form_Demo";
            this.Text = "Form_Demo";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Src)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_choise;
        private System.Windows.Forms.PictureBox pictureBox_Src;
        private System.Windows.Forms.TextBox textBox_result;
    }
}