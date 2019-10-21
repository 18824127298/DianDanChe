using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Diagnostics;
using Sigbit.App.Net.IBXService.VCodeBreak.YunNan;

namespace BYNBreak
{
    public partial class FormTestCodeBreak : Form
    {
        public FormTestCodeBreak()
        {
            InitializeComponent();
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            if (op.ShowDialog() == DialogResult.OK)
            {
                edtFilePath.Text = op.FileName;
                this.pictureBox1.ImageLocation = op.FileName;
            }
        }

        private void btnBreakVCode_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            //========== 1. 准备请求 =========
            BYNBreakRequest req = new BYNBreakRequest();
            req.ImageFileName = edtFilePath.Text.Trim();

            //========== 2. 调用破解 ===========
            BYNBreakResult uuResult = BYNBreakEngine.Instance.VCodeBreak(req);
            stopWatch.Stop();

            //========= 3. 显示破解结果 ==========
            if (uuResult.ErrorCode != "")
                memoLog.AppendText("【" + uuResult.ErrorCode + "】" + uuResult.ErrorString + "\r\n");
            else
                memoLog.AppendText("【破解结果】" + uuResult.BreakResultText + "\r\n");

            memoLog.AppendText(stopWatch.ElapsedMilliseconds.ToString() + " ms Ellapsed. \r\n\r\n");
        }
       
    }
}
