using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Net.HtmlParser
{
    public class HtmlUrl
    {
        /// <summary>
        ///  判断一个路径名是否是绝对路径名
        /// </summary>
        /// <param name="s">路径名</param>
        /// <returns>是否绝对路径</returns>
        static public bool IsAbsolutePathName(string s)
        {
            if (s.Length <= 0)
                return false;

            char ch = s[0];
            if (ch == '\\' || ch == '/')
                return true;

            if (s.Trim().Substring(1, 1) == ":")
                return true;

            return false;
        }

        /// <summary>
        /// 将URL格式化为绝对路径的URL
        /// </summary>
        /// <param name="sUrl">待格式化的URL引用</param>
        /// <param name="sRefUrl">参考URL，该URL必须为绝对路径</param>
        /// <returns>格式化过的绝对路径URL</returns>
        static public string RegulateToAbsoluteURL(string sUrl, string sRefUrl)
        {
            int nPos;

            //========== 1. 如果已是绝对路径，则返回 =========
            if (sUrl.Contains("://"))
                return sUrl;

            if (!sRefUrl.Contains("://"))
                throw new Exception("HtmlUrl.RegulateToAbsoluteURL() : "
                        + "reference URL is not absolute - " + sRefUrl);

            if (sRefUrl.EndsWith("/"))
                sRefUrl = sRefUrl + "index.html";

            //======= 2. 如果URL以'/'开头，则为参考URL的主机部分加上URL =========
            if (IsAbsolutePathName(sUrl))
            {
                return RegulateToAbsoluteURL__HostPart(sRefUrl) + sUrl.Substring(1);
            }
            else
            {
                //====== 3. 否则进行相对路径的规整处理 ===========
                if (sUrl.Substring(0, 2) == "./")
                    sUrl = sUrl.Substring(2);

                while (sUrl.Substring(0, 3) == "../")
                {
                    sUrl = sUrl.Substring(3);

                    if (sRefUrl.EndsWith("/"))
                        sRefUrl = sRefUrl.Substring(0, sRefUrl.Length - 1);

                    nPos = sRefUrl.LastIndexOf("/");
                    sRefUrl = sRefUrl.Substring(0, nPos+1);
                }


                if (sRefUrl.EndsWith("/"))
                    sRefUrl = sRefUrl.Substring(0, sRefUrl.Length - 1);

                nPos = sRefUrl.LastIndexOf("/");
                sRefUrl = sRefUrl.Substring(0, nPos+1);

                return sRefUrl + sUrl;
            }
        }

        static public string RegulateToAbsoluteURL__HostPart(string sUrl)
        {
            int nPos;

            nPos = sUrl.IndexOf("://");
            if (nPos == -1)
                throw new Exception("HtmlUrl.RegulateToAbsoluteURL__HostPart() : "
                        + "can not find '://'");

            int nHostPos;
            nHostPos = sUrl.IndexOf("/", nPos + 3);
            if (nHostPos == -1)
                throw new Exception("HtmlUrl.RegulateToAbsoluteURL__HostPart() error : "
                        + "can not locate host part.");
            return sUrl.Substring(0, nHostPos + 1);
        }
    }
}
