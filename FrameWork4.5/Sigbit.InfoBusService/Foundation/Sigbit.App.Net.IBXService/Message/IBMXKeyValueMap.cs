using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Xml;

namespace Sigbit.App.Net.IBXService.Message
{
    public class IBMXKeyValueMapItem
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
    public class IBMXKeyValueMap : ArrayList
    {
        public void AddKeyValueMap(string sKey, string sValue)
        {
            IBMXKeyValueMapItem item = new IBMXKeyValueMapItem();
            item.ItemKey = sKey;
            item.ItemValue = sValue;

            Add(item);
        }

        public IBMXKeyValueMapItem GetKeyValueMapItem(int nIndex)
        {
            return (IBMXKeyValueMapItem)this[nIndex];
        }

        public string GetValueOfKey(string sKey)
        {
            for (int i = 0; i < this.Count; i++)
            {
                IBMXKeyValueMapItem item = GetKeyValueMapItem(i);
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

            XmlElement eleRoot = xmlDoc.CreateElement("infobus");

            //======== 2. 设置根节点 ========
            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null));
            xmlDoc.AppendChild(eleRoot);

            //======== 3. 循环处理各键值对 ============
            for (int i = 0; i < this.Count; i++)
            {
                IBMXKeyValueMapItem keyValueItem = this.GetKeyValueMapItem(i);

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
                if (node.Name == "infobus")
                    nodeRoot = node;
            }
            if (nodeRoot == null)
                throw new Exception("XmlPacket.FromXmlDocument()解析错误 : "
                        + "找不到根节点infobus");

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
