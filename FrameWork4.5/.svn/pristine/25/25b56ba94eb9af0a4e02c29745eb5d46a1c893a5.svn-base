using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.IO;

namespace Sigbit.App.Net.IBXService.Message
{
    /// <summary>
    /// 请求消息基类
    /// </summary>
    public class IBMRequestBase
    {
        #region 通用属性
        private string _transCode = "";
        /// <summary>
        /// 交易码
        /// </summary>
        public string TransCode
        {
            get { return _transCode; }
            set { _transCode = value; }
        }

        private string _transCodeChs = "";
        /// <summary>
        /// 交易码的中文描述
        /// </summary>
        public string TransCodeChs
        {
            get { return _transCodeChs; }
            set { _transCodeChs = value; }
        }

        private string _fromSystem = "";
        /// <summary>
        /// 来源系统
        /// </summary>
        public string FromSystem
        {
            get { return _fromSystem; }
            set { _fromSystem = value; }
        }

        private string _fromClientId = "";
        /// <summary>
        /// 来源的客户端标识
        /// </summary>
        public string FromClientId
        {
            get { return _fromClientId; }
            set { _fromClientId = value; }
        }

        private string _fromClientVersion = "";
        /// <summary>
        /// 来源的客户端版本号
        /// </summary>
        public string FromClientVersion
        {
            get { return _fromClientVersion; }
            set { _fromClientVersion = value; }
        }

        private string _fromClientDesc = "";
        /// <summary>
        /// 客户端的描述信息
        /// </summary>
        public string FromClientDesc
        {
            get { return _fromClientDesc; }
            set { _fromClientDesc = value; }
        }
        #endregion 通用属性

        #region 同步属性和键值列表
        /// <summary>
        /// 从属性同步到键值列表中
        /// </summary>
        protected virtual void SynchronizeFromProperties()
        {
            this.ClearAllProperties();

            AddAStringValue("trans_code", this.TransCode);

            if (this.FromSystem != "")
                AddAStringValue("from_system", this.FromSystem);

            if (this.FromClientId != "")
                AddAStringValue("from_client_id", this.FromClientId);

            if (this.FromClientVersion != "")
                AddAStringValue("from_client_version", this.FromClientVersion);

            if (this.FromClientDesc != "")
                AddAStringValue("from_client_desc", this.FromClientDesc);
        }

        /// <summary>
        /// 从键值列表中同步到属性
        /// </summary>
        protected virtual void SynchronizeToProperties()
        {
            this.TransCode = GetAStringValue("trans_code");
            this.FromSystem = GetAStringValue("from_system");
            this.FromClientId = GetAStringValue("from_client_id");
            this.FromClientVersion = GetAStringValue("from_client_version");
            this.FromClientDesc = GetAStringValue("from_client_desc");
        }
        #endregion 同步属性和键值列表

        #region 键值对
        /// <summary>
        /// 键值对列表
        /// </summary>
        public IBMXKeyValueMap _keyValueMapList = new IBMXKeyValueMap();

        /// <summary>
        /// 清空键值对
        /// </summary>
        protected void ClearAllProperties()
        {
            _keyValueMapList.Clear();
        }

        /// <summary>
        /// 增加一项键值对
        /// </summary>
        /// <param name="sKey">键</param>
        /// <param name="sValue">值</param>
        protected void AddAStringValue(string sKey, string sValue)
        {
            _keyValueMapList.AddKeyValueMap(sKey, sValue);
        }

        protected string GetAStringValue(string sKey)
        {
            return _keyValueMapList.GetValueOfKey(sKey);
        }

        #endregion 键值对

        #region 通用方法
        /// <summary>
        /// 得到消息的描述
        /// </summary>
        /// <returns>消息的描述</returns>
        public virtual string GetMessageDescription()
        {
            string sRet = "【" + this.TransCode + "(" + this.TransCodeChs + ")】";
            if (this.FromSystem != "")
                sRet += "SYS:" + this.FromSystem + ";";
            if (this.FromClientId != "")
                sRet += "CLIENT_ID:" + this.FromClientId + ";";
            if (this.FromClientVersion != "")
                sRet += "CLIENT_VERSION:" + this.FromClientVersion + ";";
            if (this.FromClientDesc != "")
                sRet += "CLIENT_DESC:" + this.FromClientDesc + ";";
            return sRet;
        }
        #endregion 通用方法

        #region 读取XML包
        /// <summary>
        /// 从文件中产生包的内容
        /// </summary>
        /// <param name="sFileName">文件名</param>
        public void ReadFromFile(string sFileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(sFileName);

            this.FromXmlDocument(xmlDoc);
        }

        public void ReadFrom(string sPacketString)
        {
            byte[] bsContent = Encoding.UTF8.GetBytes(sPacketString);
            this.ReadFrom(bsContent);
        }

        public void ReadFrom(byte[] bsPacket)
        {
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(bsPacket, 0, bsPacket.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(memoryStream);

            this.FromXmlDocument(xmlDoc);
            memoryStream.Dispose();
        }

        /// <summary>
        /// 是否已同步到属性
        /// </summary>
        private bool _bHasSynchronizedToProperties = false;

        /// <summary>
        /// 从XmlDocument读取
        /// </summary>
        /// <param name="xmlDoc">XmlDocument实例</param>
        protected virtual void FromXmlDocument(XmlDocument xmlDoc)
        {
            //======= 1. 从XML中读取 ========
            _keyValueMapList.FromXmlDocument(xmlDoc);

            //=========== 2. 同步到属性 ==============
            //if (!_bHasSynchronizedToProperties)
            //{
                SynchronizeToProperties();
                _bHasSynchronizedToProperties = true;
            //}
        }
        #endregion 读取XML包

        #region 写出XML包
        /// <summary>
        /// 写入到文件
        /// </summary>
        /// <param name="sFileName">文件名</param>
        public void WriteToFile(string sFileName)
        {
            XmlDocument xmlDoc = ToXmlDocument();
            xmlDoc.Save(sFileName);
        }

        /// <summary>
        /// 是否已从属性同步
        /// </summary>
        private bool _bHasSynchronizedFromProperties = false;

        /// <summary>
        /// 写到XmlDocument中
        /// </summary>
        /// <returns>XmlDocument</returns>
        protected virtual XmlDocument ToXmlDocument()
        {
            //if (!_bHasSynchronizedFromProperties)
            //{
                SynchronizeFromProperties();
                _bHasSynchronizedFromProperties = true;
            //}

            XmlDocument xmlDoc = _keyValueMapList.ToXmlDocument();

            return xmlDoc;
        }

        /// <summary>
        /// 写出到字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            byte[] bsContent = ToBytes();
            string sXmlString = Encoding.UTF8.GetString(bsContent);
            return sXmlString;
        }

        /// <summary>
        /// 写出到字节数组
        /// </summary>
        /// <returns>字节数组</returns>
        public byte[] ToBytes()
        {
            MemoryStream memoryStream = new MemoryStream();
            XmlDocument xmlDoc = ToXmlDocument();
            xmlDoc.Save(memoryStream);

            byte[] bsContent = memoryStream.ToArray();

            if (bsContent.Length > 3)
            {
                if (bsContent[0] == 0xEF)
                {
                    byte[] bsRemoveSign = new byte[bsContent.Length - 3];
                    Array.Copy(bsContent, 3, bsRemoveSign, 0, bsRemoveSign.Length);
                    return bsRemoveSign;
                }
            }

            return bsContent;
        }
        #endregion 写出XML包

    }
}
