using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sigbit.Net.IBXService.VCodeBreak.ThirdEngine.UUCloud;

namespace BUUBreak
{
    public partial class FormTestCodeBreak : Form
    {
        public FormTestCodeBreak()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                string sFileName = op.FileName;
                int nCodeType = (int)comboBox_codeType.SelectedValue;
                try
                {
                    DateTime dtBegin = DateTime.Now;
                    BUUBreakRequest req = new BUUBreakRequest(sFileName, nCodeType.ToString());
                    this.pictureBox1.ImageLocation = sFileName;
                    this.textBox_filePath.Text=sFileName;
                    BUUBreakResult result = BUUBreakEngine.Instance.VCodeBreak(req);
                    DateTime dtEnd = DateTime.Now;
                    TimeSpan tsTake = dtEnd - dtBegin;
                    Record(result.BreakResult, tsTake.Milliseconds.ToString(), "");
                }
                catch(Exception ex)
                {
                    Record("","", "Err:" + ex.Message);
                }
                
            }
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
            this.textBox_record.AppendText(string.Format("<Result>{0}</Result> <TimeSpan>{1}</TimeSpan> <Error>{2}</Error>", sResult, sTimeSpan, sErr)+Environment.NewLine);
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
