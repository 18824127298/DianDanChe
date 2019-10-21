using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Net.WebVisitor
{
    /// <summary>
    /// 网页访问结果
    /// </summary>
    public class WVWebResponse
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

        private string _pageContentString = "";
        /// <summary>
        /// 页面的正文。若请求中未定义编码，则该属性为""。
        /// </summary>
        public string PageContentString
        {
            get { return _pageContentString; }
            set { _pageContentString = value; }
        }

        private byte[] _pageBytes = null;
        /// <summary>
        /// 页面的二进制结果
        /// </summary>
        public byte[] PageBytes
        {
            get { return _pageBytes; }
            set { _pageBytes = value; }
        }

        private bool _hasSuccess = false;
        /// <summary>
        /// 是否成功访问网页
        /// </summary>
        public bool HasSuccess
        {
            get { return _hasSuccess; }
            set { _hasSuccess = value; }
        }

        private string _errorString = "";
        /// <summary>
        /// 网页访问的错误信息
        /// </summary>
        public string ErrorString
        {
            get { return _errorString; }
            set { _errorString = value; }
        }

        private DateTime _beginVisitTime = DateTime.MinValue;
        /// <summary>
        /// 开始访问的时间
        /// </summary>
        public DateTime BeginVisitTime
        {
            get { return _beginVisitTime; }
            set { _beginVisitTime = value; }
        }

        private DateTime _endVisitTime = DateTime.MinValue;
        /// <summary>
        /// 结束访问（获取网页）的时间
        /// </summary>
        public DateTime EndVisitTime
        {
            get { return _endVisitTime; }
            set { _endVisitTime = value; }
        }

        private int _totalVisitMilliseconds = 0;
        /// <summary>
        /// 总共用去的毫秒数
        /// </summary>
        public int TotalVisitMilliseconds
        {
            get { return _totalVisitMilliseconds; }
            set { _totalVisitMilliseconds = value; }
        }

        private int _executeThreadNo = -1;
        /// <summary>
        /// 执行的线程号
        /// </summary>
        public int ExecuteThreadNo
        {
            get { return _executeThreadNo; }
            set { _executeThreadNo = value; }
        }
    }
}
