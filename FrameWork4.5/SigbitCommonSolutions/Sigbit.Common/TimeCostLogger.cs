using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

namespace Sigbit.Common
{
    /// <summary>
    /// ��ʱ��Ϣ��¼
    /// </summary>
    public class TimeCostLogger
    {
        static StringBuilder _sbOutputText = new StringBuilder();

        static DateTime _dtStartLoggerTime;
        static string _sStartLoggerMsg = "";
        static string _sStartLoggerKey = "";

        static DateTime _dtLastTickTime;

        /// <summary>
        /// ��ʼ��ʱ
        /// </summary>
        /// <param name="sMsg">��ʱ��Ϣ����</param>
        /// <param name="sKey">��ʱ�ؼ���</param>
        public static void Start(string sMsg, string sKey)
        {
            _sbOutputText = new StringBuilder();

            _dtStartLoggerTime = DateTime.Now;
            _sStartLoggerMsg = sMsg;
            _sStartLoggerKey = sKey;

            _dtLastTickTime = _dtStartLoggerTime;
        }

        /// <summary>
        /// ��ʼ��ʱ
        /// </summary>
        /// <param name="sMsg">��ʱ��Ϣ����</param>
        public static void Start(string sMsg)
        {
            Start(sMsg, "");
        }

        /// <summary>
        /// ��ʼ��ʱ
        /// </summary>
        public static void Start()
        {
            Start("", "");
        }

        /// <summary>
        /// �м��ʱ
        /// </summary>
        /// <param name="sMsg">��ʱ��Ϣ</param>
        public static void Tick(string sMsg)
        {
            string sLine = "        TICK ";

            DateTime dtNow = DateTime.Now;
            TimeSpan tsEllapsed = dtNow - _dtLastTickTime;
            _dtLastTickTime = dtNow;

            int fEllapsedMS = (int)tsEllapsed.TotalMilliseconds;

            string sEllapsedMS = fEllapsedMS.ToString();
            sEllapsedMS = sEllapsedMS.PadLeft(6, ' ');

            sLine += sEllapsedMS + " ms";
            if (sMsg != "")
                sLine += "    " + sMsg;

            _sbOutputText.AppendLine(sLine);
        }

        /// <summary>
        /// �м��ʱ
        /// </summary>
        public static void Tick()
        {
            Tick("");
        }

        /// <summary>
        /// ������ʱ������¼�ı���־��
        /// </summary>
        /// <param name="sMsg">��ʱ��Ϣ</param>
        public static void Stop(string sMsg)
        {
            lock (typeof(TimeCostLogger))
            {
                DateTime dtStopTime = DateTime.Now;

                //========= 1. ���ļ� ==========
                string sLogDirectory = SigbitCommonConfig.Instance.TimeCostLoggerDirectory;
                string sLogFileName = sLogDirectory + "\\timetune_"
                        + DateTimeUtil.Now.Substring(0, 10).Replace("-", "") + ".vlog";

                FileStream fs = File.OpenWrite(sLogFileName);
                fs.Seek(0, SeekOrigin.End);

                //====== 2. д�뿪ʼ��ʱ��Ϣ =============
                string sLine = "==== TIMECOST at " + _dtStartLoggerTime.ToString() + ", cost ";
                int nEllaspsed = (int)(dtStopTime - _dtStartLoggerTime).TotalMilliseconds;
                sLine += nEllaspsed + " ms ";

                if (_sStartLoggerKey != "" || _sStartLoggerMsg != "")
                {
                    sLine += "(";
                    if (_sStartLoggerKey != "")
                        sLine += _sStartLoggerKey + " : ";
                    sLine += _sStartLoggerMsg;
                    sLine += ") ";
                }

                sLine += "====\r\n";
                byte[] bsLog = Encoding.Default.GetBytes(sLine);
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 3. д���м��ʱ��Ϣ ==========
                string sAllTickMessage = _sbOutputText.ToString();

                bsLog = Encoding.Default.GetBytes(sAllTickMessage);
                fs.Write(bsLog, 0, bsLog.Length);

                _sbOutputText = new StringBuilder();

                //========== 4. д�������ʱ��Ϣ ============
                sLine = "        STOP ";

                int nEllapsedMS = (int)(dtStopTime - _dtLastTickTime).TotalMilliseconds;

                string sEllapsedMS = nEllapsedMS.ToString();
                sEllapsedMS = sEllapsedMS.PadLeft(6, ' ');

                sLine += sEllapsedMS + " ms";
                if (sMsg != "")
                    sLine += "    " + sMsg + "\r\n\r\n";
                bsLog = Encoding.Default.GetBytes(sLine);
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 5. �ر��ļ� ==========
                fs.Flush();
                fs.Close();
            }
        }

    }
}
