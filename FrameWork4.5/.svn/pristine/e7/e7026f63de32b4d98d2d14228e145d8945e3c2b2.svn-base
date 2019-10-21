using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

namespace Sigbit.Framework
{
    public class SbtTaskDaemonDemo2 : SbtTaskDaemonBase
    {
        public override SbtTaskRunningResult DoProcess()
        {
            //========= 1. 打开文件 ============
            StreamWriter writer;
            string sLine;
            writer = File.CreateText("c:\\temp\\task_demo_2.txt");

            //========= 2. 写入文件 ============
            sLine = "============ THIS IS THE 2ND DEMO ==============";
            writer.WriteLine(sLine);

            sLine = "history_uid\x9" + "{722662A2-9436-4af8-9993-2C46DDF216C1}";
            writer.WriteLine(sLine);

            sLine = "time\x9" + DateTime.Now.ToString();
            writer.WriteLine(sLine);

            sLine = "register_uid\x9" + "{08E57CFF-85B7-4c90-B713-69CDADB29B40}";
            writer.WriteLine(sLine);

            //========= 3. 关闭文件 ============
            writer.Flush();
            writer.Close();

            this.TaskMsg = "示例二写入文件，时间为" + DateTime.Now.ToString();

            return SbtTaskRunningResult.Finish;
        }
    }
}
