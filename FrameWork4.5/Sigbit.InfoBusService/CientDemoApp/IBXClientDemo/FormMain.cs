using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using IBXClientDemo.DNS;
using IBXClientDemo.VCodeBreak;
using IBXClientDemo.VoiceReg;

namespace IBXClientDemo
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuDNSTestClient_Click(object sender, EventArgs e)
        {
            FormTestDNSClient form = new FormTestDNSClient();
            form.ShowDialog();
        }

        private void mnuTestVCodeBreak_Click(object sender, EventArgs e)
        {
            FormTestVCodeBreakClient form = new FormTestVCodeBreakClient();
            form.ShowDialog();
        }

        private void mnuTestVoiceReg_Click(object sender, EventArgs e)
        {
            FormTestVoiceRegClient form = new FormTestVoiceRegClient();
            form.ShowDialog();
        }
    }
}
