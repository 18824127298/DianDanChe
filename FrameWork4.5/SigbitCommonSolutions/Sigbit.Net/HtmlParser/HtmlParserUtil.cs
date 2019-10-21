using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Net.HtmlParser
{
    public class HtmlParserUtil
    {
        /// <summary>
        /// 进入一个嵌套的单元
        /// </summary>
        /// <param name="parser">解析结果实例</param>
        /// <param name="nCurrentSeq">当前位置</param>
        /// <param name="sTagName">嵌套单元标识</param>
        /// <returns>进入后的位置</returns>
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
        /// 得到后面的文本
        /// </summary>
        /// <param name="parser">解析结果实例</param>
        /// <param name="nCurrentSeq">当前位置</param>
        /// <returns>得到的文本。一直取到遇到文本为止。</returns>
        static public string FetchText(HtmlParser parser, int nCurrentSeq)
        {
            return FetchText(parser, nCurrentSeq, -1);
        }

        /// <summary>
        /// 得到后面的文本
        /// </summary>
        /// <param name="parser">解析结果实例</param>
        /// <param name="nCurrentSeq">当前位置</param>
        /// <param name="nUntilSeq">一直取到的位置。</param>
        /// <returns>得到的文本</returns>
        static public string FetchText(HtmlParser parser, int nCurrentSeq, int nUntilSeq)
        {
            if (nCurrentSeq == -1)
                return "";

            //============= 1. 如果没有指定取到哪，则取到最后一项 ===========
            string sRet = "";
            int nFinalSeq;
            if (nUntilSeq == -1)
                nFinalSeq = parser.GetElementCount() - 1;
            else
                nFinalSeq = nUntilSeq;

            //======== 2. 循环取出所有文本 ==========
            while (nCurrentSeq <= nFinalSeq)
            {
                HtmlElement ele = parser.GetElement(nCurrentSeq);

                if (ele.ElementType == HtmlElementType.Text)
                {
                    sRet += ele.Value;

                    //===== 3. 如果没有指定取到哪，又遇到了文本，则直接返回该文本 =======
                    if (nUntilSeq == -1)
                        break;
                }

                nCurrentSeq ++;
            }

            return sRet;
        }

        /// <summary>
        ///  跳过指定个数的标记
        /// </summary>
        /// <param name="parser">解析结果实例</param>
        /// <param name="nCurrentSeq">当前位置</param>
        /// <param name="sTagName">嵌套单元标识</param>
        /// <param name="nNeedSkipNum">跳过的数量</param>
        /// <returns>跳过标记后的位置，如果无法完成则返回-1</returns>
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
        /// 得到相应的结束位置
        /// </summary>
        /// <param name="parser">网页解析实例</param>
        /// <param name="nThisPos">当前位置</param>
        /// <returns>结束位置</returns>
        public static int GetEndOfTagPos(HtmlParser parser, int nThisPos)
        {
            HtmlElement eleThis = parser.GetElement(nThisPos);
            if (eleThis.ElementType != HtmlElementType.Start)
                throw new Exception("HtmlParserUtil.GetEndOfTagPos() error: "
                        + "本标记非Start类型标记");

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
        /// 得到最大的无嵌套表位置
        /// </summary>
        /// <param name="parser">网页解析实例</param>
        /// <param name="nStartTableSeq">起始位置</param>
        /// <param name="nEndTableSeq">终止位置</param>
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
        /// 得到包含最多文本的无嵌套表位置
        /// </summary>
        /// <param name="parser">网页解析实例</param>
        /// <param name="nStartTableSeq">起始位置</param>
        /// <param name="nEndTableSeq">终止位置</param>
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
