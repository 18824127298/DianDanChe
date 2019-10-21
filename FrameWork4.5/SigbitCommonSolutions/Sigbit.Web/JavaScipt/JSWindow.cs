using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Sigbit.Web.JavaScipt
{
    /// <summary>
    /// Javascript���ڴ��ڵķ�װ
    /// </summary>
    public class JSWindow
    {
        /// <summary>
        /// �ص���ʷҳ��
        /// </summary>
        /// <param name="nValue">��ǰ�����ļ���</param>
        public static void GoHistory(int nValue)
        {
            string js = @"<Script language='JavaScript'>
                    history.go({0});  
                  </Script>";
            HttpContext.Current.Response.Write(string.Format(js, nValue));
        }

        /// <summary>
        /// �õ���ʾModal�����JavaScript����
        /// </summary>
        /// <param name="sWebFormUrl">WebForm�ĵ�ַ</param>
        /// <param name="sFeatures">����Form���Ե��ַ���</param>
        /// <returns>JavaScript����</returns>
        private static string ShowModalDialogJavascript(string sWebFormUrl, string sFeatures)
        {
            string js = @"<script language=javascript>       
                    showModalDialog('" + sWebFormUrl + "','','" 
                    + sFeatures + "');</script>";
            return js;
        }

        /// <summary>
        /// ��ʾModal����
        /// </summary>
        /// <param name="webFormUrl">WebForm�ĵ�ַ</param>
        /// <param name="features">�����ַ���</param>
        public static void ShowModalDialogWindow(string sWebFormUrl, string sFeatures)
        {
            string js = ShowModalDialogJavascript(sWebFormUrl, sFeatures);
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// ��ָ����Сλ�õ�ģʽ�Ի���
        /// </summary>
        /// <param name="sWebFormUrl">���ӵ�ַ</param>
        /// <param name="nWidth">��</param>
        /// <param name="nHeight">��</param>
        /// <param name="nLeft">������λ��</param>
        /// <param name="nTop">������λ��</param>
        public static void ShowModalDialogWindow(string sWebFormUrl,
                int nWidth, int nHeight, int nLeft, int nTop)
        {
            string sFeatures = "dialogWidth:" + nWidth.ToString() + "px"
                + ";dialogHeight:" + nHeight.ToString() + "px"
                + ";dialogLeft:" + nLeft.ToString() + "px"
                + ";dialogTop:" + nTop.ToString() + "px"
                + ";center:yes;help=no;resizable:no;status:no;scroll=yes";
            ShowModalDialogWindow(sWebFormUrl, sFeatures);
        }

        /// <summary>
        /// �رյ�ǰ����
        /// </summary>
        public static void CloseWindow()
        {
            string js = @"<script language='JavaScript'>
                                parent.opener=null;window.close();  
                              </script>";
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// ��ָ����С�Ĵ���,�Զ����þ���
        /// </summary>
        /// <param name="sUrl">Ŀ�괰��</param>
        /// <param name="nWidth">��</param>
        /// <param name="nHeight">��</param>           
        public static void OpenWebFormSize(string sUrl, int nWidth, int nHeight)
        {
            string js = @"<Script language='JavaScript'>"
            + "\r\nvar w, left, top;"
            + "\r\nleft = (screen.width - " + nWidth + ") / 2;"
            + "\r\nif (left < 0) { left = 0; }"
            + "\r\ntop = (screen.height - 60 - " + nHeight + ") / 2;"
            + "\r\nif (top < 0) { top = 0; }"
            + "\r\nw = window.open('"
                    + sUrl + @"','','height=" + nHeight + ",width=" + nWidth
                    + ",top='+top+',left='+left+',location=no,menubar=no,resizable=yes,scrollbars=no,"
                    + "status=no,titlebar=no,toolbar=no,directories=no');"
            + "\r\ntry { w.focus(); }"
            + "\r\ncatch (e) { }"
            + "\r\n</Script>";

            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// ��ָ����С�Ĵ���
        /// </summary>
        /// <param name="sUrl">Ŀ�괰��</param>
        /// <param name="nWidth">��</param>
        /// <param name="nHeight">��</param>
        /// <param name="nLeft">������ߵľ���</param>
        /// <param name="nTop">�����ϱߵľ���</param>
        public static void OpenWebFormSize(string sUrl, int nWidth, int nHeight, 
                int nLeft, int nTop)
        {
            string js = @"<Script language='JavaScript'>window.open('"
                    + sUrl + @"','','height=" + nHeight + ",width=" + nWidth
                    + ",top=" + nTop + ",left=" + nLeft
                    + ",location=no,menubar=no,resizable=yes,scrollbars=yes,"
                    + "status=yes,titlebar=no,toolbar=no,directories=no');</Script>";

            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// ˢ�¸����ڵ�ָ����ַ
        /// </summary>
        /// <param name="sUrl">ָ��������</param>
        public static void RefreshOpener(string sUrl)
        {
            string js = @"<Script language='JavaScript'>
                    window.opener.location.href='" + sUrl + "';window.close();</Script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void CloseAndRefreshOpener()
        {
            //string script = "<script language=\"javascript\">";
            //script = script + "  try{ ";
            //script = script + "		top.opener.location.reload(); ";
            //script = script + "		top.opener.focus(); ";
            //script = script + "	}catch(e){} ";
            //script = script + "		window.close(); ";
            //script = script + "    </script> ";

            string script = "<script language=\"javascript\">";
            script = script + "  try{ ";
            script = script + "		top.opener.location.href=top.opener.location.href; ";
            script = script + "		";
            script = script + "	}catch(e){} ";
            script = script + "		window.close(); ";
            script = script + "    </script> ";

            HttpContext.Current.Response.Write(script);
        }

        /// <summary>
        /// ˢ�´򿪴���
        /// </summary>
        public static void RefreshOpener()
        {
            string js = @"<Script language='JavaScript'>
                    opener.location.reload();
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// ˢ�������ڣ�����Frame��ˢ�°����Ĵ��ڡ�
        /// </summary>
        public static void RefreshWindow()
        {
            string js = @"<Script language='JavaScript'>
                    top.location.reload();
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// ˢ�������ڵ�ָ����ַ
        /// </summary>
        /// <param name="sUrl">ָ��������</param>
        public static void RefreshWindow(string sUrl)
        {
            string js = @"<Script language='JavaScript'>
                    top.location.href='" + sUrl + "';window.close();</Script>";
            HttpContext.Current.Response.Write(js);        
        }

        /// <summary>
        /// ˢ�������ڲ���ת��������
        /// </summary>
        /// <param name="sUrl"></param>
        public static void RefreshTopWindow(string sUrl)
        {
            string js = @"<Script language='JavaScript'>
                    top.location.href='" + sUrl + "';</Script>";
            HttpContext.Current.Response.Write(js);
        }


        //public static void CloseWindow()
        //{
        //    HttpContext.Current.Response.Write("<script>window.opener=null;window.close();</script>");
        //}

        //public static void FreshWindow(string str)
        //{
        //    HttpContext.Current.Response.Write("<script>window.opener.location='" + str + "';</script>");
        //}

        public static void OpenWindow(string str)
        {
            HttpContext.Current.Response.Write("<script>window.open('" + str + "');</script>");
        }

        public static void OpenNewWindow(string str, int a, int b)       
        {                                                                       
            //HttpContext.Current.Response.Write("<script>window.open('" + str + "','newwindow','height=" + a + ",width=" + b + ",top=220,left= 300,toolbar=no,menubar=no,scrollbars=no,status=no');</script>");
            OpenNewWindow(str, "newwindow", a, b);
        }

        public static void OpenNewWindow(string str, int m, int a, int b)
        {
            //HttpContext.Current.Response.Write("<script>window.open('" + str + "','newwindow" + m + "','height=" + a + ",width=" + b + ",top=220,left=300,toolbar=no,menubar=no,scrollbars=no,status=no');</script>");
            OpenNewWindow(str,"newwindow",a, b);
        }

        public static void OpenNewWindowOne(string str, int a, int b)
        {
            HttpContext.Current.Response.Write("<script>window.open('" + str + "','newwindow','height=" + a + ",width=" + b + ",top=150,left= 300,toolbar=no,menubar=no,scrollbars=yes,status=no');</script>");
        }

        public static void OpenWindow(string str, string target)
        {
            HttpContext.Current.Response.Write("<script>window.open('" + str + "," + target + "');</script>");
        }

        public static void OpenNewWindow(string str, string target,int a, int b)
        {
            HttpContext.Current.Response.Write("<script>window.open('" + str + "','" + target + "','height=" + a + ",width=" + b + ",top=220,left= 300,toolbar=no,menubar=no,scrollbars=no,status=no');</script>");
        }

        //public static void OpenNewWindow(string str, string target, int m, int a, int b)
        //{
        //    HttpContext.Current.Response.Write("<script>window.open('" + str + "','"+ target +"" + m + "','height=" + a + ",width=" + b + ",top=220,left=300,toolbar=no,menubar=no,scrollbars=no,status=no');</script>");
        //}

        /// <summary>
        /// ָ��Frame��ҳ��
        /// </summary>
        /// <param name="sFrameName">Frame����</param>
        /// <param name="sUrl"></param>
        public static void RefreshFrame(string sFrameName, string sUrl)
        {
            string js = @"<Script language='JavaScript'>
                    top.frames['" + sFrameName+ "'].location.href='" + sUrl + "';</Script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// ָ��˫��Frame��ҳ��
        /// </summary>
        /// <param name="sParentFrameName">��һ��Fream����</param>
        /// <param name="sFrameName">�ڶ���Frame����</param>
        /// <param name="sUrl"></param>
        public static void RefreshFrame(string sParentFrameName, string sFrameName, string sUrl)
        {
            string js = @"<Script language='JavaScript'>
                    top.frames['" + sParentFrameName + "'].frames['" + sFrameName + "'].location.href='" + sUrl + "';</Script>";
            HttpContext.Current.Response.Write(js);
        }

        public static void Redirect(string sUrl)
        { 
            string js = @"<Script language='JavaScript'>
                    location.href='" + sUrl + "';</Script>";
            HttpContext.Current.Response.Write(js);
        }

    }
}
