using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Web;
using System.Web.Configuration;

namespace Sigbit.Common
{
    /// <summary>
    /// Ӧ�õ�·��
    /// </summary>
    public class AppPath
    {
        /// <summary>
        /// Ӧ�õĸ�·��(Ӧmango 2013-12)
        /// </summary>
        public static string AppRootPath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
            }
        }

        /// <summary>
        /// �õ�Ӧ��������ȫ·��
        /// </summary>
        /// <param name="sDirName">Ŀ¼��</param>
        /// <param name="sFileName">�ļ���</param>
        /// <returns>������·��</returns>
        public static string AppFullPath(string sDirName, string sFileName)
        {
            //======== 1. Ŀ¼�Ĺ��� =============
            string sAbsDirName = sDirName;
            if (!IsAbsolutePath(sAbsDirName))
            {
                sAbsDirName = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\')
                        + "\\" + sAbsDirName;
            }

            //======== 2. ������Ŀ¼���ж� ==========
            string sRet = sAbsDirName + "\\" + sFileName;
            if (File.Exists(sRet))
                return sRet;
            if (Directory.Exists(sRet))
                return sRet;

            //========== 3. �������Ŀ¼ ============
            string sSeriesName = GetPathSeriesName();
            if (sSeriesName == "")
                sRet = sAbsDirName + "\\" + sFileName;
            else
                sRet = sAbsDirName + "\\" + sSeriesName + "\\" + sFileName;

            return sRet;
        }

        /// <summary>
        /// �Ƿ����·��
        /// </summary>
        /// <param name="sPathName">·����</param>
        /// <returns>�Ƿ����·��</returns>
        private static bool IsAbsolutePath(string sPathName)
        {
            if (sPathName.IndexOf(":") >= 0)
                return true;

            if (sPathName[0] == '\\' || sPathName[0] == '/')
                return true;

            return false;
        }

        private static string _currentVirtualPath = "PCJNAINQMB";

        /// <summary>
        /// �õ���ǰ������Ŀ¼
        /// </summary>
        /// <returns>��ǰ������Ŀ¼</returns>
        public static string GetCurrentVirtualPath()
        {
            if (_currentVirtualPath != "PCJNAINQMB")
                return _currentVirtualPath;

            string sRet;
            if (HttpContext.Current == null)
                sRet = "";
            else
            {
                try
                {
                    sRet = HttpContext.Current.Request.ApplicationPath
                            .Replace("/", "").Replace("\\", "");
                }
                catch
                {
                    sRet = "";
                }
            }
            _currentVirtualPath = sRet;
            return sRet;
        }

        private static string _pathSeriesName = "";
        /// <summary>
        /// �����������������������
        /// </summary>
        /// <param name="sSeriesName">����</param>
        public static void SetPathSeriesName(string sSeriesName)
        {
            _pathSeriesName = sSeriesName;
        }

        private static string GetPathSeriesName()
        {
            if (_pathSeriesName != "")
                return _pathSeriesName;

            return GetCurrentVirtualPath();
        }
    }
}
