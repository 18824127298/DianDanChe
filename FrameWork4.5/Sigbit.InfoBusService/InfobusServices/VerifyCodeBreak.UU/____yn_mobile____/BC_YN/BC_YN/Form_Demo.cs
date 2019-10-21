using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BC_YN
{
    public partial class Form_Demo : Form
    {
        public Form_Demo()
        {
            InitializeComponent();
        }


        private void button_choise_Click(object sender, EventArgs e)
        {
            Bitmap bmp = null;
            this.pictureBox_Src.Image = null;
            OpenFileDialog op = new OpenFileDialog();
            op.InitialDirectory = Environment.CurrentDirectory;
            op.Filter = "图像文件(*.jpg)|*.jpg|所有文件|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filePath = op.FileName;
                    bmp = new Bitmap(filePath);
                    this.pictureBox_Src.ImageLocation = filePath;
                    this.textBox_result.Text = BreakCode_YN.Identity(bmp);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
