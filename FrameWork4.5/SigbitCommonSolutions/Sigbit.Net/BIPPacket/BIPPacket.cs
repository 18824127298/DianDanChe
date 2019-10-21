using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using Sigbit.Common;

namespace Sigbit.Net.BIPPacket
{
    /// <summary>
    /// 消息包
    /// </summary>
    public class BIPPacket : BIPCustomPacket
    {
        #region 属性
        BIPPacketOrgFormat _packetOrgFormat;
        /// <summary>
        /// 数据包的组织形式
        /// </summary>
        public BIPPacketOrgFormat PacketOrgFormat
        {
            get { return _packetOrgFormat; }
            set { _packetOrgFormat = value; }
        }

        string _packetOrgVersion;
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
        public BIPPacket()
        {
            _packetOrgFormat = BIPPacketOrgFormat.Standard;
            _packetOrgVersion = "10";
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

            //========= 3. 状态、包重发次数、同步方式 =======
            sLine = "\tSTATUS=" + PacketStatus;
            sLine += "\tRESEND=" + PacketResendCount;
            sLine += "\tSYNC_METH=" + PacketSyncMeth;
            sContent += sLine + "\r\n";

            //=========== 4. DataSet ==========
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
        public void ReadFromFile(string sFileName)
        {
            FileStream fs = File.OpenRead(sFileName);
            ReadFrom(fs);
            fs.Close();
        }

        /// <summary>
        /// 从文件流中产生包的内容
        /// </summary>
        /// <param name="fileStream">文件名</param>
        public void ReadFrom(FileStream fileStream)
        {
            int nFileSize = (int)fileStream.Length;
            byte[] bsFileContent = new byte[nFileSize];
            fileStream.Read(bsFileContent, 0, nFileSize);
            ReadFrom(bsFileContent);
        }

        /// <summary>
        /// 从字节数组中产生包的内容
        /// </summary>
        /// <param name="bsPacket">字节数组</param>
        public virtual void ReadFrom(byte[] bsPacket)
        {
            ReadFrom(bsPacket, false);
        }

        /// <summary>
        /// 从字节数组中产生包的内容
        /// </summary>
        /// <param name="bsPacket">字节数组</param>
        /// <param name="bOneDataSet">是否只读每1个结果集</param>
        public void ReadFrom(byte[] bsPacket, bool bOneDataSet)
        {
            DeleteAllDataSet();
            while (DataSetCount != 0)
                DeleteDataSet(0);

            int nPacketStringTotalLength;
            nPacketStringTotalLength = bsPacket.Length;

            //========== 1. 包头标志，固定为"F7" =========
            int nPos = 0;
            byte cPacketHeaderId;
            cPacketHeaderId = BIPUtil.RXNByte(bsPacket, ref nPos);
            if (cPacketHeaderId != 0xF7)
                throw new BIPFormatException("TCBIPPacket::ReadFrom(string) : "
                        + "Invalid packet header encountered.");

            //============ 2. 包长度，包的总长度=包长度+6 ========
            int nPacketLength = BIPUtil.RXNLongNumber(bsPacket, ref nPos);
            if (nPacketStringTotalLength != 6 + nPacketLength)
                throw new BIPFormatException("TCBIPPacket::ReadFrom(string) : "
                        + "not exact one packet space in the string.");

            //============ 3. 包尾标志，固定为"FD" =========
            byte cPacketTailerID = bsPacket[nPacketStringTotalLength - 1];
            if (cPacketTailerID != 0xFD)
                throw new BIPFormatException("TCBIPPacket::ReadFrom(string) : "
                        + "Invalid packet tailer encountered.");

            //=========== 4. 包格式（长包、短包） ===========
            PacketFormat = (BIPPacketFormat)BIPUtil.RXNByteNumber(bsPacket, ref nPos);
            if (PacketFormat != BIPPacketFormat.ShortFormat 
                    && PacketFormat != BIPPacketFormat.LongFormat)
                throw new BIPFormatException("TCBIPPacket::ReadFrom(string) : "
                        + "unknow packet_format.");

            //========== 5. 包类型（请求、回应、通知、回调）==========
            PacketType = (BIPPacketType)BIPUtil.RXNByteNumber(bsPacket, ref nPos);
            if (PacketType != BIPPacketType.Request 
                    && PacketType != BIPPacketType.Answer
                    && PacketType != BIPPacketType.Inform 
                    && PacketType != BIPPacketType.Callback)
                throw new BIPFormatException("TCBIPPacket::ReadFrom(string) : "
                        + "unknow packet_type.");

            //========== 6. 包交易码 =========
            TransCode = BIPUtil.RXNStringByLength(bsPacket, ref nPos, 8);
            TransCode = TransCode.TrimEnd(new char[2] { '\0', ' ' });

            //========= 7. 分析包数据 ========
            byte[] bsUnpackData = BIPUtil.RXNByLength(bsPacket, ref nPos, 
                    nPacketStringTotalLength - 16);

            if (PacketFormat == BIPPacketFormat.ShortFormat)
                ShortPacketBuffer = bsUnpackData;
            else
                RXNUnpackData(bsUnpackData, bOneDataSet);
        }

        /// <summary>
        /// 读取包数据的内容
        /// </summary>
        /// <param name="bsData">包数据</param>
        /// <param name="bOneDataset">是否只读每个结果集</param>
        void RXNUnpackData(byte[] bsData, bool bOneDataset)
        {
            int nPos = 0;

            //=============== 1. 数据头标志N(1)固定为"B0" =======
            byte cDataHeaderID = BIPUtil.RXNByte(bsData, ref nPos);
            if (cDataHeaderID != 0xB0)
                throw new BIPFormatException("TCBIPPacket::RXNUnpackData : "
                        + "Invalid data header encountered.");

            //========== 2. 版本号 9(2) =========
            PacketOrgVersion = BIPUtil.RXNStringByLength(bsData, ref nPos, 2).TrimEnd();

            //========== 3. 包标志 N(4) =========
            PacketId = BIPUtil.RXNLongNumber(bsData, ref nPos);

            //========== 4. 数据块数量 N(1) ========
            int nDataSetCount;
            nDataSetCount = BIPUtil.RXNByteNumber(bsData, ref nPos);
            if (bOneDataset)
                nDataSetCount = 1;

            for (int i = 0; i < nDataSetCount; i++)
                NewDataSet();

            //========= 5. 包状态类型 X(4) =======
            PacketStatus = BIPUtil.RXNStringByLength(bsData, ref nPos, 4).TrimEnd();

            //======== 6. 包重发次数 N(1) =========
            PacketResendCount = BIPUtil.RXNByteNumber(bsData, ref nPos);

            //======== 7. 优先级等 ===========
            BIPUtil.RXNByLength(bsData, ref nPos, 11);

            //======== 8. 同步方式 ==========
            PacketSyncMeth = (BIPPacketSyncMeth)(BIPUtil.RXNByteNumber(bsData, ref nPos));

            //======== 9. 保留 ===========
            BIPUtil.RXNByLength(bsData, ref nPos, 7);

            //======= 10. 处理DataSet ===========
            for (int i = 0; i < nDataSetCount; i++)
            {
                GetDataSet(i).ReadFrom(bsData, ref nPos);
            }
        }
        #endregion 读取BIP包

        #region 写出BIP包
        /// <summary>
        /// 写入到文件
        /// </summary>
        /// <param name="sFileName">文件名</param>
        public void WriteToFile(string sFileName)
        {
            FileStream fs = File.Create(sFileName);
            WriteTo(fs);
            fs.Close();
        }

        /// <summary>
        /// 写入到文件流
        /// </summary>
        /// <param name="fs">文件流</param>
        public void WriteTo(FileStream fs)
        {
            byte[] bsPacketContent = ToBytes();
            fs.Write(bsPacketContent, 0, bsPacketContent.Length);
        }

        /// <summary>
        /// 得到包的字节数组
        /// </summary>
        /// <returns>包的字节数组</returns>
        public virtual byte[] ToBytes()
        {
            byte[] bsContent = WXNContent();

            BIPBytesBuilder bb = new BIPBytesBuilder();

            //========= 1. 包头标志 N(1) 固定为"F7" ========
            bb.AddByte(0xF7);

            //======= 2. 包长度 N(4) =========
            bb.AddLongNumber(bsContent.Length);

            //========= 3. 包内容 ===========
            bb.Add(bsContent);

            //========= 4. 包尾标志 N(1) 固定为"FD" =========
            bb.AddByte(0xFD);

            return bb.ToBytes();
        }

        /// <summary>
        /// 得到包内容数据
        /// </summary>
        /// <returns>包内容数据</returns>
        public byte[] WXNContent()
        {
            BIPBytesBuilder bb = new BIPBytesBuilder();

            //======== 1. 包格式 X(1) =======
            bb.AddByte((byte)PacketFormat);
            //sContent = TCString((char)GetPacketFormat());

            //========= 2. 包类型 X(1) ========
            bb.AddByte((byte)PacketType);

            //====== 3. 包交易码 X(8) ========
            if (TransCode.Length > 8)
                throw new BIPCallException("TCBIPPacket::WXNContent() : "
                        + "trans_code is too long - " + TransCode);
            bb.AddPureString(TransCode.PadRight(8));

            //======= 4. 包数据 Z ==========
            if (PacketFormat == BIPPacketFormat.ShortFormat)
            {
                if (this.ShortPacketBuffer != null)
                    bb.Add(this.ShortPacketBuffer);
                else
                {
                    string sData = ShortPacketData;
                    bb.AddPureString(sData);
                }
            }
            else
            {
                byte[] bsData;
                bsData = WXNPacketData();
                bb.Add(bsData);
            }

            return bb.ToBytes();
        }

        /// <summary>
        /// 得到包数据
        /// </summary>
        /// <returns>包数据</returns>
        public byte[] WXNPacketData()
        {
            BIPBytesBuilder bb = new BIPBytesBuilder();

            //======== 1. 数据头标志 N(1) 固定为"B0" =========
            bb.AddByte(0xB0);

            //======= 2. 版本号 9(2) =========
            if (PacketOrgVersion.Length  > 2)
                throw new BIPCallException("TCBIPPacket::WXNPacketData() : "
                        + "packet_org_version is too long - " + PacketOrgVersion);
            bb.AddPureString(PacketOrgVersion.PadRight(2));

            //======= 3. 包标志 N(4) =========
            bb.AddLongNumber(PacketId);

            //======== 4. 数据块数量 N(1) =========
            bb.AddByteNumber(DataSetCount);

            //======= 5. 包状态类型 X(4) ========
            if (PacketStatus.Length > 4)
                throw new BIPCallException("TCBIPPacket::WXNPacketData() : "
                        + "packet_status is too long - " + PacketStatus);
            bb.AddPureString(PacketStatus.PadRight(4));

            ////========= 6. 包重发次数 =========
            bb.AddByteNumber(PacketResendCount);

            //======== 7. 优先级N(1)、加密标志X(1)、压缩标志X(1)、========
            //========永久性标志X(1)、负载标示N(1)                ========
            //======= 事务时间约束N(2)、包时间约束N(2)、包内时间约束N(2)==
            //======= 暂时全填空格 =======================================
            bb.AddPureString(StringUtil.RepeatChar(' ', 11));

            //======== 8. 同步方式 ========
            bb.AddByte((byte)PacketSyncMeth);

            //====== 9. 保留 =======
            bb.AddPureString(StringUtil.RepeatChar(' ', 7));

            //========= 10. 数据块 ======
            for (int i = 0; i < DataSetCount; i++)
            {
                BIPDataSet ds = GetDataSet(i);
                ds.DataSetSeq = i + 1;
                byte[] bsDataSet = ds.ToBytes();
                bb.Add(bsDataSet);
            }

            return bb.ToBytes();
        }
        #endregion 写出BIP包
    }
}
