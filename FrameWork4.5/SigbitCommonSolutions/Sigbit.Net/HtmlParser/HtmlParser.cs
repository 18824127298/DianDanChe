using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.IO;

namespace Sigbit.Net.HtmlParser
{
    /// <summary>
    /// HTML��ǰ��־�����ͣ����ڽ����Ĺ���
    /// </summary>
    enum HtmlTagType
    {
        None,          // ��
        Comment,       // ע��
        DocType,       // �ĵ�����˵��
        End,           // �������
        Start          // ��ʼ���
    };

    /// <summary>
    /// ��ǰ������Ҫ���������ͣ����ڽ����Ĺ���
    /// </summary>
    enum ParserSkipType
    {
        SkipNone,       // ��
        SkipScript,     // ����Script
        SkipStyle       // ����Style
    };

    /// <summary>
    /// Html������
    /// </summary>
    public class HtmlParser
    {
        #region �ַ���������
        /// <summary>
        /// �ж��ַ����Ƿ���ָ�����ַ�����β
        /// </summary>
        /// <param name="sStr">ȫ�ַ���</param>
        /// <param name="sSubStr">��β�Ӵ�</param>
        /// <returns>�Ƿ���ָ�����ַ�����β</returns>
        private bool EndWith(string sStr, string sSubStr)
        {
            int nStartPos;
            nStartPos = sStr.Length - sSubStr.Length;
            if (nStartPos < 0)
                return false;

            if (sStr.Substring(nStartPos) == sSubStr)
                return true;
            else
                return false;
        }

        /// <summary>
        /// �ж��ַ����Ƿ���ָ�����ַ�����ͷ
        /// </summary>
        /// <param name="sStr">ȫ�ַ���</param>
        /// <param name="sSubStr">��ͷ�Ӵ�</param>
        /// <returns>�Ƿ���ָ�����ַ�����ͷ</returns>
        private bool StartWith(string sStr, string sSubStr)
        {
            if (sStr.Length < sSubStr.Length)
                return false;

            if (sStr.Substring(0, sSubStr.Length) == sSubStr)
                return true;
            else
                return false;
        }

        /// <summary>
        /// ȥ���ַ�����ǰ���ͺ�׺�ո�
        /// </summary>
        /// <param name="sStr">�ַ���</param>
        /// <remarks>�ո����CR��LF��TAB��SPACE</remarks>
        private void CutLeadingTrailingChar(ref string sStr)
        {
            //========== 1. ȥ��ǰ���ո� ==============
            int i = 0;
            while (i < sStr.Length
                    && (sStr[i] == ' ' || sStr[i] == '\r'
                    || sStr[i] == '\n' || sStr[i] == '\t'))
            {
                i++;
            }

            sStr = sStr.Substring(i);

            //======= 2. ȥ��β���ո� ==========
            i = sStr.Length - 1;
            while (i >= 0
                    && (sStr[i] == ' ' || sStr[i] == '\r'
                    || sStr[i] == '\n' || sStr[i] == '\t'))
            {
                i--;
            }

            sStr = sStr.Substring(0, i + 1);
        }
        #endregion

        #region �洢����Ԫ��
        private HtmlElementList _htmlElementList;
        #endregion

        #region ���캯��
        public HtmlParser()
        {
            _htmlElementList = new HtmlElementList();
        }
        #endregion

        #region ��������Ԫ��
        /// <summary>
        /// ����ע�Ͳ���Ӻ���
        /// </summary>
        /// <param name="sTagBuffer"></param>
        private void ParseCommentThenAdd(string sTagBuffer)
        {
            string sValue;

            //========= 1. ȥ����ǰ��4���ַ�: <!-- ==========
            sValue = sTagBuffer.Substring(4);

            //========= 2. ȥ����ǰ��3���ַ�: --> ==========
            if (sValue.Length >= 3)
                sValue = sValue.Substring(0, sValue.Length - 3);

            //====== 3. ����htmlԪ�� ===============
            _htmlElementList.AddHtmlElement(HtmlElementType.Comment, sTagBuffer, sValue);
        }

        private void ParseDocTypeThenAdd(string sTagBuffer)
        {
            string sValue;

            //========= 1. ȥ����ǰ��23���ַ�: <!DOCTYPE HTML PUBLIC " =========
            sValue = sTagBuffer.Substring(23);

            //======== 2. ȥ������2���ַ�: "> =========
            if (sValue.Length >= 2)
                sValue = sValue.Substring(0, sValue.Length - 2);

            //====== 3. ����htmlԪ�� ===============
            _htmlElementList.AddHtmlElement(HtmlElementType.DocType, sTagBuffer, sValue);
        }

        private void ParseEndTagThenAdd(string sTagBuffer)
        {
            string sValue;

            //========= 1. ȥ����ǰ��2���ַ�: </ =========
            sValue = sTagBuffer.Substring(2);

            //======== 2. ȥ������1���ַ�: > =========
            if (sValue.Length >= 1)
                sValue = sValue.Substring(0, sValue.Length - 1);

            sValue = sValue.ToLower();

            //====== 3. ����htmlԪ�� ===============
            _htmlElementList.AddHtmlElement(HtmlElementType.End, sTagBuffer, sValue);
        }

        private void ParseStartTagThenAdd(string sTagBuffer)
        {
            string sRawTagBuffer = sTagBuffer;

            //========= 1. ȥ����ǰ��1���ַ�: < =========
            sTagBuffer = sTagBuffer.Substring(1);

            //======== 2. ȥ������1���ַ�: > =========
            if (sTagBuffer.Length >= 1)
                sTagBuffer = sTagBuffer.Substring(0, sTagBuffer.Length - 1);

            //========= 3. ȥ��ǰ���ո� =========
            int i = 0;

            while (i < sTagBuffer.Length
                    && (sTagBuffer[i] == ' ' || sTagBuffer[i] == '\r'
                    || sTagBuffer[i] == '\n' || sTagBuffer[i] == '\t'))
            {
                i++;
            }

            //========= 4. ��ȡ���� ==========
            string sName = "";

            while (i < sTagBuffer.Length && sTagBuffer[i] != ' ')
            {
                sName += sTagBuffer[i];
                i++;
            }

            sName = sName.ToLower();

            //======== 5. ��ȡ���� ============
            string sAttributes;

            while (i < sTagBuffer.Length
                    && (sTagBuffer[i] == ' ' || sTagBuffer[i] == '\r'
                    || sTagBuffer[i] == '\n' || sTagBuffer[i] == '\t'))
            {
                i++;
            }

            sAttributes = sTagBuffer.Substring(i);

            //====== 6. ����htmlԪ�� ===============
            _htmlElementList.AddHtmlElement(HtmlElementType.Start, sRawTagBuffer, 
                    sName, sAttributes);
        }
        #endregion

        #region �������ݽ�������ȡ�������
        /// <summary>
        /// ����html�ı�����
        /// </summary>
        /// <param name="sContent">HTML�ı�����</param>
        /// <remarks>
        /// ����html��־������һ��ƽ�����Ϣ�б�����ʾ��־����������
        /// </remarks>
        public void Parse(string sContent)
        {
            //======== 1. �������Ԫ�� ==========
            _htmlElementList.Clear();

            if (sContent == "")
                return;

            string sTagBuffer = "";
            string sTextBuffer = "";
            bool bFoundTag = false;
            HtmlTagType currentTag = HtmlTagType.None;
            ParserSkipType skipTag = ParserSkipType.SkipNone;

            int nLineCounter = 1;
            int nColumnCounter = 1;

            int i;
            char cHtmlChar;

            //============ 2. ѭ������ÿһ���ַ� ============
            for (i = 0; i < sContent.Length; i++)
            {
                //========= 3. �����кš��кŵļ��� ==========
                cHtmlChar = sContent[i];
                switch (cHtmlChar)
                {
                    case '\r':
                        break;

                    case '\n':
                        nLineCounter++;
                        nColumnCounter = 1;
                        break;

                    default:
                        nColumnCounter++;
                        break;
                }

                //========== 4. �жϵ�ǰ���������� ==============
                switch (skipTag)
                {
                    //===== 4.1 SKIP_TAG == SKIP_SCRIPT��������� =======
                    case ParserSkipType.SkipScript:
                        sTextBuffer += cHtmlChar;
                        if (EndWith(sTextBuffer.ToUpper(), "</SCRIPT>"))
                        {
                            skipTag = ParserSkipType.SkipNone;

                            i -= 9;
                            nColumnCounter -= 9;

                            sTextBuffer = sTextBuffer.Substring(0, sTextBuffer.Length - 9);

                            //%%%%%% Add %%%%%
                            _htmlElementList.AddHtmlElement(HtmlElementType.Comment, sTextBuffer, sTextBuffer);
                            sTextBuffer = "";
                            //%%%%%%%% End Add %%%%
                        }
                        break;

                    //===== 4.2 SKIP_TAG == SKIP_STYLE��������� =======
                    case ParserSkipType.SkipStyle:
                        sTextBuffer += cHtmlChar;
                        if (EndWith(sTextBuffer.ToUpper(), "</STYLE>"))
                        {
                            skipTag = ParserSkipType.SkipNone;

                            i -= 8;
                            nColumnCounter -= 8;

                            sTextBuffer = sTextBuffer.Substring(0, sTextBuffer.Length - 8);
                        }
                        break;

                    //===== 4.3 SKIP_TAG == SKIP_NONE��������� =======
                    case ParserSkipType.SkipNone:
                        switch (cHtmlChar)
                        {
                            case '<':
                                #region switchCurrentTag
                                switch (currentTag)
                                {
                                    case HtmlTagType.Comment:
                                        sTagBuffer += cHtmlChar;
                                        break;

                                    case HtmlTagType.DocType:
                                        throw new HtmlParserException("Meet \'<\' while TAG_DOCTYPE",
                                                sContent, i, nLineCounter, nColumnCounter);

                                    case HtmlTagType.End:
                                        throw new HtmlParserException("Meet \'<\' while TAG_END",
                                                sContent, i, nLineCounter, nColumnCounter);

                                    case HtmlTagType.Start:
                                        //%%%%======== 20071124(Oldix Modified)���ı���������<3������ =====
                                        sTextBuffer = sTagBuffer;
                                        sTagBuffer = "";
                                        //sTextBuffer = "<" + sTextBuffer;
                                        _htmlElementList.AddHtmlElement(HtmlElementType.Text, sTextBuffer, sTextBuffer);
                                        sTextBuffer = "";
                                        sTagBuffer += cHtmlChar;
                                        bFoundTag = true;
                                        currentTag = HtmlTagType.None;
                                        break;
                                        //throw new HtmlParserException("Meet \'<\' while TAG_START",
                                        //        sContent, i, nLineCounter, nColumnCounter);

                                    default:
                                        if (!bFoundTag)
                                        {
                                            bFoundTag = true;
                                            sTagBuffer += cHtmlChar;

                                            CutLeadingTrailingChar(ref sTextBuffer);

                                            if (sTextBuffer != "")
                                                _htmlElementList.AddHtmlElement(HtmlElementType.Text,
                                                        sTextBuffer, sTextBuffer);

                                            sTextBuffer = "";
                                        }
                                        else
                                        {
                                            //throw new HtmlParserException("Meet \'<\' twice",
                                            //    sContent, i, nLineCounter, nColumnCounter);
                                        }
                                        break;
                                }
                                #endregion switchCurrentTag
                                break;

                            case '>':
                                #region switchCurrentTag
                                switch (currentTag)
                                {
                                    case HtmlTagType.Comment:
                                        if (EndWith(sTagBuffer, "--"))
                                        {
                                            currentTag = HtmlTagType.None;
                                            sTagBuffer += cHtmlChar;
                                            ParseCommentThenAdd(sTagBuffer);
                                            sTagBuffer = "";
                                        }
                                        else
                                            sTagBuffer += cHtmlChar;
                                        break;

                                    case HtmlTagType.DocType:
                                        currentTag = HtmlTagType.None;
                                        sTagBuffer += cHtmlChar;
                                        ParseDocTypeThenAdd(sTagBuffer);
                                        sTagBuffer = "";
                                        break;

                                    case HtmlTagType.End:
                                        currentTag = HtmlTagType.None;
                                        sTagBuffer += cHtmlChar;
                                        ParseEndTagThenAdd(sTagBuffer);
                                        sTagBuffer = "";
                                        break;

                                    case HtmlTagType.Start:
                                        currentTag = HtmlTagType.None;
                                        sTagBuffer += cHtmlChar;
                                        if (StartWith(sTagBuffer.ToUpper(), "<SCRIPT"))
                                            skipTag = ParserSkipType.SkipScript;

                                        if (StartWith(sTagBuffer.ToUpper(), "<STYLE"))
                                            skipTag = ParserSkipType.SkipStyle;

                                        ParseStartTagThenAdd(sTagBuffer);
                                        sTagBuffer = "";
                                        break;

                                    default:
                                        //throw new HtmlParserException("Meet \'>\' twice",
                                        //    sContent, i, nLineCounter, nColumnCounter);
                                        break;

                                }
                                #endregion
                                break;

                            default:
                                #region ifFoundTag
                                if (bFoundTag)
                                {
                                    bFoundTag = false;

                                    switch (cHtmlChar)
                                    {
                                        case '!':
                                            if (sContent[i + 1] == '-')
                                                currentTag = HtmlTagType.Comment;
                                            else
                                                currentTag = HtmlTagType.DocType;
                                            break;

                                        case '/':
                                            currentTag = HtmlTagType.End;
                                            break;

                                        default:
                                            currentTag = HtmlTagType.Start;
                                            break;
                                    }

                                    sTagBuffer += cHtmlChar;
                                }
                                else
                                {
                                    switch (currentTag)
                                    {
                                        case HtmlTagType.Comment:
                                            sTagBuffer += cHtmlChar;
                                            break;

                                        case HtmlTagType.DocType:
                                        case HtmlTagType.End:
                                        case HtmlTagType.Start:
                                            switch (cHtmlChar)
                                            {
                                                case '\r':
                                                case '\n':
                                                    break;

                                                default:
                                                    sTagBuffer += cHtmlChar;
                                                    break;
                                            }
                                            break;

                                        default:
                                            sTextBuffer += cHtmlChar;
                                            break;
                                    }
                                }
                                #endregion ifFoundTag
                                break;
                        }   // end of switch (cHtmlChar)
                        break;
                }   // end of switch (stSkipTag)
            }   // end of for (i = 1; i <= Length(sContent); i++)

            //========= 5. HTML��β�ж� =============
            if (sTagBuffer != "")
            {
                if (!EndWith(sTagBuffer, ">"))
                    throw new HtmlParserException("HTML never closed",
                            sContent, sContent.Length, nLineCounter, nColumnCounter);
            }

            CutLeadingTrailingChar(ref sTextBuffer);
            if (sTextBuffer != "")
            {
                _htmlElementList.AddHtmlElement(HtmlElementType.Text, sTextBuffer, sTextBuffer);
            }
        }

        public void ParseFile(string sFileName)
        {
            //========= 1. ���ļ� ============
            StreamReader reader;
            reader = new StreamReader(
                         (System.IO.Stream)File.OpenRead(sFileName),
                         System.Text.Encoding.GetEncoding("gb2312"));

            //========= 2. �����ļ����ر� ============
            string sContent;
            sContent = reader.ReadToEnd();

            reader.Close();

            //======= 3. �����ļ� =========
            Parse(sContent);
        }

        public int GetElementCount()
        {
            return _htmlElementList.Count;
        }

        public HtmlElement GetElement(int nIndex)
        {
            return (HtmlElement)_htmlElementList[nIndex];
        }

        #endregion

        #region ����Ԫ������
        /// <summary>
        /// ����ĳһԪ�ص�����
        /// </summary>
        /// <param name="sTagAttributes">�����ִ�</param>
        /// <returns>���Եġ��ؼ���-ֵ����</returns>
        static public Hashtable ParseAttributes(string sTagAttributes)
        {
            string sName, sValue;
            Hashtable htAttrs = new Hashtable();

            int nIndex = 0;
            char cHtmlChar;

            //======= 1. ѭ������ÿһ���ַ� ===============
            while (nIndex < sTagAttributes.Length)
            {
                cHtmlChar = sTagAttributes[nIndex];

                switch (cHtmlChar)
                {
                    //========= 2. �������Լ�Ŀհ��ַ� ==========
                    case '\r':
                    case '\n':
                    case '\t':
                    case ' ':
                        nIndex++;
                        break;

                    default:
                        sName = "";
                        sValue = "";

                        //======== 3. ��ȡ�������� ============
                        while (nIndex < sTagAttributes.Length)
                        {
                            cHtmlChar = sTagAttributes[nIndex];

                            if (cHtmlChar == '=' || cHtmlChar == ' ')
                                break;

                            sName += cHtmlChar;

                            nIndex++;
                        }

                        //========= 4. ��ȡ����ֵ =========
                        switch (cHtmlChar)
                        {
                            case '=':
                                nIndex++;

                                if (nIndex >= sTagAttributes.Length)
                                    return htAttrs;

                                cHtmlChar = sTagAttributes[nIndex];

                                switch (cHtmlChar)
                                {
                                    //=== 5. �������ֵ��˫����֮�䣬������˫���� ===
                                    case '"':
                                        nIndex++;
                                        while (nIndex < sTagAttributes.Length)
                                        {
                                            cHtmlChar = sTagAttributes[nIndex];
                                            if (cHtmlChar == '"')
                                                break;

                                            sValue += cHtmlChar;
                                            nIndex++;
                                        }
                                        nIndex++;
                                        break;

                                    //=== 6. �������ֵ�ڵ�����֮�䣬������������ ===
                                    case '\'':
                                        nIndex++;
                                        while (nIndex < sTagAttributes.Length)
                                        {
                                            cHtmlChar = sTagAttributes[nIndex];
                                            if (cHtmlChar == '\'')
                                                break;

                                            sValue += cHtmlChar;
                                            nIndex++;
                                        }
                                        nIndex++;
                                        break;

                                    default:
                                        while (nIndex < sTagAttributes.Length)
                                        {
                                            cHtmlChar = sTagAttributes[nIndex];
                                            if (cHtmlChar == ' ')
                                                break;

                                            sValue += cHtmlChar;
                                            nIndex++;
                                        }
                                        break;
                                }
                                break;  // break of case '=':

                            default:
                                sValue = "";
                                break;
                        }

                        htAttrs.Add(sName, sValue);
                        break;
                }
            }   // end of while (nIndex
            return htAttrs;
        }
        #endregion

        public void WriteParseResultToFile(string sFileName)
        {
            StreamWriter writer = new StreamWriter(
                         (System.IO.Stream)File.Create(sFileName),
                         System.Text.Encoding.GetEncoding("gb2312"));

            for (int i = 0; i < GetElementCount(); i++)
            {
                string sLine = i.ToString() + ":\t";
                HtmlElement ele = this.GetElement(i);
                sLine += ele.ElementType + "\t";
                sLine += ele.Value + "\t";
                sLine += ele.TagAttributes + "\t";

                writer.WriteLine(sLine);
            }

            writer.Close();
        }
    }

    /// <summary>
    /// HtmlԪ��
    /// </summary>
    public class HtmlElement
    {
        HtmlElementType _elementType;
        public HtmlElementType ElementType
        {
            get { return _elementType; }
            set { _elementType = value; }
        }

        string _fullText;
        public string FullText
        {
            get { return _fullText; }
            set { _fullText = value; }
        }

        string _value;
        public string Value
        {
            get 
            {
                if (_value.EndsWith("/"))
                    return _value.Substring(0, _value.Length - 1);
                else
                    return _value;
            }
            set { _value = value; }
        }

        string _tagAttributes;
        public string TagAttributes
        {
            get { return _tagAttributes; }
            set { _tagAttributes = value; }
        }

        /// <summary>
        /// �Ƿ���ʼ�ͽ�����Ϊһ�����
        /// </summary>
        /// <remarks>��β���Ƿ���'/'�����ж�</remarks>
        public bool IsStartEndElement
        {
            get
            {
                if (_elementType != HtmlElementType.Start)
                    return false;

                if (_value.EndsWith("/"))
                    return true;

                if (_tagAttributes.EndsWith("/"))
                    return true;
                else
                    return false;
            }
        }
    }

    public class HtmlElementList : ArrayList
    {
        public void AddHtmlElement(HtmlElementType elementType, string sFullText,
           string sValue, string sTagAttributes)
        {
            HtmlElement element = new HtmlElement();
            element.ElementType = elementType;
            element.FullText = sFullText;
            element.Value = sValue;
            element.TagAttributes = sTagAttributes;

            Add(element);
        }

        public void AddHtmlElement(HtmlElementType elementType, string sFullText,
           string sValue)
        {
            AddHtmlElement(elementType, sFullText, sValue, "");
        }
    }

    /// <summary>
    /// HtmlԪ�ص�����
    /// </summary>
    public enum HtmlElementType
    {
        Text = 'T',
        Comment = 'C',
        DocType = 'D',
        End = 'E',
        Start = 'S'
    };

    class HtmlParserException : Exception
    {
        private string _sHtmlContent;
        private int _nCharIndex;
        private int _nLineCounter;
        private int _nColumnCounter;

        public HtmlParserException(string sMessage, string sContent, int nIndex, 
                int nLineCounter, int nColumnCounter) : base(sMessage)
        {
            _sHtmlContent = sContent;
            _nCharIndex = nIndex;
            _nLineCounter = nLineCounter;
            _nColumnCounter = nColumnCounter;

            string sFullMessage;
            sFullMessage = sMessage;
            sFullMessage += "\n (HtmlParserError) ";
            sFullMessage += "\nEncounter error at line " + nLineCounter.ToString()
                    + ", column " + nColumnCounter.ToString();
        }
    }

    public class DemoCase_HtmlParser_ParseHtmlFile
    {
        public static void DoDemo(string sSrcFileName, string sDestFileName)
        {
            //========= 1. ���ļ� ============
            StreamReader reader;
                        //reader = File.OpenText(sSrcFileName);
            reader = new StreamReader(
                         (System.IO.Stream)File.OpenRead(sSrcFileName),
                         System.Text.Encoding.GetEncoding("gb2312"));

            //========= 2. �����ļ����ر� ============
            string sContent;
            sContent = reader.ReadToEnd();

            reader.Close();

            //======= 3. �����ļ� =========
            HtmlParser parser = new HtmlParser();
            parser.Parse(sContent);

            //======== 4. �����д���ļ� ==========
            //========= 4.1 ���ļ� ============
            StreamWriter writer;
            string sLine;
            int nLine;

            writer = new StreamWriter(
             (System.IO.Stream)File.Create(sDestFileName),
             System.Text.Encoding.GetEncoding("gb2312"));

            
            //writer = File.CreateText(sDestFileName);
            for (nLine = 0; nLine < parser.GetElementCount(); nLine++)
            {
                sLine = nLine.ToString() + ":\t";
                sLine += (char)parser.GetElement(nLine).ElementType + "\t";
                sLine += parser.GetElement(nLine).Value + "\t";
                sLine += parser.GetElement(nLine).TagAttributes + "\t";

                writer.WriteLine(sLine);
            }

            writer.Flush();
            writer.Close();

        }
    }
}
