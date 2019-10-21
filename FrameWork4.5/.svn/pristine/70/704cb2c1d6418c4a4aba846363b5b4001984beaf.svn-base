using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Sigbit.App.Net.WeiXinService.Message
{
    public class TWXNode
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


        private bool _allowNull = true;
        /// <summary>
        /// 是否允许为空
        /// </summary>
        public bool AllowNull
        {
            get { return _allowNull; }
            set { _allowNull = value; }
        }

        private bool _isCDATA = true;
        /// <summary>
        /// 是否为CDATA字段
        /// </summary>
        public bool IsCDATA
        {
            get { return _isCDATA; }
            set { _isCDATA = value; }
        }


        private TWXNodes _childNodes = new TWXNodes();

        public TWXNodes ChildNodes
        {
            get { return _childNodes; }
            set { _childNodes = value; }
        }


        public void AppendNode(TWXNode node)
        {
            _childNodes.Add(node);
        }


        public void AppendNode(string sKey, string sValue)
        {
            TWXNode node = new TWXNode();
            node.Key = sKey;
            node.Value = sValue;

            AppendNode(node);
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

            XmlNode eleRoot = xmlDoc.AppendChild(xmlDoc.CreateElement(this.Key));

            //======== 2. 递归处理各节点 ============

            AppendNode(xmlDoc, eleRoot as XmlElement, this);

            return xmlDoc;
        }

        private void AppendNode(XmlDocument xmlDoc, XmlElement eleRoot, TWXNode nodeCurrent)
        {
            for (int i = 0; i < nodeCurrent.ChildNodes.Count; i++)
            {
                TWXNode nodeItem = nodeCurrent.ChildNodes.GetNode(i);

                XmlElement eleItem = xmlDoc.CreateElement(nodeItem.Key);

                if (nodeItem.ChildNodes.Count > 0)
                {
                    XmlNode eleCurrent = eleRoot.AppendChild(eleItem);

                    AppendNode(xmlDoc, eleCurrent as XmlElement, nodeItem);
                }
                else
                {

                    if (nodeItem.IsCDATA)
                    {
                        XmlCDataSection xmlCData = xmlDoc.CreateCDataSection(nodeItem.Value);
                        eleItem.AppendChild(xmlCData);
                    }
                    else
                    {
                        eleItem.InnerText = nodeItem.Value;
                    }

                    eleRoot.AppendChild(eleItem);

                }



            }
        }



        #endregion 写出到XML

        #region 读取XML
        /// <summary>
        /// 从XmlDocument读取
        /// </summary>
        /// <param name="xmlDoc">XmlDocument实例</param>
        public void FromXmlDocument(XmlDocument xmlDoc)
        {
            this.ChildNodes.Clear();


            //========== 1. 根节点 =================
            XmlNode nodeRoot = null;
            foreach (XmlNode node in xmlDoc.ChildNodes)
            {
                if (node.Name == "xml")
                    nodeRoot = node;
            }
            if (nodeRoot == null)
                throw new Exception("TWXNode.FromXmlDocument()解析错误 : "
                        + "找不到根节点xml");

            LoadFromXml(nodeRoot, this);

        }

        private void LoadFromXml(XmlNode xmlRoot, TWXNode nodeRoot)
        {
            foreach (XmlNode xmlNodeItem in xmlRoot.ChildNodes)
            {
                if (xmlNodeItem.ChildNodes.Count > 1)
                {
                    TWXNode nodeCurrent = new TWXNode();
                    nodeCurrent.Key = xmlNodeItem.Name;

                    nodeRoot.AppendNode(nodeCurrent);

                    LoadFromXml(xmlNodeItem, nodeCurrent);
                }
                else
                {
                    TWXNode nodeCurrent = new TWXNode();

                    if (xmlNodeItem.FirstChild.NodeType == XmlNodeType.CDATA)
                    {
                        nodeCurrent.Key = xmlNodeItem.Name;
                        nodeCurrent.Value = xmlNodeItem.InnerText;
                        nodeCurrent.IsCDATA = true;
                    }
                    else
                    {
                        nodeCurrent.Key = xmlNodeItem.Name;
                        nodeCurrent.Value = xmlNodeItem.InnerText;
                        nodeCurrent.IsCDATA = false;
                    }

                    nodeRoot.AppendNode(nodeCurrent);
                }
            }
        }



        #endregion 读取XML

    }


    public class TWXNodes : ArrayList
    {
        public void AddNode(TWXNode node)
        {
            this.Add(node);
        }

        public TWXNode GetNode(int nIndex)
        {
            TWXNode node = this[nIndex] as TWXNode;

            return node;
        }


        public string GetValueOfKey(string sKey)
        {
            string sRet = "";

            for (int i = 0; i < this.Count; i++)
            {
                TWXNode node = GetNode(i);

                if (node.Key == sKey)
                {
                    sRet = node.Value;
                    break;
                }
            }

            return sRet;
        }





    }
}
