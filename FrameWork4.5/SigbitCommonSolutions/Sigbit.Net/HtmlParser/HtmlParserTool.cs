using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.IO;

namespace Sigbit.Net.HtmlParser
{
    /// <summary>
    /// 基于HtmlParser的工具
    /// </summary>
    public class HtmlParserTool
    {
        private static bool _genEmptyLineBeforeParagraph = false;
        /// <summary>
        /// 在段落前生成一个空行
        /// </summary>
        public static bool GenEmptyLineBeforeParagraph
        {
            get { return HtmlParserTool._genEmptyLineBeforeParagraph; }
            set { HtmlParserTool._genEmptyLineBeforeParagraph = value; }
        }

        /// <summary>
        /// 将网页的内容整理到文本的数组中
        /// </summary>
        /// <param name="sHtmlContent">网页的内容</param>
        /// <returns>文本的数组</returns>
        public static string[] HtmlContentToTextArray(string sHtmlContent)
        {
            ArrayList list = new ArrayList();

            //======== 1. 解析内容并循环处理每一个元素 =============
            HtmlParser parser = new HtmlParser();
            parser.Parse(sHtmlContent);

            int nElementCount = parser.GetElementCount();
            bool bSkipText = false;
            string sLine = "";

            for (int i = 0; i < nElementCount; i++)
            {
                HtmlElement ele = parser.GetElement(i);

                //========= 2. 跳过正文的判断 =============
                if (ele.ElementType == HtmlElementType.Start && ele.Value == "style")
                    bSkipText = true;
                if (ele.ElementType == HtmlElementType.Start && ele.Value == "script")
                    bSkipText = true;
                if (ele.ElementType == HtmlElementType.Start && ele.Value == "title")
                    bSkipText = true;
                if (ele.ElementType == HtmlElementType.End && ele.Value == "style")
                    bSkipText = false;
                if (ele.ElementType == HtmlElementType.End && ele.Value == "script")
                    bSkipText = false;
                if (ele.ElementType == HtmlElementType.End && ele.Value == "title")
                    bSkipText = false;

                //=========== 3. 加入正文文字 ==========
                if (ele.ElementType == HtmlElementType.Text)
                {
                    if (!bSkipText)
                        sLine += ele.Value;
                }

                //======== 4. 段落标记、分割符的处理 =========
                if (ele.ElementType == HtmlElementType.End)
                {
                    if (ele.Value == "p")
                    {
                        list.Add(sLine);

                        if (GenEmptyLineBeforeParagraph)
                            list.Add("");

                        sLine = "";
                    }
                    else if (ele.Value == "td")
                        sLine += "\t";
                    else if (ele.Value == "tr")
                    {
                        list.Add(sLine);
                        sLine = "";
                    }
                }

                if (ele.ElementType == HtmlElementType.Start)
                {
                    if (ele.Value == "br")
                    {
                        list.Add(sLine);
                        sLine = "";
                    }
                }
            }

            if (sLine != "")
            {
                list.Add(sLine);
                sLine = "";
            }

            //=========== 5. 整理到字符串数组 ==============
            string[] arrRet = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
                arrRet[i] = ((string)list[i]).Replace("\r", "").Replace("\n", "");

            return arrRet;
        }

        /// <summary>
        /// 将网页内容整理到文本文件中
        /// </summary>
        /// <param name="sHtmlContent">网页内容</param>
        /// <param name="sFileName">文本文件名</param>
        public static void HtmlContentToTextFile(string sHtmlContent, string sFileName)
        {
            string[] arrText = HtmlContentToTextArray(sHtmlContent);

            //========== 1. 打开文件 ================
            StreamWriter writer;
            writer = new StreamWriter(
                         (System.IO.Stream)File.Create(sFileName),
                         System.Text.Encoding.Default);

            //========= 2. 写出每一行 ============
            for (int i = 0; i < arrText.Length; i++)
            {
                string sLine = arrText[i];
                writer.WriteLine(sLine);
            }

            //========= 3. 关闭文件 ===============
            writer.Close();
        }
    }
}
