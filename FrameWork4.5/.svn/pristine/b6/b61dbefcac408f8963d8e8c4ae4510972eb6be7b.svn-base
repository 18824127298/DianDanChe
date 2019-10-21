using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Common.WordProcess
{
    /// <summary>
    /// 汉字字符串和数字之间的转换函数
    /// </summary>
    public class HZNumberConvert
    {
        /// <summary>
        ///  将数字转为大写的汉字人民币大写表示
        /// </summary>
        /// <param name="fMoney">浮点数表示的钱</param>
        /// <returns>汉字表示的钱</returns>
        /// <remarks>
        /// 1. 支持到亿元；
        /// 2. 不支持负数；
        /// </remarks>
        public static string FloatToHZMoney(double fMoney)
        {
            if (fMoney < 0)
                return "";

            if (fMoney == 0)
                return "零圆整";

            string[] arrDig = new string[] 
                    { "零", "壹", "贰", "叁", "肆", 
                      "伍", "陆", "柒", "捌", "玖" };

            //======== 1. 转换采用前后两部分分别转换 ==========
            string sMoney = fMoney.ToString("0.00");
            string[] arrPart = sMoney.Split('.');
            string sNewChar = "";
            string sIntegerPart = arrPart[0];

            //======= 2. 小数点前进行转化 ========
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
                    case 0: sTmpNewChar += "圆"; break;
                    case 1: if (cPerChar != '0') sTmpNewChar += "拾"; break;
                    case 2: if (cPerChar != '0') sTmpNewChar += "佰"; break;
                    case 3: if (cPerChar != '0') sTmpNewChar += "仟"; break;
                    case 4: sTmpNewChar += "万"; break;
                    case 5: if (cPerChar != '0') sTmpNewChar += "拾"; break;
                    case 6: if (cPerChar != '0') sTmpNewChar += "佰"; break;
                    case 7: if (cPerChar != '0') sTmpNewChar += "仟"; break;
                    case 8: sTmpNewChar += "亿"; break;
                    case 9: sTmpNewChar += "拾"; break;
                }
                sNewChar = sTmpNewChar + sNewChar;
            }

            //======== 3. 小数点之后进行转化 ==========
            string sDecimalPart = arrPart[1];
            if (sDecimalPart != "00")
            {
                int nDecimalPartLength = sDecimalPart.Length;
                for (int i = 0; i < nDecimalPartLength; i++)
                {
                    string sTmpNewChar = "";
                    char cPerChar = sDecimalPart[i];
                    sTmpNewChar = arrDig[(int)cPerChar - (int)'0'] + sTmpNewChar;
                    if (i == 0) sTmpNewChar += "角";
                    if (i == 1) sTmpNewChar += "分";
                    sNewChar += sTmpNewChar;
                }
            }

            //========== 4. 替换所有无用汉字 ===========
            while (sNewChar.IndexOf("零零") != -1)
                sNewChar = sNewChar.Replace("零零", "零");
            sNewChar = sNewChar.Replace("零亿", "亿");
            sNewChar = sNewChar.Replace("零万", "万");
            sNewChar = sNewChar.Replace("亿万", "亿"); 
            sNewChar = sNewChar.Replace("零圆", "圆"); 
            sNewChar = sNewChar.Replace("零角", "零"); 
            sNewChar = sNewChar.Replace("零分", "");

            if (sNewChar.Substring(0, 1) == "圆")
                sNewChar = sNewChar.Substring(1);

            if (sNewChar.Substring(0, 1) == "零")
                sNewChar = sNewChar.Substring(1);

            if (sNewChar.Substring(0, 2) == "壹拾")
                sNewChar = sNewChar.Substring(1);

            if (sNewChar.EndsWith("圆") || sNewChar.EndsWith("角"))
                sNewChar += "整";

            return sNewChar;
        }

        /// <summary>
        /// 将一串数字转换为汉字的字符串表示
        /// </summary>
        /// <param name="fNumber">浮点数</param>
        /// <returns>汉字的字符串表示</returns>
        /// <remarks>表示的精度为小数点后3位</remarks>
        public static string FloatToHZString(double fNumber)
        {
            if (fNumber < 0)
                return "";

            string[] arrDig = new string[] { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };

            //======== 1. 转换采用前后两部分分别转换 ==========
            string sMoney, sNewChar = "";
            string[] arrPart;

            sMoney = fNumber.ToString("0.000");
            arrPart = sMoney.Split('.');


            //======= 2. 小数点前进行转化 ==========
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
                    case 1: if (cPerChar != '0') sTmpNewChar += "十"; break;
                    case 2: if (cPerChar != '0') sTmpNewChar += "百"; break;
                    case 3: if (cPerChar != '0') sTmpNewChar += "千"; break;
                    case 4: sTmpNewChar += "万"; break;
                    case 5: if (cPerChar != '0') sTmpNewChar += "十"; break;
                    case 6: if (cPerChar != '0') sTmpNewChar += "百"; break;
                    case 7: if (cPerChar != '0') sTmpNewChar += "千"; break;
                    case 8: sTmpNewChar += "亿"; break;
                    case 9: sTmpNewChar += "十"; break;
                }

                sNewChar = sTmpNewChar + sNewChar;
            }

            while (sNewChar.IndexOf("零零") != -1)
                sNewChar = sNewChar.Replace("零零", "零");

            //======== 3. 小数点之后进行转化 ==========
            string sDecimalPart;
            int nDecimalPartLength;

            sDecimalPart = arrPart[1];
            if (sDecimalPart != "000")
            {
                sNewChar += '点';

                //=========== 3.1 砍掉尾巴上的"0" ======
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

            //========== 4. 替换所有无用汉字 ===========
            sNewChar = sNewChar.Replace("零亿", "亿");
            sNewChar = sNewChar.Replace("零万", "万");
            sNewChar = sNewChar.Replace("亿万", "亿");
            sNewChar = sNewChar.Replace("零十", "零");
            sNewChar = sNewChar.Replace("零点", "点");

            if (sNewChar.EndsWith("零"))
                sNewChar = sNewChar.Substring(0, sNewChar.Length - 1);

            if (sNewChar.StartsWith("一十"))
                sNewChar = sNewChar.Substring(1);

            if (sNewChar.StartsWith("点"))
                sNewChar = "零" + sNewChar;

            return sNewChar;
        }

        /// <summary>
        /// 汉字串转换为浮点数
        /// </summary>
        /// <param name="sHZString">汉字串</param>
        /// <returns>浮点数</returns>
        /// <remarks>缺省为0</remarks>
        public static double HZStringToFloat(string sHZString)
        {
            return HZStringToFloat(sHZString, 0);
        }

        /// <summary>
        /// 汉字串转换为浮点数
        /// </summary>
        /// <param name="sHZString">汉字串</param>
        /// <param name="fDefault">缺省值</param>
        /// <returns>浮点数</returns>
        public static double HZStringToFloat(string sHZString, double fDefault)
        {
            //========= 1. 先转换所有的汉字数字到阿拉伯数字 ===========
            string sDigString = sHZString;
            sDigString = sDigString.Replace("零", "0");
            sDigString = sDigString.Replace("一", "1");
            sDigString = sDigString.Replace("二", "2");
            sDigString = sDigString.Replace("两", "2");
            sDigString = sDigString.Replace("三", "3");
            sDigString = sDigString.Replace("四", "4");
            sDigString = sDigString.Replace("五", "5");
            sDigString = sDigString.Replace("六", "6");
            sDigString = sDigString.Replace("七", "7");
            sDigString = sDigString.Replace("八", "8");
            sDigString = sDigString.Replace("九", "9");
            sDigString = sDigString.Replace("点", ".");

            if (sDigString == sHZString)
            {
                if (sDigString.IndexOf("十") != -1)
                    sDigString = sDigString.Replace("十", "1十");
                else
                    return fDefault;
            }

            //======= 2. 找出亿、万、点区分的四个数字区 =======
            //===== 2.1 亿 ==> sYIString ========
            string sYIString;
            int nYIPos = sDigString.IndexOf("亿");
            if (nYIPos == -1)
                sYIString = "";
            else
            {
                sYIString = sDigString.Substring(0, nYIPos);
                sDigString = sDigString.Substring(nYIPos + 1);
            }

            //===== 2.2 万 ==> sWANString ========
            string sWANString;
            int nWANPos = sDigString.IndexOf("万");
            if (nWANPos == -1)
                sWANString = "";
            else
            {
                sWANString = sDigString.Substring(0, nWANPos);
                sDigString = sDigString.Substring(nWANPos + 1);
            }

            //======= 2.3 个 ==> sGEString，小数 ==> sDecimalString ======
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

            //========== 3. 转换亿、万、个的数字 ========
            int nYI = HZStringToFloat__4Dig(sYIString);
            int nWAN = HZStringToFloat__4Dig(sWANString);
            int nGE = HZStringToFloat__4Dig(sGEString);

            double fDecimal = HZStringToFloat__Decimal(sDecimalString);

            //======== 4. 转换小数点 ==========
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
                    case '千':
                        arrDig[3] = nCurrentDig;
                        nCurrentDig = 0;
                        break;
                    case '百':
                        arrDig[2] = nCurrentDig;
                        nCurrentDig = 0;
                        break;
                    case '十':
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
