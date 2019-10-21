using System;
using System.Collections.Generic;
using System.Text;

using System.Web;
using System.Xml;
using System.IO;

using Sigbit.Common;

namespace Sigbit.App.Net.IBXService.VCodeBreak.BTMMessage
{
    public class SRMResponseBase
    {
        public SRMResponseBase()
        {
            this._keyValueMapList.RootNodeName = "vc_response";
        }

        #region ͬ�����Ժͼ�ֵ�б�
        /// <summary>
        /// ������ͬ������ֵ�б���
        /// </summary>
        protected virtual void SynchronizeFromProperties()
        {
            this.ClearAllProperties();

            if (this.ErrorCode != "")
            {
                AddAStringValue("error_code", this.ErrorCode.ToString());
                AddAStringValue("error_string", this.ErrorString);
            }
        }

        /// <summary>
        /// �Ӽ�ֵ�б���ͬ��������
        /// </summary>
        protected virtual void SynchronizeToProperties()
        {
            this.ErrorCode = GetAStringValue("error_code");
            this.ErrorString = GetAStringValue("error_string");
        }
        #endregion ͬ�����Ժͼ�ֵ�б�

        #region ��ֵ��
        /// <summary>
        /// ��ֵ���б�
        /// </summary>
        internal SRXKeyValueMap _keyValueMapList = new SRXKeyValueMap();

        /// <summary>
        /// ��ռ�ֵ��
        /// </summary>
        protected void ClearAllProperties()
        {
            _keyValueMapList.Clear();
        }

        /// <summary>
        /// ����һ���ֵ��
        /// </summary>
        /// <param name="sKey">��</param>
        /// <param name="sValue">ֵ</param>
        protected void AddAStringValue(string sKey, string sValue)
        {
            _keyValueMapList.AddKeyValueMap(sKey, sValue);
        }

        protected string GetAStringValue(string sKey)
        {
            return _keyValueMapList.GetValueOfKey(sKey);
        }

        #endregion ��ֵ��

        #region ͨ������
        private string _errorCode = "";
        /// <summary>
        /// �������
        /// </summary>
        public string ErrorCode
        {
            get { return _errorCode; }
            set { _errorCode = value; }
        }

        private string _errorString = "";
        /// <summary>
        /// ��������
        /// </summary>
        public string ErrorString
        {
            get { return _errorString; }
            set { _errorString = value; }
        }
        #endregion ͨ������

        #region ͨ�÷���
        /// <summary>
        /// �õ���Ϣ������
        /// </summary>
        /// <returns>��Ϣ������</returns>
        public virtual string GetMessageDescription()
        {
            string sRet = "";
            if (ErrorCode != "")
            {
                sRet = "�����󡿴�����:" + this.ErrorCode + ";";
                sRet += "��������:" + this.ErrorString + ";";
            }
            return sRet;
        }
        #endregion ͨ�÷���

        #region ��ȡXML��
        /// <summary>
        /// ���ļ��в�����������
        /// </summary>
        /// <param name="sFileName">�ļ���</param>
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
        /// �Ƿ���ͬ��������
        /// </summary>
        private bool _bHasSynchronizedToProperties = false;

        /// <summary>
        /// ��XmlDocument��ȡ
        /// </summary>
        /// <param name="xmlDoc">XmlDocumentʵ��</param>
        protected virtual void FromXmlDocument(XmlDocument xmlDoc)
        {
            //======= 1. ��XML�ж�ȡ ========
            _keyValueMapList.FromXmlDocument(xmlDoc);

            //=========== 2. ͬ�������� ==============
            if (!_bHasSynchronizedToProperties)
            {
                SynchronizeToProperties();
                _bHasSynchronizedToProperties = true;
            }
        }
        #endregion ��ȡXML��

        #region д��XML��
        /// <summary>
        /// д�뵽�ļ�
        /// </summary>
        /// <param name="sFileName">�ļ���</param>
        public void WriteToFile(string sFileName)
        {
            XmlDocument xmlDoc = ToXmlDocument();
            xmlDoc.Save(sFileName);
        }

        /// <summary>
        /// �Ƿ��Ѵ�����ͬ��
        /// </summary>
        private bool _bHasSynchronizedFromProperties = false;

        /// <summary>
        /// д��XmlDocument��
        /// </summary>
        /// <returns>XmlDocument</returns>
        protected virtual XmlDocument ToXmlDocument()
        {
            if (!_bHasSynchronizedFromProperties)
            {
                SynchronizeFromProperties();
                _bHasSynchronizedFromProperties = true;
            }

            XmlDocument xmlDoc = _keyValueMapList.ToXmlDocument();

            return xmlDoc;
        }

        /// <summary>
        /// д�����ַ���
        /// </summary>
        /// <returns>�ַ���</returns>
        public override string ToString()
        {
            byte[] bsContent = ToBytes();
            string sXmlString = Encoding.UTF8.GetString(bsContent);
            return sXmlString;
        }

        /// <summary>
        /// д�����ֽ�����
        /// </summary>
        /// <returns>�ֽ�����</returns>
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
        #endregion д��XML��
    }
}
