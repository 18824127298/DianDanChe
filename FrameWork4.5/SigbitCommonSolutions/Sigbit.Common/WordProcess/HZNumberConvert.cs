using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Common.WordProcess
{
    /// <summary>
    /// �����ַ���������֮���ת������
    /// </summary>
    public class HZNumberConvert
    {
        /// <summary>
        ///  ������תΪ��д�ĺ�������Ҵ�д��ʾ
        /// </summary>
        /// <param name="fMoney">��������ʾ��Ǯ</param>
        /// <returns>���ֱ�ʾ��Ǯ</returns>
        /// <remarks>
        /// 1. ֧�ֵ���Ԫ��
        /// 2. ��֧�ָ�����
        /// </remarks>
        public static string FloatToHZMoney(double fMoney)
        {
            if (fMoney < 0)
                return "";

            if (fMoney == 0)
                return "��Բ��";

            string[] arrDig = new string[] 
                    { "��", "Ҽ", "��", "��", "��", 
                      "��", "½", "��", "��", "��" };

            //======== 1. ת������ǰ�������ֱַ�ת�� ==========
            string sMoney = fMoney.ToString("0.00");
            string[] arrPart = sMoney.Split('.');
            string sNewChar = "";
            string sIntegerPart = arrPart[0];

            //======= 2. С����ǰ����ת�� ========
            int nIntegerPartLenght = sIntegerPart.Length;
            if (nIntegerPartLenght > 10)
                return "";

            for (int i = nIntegerPartLenght - 1; i >= 0; i--)
            {
                string sTmpNewChar = "";
                char cPerChar = sIntegerPart[i];
                sTmpNewChar = arrDig[(int)cPerChar - (int)'0'] + sTmpNewChar;

                switch (nIntegerPartLenght - i - 1)
                {
                    case 0: sTmpNewChar += "Բ"; break;
                    case 1: if (cPerChar != '0') sTmpNewChar += "ʰ"; break;
                    case 2: if (cPerChar != '0') sTmpNewChar += "��"; break;
                    case 3: if (cPerChar != '0') sTmpNewChar += "Ǫ"; break;
                    case 4: sTmpNewChar += "��"; break;
                    case 5: if (cPerChar != '0') sTmpNewChar += "ʰ"; break;
                    case 6: if (cPerChar != '0') sTmpNewChar += "��"; break;
                    case 7: if (cPerChar != '0') sTmpNewChar += "Ǫ"; break;
                    case 8: sTmpNewChar += "��"; break;
                    case 9: sTmpNewChar += "ʰ"; break;
                }
                sNewChar = sTmpNewChar + sNewChar;
            }

            //======== 3. С����֮�����ת�� ==========
            string sDecimalPart = arrPart[1];
            if (sDecimalPart != "00")
            {
                int nDecimalPartLength = sDecimalPart.Length;
                for (int i = 0; i < nDecimalPartLength; i++)
                {
                    string sTmpNewChar = "";
                    char cPerChar = sDecimalPart[i];
                    sTmpNewChar = arrDig[(int)cPerChar - (int)'0'] + sTmpNewChar;
                    if (i == 0) sTmpNewChar += "��";
                    if (i == 1) sTmpNewChar += "��";
                    sNewChar += sTmpNewChar;
                }
            }

            //========== 4. �滻�������ú��� ===========
            while (sNewChar.IndexOf("����") != -1)
                sNewChar = sNewChar.Replace("����", "��");
            sNewChar = sNewChar.Replace("����", "��");
            sNewChar = sNewChar.Replace("����", "��");
            sNewChar = sNewChar.Replace("����", "��"); 
            sNewChar = sNewChar.Replace("��Բ", "Բ"); 
            sNewChar = sNewChar.Replace("���", "��"); 
            sNewChar = sNewChar.Replace("���", "");

            if (sNewChar.Substring(0, 1) == "Բ")
                sNewChar = sNewChar.Substring(1);

            if (sNewChar.Substring(0, 1) == "��")
                sNewChar = sNewChar.Substring(1);

            if (sNewChar.Substring(0, 2) == "Ҽʰ")
                sNewChar = sNewChar.Substring(1);

            if (sNewChar.EndsWith("Բ") || sNewChar.EndsWith("��"))
                sNewChar += "��";

            return sNewChar;
        }

        /// <summary>
        /// ��һ������ת��Ϊ���ֵ��ַ�����ʾ
        /// </summary>
        /// <param name="fNumber">������</param>
        /// <returns>���ֵ��ַ�����ʾ</returns>
        /// <remarks>��ʾ�ľ���ΪС�����3λ</remarks>
        public static string FloatToHZString(double fNumber)
        {
            if (fNumber < 0)
                return "";

            string[] arrDig = new string[] { "��", "һ", "��", "��", "��", "��", "��", "��", "��", "��" };

            //======== 1. ת������ǰ�������ֱַ�ת�� ==========
            string sMoney, sNewChar = "";
            string[] arrPart;

            sMoney = fNumber.ToString("0.000");
            arrPart = sMoney.Split('.');


            //======= 2. С����ǰ����ת�� ==========
            string sIntegerPart;
            int nIntegerPartLength, i;
            string sTmpNewChar;
            char cPerChar;

            sIntegerPart = arrPart[0];
            nIntegerPartLength = sIntegerPart.Length;
            if (nIntegerPartLength > 10)
                return "";

            for (i = nIntegerPartLength - 1; i >= 0; i--)
            {
                sTmpNewChar = "";
                cPerChar = sIntegerPart[i];
                sTmpNewChar = arrDig[(int)cPerChar - (int)'0'] + sTmpNewChar;

                switch (nIntegerPartLength - i - 1)
                {
                    case 0: sTmpNewChar += ""; break;
                    case 1: if (cPerChar != '0') sTmpNewChar += "ʮ"; break;
                    case 2: if (cPerChar != '0') sTmpNewChar += "��"; break;
                    case 3: if (cPerChar != '0') sTmpNewChar += "ǧ"; break;
                    case 4: sTmpNewChar += "��"; break;
                    case 5: if (cPerChar != '0') sTmpNewChar += "ʮ"; break;
                    case 6: if (cPerChar != '0') sTmpNewChar += "��"; break;
                    case 7: if (cPerChar != '0') sTmpNewChar += "ǧ"; break;
                    case 8: sTmpNewChar += "��"; break;
                    case 9: sTmpNewChar += "ʮ"; break;
                }

                sNewChar = sTmpNewChar + sNewChar;
            }

            while (sNewChar.IndexOf("����") != -1)
                sNewChar = sNewChar.Replace("����", "��");

            //======== 3. С����֮�����ת�� ==========
            string sDecimalPart;
            int nDecimalPartLength;

            sDecimalPart = arrPart[1];
            if (sDecimalPart != "000")
            {
                sNewChar += '��';

                //=========== 3.1 ����β���ϵ�"0" ======
                while (sDecimalPart.EndsWith("0"))
                    sDecimalPart = sDecimalPart.Substring(0, sDecimalPart.Length - 1);

                nDecimalPartLength = sDecimalPart.Length;
                for (i = 0; i < nDecimalPartLength; i++)
                {
                    cPerChar = sDecimalPart[i];
                    sTmpNewChar = arrDig[(int)cPerChar - (int)'0'];

                    sNewChar += sTmpNewChar;
                }
            }

            //========== 4. �滻�������ú��� ===========
            sNewChar = sNewChar.Replace("����", "��");
            sNewChar = sNewChar.Replace("����", "��");
            sNewChar = sNewChar.Replace("����", "��");
            sNewChar = sNewChar.Replace("��ʮ", "��");
            sNewChar = sNewChar.Replace("���", "��");

            if (sNewChar.EndsWith("��"))
                sNewChar = sNewChar.Substring(0, sNewChar.Length - 1);

            if (sNewChar.StartsWith("һʮ"))
                sNewChar = sNewChar.Substring(1);

            if (sNewChar.StartsWith("��"))
                sNewChar = "��" + sNewChar;

            return sNewChar;
        }

        /// <summary>
        /// ���ִ�ת��Ϊ������
        /// </summary>
        /// <param name="sHZString">���ִ�</param>
        /// <returns>������</returns>
        /// <remarks>ȱʡΪ0</remarks>
        public static double HZStringToFloat(string sHZString)
        {
            return HZStringToFloat(sHZString, 0);
        }

        /// <summary>
        /// ���ִ�ת��Ϊ������
        /// </summary>
        /// <param name="sHZString">���ִ�</param>
        /// <param name="fDefault">ȱʡֵ</param>
        /// <returns>������</returns>
        public static double HZStringToFloat(string sHZString, double fDefault)
        {
            //========= 1. ��ת�����еĺ������ֵ����������� ===========
            string sDigString = sHZString;
            sDigString = sDigString.Replace("��", "0");
            sDigString = sDigString.Replace("һ", "1");
            sDigString = sDigString.Replace("��", "2");
            sDigString = sDigString.Replace("��", "2");
            sDigString = sDigString.Replace("��", "3");
            sDigString = sDigString.Replace("��", "4");
            sDigString = sDigString.Replace("��", "5");
            sDigString = sDigString.Replace("��", "6");
            sDigString = sDigString.Replace("��", "7");
            sDigString = sDigString.Replace("��", "8");
            sDigString = sDigString.Replace("��", "9");
            sDigString = sDigString.Replace("��", ".");

            if (sDigString == sHZString)
            {
                if (sDigString.IndexOf("ʮ") != -1)
                    sDigString = sDigString.Replace("ʮ", "1ʮ");
                else
                    return fDefault;
            }

            //======= 2. �ҳ��ڡ��򡢵����ֵ��ĸ������� =======
            //===== 2.1 �� ==> sYIString ========
            string sYIString;
            int nYIPos = sDigString.IndexOf("��");
            if (nYIPos == -1)
                sYIString = "";
            else
            {
                sYIString = sDigString.Substring(0, nYIPos);
                sDigString = sDigString.Substring(nYIPos + 1);
            }

            //===== 2.2 �� ==> sWANString ========
            string sWANString;
            int nWANPos = sDigString.IndexOf("��");
            if (nWANPos == -1)
                sWANString = "";
            else
            {
                sWANString = sDigString.Substring(0, nWANPos);
                sDigString = sDigString.Substring(nWANPos + 1);
            }

            //======= 2.3 �� ==> sGEString��С�� ==> sDecimalString ======
            string sGEString, sDecimalString;
            int nPointPos = sDigString.IndexOf(".");
            if (nPointPos == -1)
            {
                sGEString = sDigString;
                sDecimalString = "";
            }
            else
            {
                sGEString = sDigString.Substring(0, nPointPos);
                sDecimalString = sDigString.Substring(nPointPos + 1);
            }

            //========== 3. ת���ڡ��򡢸������� ========
            int nYI = HZStringToFloat__4Dig(sYIString);
            int nWAN = HZStringToFloat__4Dig(sWANString);
            int nGE = HZStringToFloat__4Dig(sGEString);

            double fDecimal = HZStringToFloat__Decimal(sDecimalString);

            //======== 4. ת��С���� ==========
            return nYI * 100000000 + nWAN * 10000 + nGE + fDecimal;
        }

        private static int HZStringToFloat__4Dig(string sHZString)
        {
            int[] arrDig = new int[] { 0, 0, 0, 0 };
            int nCurrentDig = 0;

            for (int i = 0; i < sHZString.Length; i++)
            {
                char cChar = sHZString[i];

                if (cChar >= '0' && cChar <= '9')
                    nCurrentDig = (int)cChar - (int)'0';

                switch (cChar)
                {
                    case 'ǧ':
                        arrDig[3] = nCurrentDig;
                        nCurrentDig = 0;
                        break;
                    case '��':
                        arrDig[2] = nCurrentDig;
                        nCurrentDig = 0;
                        break;
                    case 'ʮ':
                        if (nCurrentDig == 0)
                            nCurrentDig = 1;
                        arrDig[1] = nCurrentDig;
                        nCurrentDig = 0;
                        break;
                }
            }

            if (nCurrentDig != 0)
                arrDig[0] = nCurrentDig;

            return arrDig[3] * 1000 + arrDig[2] * 100 + arrDig[1] * 10 + arrDig[0];
        }

        private static double HZStringToFloat__Decimal(string sHZString)
        {
            string sNumberString = "";
            for (int i = 0; i < sHZString.Length; i++)
            {
                char cChar = sHZString[i];
                if (cChar >= '0' && cChar <= '9')
                    sNumberString += cChar;
            }

            sNumberString = "0." + sNumberString;

            double fRet = Convert.ToDouble(sNumberString);

            return fRet;
        }
    }
}
