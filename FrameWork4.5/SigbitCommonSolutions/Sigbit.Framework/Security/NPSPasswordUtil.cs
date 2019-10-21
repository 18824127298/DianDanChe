using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Framework.Security
{
    /// <summary>
    /// 密码安全相关的应用类
    /// </summary>
    public class NPSPasswordUtil
    {
        /// <summary>
        /// 密码的强度
        /// </summary>
        /// <param name="sPassword">密码</param>
        /// <returns>强度</returns>
        public static int PasswordStrengthOfPassword(string sPassword)
        {
            //========== 1. 零级强度，可以为空 =============
            if (sPassword == "")
                return 0;

            //============ 2. 一级强度 =========
            if (sPassword.Length < 6)
                return 1;

            //======= 3. 得到组合数量 =============
            int nCombinationCount = PasswordCombinationCount(sPassword);
            if (nCombinationCount >= 4)
                return 4;
            else if (nCombinationCount >= 3)
                return 3;
            else if (nCombinationCount >= 2)
                return 2;
            else
                return 1;
        }

        /// <summary>
        /// 密码的组合数量
        /// </summary>
        /// <param name="sPassword">密码</param>
        /// <returns>组合数量</returns>
        private static int PasswordCombinationCount(string sPassword)
        {
            //========== 1. 标记各类组合 ===========
            bool bLower = false;
            bool bUpper = false;
            bool bDigit = false;
            bool bOther = false;

            for (int i = 0; i < sPassword.Length; i++)
            {
                char ch = sPassword[i];

                if (ch >= '0' && ch <= '9')
                    bDigit = true;
                else if (ch >= 'A' && ch <= 'Z')
                    bUpper = true;
                else if (ch >= 'a' && ch <= 'z')
                    bLower = true;
                else
                    bOther = true;
            }

            //============ 2. 对组合数量计数 ============
            int nRet = 0;
            if (bLower)
                nRet++;
            if (bUpper)
                nRet++;
            if (bDigit)
                nRet++;
            if (bOther)
                nRet++;

            return nRet;
        }
    }
}
