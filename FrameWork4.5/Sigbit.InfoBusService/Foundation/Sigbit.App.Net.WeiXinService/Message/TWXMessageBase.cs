using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using Sigbit.Common;

namespace Sigbit.App.Net.WeiXinService.Message
{

    public enum MsgType
    {
        None,
        /// <summary>
        /// 文本消息
        /// </summary>
        [SbtEnumDescString("文本消息")]
        Text,
        /// <summary>
        /// 图片消息
        /// </summary>
        [SbtEnumDescString("图片消息")]
        Image,
        /// <summary>
        /// 语音消息
        /// </summary>
        [SbtEnumDescString("语音消息")]
        Voice,
        /// <summary>
        /// 视频消息
        /// </summary>
        [SbtEnumDescString("视频消息")]
        Video,
        /// <summary>
        /// 小视频消息
        /// </summary>
        [SbtEnumDescString("小视频消息")]
        Shortvideo,
        /// <summary>
        /// 地理位置消息
        /// </summary>
        [SbtEnumDescString("地理位置消息")]
        Location,
        /// <summary>
        /// 链接消息
        /// </summary>
        [SbtEnumDescString("链接消息")]
        Link,
        /// <summary>
        /// 事件消息
        /// </summary>
        [SbtEnumDescString("事件消息")]
        Event,
        /// <summary>
        /// 回复音乐消息
        /// </summary>
        [SbtEnumDescString("回复音乐消息")]
        Music,
        /// <summary>
        /// 回复图文消息
        /// </summary>
        [SbtEnumDescString("回复图文消息")]
        News

    }


    /// <summary>
    /// 基础消息内容
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    [XmlRoot(ElementName = "xml")]
    public class TWXMessageBase
    {
        private TWXNode _rootNode = new TWXNode();

        public TWXMessageBase()
        {
            _rootNode.Key = "xml";
        }

        #region 通用属性

        private string _toUserName = "";
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName
        {
            get { return _toUserName; }
            set { _toUserName = value; }
        }


        private string _fromUserName = "";
        /// <summary>
        /// 发送方帐号（一个OpenID）
        /// </summary>
        public string FromUserName
        {
            get { return _fromUserName; }
            set { _fromUserName = value; }
        }

        private DateTime _createTime;
        /// <summary>
        /// 消息创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return _createTime; }
            set
            {
                _createTime = value;
                _createTimeStamp = DateTimeStampUtil.ConvertToTimeStamp(value);
            }
        }


        private int _createTimeStamp = 0;
        /// <summary>
        /// 消息创建时间戳
        /// </summary>
        public int CreateTimeStamp
        {
            get { return _createTimeStamp; }
            set
            {
                _createTimeStamp = value;
                _createTime = DateTimeStampUtil.ConvertToDateTime(value);
            }
        }

        private MsgType _msgType = MsgType.None;
        /// <summary>
        /// 消息类型
        /// </summary>
        public MsgType MsgType
        {
            get { return _msgType; }
            set { _msgType = value; }
        }

        #endregion

        #region 同步属性和键值列表
        /// <summary>
        /// 从属性同步到键值列表中
        /// </summary>
        protected virtual void SynchronizeFromProperties()
        {
            this.ClearAllProperties();

            AddAStringValue("ToUserName", this.ToUserName);
            AddAStringValue("FromUserName", this.FromUserName);
            AddAStringValue("CreateTime", this.CreateTimeStamp.ToString(), false, true);
            AddAStringValue("MsgType", this.MsgType.ToCodeString());
        }

        /// <summary>
        /// 从键值列表中同步到属性
        /// </summary>
        protected virtual void SynchronizeToProperties()
        {
            this.ToUserName = GetAStringValue("ToUserName");
            this.FromUserName = GetAStringValue("FromUserName");
            this.CreateTimeStamp = GetAIntValue("CreateTime");
            this.MsgType = (MsgType)MsgType.CodeToEnum(GetAStringValue("MsgType"));
        }

        #endregion 同步属性和键值列表

        #region 键值对

        /// <summary>
        /// 清空键值对
        /// </summary>
        protected void ClearAllProperties()
        {
            _rootNode.ChildNodes.Clear();
        }


        protected void AddAStringValue(string sKey, string sValue)
        {
            AddAStringValue(sKey, sValue, true, true);
        }


        protected void AddAStringValue(string sKey, string sValue, bool bIsCDATA)
        {
            AddAStringValue(sKey, sValue, bIsCDATA, false);
        }

        /// <summary>
        /// 增加一项键值对
        /// </summary>
        /// <param name="sKey">键</param>
        /// <param name="sValue">值</param>
        /// <param name="bIsCData">是否为CDATA字段</param>
        protected void AddAStringValue(string sKey, string sValue, bool bIsCDATA, bool bAllowNull)
        {
            TWXNode node = new TWXNode();
            node.Key = sKey;
            node.Value = sValue;
            node.IsCDATA = bIsCDATA;
            node.AllowNull = bAllowNull;

            _rootNode.AppendNode(node);
        }


        protected void AddNode(TWXNode node)
        {
            _rootNode.AppendNode(node);
        }


        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sKey"></param>
        /// <param name="bIsCData"></param>
        /// <returns></returns>
        protected string GetAStringValue(string sKey)
        {
            return _rootNode.ChildNodes.GetValueOfKey(sKey);
        }

        protected int GetAIntValue(string sKey)
        {
            int nRet = ConvertUtil.ToInt(GetAStringValue(sKey));

            return nRet;
        }


        protected double GetAFloatValue(string sKey)
        {
            double fRet = ConvertUtil.ToFloat(GetAStringValue(sKey));
            return fRet;
        }

        protected TWXNode GetNode(string sKey)
        {
            for (int i = 0; i < _rootNode.ChildNodes.Count; i++)
            {
                TWXNode nodeCurrent = _rootNode.ChildNodes.GetNode(i);

                if (nodeCurrent.Key == sKey)
                    return nodeCurrent;
            }

            return null;
        }



        #endregion 键值对

        #region 通用方法
        /// <summary>
        /// 得到消息的描述
        /// </summary>
        /// <returns>消息的描述</returns>
        public virtual string GetMessageDescription()
        {
            string sRet = "【" + this.MsgType.ToDescString() + "】";
            sRet += "开发者微信号:" + this.ToUserName + ";";
            sRet += "发送方帐号:" + this.FromUserName + ";";
            sRet += "消息时间:" + DateTimeUtil.ToDateTimeStr(this.CreateTime) + ";";

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
            _rootNode.FromXmlDocument(xmlDoc);

            //=========== 2. 同步到属性 ==============
            if (!_bHasSynchronizedToProperties)
            {
                SynchronizeToProperties();
                _bHasSynchronizedToProperties = true;
            }
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
            if (!_bHasSynchronizedFromProperties)
            {
                SynchronizeFromProperties();
                _bHasSynchronizedFromProperties = true;
            }

            XmlDocument xmlDoc = _rootNode.ToXmlDocument();

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
