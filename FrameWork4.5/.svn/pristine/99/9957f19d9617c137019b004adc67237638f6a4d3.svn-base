using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Xml;

using Sigbit.Common;
using Sigbit.Net.BIPPacket;

namespace Sigbit.Net.XmlPacket
{
    /// <summary>
    /// XMLPacket类
    /// </summary>
    public class XmlPacket : BIPCustomPacket
    {
        #region 同步属性和结果集
        /// <summary>
        /// 从属性同步到DataSet中
        /// </summary>
        protected virtual void SynchronizeFromProperties()
        {
        }

        /// <summary>
        /// 从DataSet中同步到属性
        /// </summary>
        protected virtual void SynchronizeToProperties()
        {
        }
        #endregion 同步属性和结果集

        #region 属性
        string _packetOrgVersion = "";
        /// <summary>
        /// 数据包组织的版本号
        /// </summary>
        public string PacketOrgVersion
        {
            get { return _packetOrgVersion; }
            set { _packetOrgVersion = value; }
        }

        #endregion 属性

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public XmlPacket()
        {
        }
        #endregion 构造函数

        #region 内容显示
        /// <summary>
        /// 得到显示Packet内容的文本
        /// </summary>
        /// <returns>Packet内容文本</returns>
        public string GetDisplayContentText()
        {
            string sContent = "";
            string sLine;

            //======== 1. 包格式 ========
            sLine = "<<BIP_PACKET>>    FORMAT:" + PacketFormat;
            sLine += "\tTYPE:" + PacketType;
            sLine += "\tTRANSCODE:" + TransCode;
            sContent += sLine + "\r\n";

            if (PacketFormat == BIPPacketFormat.ShortFormat)
            {
                sLine = "SHORT_DATA:" + ShortPacketData;
                sContent += sLine + "\r\n";
                return sContent;
            }

            //======== 2. 版本号、包标志、数据块数量 ========
            sLine = "\tVERSION=" + PacketOrgVersion;
            sLine += "\tPACKETID=" + PacketId;
            sLine += "\tDATASET_COUNT=" + DataSetCount;
            sContent += sLine + "\r\n";

            //=========== 3. DataSet ==========
            int i;

            for (i = 0; i < DataSetCount; i++)
                sContent += GetDataSet(i).GetDisplayContentText();

            return sContent;
        }
        #endregion 内容显示

        #region 读取BIP包
        /// <summary>
        /// 从文件中产生包的内容
        /// </summary>
        /// <param name="sFileName">文件名</param>
        public virtual void ReadFromFile(string sFileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(sFileName);

            this.FromXmlDocument(xmlDoc);
        }

        public virtual void ReadFrom(string sPacketString)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(sPacketString);

            this.FromXmlDocument(xmlDoc);
        }

        public virtual void ReadFrom(byte[] bsPacket)
        {
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(bsPacket, 0, bsPacket.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(memoryStream);

            this.FromXmlDocument(xmlDoc);
            memoryStream.Dispose();
        }

        private bool _bHasSynchronizedToProperties = false;

        protected void MaySynchronizeToProperties()
        {
            if (!_bHasSynchronizedToProperties)
            {
                SynchronizeToProperties();
                _bHasSynchronizedToProperties = true;
            }
        }

        /// <summary>
        /// 从XmlDocument读取
        /// </summary>
        /// <param name="xmlDoc">XmlDocument实例</param>
        private void FromXmlDocument(XmlDocument xmlDoc)
        {
            //======== 1. 清除所有的DataSet =======
            this.DeleteAllDataSet();
            for (int i = this.DataSetCount - 1; i >= 0; i--)
                DeleteDataSet(i);

            //======= 2. root ==========
            XmlNode nodeRoot = null;
            foreach (XmlNode node in xmlDoc.ChildNodes)
            {
                if (node.Name == "packet")
                    nodeRoot = node;
            }
            if (nodeRoot == null)
                throw new Exception("XmlPacket.FromXmlDocument()解析错误 : "
                        + "找不到根节点packet");

            //========= 3. 处理根节点的每一个子节点 ==========
            foreach (XmlNode nodeRootChild in nodeRoot.ChildNodes)
            {
                if (nodeRootChild.Name == "dataSets")
                {
                    foreach (XmlNode nodeDataSet in nodeRootChild.ChildNodes)
                        AddDataSetByNode(nodeDataSet);
                }
                else
                {
                    if (nodeRootChild.Name == "transactionCode")
                        this.TransCode = nodeRootChild.InnerText;
                    else if (nodeRootChild.Name == "packetId")
                        this.PacketId = ConvertUtil.ToInt(nodeRootChild.InnerText);
                    else if (nodeRootChild.Name == "packetVersion")
                        this.PacketOrgVersion = nodeRootChild.InnerText;
                }
            }

            MaySynchronizeToProperties();
        }

        /// <summary>
        /// 通过结果集的节点创建一个结果集
        /// </summary>
        /// <param name="dataSetNode">结果集节点</param>
        private void AddDataSetByNode(XmlNode dataSetNode)
        {
            //========== 1. 结果集的名称 ============
            string sDataSetName;
            XmlAttribute attrDataSetName = dataSetNode.Attributes["name"];
            if (attrDataSetName == null)
                sDataSetName = "";
            else
                sDataSetName = attrDataSetName.Value;

            BIPDataSet dataSet = this.NewDataSet(sDataSetName);

            //=========== 2. 结果集的字段和数据 =============
            foreach (XmlNode childNode in dataSetNode.ChildNodes)
            {
                if (childNode.Name == "fieldDefine")
                {
                    foreach (XmlNode nodeField in childNode.ChildNodes)
                    {
                        string sFieldName = nodeField.Attributes["name"].Value;
                        if (sFieldName == "")
                            dataSet.AddFields(1);
                        else
                            dataSet.AddField(sFieldName);
                    }
                }
                else if (childNode.Name == "dataRows")
                {
                    AddDataRowsByNode(childNode, dataSet);
                }
                else
                    throw new Exception("xmlPacket解析错误："
                            + "数据集的子节点并非fieldDefine或dataRows");
            }
        }

        private void AddDataRowsByNode(XmlNode dataRowsNode, BIPDataSet dataSet)
        {
            int nCurrentRowNum = 1;

            foreach (XmlNode nodeRow in dataRowsNode.ChildNodes)
            {
                int nCurrentColSeq = 0;
                foreach (XmlNode nodeCol in nodeRow.ChildNodes)
                {
                    string sData = nodeCol.InnerText;

                    //%%%% IPHONE ADD %%%%
                    if (nodeCol.InnerText == null)
                        sData = "";

                    dataSet.SetItemString(nCurrentRowNum, nCurrentColSeq, sData);

                    nCurrentColSeq++;
                }

                nCurrentRowNum++;
            }
        }

        #endregion 读取BIP包

        #region 写出XML包
        /// <summary>
        /// 写入到文件
        /// </summary>
        /// <param name="sFileName">文件名</param>
        public virtual void WriteToFile(string sFileName)
        {
            XmlDocument xmlDoc = ToXmlDocument();
            xmlDoc.Save(sFileName);
        }

        private bool _bHasSynchronizedFromProperties = false;

        protected void MaySynchronizeFromProperties()
        {
            if (!_bHasSynchronizedFromProperties)
            {
                SynchronizeFromProperties();
                _bHasSynchronizedFromProperties = true;
            }
        }

        private XmlDocument ToXmlDocument()
        {
            MaySynchronizeFromProperties();

            //======= 1. 创建XmlDocument ==========
            XmlDocument xmlDoc = new XmlDocument();

            XmlElement eleRoot = xmlDoc.CreateElement("packet");

            //======== 2. 设置根节点 ========
            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));
            xmlDoc.AppendChild(eleRoot);

            //======== 3. 写入包属性 ============
            //========= 3.1 交易码 =============
            if (this.TransCode != "")
            {
                XmlElement ele = xmlDoc.CreateElement("transactionCode");
                ele.InnerText = this.TransCode;
                eleRoot.AppendChild(ele);
            }

            //========= 3.2 包标识 ==========
            if (this.PacketId != 0)
            {
                XmlElement ele = xmlDoc.CreateElement("packetId");
                ele.InnerText = this.PacketId.ToString();
                eleRoot.AppendChild(ele);
            }

            //========= 3.3 包版本号 =============
            if (this.PacketOrgVersion != "")
            {
                XmlElement ele = xmlDoc.CreateElement("packetVersion");
                ele.InnerText = this.PacketOrgVersion;
                eleRoot.AppendChild(ele);
            }

            //========== 4. 包数据集 ============
            XmlElement eleDataSets = xmlDoc.CreateElement("dataSets");
            XmlAttribute attrDataSets = xmlDoc.CreateAttribute("dataSetCount");
            attrDataSets.Value = this.DataSetCount.ToString();
            eleDataSets.Attributes.Append(attrDataSets);

            for (int i = 0; i < this.DataSetCount; i++)
            {
                XmlElement ele = GetDataSetElement(xmlDoc, i);
                eleDataSets.AppendChild(ele);
            }

            eleRoot.AppendChild(eleDataSets);

            return xmlDoc;
        }

        private XmlElement GetDataSetElement(XmlDocument xmlDoc, int nDataSetIndex)
        {
            //========= 1. 结果集Element ===========
            XmlElement eleDataSet = xmlDoc.CreateElement("dataSet");
            BIPDataSet dataSet = GetDataSet(nDataSetIndex);

            XmlAttribute attrDataSetSeq = xmlDoc.CreateAttribute("seq");
            attrDataSetSeq.Value = (nDataSetIndex + 1).ToString();
            eleDataSet.Attributes.Append(attrDataSetSeq);

            if (dataSet.DataSetName != "")
            {
                XmlAttribute attrDataSetName = xmlDoc.CreateAttribute("name");
                attrDataSetName.Value = dataSet.DataSetName;
                eleDataSet.Attributes.Append(attrDataSetName);
            }

            //========= 2. 字段定义 =============
            XmlElement eleFieldDefine = xmlDoc.CreateElement("fieldDefine");
            eleDataSet.AppendChild(eleFieldDefine);

            for (int i = 0; i < dataSet.GetFieldCount(); i++)
            {
                XmlElement eleField = xmlDoc.CreateElement("field");

                XmlAttribute attrFieldSeq = xmlDoc.CreateAttribute("seq");
                attrFieldSeq.Value = (i + 1).ToString();
                eleField.Attributes.Append(attrFieldSeq);

                XmlAttribute attrFieldName = xmlDoc.CreateAttribute("name");
                attrFieldName.Value = dataSet.GetFieldName(i);
                eleField.Attributes.Append(attrFieldName);

                eleFieldDefine.AppendChild(eleField);
            }

            //========= 3. 各数据行 ===========
            XmlElement eleDataRows = xmlDoc.CreateElement("dataRows");
            XmlAttribute attrRowCount = xmlDoc.CreateAttribute("rowCount");
            attrRowCount.Value = dataSet.GetRecordCount().ToString();
            eleDataRows.Attributes.Append(attrRowCount);
            eleDataSet.AppendChild(eleDataRows);

            //========== 4. 得到每一行的Element ========
            int nRecordCount = dataSet.GetRecordCount();
            for (int i = 1; i <= nRecordCount; i++)
            {
                XmlElement eleRow = GetDataRowElement(xmlDoc, dataSet, i);
                eleDataRows.AppendChild(eleRow);
            }

            return eleDataSet;
        }

        /// <summary>
        /// 得到一行的Xml元素
        /// </summary>
        /// <param name="xmlDoc">xmlDocument文档</param>
        /// <param name="dataSet">结果集</param>
        /// <param name="nRowNum">记录号</param>
        /// <returns>Xml元素</returns>
        private XmlElement GetDataRowElement(XmlDocument xmlDoc,
                BIPDataSet dataSet, int nRowNum)
        {
            XmlElement eleRow = xmlDoc.CreateElement("row");
            XmlAttribute attrRow = xmlDoc.CreateAttribute("seq");
            attrRow.Value = nRowNum.ToString();
            eleRow.Attributes.Append(attrRow);

            int nFieldCount = dataSet.GetFieldCount();
            for (int i = 0; i < nFieldCount; i++)
            {
                string sDataValue = dataSet.GetItemString(nRowNum, i);
                XmlElement eleCol = xmlDoc.CreateElement("col");

                //%%%% IPHONE ADD %%%%
                if (sDataValue != "")
                    eleCol.InnerText = sDataValue;
                //%%%%%%%%%%%%%%%%

                eleRow.AppendChild(eleCol);
            }

            return eleRow;
        }

        public override string ToString()
        {
            byte[] bsContent = ToBytes();
            string sPacketString = Encoding.UTF8.GetString(bsContent);

            return sPacketString;
        }

        public virtual byte[] ToBytes()
        {
            MemoryStream memoryStream = new MemoryStream();
            XmlDocument xmlDoc = ToXmlDocument();
            xmlDoc.Save(memoryStream);

            byte[] bsContent = memoryStream.ToArray();
            return bsContent;
        }

        #endregion 写出BIP包
    }
}
