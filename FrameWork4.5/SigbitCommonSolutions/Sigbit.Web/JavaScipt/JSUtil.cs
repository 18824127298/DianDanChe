using System;
using System.Collections.Generic;
using System.Text;

using System.Web.UI;

namespace Sigbit.Web.JavaScipt
{
    public class JSUtil
    {
	
		public static string GenJSRootPath(object objParm)
        {
            return "../../";
        }
		
        /// <summary>
        /// 生成JS根路径,示例../../
        /// </summary>
        /// <param name="CurrentPage">当前页</param>
        /// <returns></returns>
        public static string GenJSRootPath(Page CurrentPage)
        {
            string sRet = "";
            int nFindCount = Occurs("/", CurrentPage.Page.AppRelativeVirtualPath);
            for (int i = 1; i < nFindCount; i++)
            {
                sRet += "../";
            }
            return sRet;
        }

        /// <summary>
        /// 计数某子串在字符串中产生的次数
        /// </summary>
        /// <param name="sSubStr">待寻找的子串</param>
        /// <param name="sString">包含子串的字符串</param>
        /// <returns>计数得到的次数</returns>
        private static int Occurs(string sSubStr, string sString)
        {
            int nRet = 0;
            bool bFound = true;

            while (bFound)
            {
                int nFindPos = sString.IndexOf(sSubStr);
                if (nFindPos != -1)
                {
                    nRet++;
                    sString = sString.Substring(nFindPos + sSubStr.Length);
                }
                else
                    bFound = false;
            }

            return nRet;
        }
    }

    
}
