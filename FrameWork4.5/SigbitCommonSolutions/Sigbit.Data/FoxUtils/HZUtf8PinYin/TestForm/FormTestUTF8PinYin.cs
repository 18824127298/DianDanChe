using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Diagnostics;

namespace Sigbit.Data.FoxUtils.HZUtf8PinYin.TestForm
{
    public partial class FormTestUTF8PinYin : Form
    {
        public FormTestUTF8PinYin()
        {
            InitializeComponent();
        }

        private void btnGetPinYinOfEveryHZ_Click(object sender, EventArgs e)
        {
            Stopwatch stopW = new Stopwatch();
            stopW.Start();

            StringBuilder sbOutput = new StringBuilder();


            string sInput = edtInput.Text;

            for (int i = 0; i < sInput.Length; i++)
            {
                char cHZ = sInput[i];
                string sPinYin = FUXHanzPinYin.Instance.GetPinYinOfHZChar(cHZ);

                sbOutput.Append(sPinYin + "(" + cHZ + ") ");
            }

            stopW.Stop();
            sbOutput.AppendLine();
            sbOutput.AppendLine("(¹²" + sInput.Length.ToString() + "ºº×Ö¡£" 
                    + stopW.ElapsedMilliseconds.ToString() + " ms. )");

            memoOutput.Text = sbOutput.ToString();
        }

        private void btnGetPinYinOfString_Click(object sender, EventArgs e)
        {
            Stopwatch stopW = new Stopwatch();
            stopW.Start();

            StringBuilder sbOutput = new StringBuilder();

            string sInput = edtInput.Text;
            string sPYOfString = FUXHanzPinYin.Instance.GetPinYinOfHZString(sInput);
            sbOutput.Append(sPYOfString);

            stopW.Stop();
            sbOutput.AppendLine();
            sbOutput.AppendLine("(¹²" + sInput.Length.ToString() + "ºº×Ö¡£"
                    + stopW.ElapsedMilliseconds.ToString() + " ms. )");

            memoOutput.Text = sbOutput.ToString();
        }

        private void btnGetJianPinOfString_Click(object sender, EventArgs e)
        {
            Stopwatch stopW = new Stopwatch();
            stopW.Start();

            StringBuilder sbOutput = new StringBuilder();

            string sInput = edtInput.Text;
            string sJianPinOfString = FUXHanzPinYin.Instance.GetJianPinOfHZString(sInput);
            sbOutput.Append(sJianPinOfString);

            stopW.Stop();
            sbOutput.AppendLine();
            sbOutput.AppendLine("(¹²" + sInput.Length.ToString() + "ºº×Ö¡£"
                    + stopW.ElapsedMilliseconds.ToString() + " ms. )");

            memoOutput.Text = sbOutput.ToString();
            //FUXHanzPinYin.Instance.TerminateProcess();
        }
    }
}