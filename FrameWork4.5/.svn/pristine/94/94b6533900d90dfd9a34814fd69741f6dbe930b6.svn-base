using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Diagnostics;

using Sigbit.Common;

namespace Sigbit.Data.FoxDBF.TestForm
{
    public partial class FormTestFoxDBFClasses : Form
    {
        public FormTestFoxDBFClasses()
        {
            InitializeComponent();
        }

        private const string TEST_DBF = "C:\\TEMP\\PERSON_1.DBF";

        private void btn0CreateDBF_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            DBFCreate dcCreate = new DBFCreate(TEST_DBF, 5);

            dcCreate.AddField("name", DBFFieldType.Char, 10);
            dcCreate.AddField("age", DBFFieldType.Number, 7);
            dcCreate.AddField("career", DBFFieldType.Char, 12);
            dcCreate.AddField("birthday", DBFFieldType.Date);
            dcCreate.AddField("married", DBFFieldType.Logic);

            dcCreate.CreateDBF();

            stopWatch.Stop();
            memoOutput.Text = "(" + stopWatch.ElapsedMilliseconds.ToString() + " ms)";
        }

        private void btn1Append_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            FoxDBFile foxDBF = new FoxDBFile();
            foxDBF.AttachFile(TEST_DBF);

            for (int i = 1; i <= 1000; i++)
            {
                foxDBF.SetRecordData("name", i.ToString() + RandUtil.NewChsString(1,3));
                int nAge = RandUtil.NewNumber(15, 55);
                foxDBF.SetRecordData("age", nAge);
                foxDBF.SetRecordData("career", RandUtil.NewString(4, 9, RandStringType.Lower));
                foxDBF.SetRecordData("birthday", DateTime.Now.AddYears(-1 * nAge).AddDays(-1 * RandUtil.NewNumber(0, 365)));
                bool bMarried = false;
                if (nAge >= 20)
                {
                    if (nAge % 10 != 0)
                        bMarried = true;
                }
                foxDBF.SetRecordData("married", bMarried);

                foxDBF.Append();
            }

            foxDBF.CloseDBF();

            stopWatch.Stop();
            memoOutput.Text = "1000 records appended.\r\n";
            memoOutput.Text += "(" + stopWatch.ElapsedMilliseconds.ToString() + " ms)";
        }

        private void btn2Browse_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            StringBuilder sbOutput = new StringBuilder();

            FoxDBFile foxDBF = new FoxDBFile();
            foxDBF.AttachFile(TEST_DBF);

            for (int i = 1; i <= foxDBF.RecCount; i++)
            {
                foxDBF.Go(i);

                string sName = foxDBF.GetRecordString("name");
                int nAge = foxDBF.GetRecordInt("age");
                string sCareer = foxDBF.GetRecordString("career");
                DateTime dtBirthday = foxDBF.GetRecordDateTime("birthday");
                bool bMarried = foxDBF.GetRecordBool("married");

                sbOutput.Append(i.ToString() + ": ");

                sbOutput.Append(sName + ",");
                sbOutput.Append(nAge.ToString() + ",");
                sbOutput.Append(sCareer + ",");
                sbOutput.Append(dtBirthday.ToString() + ",");
                sbOutput.AppendLine(bMarried.ToString());
            }

            foxDBF.CloseDBF();

            stopWatch.Stop();
            sbOutput.AppendLine();
            sbOutput.AppendLine("(" + stopWatch.ElapsedMilliseconds.ToString() + " ms)");
            memoOutput.Text = sbOutput.ToString();
        }

        private void btn3Delete_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            StringBuilder sbOutput = new StringBuilder();

            int nDeleteCount = 0;
            int nHasDeletedCount = 0;
            int nNormalCount = 0;

            FoxDBFile fdFoxDBF = new FoxDBFile();
            fdFoxDBF.AttachFile(TEST_DBF);

            for (int i = 1; i <= fdFoxDBF.RecCount; i++)
            {
                fdFoxDBF.Go(i);

                if (fdFoxDBF.Deleted)
                {
                    nHasDeletedCount++;
                    sbOutput.AppendLine(i.ToString() + ": HAS BEEN DELETED");
                }
                else
                {
                    if (RandUtil.NewNumber(3) == 0)
                    {
                        fdFoxDBF.Delete();
                        nDeleteCount++;
                        sbOutput.AppendLine(i.ToString() + ": ---------- DELETE ----------");
                    }
                    else
                    {
                        nNormalCount++;
                        sbOutput.AppendLine(i.ToString() + ":");
                    }
                }
            }

            fdFoxDBF.CloseDBF();

            sbOutput.AppendLine();
            sbOutput.AppendLine(nDeleteCount.ToString() + " records deleted in this process.");
            sbOutput.AppendLine(nHasDeletedCount.ToString() + " records has been deleted before.");
            sbOutput.AppendLine(nNormalCount.ToString() + " records remain in DB.");

            stopWatch.Stop();
            sbOutput.AppendLine("(" + stopWatch.ElapsedMilliseconds.ToString() + " ms)");

            memoOutput.Text = sbOutput.ToString();
        }

        private void btn4Insert_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            
            FoxDBFile fdFoxDBF = new FoxDBFile();
            fdFoxDBF.AttachFile(TEST_DBF);

            for (int i = 1; i <= 10; i++)
            {
                fdFoxDBF.Go(i * 10);

                fdFoxDBF.SetRecordData("name", "====IN====");
                fdFoxDBF.SetRecordData("age", i * 100);
                fdFoxDBF.SetRecordData("career", "INSERTED");
                fdFoxDBF.SetRecordData("birthday", DateTime.Now);
                fdFoxDBF.SetRecordData("married", true);

                fdFoxDBF.Insert();
            }

            fdFoxDBF.CloseDBF();

            stopWatch.Stop();
            memoOutput.Text = "(" + stopWatch.ElapsedMilliseconds.ToString() + " ms)";
        }

        private void btn5ZAP_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            FoxDBFile fdFoxDBF = new FoxDBFile();
            fdFoxDBF.AttachFile(TEST_DBF);

            fdFoxDBF.ZAP();

            fdFoxDBF.CloseDBF();

            stopWatch.Stop();
            memoOutput.Text = "(" + stopWatch.ElapsedMilliseconds.ToString() + " ms)";
        }

        private void btn6Pack_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            FoxDBFile fdFoxDBF = new FoxDBFile();
            fdFoxDBF.AttachFile(TEST_DBF);

            fdFoxDBF.Pack();

            fdFoxDBF.CloseDBF();

            stopWatch.Stop();
            memoOutput.Text = "(" + stopWatch.ElapsedMilliseconds.ToString() + " ms)";
        }

        private void btn7Update_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            FoxDBFile fdFoxDBF = new FoxDBFile();
            fdFoxDBF.AttachFile(TEST_DBF);

            for (int i = 1; i <= fdFoxDBF.RecCount; i++)
            {
                fdFoxDBF.Go(i);

                int nAge = fdFoxDBF.GetRecordInt("age");
                nAge += 100;
                fdFoxDBF.SetRecordData("age", nAge);

                fdFoxDBF.Update();
            }

            fdFoxDBF.CloseDBF();

            stopWatch.Stop();
            memoOutput.Text = "(" + stopWatch.ElapsedMilliseconds.ToString() + " ms)";
        }
    }
}