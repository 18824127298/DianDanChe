using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Data.DBPerfMonitor.Common
{
    public class UTDCMSqlRegulator
    {
        public static string RegulaterSQL(string sInputSQL)
        {
            //=========== 1. 压缩所有的空格 =================
            string sCompressSpace = CompressSpace(sInputSQL);

            //========== 2. 压缩符号的前后空格 ==============
            string sCompressSymbol = CompressSymbolSpace(sCompressSpace);

            //========= 3. 置换字符串 =============
            string sReplaceString = ReplaceStringContent(sCompressSymbol);

            //========== 4. 置换数字 ==============
            string sReplaceDigit = ReplaceDigitContent(sReplaceString);

            return sReplaceDigit;
        }

        private static string CompressSpace(string sInputString)
        {
            string sRet = "";
            bool bMeetSpace = false;

            for (int i = 0; i < sInputString.Length; i++)
            {
                char ch = sInputString[i];

                if (IsSpaceChar(ch))
                {
                    bMeetSpace = true;
                    continue;
                }
                else
                {
                    if (bMeetSpace)
                    {
                        sRet += ' ';
                        bMeetSpace = false;
                    }

                    sRet += ch;
                }
            }

            return sRet;
        }

        private static bool IsSpaceChar(char ch)
        {
            switch (ch)
            {
                case '\r':
                case '\n':
                case '\t':
                case ' ':
                    return true;
                default:
                    return false;
            }
        }

        private static string CompressSymbolSpace(string sInputString)
        {
            string sRet = "";
            bool bMeetSpace = false;
            bool bMeetSymbol = false;

            for (int i = 0; i < sInputString.Length; i++)
            {
                char ch = sInputString[i];

                if (ch == ' ')
                {
                    if (!bMeetSymbol)
                        bMeetSpace = true;
                    
                    bMeetSymbol = false;
                    continue;
                }
                else if (IsSymbolChar(ch))
                {
                    bMeetSymbol = true;
                    bMeetSpace = false;
                    sRet += ch;
                    continue;
                }
                else
                {
                    bMeetSymbol = false;

                    if (bMeetSpace)
                    {
                        sRet += ' ';
                        bMeetSpace = false;
                    }

                    sRet += ch;
                }
            }

            return sRet;
        }

        private static bool IsSymbolChar(char ch)
        {
            switch (ch)
            {
                case '<':
                case '>':
                case '=':
                case '(':
                case ')':
                case ',':
                    return true;
                default:
                    return false;
            }
        }

        private static string ReplaceStringContent(string sInputString)
        {
            string sRet = "";
            bool bMeetQuote = false;

            for (int i = 0; i < sInputString.Length; i++)
            {
                char ch = sInputString[i];

                if (ch == '\'')
                {
                    if (bMeetQuote)
                    {
                        if (IsContinuousQuote(sInputString, i))
                        {
                            i++;
                        }
                        else
                        {
                            bMeetQuote = false;
                            sRet += "'xxxx'";
                        }
                    }
                    else
                        bMeetQuote = true;

                    continue;
                }
                else
                {
                    if (bMeetQuote)
                        continue;

                    sRet += ch;
                }
            }

            return sRet;
        }

        private static bool IsContinuousQuote(string sInputString, int nCurrentPos)
        {
            if (nCurrentPos >= sInputString.Length - 1)
                return false;

            char cCurrentChar = sInputString[nCurrentPos];
            if (cCurrentChar != '\'')
                return false;

            char cNextChar = sInputString[nCurrentPos + 1];
            if (cNextChar != '\'')
                return false;

            return true;
        }

        private static string ReplaceDigitContent(string sInputString)
        {
            string sRet = "";
            bool bMeetDigit = false;

            for (int i = 0; i < sInputString.Length; i++)
            {
                char ch = sInputString[i];

                if (IsDigitChar(ch))
                {
                    if (!bMeetDigit)
                    {
                        //======== 1. 判断之前的字符是不是符号 ============
                        bool bPreviousCharIsSymbol = false;
                        if (i != 0)
                        {
                            if (IsSymbolChar(sInputString[i - 1]))
                                bPreviousCharIsSymbol = true;
                        }

                        if (bPreviousCharIsSymbol)
                            bMeetDigit = true;
                        else
                            sRet += ch;
                    }
                }
                else
                {
                    if (bMeetDigit)
                    {
                        bMeetDigit = false;
                        sRet += 9999;
                    }

                    sRet += ch;
                }
            }

            return sRet;
        }

        private static bool IsDigitChar(char ch)
        {
            if (ch >= '0' && ch <= '9')
                return true;

            if (ch == '.')
                return true;

            return false;
        }
    
    }
}
