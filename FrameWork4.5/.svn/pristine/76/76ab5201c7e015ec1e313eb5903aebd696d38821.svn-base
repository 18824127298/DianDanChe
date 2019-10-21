

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
namespace Sigbit.Web.OutLookBar
{
    /// <summary>
    /// OutlookBar按钮类
    /// </summary>
    public class OutlookbarButton
    {
        string _ButtonImage;
        /// <summary>
        /// 按钮图像
        /// </summary>
        public string ButtonImage
        {
            get { return _ButtonImage; }
            set { _ButtonImage = value; }
        }
        string _ButtonUrl;

        /// <summary>
        /// 按钮URL
        /// </summary>
        public string ButtonUrl
        {
            get { return _ButtonUrl; }
            set { _ButtonUrl = value; }
        }
        string _ButtonCaption;

        /// <summary>
        /// 按钮标题
        /// </summary>
        public string ButtonCaption
        {
            get { return _ButtonCaption; }
            set { _ButtonCaption = value; }
        }
    }

    /// <summary>
    /// Outlook Bar Header 类
    /// </summary>
    public class OutlookbarHeader
    {
        string _HeaderCaption;

        /// <summary>
        /// 标题
        /// </summary>
        public string HeaderCaption
        {
            get { return _HeaderCaption; }
            set { _HeaderCaption = value; }
        }
        ArrayList _arrButton = new ArrayList();

        /// <summary>
        /// 按钮数组
        /// </summary>
        public ArrayList ArrButton
        {
            get { return _arrButton; }
            set { _arrButton = value; }
        }
    }


    /// <summary>
    /// 生成Outlookbar代码
    /// </summary>
    public class OutlookbarGenerate
    {
        ArrayList _arrHeader = new ArrayList();
        string sImagePath = "./images/outlookbar/";


        /// <summary>
        /// 输出脚本
        /// </summary>
        /// <returns>脚本</returns>
        public string EchoScript()
        {
            StringBuilder Result = new StringBuilder();
            int i;

            i = 0;
            foreach (OutlookbarHeader Header in _arrHeader)
            {
                Result.Append(EchoScript__Header(i, ((OutlookbarHeader)(_arrHeader[i])).HeaderCaption));
                i++;
            }

            Result.Append(EchoScript__HeaderClose());

            i = 0;

            foreach (OutlookbarHeader Header in _arrHeader)
            {
                Result.Append(EchoScript__Buttons(i));
                i++;
            }


            Result.Append(EchoScript__ButtonsClose());

            return Result.ToString();

            //this->EchoScript__ButtonsClose();


            //for (i = 0; i < count(_arrHeader); i++)
            //{
            //    this->EchoScript__Header(i, _arrHeader[i]->m_sHeaderCaption);
            //}
            //this->EchoScript__HeaderClose();

            //for (i = 0; i < count(_arrHeader); i++)
            //{
            //    this->EchoScript__Buttons(i);
            //}
            //this->EchoScript__ButtonsClose();
        }

        /// <summary>
        /// 输出分组脚本
        /// </summary>
        /// <param name="nHeaderSeq">分组编号</param>
        /// <param name="sHeaderCaption">分组标题</param>
        /// <returns></returns>
        public string EchoScript__Header(int nHeaderSeq, string sHeaderCaption)
        {
            StringBuilder sbResult = new StringBuilder();
            if (nHeaderSeq == 0)
            {
                sbResult.Append("<div id=\"cspbPanelBar\" onselectstart=\"return false;\" style=\"position:relative; ");
                sbResult.Append("left: 0; top: 0; z-index: 0\">");
                sbResult.Append("<div id=\"cspbPanelBarLoading\">");
                sbResult.Append("<table width=140 border=1 cellpadding=3 cellspacing=0 bgcolor=\"\" bordercolor=\"#000000\">");
                //            echo '<tr><td class="cspbBtn">Loading PanelBar...</td></tr></table></div>";
                //sResult += "<tr><td class=\"cspbBtn\">"+Application["AppName"]+"</td></tr></table></div>";
                sbResult.Append("<tr><td class=\"cspbBtn\"></td></tr></table></div>");
                sbResult.Append("<div id=\"cspbButtons\" style=\"cursor:default; display:none\">");
                sbResult.Append( "<table width=140 border=0 cellpadding=0 cellspacing=0>");
                 sbResult.Append("<tr align=\"center\"><td bgcolor=\"#E1EBF7\" width=140>");
                sbResult.Append( "<table width=140 border=0 cellpadding=0 cellspacing=0><tr><td bgcolor=\"\" width=140>");
                sbResult.Append( "<table width=140 border=0 cellpadding=0 cellspacing=0><tr>");
                sbResult.Append( "<td class=\"cspbBtnCell\" id=\"cspbBtnCell0\" onClick=\"cspbShowSection(0);\" ");
                sbResult.Append( "onmouseover=\"cspbHBT(0);\" onmouseout=\"cspbDBT(0);\">");
                sbResult.Append( "<p class=\"cspbBtn\" id=\"cspbBtnText0\">");
                sbResult.Append( sHeaderCaption);
                sbResult.Append( "</td></tr></table></td></tr></table>" + "\r\n");
            }
            else
            {
                sbResult.Append("<div id=\"cspbGroup" + (nHeaderSeq - 1).ToString() + "\" style=\"display:none;overflow:hidden; ");
                sbResult.Append("background-image: url(" + sImagePath + "/bg_left.gif)\">");
                sbResult.Append( "<table width=140 border=0 cellpadding=0 cellspacing=0><tr>");
                sbResult.Append( "<td id=\"cspbGroup" + (nHeaderSeq - 1).ToString() + "End\">");
                sbResult.Append( "</td></tr></table></div></td></tr><tr align=\"center\">");
                sbResult.Append( "<td bgcolor=\"#E1EBF7\" width=140><table width=140 border=0 cellpadding=0 cellspacing=0>");
                sbResult.Append( "<tr><td bgcolor=\"\" width=140><table width=140 border=0 cellpadding=0 cellspacing=0>");
                sbResult.Append( "<tr><td class=\"cspbBtnCell\" id=\"cspbBtnCell" + nHeaderSeq.ToString() + "\" ");
                sbResult.Append( "onClick=\"cspbShowSection(" + nHeaderSeq.ToString() + ");\" ");
                sbResult.Append( "onmouseover=\"cspbHBT(" + nHeaderSeq.ToString() + ");\" ");
                sbResult.Append( "onmouseout=\"cspbDBT(" + nHeaderSeq.ToString() + ");\">");
                sbResult.Append( "<p class=\"cspbBtn\" id=\"cspbBtnText" + nHeaderSeq.ToString() + "\">");
                sbResult.Append( sHeaderCaption);
                sbResult.Append( "</td></tr></table></td></tr></table>" + "\r\n");
            }
            return sbResult.ToString();
        }

        /// <summary>
        /// 输出分组结束脚本
        /// </summary>
        /// <returns>分组结束脚本HTML代码</returns>
        private string EchoScript__HeaderClose()
        {
            StringBuilder sbResult = new StringBuilder();

            int nHeaderCount = _arrHeader.Count;
            sbResult.Append("<div id=\"cspbGroup" + (nHeaderCount - 1).ToString() + "\" style=\"display:none;overflow:hidden; ");
            sbResult.Append( "background-image: url((" + sImagePath + "bg_left.gif)\">");
            sbResult.Append( "<table width=140 border=0 cellpadding=0 cellspacing=0><tr>");
            sbResult.Append( "<td id=\"cspbGroup" + (nHeaderCount - 1).ToString() + "End\">");
            sbResult.Append( "</td></tr></table></div></td></tr></table></div>" + "\r\n");
            return sbResult.ToString();
        }

        //==========================================================================
        // 用途 : 输出按钮脚本
        //==========================================================================
        /// <summary>
        /// 输出按钮脚本
        /// </summary>
        /// <param name="nHeaderSeq">分组编号</param>
        /// <returns>输出按钮脚本HTML</returns>
        string EchoScript__Buttons(int nHeaderSeq)
        {
            StringBuilder sbResult = new StringBuilder();
            string sButtonCaption;
            ArrayList arrButton = ((OutlookbarHeader)(_arrHeader[nHeaderSeq])).ArrButton;
            string sButtonImage;
            string sButtonUrl;
            OutlookbarButton Button;

            sbResult.Append("<div id=\"cspbIcons" + nHeaderSeq.ToString() + "\" style=\"position:absolute;top:0;left:0;display:none;overflow:hidden;\">");
            sbResult.Append("<table width=140 border=0 cellpadding=0 cellspacing=0><tr><td height=10></td></tr>");

            for (int i = 0; i < arrButton.Count; i++)
            {
                Button = (OutlookbarButton)(arrButton[i]);
                sButtonCaption = Button.ButtonCaption;
                sButtonImage = Button.ButtonImage;
                sButtonUrl = Button.ButtonUrl;
                sbResult.Append( "<tr><td class=\"cspbItmAlign\">");
                //sbResult.Append("<a class=\"cspbItm\" onClick=\"pageshow('" + sButtonUrl + "\',\'ifroper\')\">");
                sbResult.Append("<a class=\"cspbItm\" href=\"javascript:\" onClick=\"GotoURL('" + sButtonUrl + "\',\'ifroper\')\">");
                sbResult.Append("<img align=\"absmiddle\" src=\"" + sImagePath + "mnu_ico/" + sButtonImage + "\" width=20 height=20 border=\"0\">");
                sbResult.Append("<span style=\"width:5px\"></span>");
                sbResult.Append(sButtonCaption + "</a></td></tr><tr><td height=10></td></tr>");
            }

            sbResult.Append( "</table></div>" + "\r\n");
            return sbResult.ToString();
        }

        //==========================================================================
        // 用途 : 输出按钮结束
        //==========================================================================
        string EchoScript__ButtonsClose()
        {
            StringBuilder sbResult = new StringBuilder();
            sbResult.Append("<div id=\"cspbScrollUp\" style=\"position:absolute; top:0; left:0; display:none\">");
            sbResult.Append("<img id=\"cspbScrollUpImg\" ");
            sbResult.Append("src=\"(" + sImagePath + "up_disabled.gif\" onMouseDown=\"cspbSS=false;cspbSU(50);\" ");
            sbResult.Append("onMouseUp=\"cspbSS=true;cspbSI(\'inactive\');\" ");
            sbResult.Append("onMouseOut=\"cspbSS=true;cspbSI(\'inactive\');\" width=16 height=16 alt=\"\" border=0>");
            sbResult.Append("</div><div id=\"cspbScrollDown\" style=\"position:absolute; top:0; left:0; display:none\">");
            sbResult.Append("<img id=\"cspbScrollDownImg\" src=\"(" + sImagePath + "down_enabled.gif\" ");
            sbResult.Append("onMouseDown=\"cspbSS=false;cspbSD(50);\" onMouseUp=\"cspbSS=true;cspbSI(\'inactive\');\" ");
            sbResult.Append("onMouseOut=\"cspbSS=true;cspbSI(\'inactive\');\" width=16 height=16 alt=\"\" border=0>");
            sbResult.Append("</div></div>");
            return sbResult.ToString();

            //echo '<div id="cspbScrollUp" style="position:absolute; top:0; left:0; display:none">';
            //echo '<img id="cspbScrollUpImg" ';
            //echo 'src="(" + sImagePath + "up_disabled.gif" onMouseDown="cspbSS=false;cspbSU(50);" ';
            //echo 'onMouseUp="cspbSS=true;cspbSI(\'inactive\');" ';
            //echo 'onMouseOut="cspbSS=true;cspbSI(\'inactive\');" width=16 height=16 alt="" border=0>';
            //echo '</div><div id="cspbScrollDown" style="position:absolute; top:0; left:0; display:none">';
            //echo '<img id="cspbScrollDownImg" src="(" + sImagePath + "down_enabled.gif" ';
            //echo 'onMouseDown="cspbSS=false;cspbSD(50);" onMouseUp="cspbSS=true;cspbSI(\'inactive\');" ';
            //echo 'onMouseOut="cspbSS=true;cspbSI(\'inactive\');" width=16 height=16 alt="" border=0>';
            //echo '</div></div>';
        }

        /// <summary>
        /// 设置分组标题
        /// </summary>
        /// <param name="nHeaderSeq">分组编号</param>
        /// <param name="sHeaderCaption">分组标题</param>
        /// <returns>错误或者分组</returns>
        public void SetHeaderCaption(int nHeaderSeq, string sHeaderCaption)
        {
            if (nHeaderSeq != _arrHeader.Count)
            {
                throw new Exception( "OutlookbarGenerate::SetHeaderCaption() error : sequence out of range."); 
            }
            OutlookbarHeader header = new OutlookbarHeader();
            header.HeaderCaption = sHeaderCaption;
            //_arrHeader[nHeaderSeq] = header;
            _arrHeader.Add(header);

            //return "";
        }

        //==========================================================================
        // 用途 : 设置按钮属性前的处理，判断是否有效，增加一个按钮
        //==========================================================================
        private string SetButton__Check(int nHeaderSeq, int nButtonSeq)
        {
            if (nHeaderSeq >= _arrHeader.Count)
            {
                return "OutlookbarGenerate::SetButtonCaption() error : header sequence out of range.";
            }

            if (nButtonSeq > ((OutlookbarHeader)(_arrHeader[nHeaderSeq])).ArrButton.Count)
            {
                return "OutlookbarGenerate::SetButtonCaption() error : button sequence out of range.";
            }

            if (nButtonSeq == ((OutlookbarHeader)(_arrHeader[nHeaderSeq])).ArrButton.Count)
            {
                OutlookbarButton button = new OutlookbarButton();
                ((OutlookbarHeader)(_arrHeader[nHeaderSeq])).ArrButton.Add( button);
                //((OutlookbarHeader)(_arrHeader[nHeaderSeq])).ArrButton[nButtonSeq] = button;
            }
            return "";

        }

        /// <summary>
        /// 设置按钮标题
        /// </summary>
        /// <param name="nHeaderSeq">分组编号</param>
        /// <param name="nButtonSeq">按钮编号</param>
        /// <param name="sButtonCaption">按钮标题</param>
        public void SetButtonCaption(int nHeaderSeq, int nButtonSeq, string sButtonCaption)
        {
            SetButton__Check(nHeaderSeq, nButtonSeq);
            OutlookbarButton Button;
            Button = (OutlookbarButton)(((OutlookbarHeader)(_arrHeader[nHeaderSeq])).ArrButton[nButtonSeq]);
            Button.ButtonCaption = sButtonCaption;
        }

        /// <summary>
        /// 设置按钮图像
        /// </summary>
        /// <param name="nHeaderSeq">分组编号</param>
        /// <param name="nButtonSeq">按钮编号</param>
        /// <param name="sButtonImage">按钮图标</param>
        public void SetButtonImage(int nHeaderSeq, int nButtonSeq, string sButtonImage)
        {
            SetButton__Check(nHeaderSeq, nButtonSeq);
            OutlookbarButton Button = (OutlookbarButton)(((OutlookbarHeader)(_arrHeader[nHeaderSeq])).ArrButton[nButtonSeq]);
            Button.ButtonImage = sButtonImage;
        }

        /// <summary>
        /// 设置按钮键接
        /// </summary>
        /// <param name="nHeaderSeq">分组编号</param>
        /// <param name="nButtonSeq">按钮编号</param>
        /// <param name="sButtonUrl">按钮URL</param>
        public void SetButtonUrl(int nHeaderSeq, int nButtonSeq, string sButtonUrl)
        {
            SetButton__Check(nHeaderSeq, nButtonSeq);
            OutlookbarButton Button = (OutlookbarButton)(((OutlookbarHeader)(_arrHeader[nHeaderSeq])).ArrButton[nButtonSeq]);
            Button.ButtonUrl = sButtonUrl;
        }

    }
}




