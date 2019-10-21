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
        /// ����JS��·��,ʾ��../../
        /// </summary>
        /// <param name="CurrentPage">��ǰҳ</param>
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
        /// ����ĳ�Ӵ����ַ����в����Ĵ���
        /// </summary>
        /// <param name="sSubStr">��Ѱ�ҵ��Ӵ�</param>
        /// <param name="sString">�����Ӵ����ַ���</param>
        /// <returns>�����õ��Ĵ���</returns>
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
