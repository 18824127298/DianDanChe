using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Web;
using System.Web.UI;

using Sigbit.Common;

namespace Sigbit.Net.CsvPacket.BatchIO
{
    public class CSVExportPathUtil
    {
        private static DateTime _lastRemoveFileTime = DateTime.Now.AddDays(-1);

        public static string FileNameOfPrefix(string sFileNamePrefix)
        {
            //=========== 3. 文件名 ===========
            string sFileName = sFileNamePrefix + "_" + DateTimeUtil.ToDateTime14Str(DateTimeUtil.Now) 
                    + "_" + RandUtil.NewString(4, RandStringType.Lower) + ".csv";
            return sFileName;
        }

        public static string FilePathName(string sFileName)
        {
            //============= 1. 在data\downloads\目录下 ============
            string sDirPath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\data\\downloads";
            Directory.CreateDirectory(sDirPath);

            //=========== 2. 删掉两个小时前的文件 ===========
            //========== 2.1 每15分钟清一次数据 ====================
            if ((DateTime.Now - _lastRemoveFileTime).TotalMinutes > 15)
            {
                _lastRemoveFileTime = DateTime.Now;

                //======== 2.2 每次清两个小时前的数据 ===========
                int nRemoveFileCount = FileUtil.RemoveFilesBeforeTime(sDirPath, DateTime.Now.AddHours(-2));

                if (nRemoveFileCount != 0)
                {
                    DebugLogger.LogDebugMessage("删除文件夹" + sDirPath + "中的" + nRemoveFileCount.ToString() + "个文件",
                            "CSV_EXPORT_PATH_UTIL");
                }
            }

            //=========== 3. 全路径 ===========
            string sRet = sDirPath + "\\" + sFileName;

            return sRet;
        }

        public static string DownloadUrl(string sFileName)
        {
            return DownloadUrl(sFileName, false);
        }

        public static string DownloadUrl(string sFileName, bool bNeedAbsoluteUrl)
        {
            string sRelativeUrl = "../../data/downloads/" + sFileName;
            if (!bNeedAbsoluteUrl)
                return sRelativeUrl;

            Uri baseUri = HttpContext.Current.Request.Url;
            Uri absoluteUri = new Uri(baseUri, sRelativeUrl);

            return absoluteUri.AbsoluteUri;

            //=========== 以下是另一种方式得到绝对地址，保存在这里备用 =======

            //Page thePage = HttpContext.Current.Handler as Page;
            //if (thePage != null)
            //{
            //    string sAbsoluteUrl = thePage.ResolveUrl(sRelativeUrl);
            //    string sWithHttpUrl = "";

            //    HttpRequest request = HttpContext.Current.Request;

            //    if (request.IsSecureConnection)
            //        sWithHttpUrl = string.Format("https://{0}:{1}{2}", request.Url.Host, request.Url.Port, sAbsoluteUrl);
            //    else
            //        sWithHttpUrl = string.Format("http://{0}:{1}{2}", request.Url.Host, request.Url.Port, sAbsoluteUrl);

            //    return sWithHttpUrl;
            //}
            //else
            //    throw new Exception("CSVExportPathUtil.DownloadUrl() cannot get the current page");
        }
    }
}
