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
        /// 弹出JavaScript小窗口
        /// </summary>
        /// <param name="message">窗口信息</param>
        public static void Alert(string sMessage)
        {
            sMessage = sMessage.Replace("\r\n", "\\n");
            sMessage = sMessage.Replace("\n", "\\n");
            string js = @"<Script language='JavaScript'>
                    alert('" + sMessage + "');</Script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// 弹出消息框并且转向到新的URL
        /// </summary>
        /// <param name="sMessage">消息内容</param>
        /// <param name="sToURL">转向的新地址</param>
        public static void AlertAndRedirect(string sMessage, string sToURL)
        {
            sMessage = sMessage.Replace("\r\n", "\\n");
            sMessage = sMessage.Replace("\n", "\\n");
            string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, sMessage, sToURL));
        }





        /// <summary>
        /// 显示消息提示对话框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void Show(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(System.Type.GetType("string"), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
        }

        /// <summary>
        /// 控件点击 消息确认提示框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void ShowConfirm(System.Web.UI.WebControls.WebControl Control, string msg)
        {
            //Control.Attributes.Add("onClick","if (!window.confirm('"+msg+"')){return false;}");
            Control.Attributes.Add("onclick", "return confirm('" + msg + "');");
        }

        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
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
        /// 输出自定义脚本信息
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="script">输出脚本</param>
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
