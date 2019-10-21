using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Sigbit.Common;
using Sigbit.App.Net.IBXService.VCodeBreak.Message;
using Sigbit.App.Net.IBXService.Cient;
using Sigbit.App.Net.IBXService.Upload.Message;

namespace IBXClientDemo.VCodeBreak
{
    public partial class FormTestVCodeBreakClient : Form
    {
        public FormTestVCodeBreakClient()
        {
            InitializeComponent();
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                edtFileName.Text = openFileDialog1.FileName;
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            IBMVCodeBreakREQ req = new IBMVCodeBreakREQ();
            req.VcodeId = "571_zj_mobile";

            IBMUploadReceiptRESP receiptRESP = new IBMUploadReceiptRESP();

            IBXBusClient.Instance.UploadFile(req, edtFileName.Text, receiptRESP);

            MessageBox.Show(receiptRESP.GetMessageDescription());
        }

        private void btnUploadAndBreak_Click(object sender, EventArgs e)
        {
            //========= 1. 准备请求 ============
            IBMVCodeBreakREQ req = new IBMVCodeBreakREQ();
            req.VcodeId = "571_zj_mobile";
            req.RequestThirdId = RandUtil.NewString(4, RandStringType.Number) + "-" + RandUtil.NewString(6, RandStringType.Number);
            req.ImageFileName = edtFileName.Text.Trim();

            //========== 2. 上传并得到回执 =============
            IBMUploadReceiptRESP receiptRESP = new IBMUploadReceiptRESP();
            IBXBusClient.Instance.UploadFile(req, edtFileName.Text, receiptRESP);

            //============ 3. 填写回执并得到结果 ============
            req.UploadReceipt = receiptRESP.Receipt;

            IBMVCodeBreakRESP resp = new IBMVCodeBreakRESP();
            IBXBusClient.Instance.GetResponse(req, resp);

            string sBreakResult = resp.GetMessageDescription();

            MessageBox.Show(sBreakResult);
        }

        private void btnFetchBreakResult_Click(object sender, EventArgs e)
        {
            IBMVCodeBreakResultFetchREQ req = new IBMVCodeBreakResultFetchREQ();
            IBMVCodeBreakResultFetchRESP resp = new IBMVCodeBreakResultFetchRESP();

            IBXBusClient.Instance.GetResponse(req, resp);

            MessageBox.Show(resp.GetMessageDescription());
        }
    }
}
