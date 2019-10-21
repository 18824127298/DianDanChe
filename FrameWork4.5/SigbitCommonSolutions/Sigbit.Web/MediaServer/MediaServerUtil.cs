using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Text.RegularExpressions;
using System.IO;

using Sigbit.Common;

namespace Sigbit.Web.MediaServer
{
    /// <summary>
    /// 媒体服务工具类
    /// </summary>
    public class MediaServerUtil
    {

        private static string _formatKey = "（～＃×＃～）";
        /// <summary>
        /// 对文本内容进行格式化
        /// </summary>
        /// <returns></returns>
        public static string UrlFormat(string sText)
        {
            string sRootUrl = MediaServerConfig.Instance.RootUrl;
            return sText.Replace(sRootUrl, _formatKey);
        }

        /// <summary>
        /// 对文本内容进行还原
        /// </summary>
        /// <param name="sText"></param>
        /// <returns></returns>
        public static string UrlUnFormat(string sText)
        {
            string sRootUrl = MediaServerConfig.Instance.RootUrl;
            return sText.Replace(_formatKey, sRootUrl);
        }

        /// <summary>
        /// 获取文本内容中的MediaServer下的文件
        /// </summary>
        /// <param name="sContent"></param>
        /// <returns></returns>
        public static string[] GetMediaServerFiles(string sContent)
        {
            string sPattern = "\"(（～＃×＃～）.*?)\"";
            Regex reg = new Regex(sPattern);

            ArrayList lstFiles = new ArrayList();
            MatchCollection mc = reg.Matches(sContent);

            foreach (Match m in mc)
            {
                string sUrlFile = m.Groups[1].Value;
                MediaServerPath path = new MediaServerPath();
                path.FormatFullUrl = sUrlFile;

                if (!lstFiles.Contains(path.FullPath))
                {
                    lstFiles.Add(path.FullPath);
                }
            }

            return (string[])lstFiles.ToArray("".GetType());

        }




       
    }
}
