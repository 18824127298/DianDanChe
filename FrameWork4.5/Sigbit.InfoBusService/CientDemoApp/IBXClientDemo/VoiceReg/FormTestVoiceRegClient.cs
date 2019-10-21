using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Sigbit.Common;
using Sigbit.App.Net.IBXService.VoiceReg.Message;
using Sigbit.App.Net.IBXService.Upload.Message;
using Sigbit.App.Net.IBXService.Cient;

namespace IBXClientDemo.VoiceReg
{
    public partial class FormTestVoiceRegClient : Form
    {
        public FormTestVoiceRegClient()
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
            IBMVoiceRegREQ req = new IBMVoiceRegREQ();
            
            IBMUploadReceiptRESP receiptRESP = new IBMUploadReceiptRESP();

            IBXBusClient.Instance.UploadFile(req, edtFileName.Text, receiptRESP);

            MessageBox.Show(receiptRESP.GetMessageDescription());
        }

        private void btnUploadAndSendVoiceRegRequest_Click(object sender, EventArgs e)
        {
            //========= 1. 准备请求 ============
            IBMVoiceRegREQ req = new IBMVoiceRegREQ();
            req.GrammarId = "telecom";
            req.RequestThirdId = RandUtil.NewString(4, RandStringType.Number) + "-" + RandUtil.NewString(6, RandStringType.Number);
            req.VoiceFileName = edtFileName.Text.Trim();

            //========== 2. 上传并得到回执 =============
            IBMUploadReceiptRESP receiptRESP = new IBMUploadReceiptRESP();
            IBXBusClient.Instance.UploadFile(req, edtFileName.Text, receiptRESP);

            //============ 3. 填写回执并得到结果 ============
            req.UploadReceipt = receiptRESP.Receipt;

            IBMVoiceRegRESP resp = new IBMVoiceRegRESP();
            IBXBusClient.Instance.GetResponse(req, resp);

            string sVoiceRegResult = resp.GetMessageDescription();

            MessageBox.Show(sVoiceRegResult);
        }

        private void btnFetchRegResult_Click(object sender, EventArgs e)
        {
            IBMVoiceRegResultFetchREQ req = new IBMVoiceRegResultFetchREQ();
            IBMVoiceRegResultFetchRESP resp = new IBMVoiceRegResultFetchRESP();

            IBXBusClient.Instance.GetResponse(req, resp);

            MessageBox.Show(resp.GetMessageDescription());
        }


    }
}
