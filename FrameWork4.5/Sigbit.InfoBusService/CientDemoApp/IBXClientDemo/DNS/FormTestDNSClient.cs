using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Sigbit.App.Net.IBXService.Message;
using Sigbit.App.Net.IBXService.DNS.Client;

namespace IBXClientDemo.DNS
{
    public partial class FormTestDNSClient : Form
    {
        public FormTestDNSClient()
        {
            InitializeComponent();
        }

        private void btnGetServiceUrl_Click(object sender, EventArgs e)
        {
            string sTransCode = edtTransCode.Text;

            IBMRequestBase req = new IBMRequestBase();
            req.TransCode = sTransCode;
            req.TransCodeChs = "验证码图片破解";

            string sServiceAddress = "";
            try
            {
                sServiceAddress = IBXDnsClient.Instance.ServiceAddressOfREQ(req);
            }
            catch (Exception ex)
            {
                edtServiceUrl.Text = "错误: " + ex.Message;
                return;
            }

            edtServiceUrl.Text = sServiceAddress;
        }
    }
}
