using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Net.BIPPacket
{
    /// <summary>
    /// 表示数据包的格式
    /// </summary>
    public enum BIPPacketFormat
    {
        /// <summary>
        /// 长包
        /// </summary>
        LongFormat = 'L',
        /// <summary>
        /// 短包
        /// </summary>
        ShortFormat = 'S'
    }

    /// <summary>
    ///  数据包的类型
    /// </summary>
    public enum BIPPacketType
    {
        /// <summary>
        /// 请求包
        /// </summary>
        Request = 'R',        // 请求包
        /// <summary>
        /// 回应包
        /// </summary>
        Answer = 'A',         // 回应包
        /// <summary>
        /// 通知包
        /// </summary>
        Inform = 'I',         // 通知包
        /// <summary>
        /// 回调包
        /// </summary>
        Callback = 'C'        // 回调包
    }

    /// <summary>
    /// 同步方式
    /// </summary>
    public enum BIPPacketSyncMeth
    {
        /// <summary>
        /// 本地调用返回
        /// </summary>
        LocalCall = '1',      // 本地调用返回
        /// <summary>
        /// 发送到目标地址返回
        /// </summary>
        ArriveDest = '2',     // 发送到目标地址返回
        /// <summary>
        /// 目标服务取出返回
        /// </summary>
        DestFetched = '3',    // 目标服务取出返回
        /// <summary>
        /// 目标服务完成返回（同步方式）
        /// </summary>
        DestDone = '4'        // 目标服务完成返回（同步方式）
    }

    /// <summary>
    /// 表示数据包的组织形式，目前仅有一种形式，之后可能可以扩充XML、
    /// ANSI等
    /// </summary>
    public enum BIPPacketOrgFormat
    {
        /// <summary>
        /// 目前定义的包组织形式
        /// </summary>
        Standard            // 目前定义的包组织形式
    }

    /// <summary>
    /// 调用操作方法产生的例外
    /// </summary>
    public class BIPCallException : Exception
    {
        /// <summary>
        /// 调用操作方法产生的例外
        /// </summary>
        /// <param name="sMsg"></param>
        public BIPCallException(string sMsg)
            : base(sMsg)
        {
        }
    }

    /// <summary>
    /// 数据包格式错产生的例外
    /// </summary>
    public class BIPFormatException : Exception
    {
        /// <summary>
        /// 数据包格式错产生的例外
        /// </summary>
        /// <param name="sMsg"></param>
        public BIPFormatException(string sMsg)
            : base(sMsg)
        {
        }
    }
}
