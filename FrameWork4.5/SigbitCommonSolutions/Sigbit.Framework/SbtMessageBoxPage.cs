using System;
using System.Collections.Generic;
using System.Text;

using System.Web;

namespace Sigbit.Framework
{
    /// <summary>
    /// 通用消息框
    /// </summary>
    public class SbtMessageBoxPage
    {
        /// <summary>
        /// 显示消息框的父页面
        /// </summary>
        /// <remarks>
        /// 由于要用到父页面的参数和Response，所以在本设计中，通过构造函
        /// 数传入父页面
        /// </remarks>
        SbtPageBase _selfPage;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="selfPage">调用MessageBox的父页面</param>
        public SbtMessageBoxPage(SbtPageBase selfPage)
        {
            _selfPage = selfPage;
        }

        private string _messageText = "";
        /// <summary>
        /// 显示的消息正文
        /// </summary>
        public string MessageText
        {
            get { return _messageText; }
            set { _messageText = value; }
        }

        private string _returnUrl = "";
        /// <summary>
        /// 返回的页面
        /// </summary>
        public string ReturnUrl
        {
            get { return _returnUrl; }
            set { _returnUrl = value; }
        }

        /// <summary>
        /// 显示消息页面
        /// </summary>
        public void Show()
        {
            _selfPage.PageParameter.StringParam[0] = _messageText;
            _selfPage.PageParameter.StringParam[1] = _returnUrl;
            _selfPage.Response.Redirect("~/framework/general_win/message_box.aspx");
        }
    }
}
