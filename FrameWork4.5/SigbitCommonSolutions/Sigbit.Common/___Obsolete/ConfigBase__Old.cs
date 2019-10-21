using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using System.Xml;

namespace Sigbit.Common
{
    /// <summary>
    /// 配置的基类。配置基于XML文件的格式，按相对固定的格式组织。
    /// 如果需要针对一个特定文件进行处理时，一般需要继承该类。
    /// </summary>
    [Obsolete]
    public class ConfigBase__Old
    {
        Hashtable _htSections;

        private void LoadSectionNode(XmlNode sectionNode)
        {
            Hashtable htKeys = new Hashtable();
            
            foreach (XmlNode keyNode in sectionNode.ChildNodes)
            {
                if (keyNode.NodeType != XmlNodeType.Comment)
                {
                    foreach (XmlAttribute attr in keyNode.Attributes)
                    {
                        if (attr.Name == "value")
                            htKeys.Add(keyNode.Name, attr.Value);
                    }
                }
            }

            if (sectionNode.NodeType != XmlNodeType.Comment)
                _htSections.Add(sectionNode.Name, htKeys);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfigBase__Old()
        {
            _htSections = new Hashtable();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sFileName">配置文件名</param>
        public ConfigBase__Old(string sFileName)
        {
            _htSections = new Hashtable();
            LoadFromFile(sFileName);
        }

        /// <summary>
        /// 从文件中加载配置项
        /// </summary>
        /// <param name="sFileName">配置文件名</param>
        public void LoadFromFile(string sFileName)
        {
            //========== 1. 打开文件 ============
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(sFileName);
            XmlNode nodeRoot = xmlDoc.DocumentElement;

            //============= 2. 处理每一个子节点 ========
            foreach (XmlNode node in nodeRoot.ChildNodes)
                LoadSectionNode(node);
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
        /// 得到配置字符串
        /// </summary>
        /// <param name="sSectionName">段名</param>
        /// <param name="sKeyName">关键字名</param>
        /// <param name="sDefault">缺省值</param>
        /// <returns>配置的字符串</returns>
        public string GetString(string sSectionName, string sKeyName, string sDefault)
        {
            string sRet;
            try
            {
                sRet = (string)((Hashtable)_htSections[sSectionName])[sKeyName];
            }
            catch
            {
                return sDefault;
            }
            if (sRet == null)
                return sDefault;
            else
                return sRet;
        }

        /// <summary>
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
        /// 得到配置的整数
        /// </summary>
        /// <param name="sSectionName">段名</param>
        /// <param name="sKeyName">关键字名</param>
        /// <param name="nDefault">缺省值</param>
        /// <returns>配置的整数</returns>
        public int GetInt(string sSectionName, string sKeyName, int nDefault)
        {
            Object objRet;
            int nRet;
            try
            {
                objRet = ((Hashtable)_htSections[sSectionName])[sKeyName];
            }
            catch
            {
                return nDefault;
            }

            if (objRet == null)
                return nDefault;

            try
            {
                nRet = Convert.ToInt32(objRet);
                return nRet;
            }
            catch
            {
                return nDefault;
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
        /// 得到配置的三态布尔值
        /// </summary>
        /// <param name="sSectionName">段名</param>
        /// <param name="sKeyName">关键字名</param>
        /// <returns>配置的三态布尔值</returns>
        public Bool3State GetBool3State(string sSectionName, string sKeyName)
        {
            return GetBool3State(sSectionName, sKeyName, Bool3State.Undefine);
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
        /// 得到一个段里的配置Hash表。
        /// </summary>
        /// <param name="sSectionName">段名</param>
        /// <returns>段的配置Hash表</returns>
        public Hashtable GetSection(string sSectionName)
        {
            Hashtable htRet;
            htRet = (Hashtable)_htSections[sSectionName];
            return htRet;
        }
    }
}
