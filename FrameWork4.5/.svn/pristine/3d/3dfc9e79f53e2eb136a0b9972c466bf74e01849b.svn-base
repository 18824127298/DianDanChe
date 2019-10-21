using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Net.WebVisitor
{
    /// <summary>
    /// 网页访问请求
    /// </summary>
    public class WVWebRequest
    {
        private string _requestId = "";
        /// <summary>
        /// 请求的标识串
        /// </summary>
        public string RequestId
        {
            get { return _requestId; }
            set { _requestId = value; }
        }

        private object _requestObjectData = null;
        /// <summary>
        /// 请求附带的对象
        /// </summary>
        public object RequestObjectData
        {
            get { return _requestObjectData; }
            set { _requestObjectData = value; }
        }

        private string _visitUrl = "";
        /// <summary>
        /// 访问的网页地址
        /// </summary>
        public string VisitUrl
        {
            get { return _visitUrl; }
            set { _visitUrl = value; }
        }

        private WVPageCharset _pageCharset = WVPageCharset.UTF8;
        /// <summary>
        /// 页面字符集
        /// </summary>
        public WVPageCharset PageCharset
        {
            get { return _pageCharset; }
            set { _pageCharset = value; }
        }
    }
}
