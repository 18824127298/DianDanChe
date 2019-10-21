using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Xml;

namespace Sigbit.App.Net.IBXService.VCodeBreak.BTMMessage
{
    /// <summary>
    /// 键值对
    /// </summary>
    class SRXKeyValueMapItem
    {
        private string _itemKey = "";
        /// <summary>
        /// 关键字
        /// </summary>
        public string ItemKey
        {
            get { return _itemKey; }
            set { _itemKey = value; }
        }

        private string _itemValue = "";
        /// <summary>
        /// 值
        /// </summary>
        public string ItemValue
        {
            get { return _itemValue; }
            set { _itemValue = value; }
        }
    }

    /// <summary>
    /// 键值对列表
    /// </summary>
    internal class SRXKeyValueMap : ArrayList
    {
        private string _rootNodeName = "root";
        /// <summary>
        /// 根节点的名称
        /// </summary>
        public string RootNodeName
        {
            get { return _rootNodeName; }
            set { _rootNodeName = value; }
        }

        public void AddKeyValueMap(string sKey, string sValue)
        {
            SRXKeyValueMapItem item = new SRXKeyValueMapItem();
            item.ItemKey = sKey;
            item.ItemValue = sValue;

            Add(item);
        }

        public SRXKeyValueMapItem GetKeyValueMapItem(int nIndex)
        {
            return (SRXKeyValueMapItem)this[nIndex];
        }

        public string GetValueOfKey(string sKey)
        {
            for (int i = 0; i < this.Count; i++)
            {
                SRXKeyValueMapItem item = GetKeyValueMapItem(i);
                if (item.ItemKey == sKey)
                    return item.ItemValue;
            }

            return "";
        }

        #region 写出到XML
        /// <summary>
        /// 写到XmlDocument中
        /// </summary>
        /// <returns>XmlDocument</returns>
        public XmlDocument ToXmlDocument()
        {
            //======= 1. 创建XmlDocument ==========
            XmlDocument xmlDoc = new XmlDocument();

            XmlElement eleRoot = xmlDoc.CreateElement(this.RootNodeName);

            //======== 2. 设置根节点 ========
            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null));
            xmlDoc.AppendChild(eleRoot);

            //======== 3. 循环处理各键值对 ============
            for (int i = 0; i < this.Count; i++)
            {
                SRXKeyValueMapItem keyValueItem = this.GetKeyValueMapItem(i);

                XmlElement ele = xmlDoc.CreateElement(keyValueItem.ItemKey);
                if (keyValueItem.ItemValue != "")
                    ele.InnerText = keyValueItem.ItemValue;

                eleRoot.AppendChild(ele);
            }

            return xmlDoc;
        }
        #endregion 写出到XML

        #region 读取XML
        /// <summary>
        /// 从XmlDocument读取
        /// </summary>
        /// <param name="xmlDoc">XmlDocument实例</param>
        public void FromXmlDocument(XmlDocument xmlDoc)
        {
            this.Clear();

            //========== 1. 根节点 =================
            XmlNode nodeRoot = null;
            foreach (XmlNode node in xmlDoc.ChildNodes)
            {
                if (node.Name == this.RootNodeName)
                    nodeRoot = node;
            }
            if (nodeRoot == null)
                throw new Exception("XmlPacket.FromXmlDocument()解析错误 : "
                        + "找不到根节点" + this.RootNodeName);

            //========= 2. 处理根节点的每一个子节点 ==========
            foreach (XmlNode nodeRootChild in nodeRoot.ChildNodes)
            {
                string sItemValue = nodeRootChild.InnerText;
                if (sItemValue == null)
                    sItemValue = "";

                this.AddKeyValueMap(nodeRootChild.Name, sItemValue);
            }
        }
        #endregion 读取XML
    }
}
