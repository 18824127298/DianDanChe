using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Net.WebVisitor
{
    /// <summary>
    /// 网页字符集
    /// </summary>
    public enum WVPageCharset
    {
        /// <summary>
        /// 未定义
        /// </summary>
        Undefine,
        /// <summary>
        /// GB2312编码
        /// </summary>
        GB2312,
        /// <summary>
        /// UTF8编码
        /// </summary>
        UTF8
    }

    /// <summary>
    /// 网页访问配置
    /// </summary>
    public class WVWebVisitorConfig
    {
        private static WVWebVisitorConfig _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static WVWebVisitorConfig Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new WVWebVisitorConfig();
                return _thisInstance;
            }
        }

        private int _visitThreadCount = 10;
        /// <summary>
        /// 线程数量（缺省为10个）
        /// </summary>
        public int VisitThreadCount
        {
            get { return _visitThreadCount; }
            set { _visitThreadCount = value; }
        }

        private bool _logEventEnabled = true;
        /// <summary>
        /// 是否记录事件日志。以一天一个文件记录网页的地址和访问结果等。
        /// </summary>
        public bool LogEventEnabled
        {
            get { return _logEventEnabled; }
            set { _logEventEnabled = value; }
        }

        private bool _logDataEnabled = true;
        /// <summary>
        /// 是否记录详细的交互数据。详细的交互数据包括详细的请求、获取的页面等。
        /// </summary>
        public bool LogDataEnabled
        {
            get { return _logDataEnabled; }
            set { _logDataEnabled = value; }
        }

        private WVPageCharset _defaultCharset = WVPageCharset.UTF8;
        /// <summary>
        /// 缺省的字符集
        /// </summary>
        public WVPageCharset DefaultCharset
        {
            get { return _defaultCharset; }
            set { _defaultCharset = value; }
        }
    }
}
