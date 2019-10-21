using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Sigbit.Web.JavaScipt
{
    public class JSMessageBox
    {
        private JSMessageBox()
        {
        }

        /// <summary>
        /// ����JavaScriptС����
        /// </summary>
        /// <param name="message">������Ϣ</param>
        public static void Alert(string sMessage)
        {
            sMessage = sMessage.Replace("\r\n", "\\n");
            sMessage = sMessage.Replace("\n", "\\n");
            string js = @"<Script language='JavaScript'>
                    alert('" + sMessage + "');</Script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// ������Ϣ����ת���µ�URL
        /// </summary>
        /// <param name="sMessage">��Ϣ����</param>
        /// <param name="sToURL">ת����µ�ַ</param>
        public static void AlertAndRedirect(string sMessage, string sToURL)
        {
            sMessage = sMessage.Replace("\r\n", "\\n");
            sMessage = sMessage.Replace("\n", "\\n");
            string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, sMessage, sToURL));
        }





        /// <summary>
        /// ��ʾ��Ϣ��ʾ�Ի���
        /// </summary>
        /// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
        /// <param name="msg">��ʾ��Ϣ</param>
        public static void Show(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(System.Type.GetType("string"), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
        }

        /// <summary>
        /// �ؼ���� ��Ϣȷ����ʾ��
        /// </summary>
        /// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
        /// <param name="msg">��ʾ��Ϣ</param>
        public static void ShowConfirm(System.Web.UI.WebControls.WebControl Control, string msg)
        {
            //Control.Attributes.Add("onClick","if (!window.confirm('"+msg+"')){return false;}");
            Control.Attributes.Add("onclick", "return confirm('" + msg + "');");
        }

        /// <summary>
        /// ��ʾ��Ϣ��ʾ�Ի��򣬲�����ҳ����ת
        /// </summary>
        /// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
        /// <param name="msg">��ʾ��Ϣ</param>
        /// <param name="url">��ת��Ŀ��URL</param>
        public static void ShowAndRedirect(System.Web.UI.Page page, string msg, string url)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("alert('{0}');", msg);
            Builder.AppendFormat("top.location.href='{0}'", url);
            Builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(System.Type.GetType("string"), "message", Builder.ToString());
            

        }
        public static void ShowConfirmAndRedirect(System.Web.UI.Page page, string msg, string url)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            //Builder.AppendFormat("return confirm(('{0}');", msg);
            //Builder.AppendFormat("top.location.href='{0}'", url);
            Builder.AppendFormat("alert('{0}');", msg);
            Builder.AppendFormat("top.location.href='{0}'", url);
            Builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(System.Type.GetType("System.String"), "message", Builder.ToString());

        }
        /// <summary>
        /// ����Զ���ű���Ϣ
        /// </summary>
        /// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
        /// <param name="script">����ű�</param>
        public static void ResponseScript(System.Web.UI.Page page, string script)
        {
            page.ClientScript.RegisterStartupScript(System.Type.GetType("string"), "message", "<script language='javascript' defer>" + script + "</script>");
        }

        public void OpenNewWindow(string str, int a, int b)
        {
            HttpContext.Current.Response.Write("<script>window.open('" + str + "','newwindow','height=" + a + ",width=" + b + ",top=220,left= 300,toolbar=no,menubar=no,scrollbars=no,status=no');</script>");
        }

    }
}
