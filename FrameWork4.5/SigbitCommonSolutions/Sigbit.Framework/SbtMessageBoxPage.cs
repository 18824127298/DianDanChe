using System;
using System.Collections.Generic;
using System.Text;

using System.Web;

namespace Sigbit.Framework
{
    /// <summary>
    /// ͨ����Ϣ��
    /// </summary>
    public class SbtMessageBoxPage
    {
        /// <summary>
        /// ��ʾ��Ϣ��ĸ�ҳ��
        /// </summary>
        /// <remarks>
        /// ����Ҫ�õ���ҳ��Ĳ�����Response�������ڱ�����У�ͨ�����캯
        /// �����븸ҳ��
        /// </remarks>
        SbtPageBase _selfPage;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="selfPage">����MessageBox�ĸ�ҳ��</param>
        public SbtMessageBoxPage(SbtPageBase selfPage)
        {
            _selfPage = selfPage;
        }

        private string _messageText = "";
        /// <summary>
        /// ��ʾ����Ϣ����
        /// </summary>
        public string MessageText
        {
            get { return _messageText; }
            set { _messageText = value; }
        }

        private string _returnUrl = "";
        /// <summary>
        /// ���ص�ҳ��
        /// </summary>
        public string ReturnUrl
        {
            get { return _returnUrl; }
            set { _returnUrl = value; }
        }

        /// <summary>
        /// ��ʾ��Ϣҳ��
        /// </summary>
        public void Show()
        {
            _selfPage.PageParameter.StringParam[0] = _messageText;
            _selfPage.PageParameter.StringParam[1] = _returnUrl;
            _selfPage.Response.Redirect("~/framework/general_win/message_box.aspx");
        }
    }
}
