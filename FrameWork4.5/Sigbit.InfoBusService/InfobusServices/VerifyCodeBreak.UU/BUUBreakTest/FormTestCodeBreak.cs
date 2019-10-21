using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Diagnostics;

using Sigbit.Common;
using Sigbit.App.Net.IBXService.VCodeBreak.ThirdEngine.UUCloud;

namespace BUUBreak
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
            BUUBreakRequest req = new BUUBreakRequest();
            req.ImageFileName = edtFilePath.Text.Trim();
            req.UUCodeType = ConvertUtil.ToInt(comboBox_codeType.SelectedValue);

            //========== 2. 调用破解 ===========
            BUUBreakResult uuResult = BUUBreakEngine.Instance.VCodeBreak(req);
            stopWatch.Stop();

            //========= 3. 显示破解结果 ==========
            if (uuResult.ErrorCode != "")
                memoLog.AppendText("【" + uuResult.ErrorCode + "】" + uuResult.ErrorString + "\r\n");
            else
                memoLog.AppendText("【破解结果】" + uuResult.BreakResultText + "\r\n");

            memoLog.AppendText(stopWatch.ElapsedMilliseconds.ToString() + " ms Ellapsed. \r\n\r\n");
        }

        
        private void FormTestCodeBreak_Load(object sender, EventArgs e)
        {
            InitCodeType();
        }

        #region Method

        private void InitCodeType()
        {
            List<CodeType> list = new List<CodeType>() { };
            list.Add(new CodeType(1004, "1～4位英文或数字"));
            list.Add(new CodeType(1006, "1～6位英文或数字"));
            list.Add(new CodeType(8001, "不固定长度、类型"));

            comboBox_codeType.Items.Clear();
            comboBox_codeType.DataSource = list;
            comboBox_codeType.DisplayMember = "Descript";
            comboBox_codeType.ValueMember = "Code";
            comboBox_codeType.SelectedIndex = 0;
        }

        private void Record(string sResult,string sTimeSpan,string sErr)
        {
            if (sErr == null || sErr.Trim() == "")
            {
                sErr = "NULL";
            }
            else
            {
                sResult = sTimeSpan = "NULL";
            }
            this.memoLog.AppendText(string.Format("<Result>{0}</Result> <TimeSpan>{1}</TimeSpan> <Error>{2}</Error>", sResult, sTimeSpan, sErr)+Environment.NewLine);
        }

        #endregion

    }

    public class CodeType
    {
        private int _code;
        /// <summary>
        /// Code
        /// </summary>
        public int Code
        {
            get
            { return _code; }
        }

        private string _descript;
        /// <summary>
        /// 描述
        /// </summary>
        public string Descript
        {
            get
            { return _descript; }
        }

        public CodeType(int code, string descript)
        {
            _code = code;
            _descript = code + " : " + descript;
        }
    }
}
