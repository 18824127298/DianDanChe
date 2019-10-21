using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Net;


using Sigbit.Common;

using Sigbit.App.Net.IBXService.Cient;
using Sigbit.App.Net.IBXService.VCodeBreak.Message;
using Sigbit.App.Net.IBXService.Upload.Message;



namespace ZJVCodeDemo
{
    public partial class FormMain : Form
    {
        private string _dtStartTime = "";


        private string _thirtCode = "";

        public FormMain()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                edtPhotoPath.Text = openFileDialog1.FileName;
                picPhoto.Image = Bitmap.FromFile(edtPhotoPath.Text.Trim());
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            lblResult.Visible = false;
            btnBreakCode.Enabled = true;
        }

        private void btnBreakCode_Click(object sender, EventArgs e)
        {
            if (!File.Exists(edtPhotoPath.Text.Trim()))
            {
                MessageBox.Show("��ѡ��ͼƬ");
                return;
            }

            btnBreakCode.Enabled = false;

            lblMessage.Text = "��ʼʶ��";
            lblMessage.Visible = true;
            lblResult.Visible = false;

            Application.DoEvents();


            //�ϴ�ͼƬ
            //========= 1. ׼������ ============
            IBMVCodeBreakREQ req = new IBMVCodeBreakREQ();
            req.VcodeId = "571_zj_mobile";
            req.RequestThirdId = RandUtil.NewString(4, RandStringType.Number) + "-" + RandUtil.NewString(6, RandStringType.Number);
            req.ImageFileName = edtPhotoPath.Text.Trim();

            //========== 2. �ϴ����õ���ִ =============
            IBMUploadReceiptRESP receiptRESP = new IBMUploadReceiptRESP();
            IBXBusClient.Instance.UploadFile(req, edtPhotoPath.Text, receiptRESP);

            //============ 3. ��д��ִ���õ���� ============
            req.UploadReceipt = receiptRESP.Receipt;

            IBMVCodeBreakRESP resp = new IBMVCodeBreakRESP();
            IBXBusClient.Instance.GetResponse(req, resp);

            _dtStartTime = DateTimeUtil.Now;
            _thirtCode = req.RequestThirdId;

            timerMain.Enabled = true;

        }




        private void timerMain_Tick(object sender, EventArgs e)
        {
            lblMessage.Text = "����ʶ����.......";

            IBMVCodeBreakResultFetchREQ req = new IBMVCodeBreakResultFetchREQ();
            IBMVCodeBreakResultFetchRESP resp = new IBMVCodeBreakResultFetchRESP();

            IBXBusClient.Instance.GetResponse(req, resp);

            int nBreakDuration = DateTimeUtil.SecondsAfter(_dtStartTime, DateTimeUtil.Now);

            if (nBreakDuration > 30)
            {
                lblMessage.Text = "ʶ��ʱ��δ��ȡʶ����";
                timerMain.Enabled = false;
                lblResult.Text = "";
                btnBreakCode.Enabled = true;
                btnAutoLoad.Enabled = true;

                return;
            }


            if (resp.BreakResultText == "")
            {
                return;
            }

            if (resp.RequestThirdId != _thirtCode)
                return;

            if (_dtStartTime == "")
                return;


            lblMessage.Text = "ʶ����Ϊ��";
            lblResult.Text = resp.BreakResultText;
            lblResult.Visible = true;

            timerMain.Enabled = false;

            btnBreakCode.Enabled = true;
            btnAutoLoad.Enabled = true;

        }

        private void btnAutoLoad_Click(object sender, EventArgs e)
        {
            string sFilePath = AppDomain.CurrentDomain.BaseDirectory + "photo";

            if (!Directory.Exists(sFilePath))
            {
                Directory.CreateDirectory(sFilePath);
            }

            string sFileName = DateTime.Now.ToString("HHmmss") + ".jpg";

            string sFullFileName = AppPath.AppFullPath("photo", sFileName);

            WebClient client = new WebClient();

            client.DownloadFile("https://zj.ac.10086.cn/ImgDisp", sFullFileName);

            picPhoto.Image = Bitmap.FromFile(sFullFileName);

            edtPhotoPath.Text = sFullFileName;

            FileUtil.RemoveFilesBeforeTime(sFilePath, DateTime.Now.AddDays(-7.0));

            //btnAutoLoad.Enabled = false;

            //lblMessage.Text = "��ʼʶ��";
            //lblMessage.Visible = true;
            //lblResult.Visible = false;


            ////�ϴ�ͼƬ
            ////========= 1. ׼������ ============
            //IBMVCodeBreakREQ req = new IBMVCodeBreakREQ();
            //req.VcodeId = "571_zj_mobile";
            //req.RequestThirdId = RandUtil.NewString(4, RandStringType.Number) + "-" + RandUtil.NewString(6, RandStringType.Number);
            //req.ImageFileName = sFullFileName;

            ////========== 2. �ϴ����õ���ִ =============
            //IBMUploadReceiptRESP receiptRESP = new IBMUploadReceiptRESP();
            //IBXBusClient.Instance.UploadFile(req, sFullFileName, receiptRESP);

            ////============ 3. ��д��ִ���õ���� ============
            //req.UploadReceipt = receiptRESP.Receipt;

            //IBMVCodeBreakRESP resp = new IBMVCodeBreakRESP();
            //IBXBusClient.Instance.GetResponse(req, resp);

            //_dtStartTime = DateTimeUtil.Now;
            //_thirtCode = req.RequestThirdId;

            //timerMain.Enabled = true;

        }


    }
}