using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Sigbit.App.Net.WeiXinService.Event;
using Sigbit.Common;

namespace Sigbit.App.Net.WeiXinService.Message
{
    public class TWXMessageConvert
    {
        public static TWXMessageBaseReq StringToReqMsg(string sPacket)
        {
            TWXMessageBaseReq messRet;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(sPacket);

            messRet = XmlToReqMsg(xmlDoc);

            messRet.ReadFrom(sPacket);

            return messRet;
        }

        public static TWXMessageBaseReq BytesToReqMsg(byte[] bsPacket)
        {
            TWXMessageBaseReq messRet;

            //========== 2. 加载字节数组 ===========
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(bsPacket, 0, bsPacket.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(memoryStream);

            memoryStream.Dispose();

            messRet = XmlToReqMsg(xmlDoc);

            messRet.ReadFrom(bsPacket);

            return messRet;
        }

        public static TWXMessageBaseReq XmlToReqMsg(XmlDocument xmlDoc)
        {
            TWXMessageBaseReq messRet;

            //========== 3. 根节点 =================
            XmlNode nodeMsgType = null;

            XmlNode nodeEvent = null;

            XmlNode nodeEventKey = null;

            foreach (XmlNode node in xmlDoc.FirstChild.ChildNodes)
            {
                if (node.Name == "MsgType")
                    nodeMsgType = node;

                if (node.Name == "Event")
                    nodeEvent = node;

                if (node.Name == "EventKey")
                    nodeEventKey = node;
            }

            if (nodeMsgType == null)
                throw new Exception("TWXMessageConvert.BytesToReqMsg()解析错误 : "
                                    + "找不到MsgType结点");

            string sMsgType = nodeMsgType.InnerText;



            //=========== 5. 根据命令字判断包类型 ==================
            MsgType eMsgType = (MsgType)EnumExUtil.CodeToEnum(MsgType.None, sMsgType.ToLower());

            if (eMsgType == MsgType.None)
                throw new Exception("未实现处理的消息类型:" + sMsgType);

            switch (eMsgType)
            {
                case MsgType.Text:
                    messRet = new TWXTextMessageReq();
                    break;
                case MsgType.Image:
                    messRet = new TWXImageMessageReq();
                    break;
                case MsgType.Voice:
                    messRet = new TWXVoiceMessageReq();
                    break;
                case MsgType.Video:
                    messRet = new TWXVideoMessageReq();
                    break;
                case MsgType.Shortvideo:
                    messRet = new TWXShortvideoMessageReq();
                    break;
                case MsgType.Location:
                    messRet = new TWXLocationMessageReq();
                    break;
                case MsgType.Link:
                    messRet = new TWXLinkMessageReq();
                    break;
                case MsgType.Event:
                    if (nodeEvent == null)
                        throw new Exception("事件消息未找到相应的事件类型");

                    string sEventKey = nodeEvent.InnerText.ToLower();

                    TWXEvent eEvent = (TWXEvent)(EnumExUtil.CodeToEnum(TWXEvent.None, sEventKey));

                    switch (eEvent)
                    {
                        case TWXEvent.Subscribe:
                            if (nodeEventKey == null)
                                messRet = new TWXSubscribeEvent();
                            else
                                messRet = new TWXQRsceneSubscribeEvent();
                            break;
                        case TWXEvent.Unsubscribe:
                            messRet = new TWXUnsubscribeEvent();
                            break;
                        case TWXEvent.Scan:
                            messRet = new TWXScanEvent();
                            break;
                        case TWXEvent.Location:
                            messRet = new TWXLocationEvent();
                            break;
                        case TWXEvent.Click:
                            messRet = new TWXClickEvent();
                            break;
                        case TWXEvent.View:
                            messRet = new TWXViewEvent();
                            break;
                        default:
                            throw new Exception("未实现的事件处理:" + eEvent.ToDescString());
                    }
                    break;
                default:
                    throw new Exception("未实现的消息处理:" + eMsgType.ToDescString());
            }
           
            return messRet;
        }


    }
}
