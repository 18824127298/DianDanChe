using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Net.HtmlParser
{
    public class HtmlParserUtil
    {
        /// <summary>
        /// ����һ��Ƕ�׵ĵ�Ԫ
        /// </summary>
        /// <param name="parser">�������ʵ��</param>
        /// <param name="nCurrentSeq">��ǰλ��</param>
        /// <param name="sTagName">Ƕ�׵�Ԫ��ʶ</param>
        /// <returns>������λ��</returns>
        static public int EnterNestingTag(HtmlParser parser, int nCurrentSeq, string sTagName)
        {
            if (nCurrentSeq == -1)
                return -1;

            int nElementCount = parser.GetElementCount();
            HtmlElement element;
            while (nCurrentSeq < nElementCount)
            {
                element = parser.GetElement(nCurrentSeq);
                if (element.ElementType == HtmlElementType.Start && element.Value == sTagName)
                {
                    if (nCurrentSeq < nElementCount - 1)
                        return nCurrentSeq + 1;
                    else
                        return -1;
                }

                nCurrentSeq++;
            }
            return -1;
        }

        /// <summary>
        /// �õ�������ı�
        /// </summary>
        /// <param name="parser">�������ʵ��</param>
        /// <param name="nCurrentSeq">��ǰλ��</param>
        /// <returns>�õ����ı���һֱȡ�������ı�Ϊֹ��</returns>
        static public string FetchText(HtmlParser parser, int nCurrentSeq)
        {
            return FetchText(parser, nCurrentSeq, -1);
        }

        /// <summary>
        /// �õ�������ı�
        /// </summary>
        /// <param name="parser">�������ʵ��</param>
        /// <param name="nCurrentSeq">��ǰλ��</param>
        /// <param name="nUntilSeq">һֱȡ����λ�á�</param>
        /// <returns>�õ����ı�</returns>
        static public string FetchText(HtmlParser parser, int nCurrentSeq, int nUntilSeq)
        {
            if (nCurrentSeq == -1)
                return "";

            //============= 1. ���û��ָ��ȡ���ģ���ȡ�����һ�� ===========
            string sRet = "";
            int nFinalSeq;
            if (nUntilSeq == -1)
                nFinalSeq = parser.GetElementCount() - 1;
            else
                nFinalSeq = nUntilSeq;

            //======== 2. ѭ��ȡ�������ı� ==========
            while (nCurrentSeq <= nFinalSeq)
            {
                HtmlElement ele = parser.GetElement(nCurrentSeq);

                if (ele.ElementType == HtmlElementType.Text)
                {
                    sRet += ele.Value;

                    //===== 3. ���û��ָ��ȡ���ģ����������ı�����ֱ�ӷ��ظ��ı� =======
                    if (nUntilSeq == -1)
                        break;
                }

                nCurrentSeq ++;
            }

            return sRet;
        }

        /// <summary>
        ///  ����ָ�������ı��
        /// </summary>
        /// <param name="parser">�������ʵ��</param>
        /// <param name="nCurrentSeq">��ǰλ��</param>
        /// <param name="sTagName">Ƕ�׵�Ԫ��ʶ</param>
        /// <param name="nNeedSkipNum">����������</param>
        /// <returns>������Ǻ��λ�ã�����޷�����򷵻�-1</returns>
        static public int SkipNestingTag(HtmlParser parser, int nCurrentSeq, 
                string sTagName, int nNeedSkipNum)
        {
            if (nCurrentSeq == -1)
                return -1;

            int nElementCount = parser.GetElementCount();

            int nSkipCount = 0;
            int nNestingLoopCount = 0;

            while (nCurrentSeq < nElementCount)
            {
                HtmlElement ele = parser.GetElement(nCurrentSeq);
                
                if (ele.ElementType == HtmlElementType.Start && ele.Value == sTagName)
                {
                    nNestingLoopCount ++;
                }

                if (ele.ElementType == HtmlElementType.End && ele.Value == sTagName)
                {
                    nNestingLoopCount --;
                    if (nNestingLoopCount == 0)
                    {
                        nSkipCount ++;
                        if (nSkipCount == nNeedSkipNum)
                        {
                            if (nCurrentSeq < nElementCount - 1)
                                return nCurrentSeq + 1;
                            else
                                return -1;
                        }
                    }
                }

                nCurrentSeq ++;
            }

            return -1;
        }

        /// <summary>
        /// �õ���Ӧ�Ľ���λ��
        /// </summary>
        /// <param name="parser">��ҳ����ʵ��</param>
        /// <param name="nThisPos">��ǰλ��</param>
        /// <returns>����λ��</returns>
        public static int GetEndOfTagPos(HtmlParser parser, int nThisPos)
        {
            HtmlElement eleThis = parser.GetElement(nThisPos);
            if (eleThis.ElementType != HtmlElementType.Start)
                throw new Exception("HtmlParserUtil.GetEndOfTagPos() error: "
                        + "����Ƿ�Start���ͱ��");

            string sThisTag = eleThis.Value;
            int nTagCount = 1;
            for (int i = nThisPos + 1; i < parser.GetElementCount(); i++)
            {
                HtmlElement ele = parser.GetElement(i);
                if (ele.ElementType == HtmlElementType.Start && ele.Value == sThisTag)
                {
                    if (!ele.IsStartEndElement)
                        nTagCount++;
                }
                if (ele.ElementType == HtmlElementType.End && ele.Value == sThisTag)
                {
                    nTagCount--;
                    if (nTagCount == 0)
                        return i;
                }
            }
            return parser.GetElementCount() - 1;
        }

        /// <summary>
        /// �õ�������Ƕ�ױ�λ��
        /// </summary>
        /// <param name="parser">��ҳ����ʵ��</param>
        /// <param name="nStartTableSeq">��ʼλ��</param>
        /// <param name="nEndTableSeq">��ֹλ��</param>
        static public void GetLargestNonNestingTable(HtmlParser parser, ref int nStartTableSeq, ref int nEndTableSeq)
        {
            int i;
            HtmlElement ele;
            nStartTableSeq = -1;
            nEndTableSeq = -1;

            int nStart = -1;
            int nEnd = -1;
            int nMaxSize = -1;
            for (i = 0; i < parser.GetElementCount(); i++)
            {
                ele = parser.GetElement(i);
                if (ele.ElementType == HtmlElementType.Start && ele.Value == "table")
                    nStart = i;
                if (ele.ElementType == HtmlElementType.End && ele.Value == "table")
                {
                    if (nStart != -1)
                    {
                        nEnd = i;

                        int nSize = nEnd - nStart + 1;
                        if (nSize > nMaxSize)
                        {
                            nMaxSize = nSize;
                            nStartTableSeq = nStart;
                            nEndTableSeq = nEnd;
                        }

                        nStart = -1;
                        nEnd = -1;
                    }
                }
            }
        }

        /// <summary>
        /// �õ���������ı�����Ƕ�ױ�λ��
        /// </summary>
        /// <param name="parser">��ҳ����ʵ��</param>
        /// <param name="nStartTableSeq">��ʼλ��</param>
        /// <param name="nEndTableSeq">��ֹλ��</param>
        static public void GetLargestTextTableSection(HtmlParser parser, ref int nStartTableSeq, ref int nEndTableSeq)
        {
            int i;
            HtmlElement ele;
            nStartTableSeq = -1;
            nEndTableSeq = -1;

            int nStart = -1;
            int nEnd = -1;
            int nMaxSize = -1;
            int nSize = 0;
            for (i = 0; i < parser.GetElementCount(); i++)
            {
                ele = parser.GetElement(i);
                if (ele.ElementType == HtmlElementType.Text)
                {
                    if (nStart != -1)
                    {
                        nSize += ele.Value.Length;
                    }
                }

                if (ele.Value == "table")
                {
                    if (nStart != -1)
                    {
                        nEnd = i;

                        if (nSize > nMaxSize)
                        {
                            nMaxSize = nSize;
                            nStartTableSeq = nStart;
                            nEndTableSeq = nEnd;
                        }
                    }
                    nStart = i;
                    nEnd = -1;
                    nSize = 0;
                }
            }
        }
    }
}
