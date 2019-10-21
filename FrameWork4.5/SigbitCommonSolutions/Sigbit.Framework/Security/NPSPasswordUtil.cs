using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Framework.Security
{
    /// <summary>
    /// ���밲ȫ��ص�Ӧ����
    /// </summary>
    public class NPSPasswordUtil
    {
        /// <summary>
        /// �����ǿ��
        /// </summary>
        /// <param name="sPassword">����</param>
        /// <returns>ǿ��</returns>
        public static int PasswordStrengthOfPassword(string sPassword)
        {
            //========== 1. �㼶ǿ�ȣ�����Ϊ�� =============
            if (sPassword == "")
                return 0;

            //============ 2. һ��ǿ�� =========
            if (sPassword.Length < 6)
                return 1;

            //======= 3. �õ�������� =============
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
        /// ������������
        /// </summary>
        /// <param name="sPassword">����</param>
        /// <returns>�������</returns>
        private static int PasswordCombinationCount(string sPassword)
        {
            //========== 1. ��Ǹ������ ===========
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

            //============ 2. ������������� ============
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
