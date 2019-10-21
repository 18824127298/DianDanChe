using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using System.Xml;
using System.Diagnostics;

namespace Sigbit.Common
{
    /// <summary>
    /// 注释
    /// </summary>
    class CBComment
    {
        private string _commentString = "";
        /// <summary>
        /// 注释文字
        /// </summary>
        public string CommentString
        {
            get { return _commentString; }
            set { _commentString = value; }
        }
    }

    /// <summary>
    /// 键值对
    /// </summary>
    class CBKeyValuePair
    {
        private string _key = "";
        /// <summary>
        /// 键
        /// </summary>
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        private string _value = "";
        /// <summary>
        /// 值
        /// </summary>
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }

    class CBSection : ArrayList
    {
        private string _sectionName = "";
        /// <summary>
        /// 段名
        /// </summary>
        public string SectionName
        {
            get { return _sectionName; }
            set { _sectionName = value; }
        }

        public void AddKeyValuePair(CBKeyValuePair pair)
        {
            this.Add(pair);
        }

        public void AddComment(CBComment comment)
        {
            this.Add(comment);
        }

        public object GetItem(int nIndex)
        {
            object objItem = this[nIndex];
            return objItem;
        }

        public CBKeyValuePair GetKeyValuePairByKeyName(string sKeyName)
        {
            for (int i = 0; i < this.Count; i++)
            {
                object objSectionItem = GetItem(i);
                if (objSectionItem is CBKeyValuePair)
                {
                    CBKeyValuePair pair = (CBKeyValuePair)objSectionItem;
                    if (pair.Key == sKeyName)
                        return pair;
                }
            }

            return null;
        }

        public int GetKeyValuePairIndexByKeyName(string sKeyName)
        {
            for (int i = 0; i < this.Count; i++)
            {
                object objSectionItem = GetItem(i);
                if (objSectionItem is CBKeyValuePair)
                {
                    CBKeyValuePair pair = (CBKeyValuePair)objSectionItem;
                    if (pair.Key == sKeyName)
                        return i;
                }
            }

            return -1;
        }

        public void RemoveKeyValuePair(string sKeyName)
        {
            int nIndex = GetKeyValuePairIndexByKeyName(sKeyName);
            if (nIndex != -1)
                this.RemoveAt(nIndex);
        }
    }

    class CBContent : ArrayList
    {
        public void AddSection(CBSection section)
        {
            this.Add(section);
        }

        public void AddComment(CBComment comment)
        {
            this.Add(comment);
        }

        public object GetItem(int nIndex)
        {
            object objItem = this[nIndex];
            return objItem;
        }

        public CBSection GetSectionByName(string sSectionName)
        {
            for (int i = 0; i < this.Count; i++)
            {
                object objContentItem = GetItem(i);
                if (objContentItem is CBSection)
                {
                    CBSection section = (CBSection)objContentItem;
                    if (section.SectionName == sSectionName)
                        return section;
                }
            }

            return null;
        }

        public int GetSectionIndexByName(string sSectionName)
        {
            for (int i = 0; i < this.Count; i++)
            {
                object objContentItem = GetItem(i);
                if (objContentItem is CBSection)
                {
                    CBSection section = (CBSection)objContentItem;
                    if (section.SectionName == sSectionName)
                        return i;
                }
            }

            return -1;
        }

        public void RemoveSection(string sSectionName)
        {
            int nIndex = GetSectionIndexByName(sSectionName);
            if (nIndex != -1)
                this.RemoveAt(nIndex);
        }
    }


    /// <summary>
    /// 配置的基类。配置基于XML文件的格式，按相对固定的格式组织。
    /// 如果需要针对一个特定文件进行处理时，一般需要继承该类。
    /// </summary>
    public class ConfigBase
    {
        private CBContent _content = new CBContent();

        private string _configFileName = "";
        /// <summary>
        /// 得到配置文件的名称
        /// </summary>
        /// <returns>配置文件的名称</returns>
        public string GetFileName()
        {
            return _configFileName;
        }

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfigBase()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sFileName">配置文件名</param>
        public ConfigBase(string sFileName)
        {
            LoadFromFile(sFileName);
        }
        #endregion

        #region 文件读取
        /// <summary>
        /// 从文件中加载配置项
        /// </summary>
        /// <param name="sFileName">配置文件名</param>
        public void LoadFromFile(string sFileName)
        {
            _content.Clear();
            _configFileName = sFileName;

            //========== 1. 打开文件 ============
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(sFileName);
            XmlNode nodeRoot = xmlDoc.DocumentElement;

            //============= 2. 处理每一个子节点 ========
            foreach (XmlNode node in nodeRoot.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Comment)
                    LoadFromFile__Comment(node);
                else if (node.NodeType == XmlNodeType.Element)
                    LoadFromFile__Section(node);
                else
                    throw new Exception("加载配置文件出错，未知的一级节点类型 - "
                            + node.NodeType.ToString());
            }
        }

        /// <summary>
        /// 加载一级注释
        /// </summary>
        /// <param name="commentNode"></param>
        public void LoadFromFile__Comment(XmlNode commentNode)
        {
            string sCommentString = commentNode.InnerText;

            CBComment comment = new CBComment();
            comment.CommentString = sCommentString;

            _content.AddComment(comment);
        }

        /// <summary>
        /// 加载一级段
        /// </summary>
        /// <param name="sectionNode">段节点</param>
        public void LoadFromFile__Section(XmlNode sectionNode)
        {
            //========== 1. 得到段名 ===========
            string sSectionName = sectionNode.Name;
            CBSection section = new CBSection();
            section.SectionName = sSectionName;

            //======== 2. 逐一处理段中的每一项 ==========
            foreach (XmlNode keyNode in sectionNode.ChildNodes)
            {
                //========= 2.1 注释 ===========
                if (keyNode.NodeType == XmlNodeType.Comment)
                {
                    CBComment comment = new CBComment();
                    comment.CommentString = keyNode.InnerText;

                    section.AddComment(comment);
                }
                //========= 2.2 键值配置 ==============
                else if (keyNode.NodeType == XmlNodeType.Element)
                {
                    string sValue = "";
                    XmlAttribute attr = keyNode.Attributes["value"];
                    if (attr != null)
                        sValue = attr.Value;

                    CBKeyValuePair pair = new CBKeyValuePair();
                    pair.Key = keyNode.Name;
                    pair.Value = sValue;

                    section.AddKeyValuePair(pair);
                }
                else
                    throw new Exception("加载配置文件出错，未知的二级节点类型 - "
                            + keyNode.NodeType.ToString());
            }

            //=========== 3. 加到一级内容中 ===========
            _content.AddSection(section);
        }
        #endregion

        #region 文件写入
        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="sFileName">文件名</param>
        public void SaveToFile(string sFileName)
        {
            //======= 1. 创建XmlDocument ==========
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement eleRoot = xmlDoc.CreateElement("configRoot");

            //======== 2. 设置根节点 ========
            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));
            xmlDoc.AppendChild(eleRoot);

            //========== 3. 逐一处理一级节点 =============
            for (int i = 0; i < _content.Count; i++)
            {
                object objContentItem = _content.GetItem(i);
                if (objContentItem is CBComment)
                {
                    CBComment comment = (CBComment)objContentItem;
                    XmlComment eleComment = SaveToFile__GetCommentElement(xmlDoc, comment);

                    eleRoot.AppendChild(eleComment);
                }
                else if (objContentItem is CBSection)
                {
                    CBSection section = (CBSection)objContentItem;
                    XmlElement eleSection = SaveToFile__GetSectionElement(xmlDoc, section);

                    eleRoot.AppendChild(eleSection);
                }
                else
                    throw new Exception("一级节点不是Comment, Section");
            }

            //============= 4. 保存到文件 =========
            xmlDoc.Save(sFileName);
        }

        /// <summary>
        /// 得到注释的XML节点
        /// </summary>
        /// <param name="xmlDoc">Xml文档</param>
        /// <param name="comment">注释</param>
        /// <returns>注释的Xml节点</returns>
        private XmlComment SaveToFile__GetCommentElement(XmlDocument xmlDoc, CBComment comment)
        {
            XmlComment xmlComment = xmlDoc.CreateComment(comment.CommentString);
            return xmlComment;
        }

        /// <summary>
        /// 得到段的Xml节点
        /// </summary>
        /// <param name="xmlDoc">Xml文档</param>
        /// <param name="section">段</param>
        /// <returns>段的Xml节点</returns>
        private XmlElement SaveToFile__GetSectionElement(XmlDocument xmlDoc, CBSection section)
        {
            XmlElement eleSection = xmlDoc.CreateElement(section.SectionName);

            //============ 1. 逐一处理段中的注释和键值 =============
            for (int i = 0; i < section.Count; i++)
            {
                object objSectiontItem = section.GetItem(i);

                //========= 2. 注释的处理 ===========
                if (objSectiontItem is CBComment)
                {
                    CBComment comment = (CBComment)objSectiontItem;
                    XmlComment eleComment = xmlDoc.CreateComment(comment.CommentString);

                    eleSection.AppendChild(eleComment);
                }

                //=========== 3. 键值对的处理 =========
                else if (objSectiontItem is CBKeyValuePair)
                {
                    CBKeyValuePair pair = (CBKeyValuePair)objSectiontItem;
                    XmlElement elePair = xmlDoc.CreateElement(pair.Key);

                    XmlAttribute attrValue = xmlDoc.CreateAttribute("value");
                    attrValue.Value = pair.Value;
                    elePair.Attributes.Append(attrValue);

                    eleSection.AppendChild(elePair);
                }
                else
                    throw new Exception("二级节点不是Comment, KeyValuePair");
            }

            return eleSection;
        }
        #endregion

        #region 读取键值
        /// <summary>
        /// 得到配置字符串
        /// </summary>
        /// <param name="sSectionName">段名</param>
        /// <param name="sKeyName">关键字名</param>
        /// <param name="sDefault">缺省值</param>
        /// <returns>配置的字符串</returns>
        public string GetString(string sSectionName, string sKeyName, string sDefault)
        {
            CBSection section = _content.GetSectionByName(sSectionName);
            if (section == null)
                return sDefault;

            CBKeyValuePair pair = section.GetKeyValuePairByKeyName(sKeyName);
            if (pair == null)
                return sDefault;

            string sRet = pair.Value;
            return sRet;
        }

        /// <summary>
        /// 得到配置字符串
        /// </summary>
        /// <param name="sSectionName">段名</param>
        /// <param name="sKeyName">关键字名</param>
        /// <returns>配置的字符串</returns>
        public string GetString(string sSectionName, string sKeyName)
        {
            return GetString(sSectionName, sKeyName, "");
        }


        /// <summary>
        /// 得到配置的整数
        /// </summary>
        /// <param name="sSectionName">段名</param>
        /// <param name="sKeyName">关键字名</param>
        /// <param name="nDefault">缺省值</param>
        /// <returns>配置的整数</returns>
        public int GetInt(string sSectionName, string sKeyName, int nDefault)
        {
            string sValue = GetString(sSectionName, sKeyName, nDefault.ToString());
            int nRet = ConvertUtil.ToInt(sValue);
            return nRet;
        }

        ///<summary>
        /// 得到配置的整数
        /// </summary>
        /// <param name="sSectionName">段名</param>
        /// <param name="sKeyName">关键字名</param>
        /// <returns>配置的整数</returns>
        public int GetInt(string sSectionName, string sKeyName)
        {
            return GetInt(sSectionName, sKeyName, 0);
        }

        /// <summary>
        /// 得到配置的布尔值
        /// </summary>
        /// <param name="sSectionName">段名</param>
        /// <param name="sKeyName">关键字名</param>
        /// <param name="bDefault">缺省值</param>
        /// <returns>配置的布尔值</returns>
        /// <remarks>
        /// 判断配置项的第一个字符，为1,T,t,Y,y则返回真，
        /// 为0,F,f,N,n返回假。这样，对于true, false, yes,
        /// no的处理也能够一网打尽。
        /// </remarks>
        public bool GetBool(string sSectionName, string sKeyName, bool bDefault)
        {
            string sRet;
            sRet = GetString(sSectionName, sKeyName, "HlThoz");
            if (sRet == "HlThoz")
                return bDefault;

            if (sRet.Length == 0)
                return bDefault;

            char cFirstChar = sRet[0];
            switch (cFirstChar)
            {
                case '1':
                case 'T':
                case 't':
                case 'Y':
                case 'y':
                    return true;
                case '0':
                case 'F':
                case 'f':
                case 'N':
                case 'n':
                    return false;
                default:
                    return bDefault;
            }
        }

        /// <summary>
        /// 得到配置的布尔值
        /// </summary>
        /// <param name="sSectionName">段名</param>
        /// <param name="sKeyName">关键字名</param>
        /// <returns>配置的布尔值</returns>
        /// <remarks>缺省返回false</remarks>
        public bool GetBool(string sSectionName, string sKeyName)
        {
            return GetBool(sSectionName, sKeyName, false);
        }

        /// <summary>
        /// 得到配置的三态布尔值
        /// </summary>
        /// <param name="sSectionName">段名</param>
        /// <param name="sKeyName">关键字名</param>
        /// <param name="bDefault">缺省值</param>
        /// <returns>配置的三态布尔值</returns>
        public Bool3State GetBool3State(string sSectionName, string sKeyName, Bool3State bDefault)
        {
            string sRet;
            sRet = GetString(sSectionName, sKeyName, "HlThoz");
            if (sRet == "HlThoz")
                return bDefault;

            if (sRet.Length == 0)
                return bDefault;

            char cFirstChar = sRet[0];
            switch (cFirstChar)
            {
                case '1':
                case 'T':
                case 't':
                case 'Y':
                case 'y':
                    return Bool3State.True;
                case '0':
                case 'F':
                case 'f':
                case 'N':
                case 'n':
                    return Bool3State.False;
                default:
                    return bDefault;
            }
        }

        /// <summary>
        /// 得到配置的三态布尔值
        /// </summary>
        /// <param name="sSectionName">段名</param>
        /// <param name="sKeyName">关键字名</param>
        /// <returns>配置的三态布尔值</returns>
        public Bool3State GetBool3State(string sSectionName, string sKeyName)
        {
            return GetBool3State(sSectionName, sKeyName, Bool3State.Undefine);
        }
        #endregion

        #region 设置键值

        /// <summary>
        /// 设置字符串键值配置
        /// </summary>
        /// <param name="sSectionName">段名</param>
        /// <param name="sKeyName">键名</param>
        /// <param name="sValue">值</param>
        public void SetString(string sSectionName, string sKeyName, string sValue)
        {
            //========== 1. 得到段 ========
            CBSection section = _content.GetSectionByName(sSectionName);

            //========== 2. 如果没有这个段，就加进去 ========
            if (section == null)
            {
                section = new CBSection();
                section.SectionName = sSectionName;
                _content.AddSection(section);
            }

            //======== 3. 得到键值对 ==============
            CBKeyValuePair pair = section.GetKeyValuePairByKeyName(sKeyName);

            //========= 4. 如果没有这个键值对，就加一个进去 =======
            if (pair == null)
            {
                pair = new CBKeyValuePair();
                pair.Key = sKeyName;
                section.AddKeyValuePair(pair);
            }

            //========= 5. 设置这个值 =========
            pair.Value = sValue;
        }

        /// <summary>
        /// 设置整型值
        /// </summary>
        /// <param name="sSectionName">段</param>
        /// <param name="sKeyName">键</param>
        /// <param name="nValue">值</param>
        public void SetInt(string sSectionName, string sKeyName, int nValue)
        {
            SetString(sSectionName, sKeyName, nValue.ToString());
        }

        /// <summary>
        /// 设置布尔值
        /// </summary>
        /// <param name="sSectionName">段</param>
        /// <param name="sKeyName">键</param>
        /// <param name="bValue">值</param>
        public void SetBool(string sSectionName, string sKeyName, bool bValue)
        {
            string sBoolValue = "false";
            if (bValue == true)
                sBoolValue = "true";

            SetString(sSectionName, sKeyName, sBoolValue);
        }

        /// <summary>
        /// 设置三态布尔值
        /// </summary>
        /// <param name="sSectionName">段</param>
        /// <param name="sKeyName">键</param>
        /// <param name="bValue">值</param>
        public void SetBool3State(string sSectionName, string sKeyName, Bool3State bValue)
        {
            string sBoolValue = "";
            if (bValue == Bool3State.True)
                sBoolValue = "true";
            else if (bValue == Bool3State.False)
                sBoolValue = "false";

            SetString(sSectionName, sKeyName, sBoolValue);
        }

        #endregion

        #region 列举段和键值
        /// <summary>
        /// 得到一个段的哈希键值对
        /// </summary>
        /// <param name="sSectionName">段名</param>
        /// <returns>哈希键值对</returns>
        /// <remarks>主要用于兼容之前的函数</remarks>
        [Obsolete]
        public Hashtable GetSection(string sSectionName)
        {
            Hashtable htRet = new Hashtable();

            CBSection section = _content.GetSectionByName(sSectionName);
            if (section == null)
                return htRet;

            for (int i = 0; i < section.Count; i++)
            {
                object objSectionItem = section.GetItem(i);

                if (objSectionItem is CBKeyValuePair)
                {
                    CBKeyValuePair pair = (CBKeyValuePair)objSectionItem;
                    htRet[pair.Key] = pair.Value;
                }
            }

            return htRet;
        }

        /// <summary>
        /// 得到一个段的所有键名
        /// </summary>
        /// <param name="sSectionName">段名</param>
        /// <returns>键名列表</returns>
        public string[] ReadSectionKeys(string sSectionName)
        {
            ArrayList lstKeys = ReadSectionKeys__ArrayList(sSectionName);
            string[] arrRet = ArrayUtil.ToStringArray(lstKeys);

            return arrRet;
        }

        /// <summary>
        /// 得到一个段的所有键名
        /// </summary>
        /// <param name="sSectionName">段名</param>
        /// <returns>键名列表（ArrayList）</returns>
        private ArrayList ReadSectionKeys__ArrayList(string sSectionName)
        {
            ArrayList arrKeys = new ArrayList();

            CBSection section = _content.GetSectionByName(sSectionName);
            if (section == null)
                return arrKeys;

            for (int i = 0; i < section.Count; i++)
            {
                object objSectionItem = section.GetItem(i);

                if (objSectionItem is CBKeyValuePair)
                {
                    CBKeyValuePair pair = (CBKeyValuePair)objSectionItem;
                    arrKeys.Add(pair.Key);
                }
            }

            return arrKeys;
        }

        /// <summary>
        /// 得到段名列表
        /// </summary>
        /// <returns>段名列表</returns>
        public string[] ReadSections()
        {
            ArrayList lstSections = ReadSections__ArrayList();
            string[] arrRet = ArrayUtil.ToStringArray(lstSections);

            return arrRet;
        }

        /// <summary>
        /// 得到段名列表
        /// </summary>
        /// <returns>段名列表（ArrayList）</returns>
        private ArrayList ReadSections__ArrayList()
        {
            ArrayList arrSections = new ArrayList();

            for (int i = 0; i < _content.Count; i++)
            {
                object objSectionItem = _content.GetItem(i);

                if (objSectionItem is CBSection)
                {
                    CBSection section = (CBSection)objSectionItem;
                    arrSections.Add(section.SectionName);
                }
            }

            return arrSections;
        }
        #endregion

        #region 判断是否存在
        /// <summary>
        /// 是否已经存在一个段
        /// </summary>
        /// <param name="sSectionName">段名</param>
        /// <returns>是否已经存在</returns>
        public bool SectionExists(string sSectionName)
        {
            CBSection section = _content.GetSectionByName(sSectionName);
            if (section == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 是否已经存在一个键
        /// </summary>
        /// <param name="sSectionName">段名</param>
        /// <param name="sKeyName">键名</param>
        /// <returns>是否已经存在</returns>
        public bool KeyExists(string sSectionName, string sKeyName)
        {
            CBSection section = _content.GetSectionByName(sSectionName);
            if (section == null)
                return false;

            CBKeyValuePair pair = section.GetKeyValuePairByKeyName(sKeyName);
            if (pair == null)
                return false;
            else
                return true;
        }

        #endregion

        #region 移除
        /// <summary>
        /// 移除一个段。如果该段不存在，则抛出例外。
        /// </summary>
        /// <param name="sSectionName">段名</param>
        public void RemoveSection(string sSectionName)
        {
            if (!SectionExists(sSectionName))
            {
                throw new Exception("ConfigBase.RemoveSection() error: 不能找到段名，无法移除 - "
                        + sSectionName);
            }

            _content.RemoveSection(sSectionName);
        }

        /// <summary>
        /// 移除一个键。如果该键不存在，则抛出例外。
        /// </summary>
        /// <param name="sSectionName">段名</param>
        /// <param name="sKeyName">键名</param>
        public void RemoveKey(string sSectionName, string sKeyName)
        {
            if (!KeyExists(sSectionName, sKeyName))
            {
                throw new Exception("ConfigBase.RemoveKey() error: 不能找到键名，无法移除 - "
                        + sSectionName + "," + sKeyName);
            }

            CBSection section = _content.GetSectionByName(sSectionName);
            Debug.Assert(section != null);

            section.RemoveKeyValuePair(sKeyName);
        }
        #endregion
    }
}
