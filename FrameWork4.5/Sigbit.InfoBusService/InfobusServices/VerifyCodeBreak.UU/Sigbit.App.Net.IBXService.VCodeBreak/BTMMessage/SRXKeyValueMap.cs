using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Xml;

namespace Sigbit.App.Net.IBXService.VCodeBreak.BTMMessage
{
    /// <summary>
    /// ��ֵ��
    /// </summary>
    class SRXKeyValueMapItem
    {
        private string _itemKey = "";
        /// <summary>
        /// �ؼ���
        /// </summary>
        public string ItemKey
        {
            get { return _itemKey; }
            set { _itemKey = value; }
        }

        private string _itemValue = "";
        /// <summary>
        /// ֵ
        /// </summary>
        public string ItemValue
        {
            get { return _itemValue; }
            set { _itemValue = value; }
        }
    }

    /// <summary>
    /// ��ֵ���б�
    /// </summary>
    internal class SRXKeyValueMap : ArrayList
    {
        private string _rootNodeName = "root";
        /// <summary>
        /// ���ڵ������
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

        #region д����XML
        /// <summary>
        /// д��XmlDocument��
        /// </summary>
        /// <returns>XmlDocument</returns>
        public XmlDocument ToXmlDocument()
        {
            //======= 1. ����XmlDocument ==========
            XmlDocument xmlDoc = new XmlDocument();

            XmlElement eleRoot = xmlDoc.CreateElement(this.RootNodeName);

            //======== 2. ���ø��ڵ� ========
            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null));
            xmlDoc.AppendChild(eleRoot);

            //======== 3. ѭ���������ֵ�� ============
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
        #endregion д����XML

        #region ��ȡXML
        /// <summary>
        /// ��XmlDocument��ȡ
        /// </summary>
        /// <param name="xmlDoc">XmlDocumentʵ��</param>
        public void FromXmlDocument(XmlDocument xmlDoc)
        {
            this.Clear();

            //========== 1. ���ڵ� =================
            XmlNode nodeRoot = null;
            foreach (XmlNode node in xmlDoc.ChildNodes)
            {
                if (node.Name == this.RootNodeName)
                    nodeRoot = node;
            }
            if (nodeRoot == null)
                throw new Exception("XmlPacket.FromXmlDocument()�������� : "
                        + "�Ҳ������ڵ�" + this.RootNodeName);

            //========= 2. ������ڵ��ÿһ���ӽڵ� ==========
            foreach (XmlNode nodeRootChild in nodeRoot.ChildNodes)
            {
                string sItemValue = nodeRootChild.InnerText;
                if (sItemValue == null)
                    sItemValue = "";

                this.AddKeyValueMap(nodeRootChild.Name, sItemValue);
            }
        }
        #endregion ��ȡXML
    }
}
