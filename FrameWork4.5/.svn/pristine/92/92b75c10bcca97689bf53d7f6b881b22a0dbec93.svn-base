using System;
using System.Collections.Generic;
using System.Text;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Sigbit.Common;
using Sigbit.Framework.NaviTab;

namespace Sigbit.Framework
{
    /// <summary>
    /// 大多数页面的基类
    /// </summary>
    public class SbtPageBase : System.Web.UI.Page
    {
        private SbtUser _currentUser = null;
        /// <summary>
        /// 当前的用户
        /// </summary>
        public SbtUser CurrentUser
        {
            get
            {
                if (_currentUser == null)
                    _currentUser = (SbtUser)Session["currentUserMEFTMG"];
                if (_currentUser == null)
                    this.Response.Redirect("~/framework/admin/admin_go.aspx");
                //throw new Exception("SbtPageBase.CurrentUser.get error: "
                //        + "Session中没有当前用户的记录信息");
                return _currentUser;
            }
        }

        private SbtPageParameter _pageParameter = null;
        /// <summary>
        /// 页面之间的参数
        /// </summary>
        public SbtPageParameter PageParameter
        {
            get
            {
                //======= 1. 如果已经赋了值，则直接返回 =======
                if (_pageParameter != null)
                    return _pageParameter;

                //======== 2. 判断session中的页面参数情况 =========
                _pageParameter = (SbtPageParameter)Session["pageParameterMEFTMG"];

                //======= 3. 如果session中存有值，则用session中的值 ======
                if (_pageParameter != null)
                    return _pageParameter;

                //========= 4. 否则创建一个值放到session中 =======
                _pageParameter = new SbtPageParameter();
                Session["pageParameterMEFTMG"] = _pageParameter;
                return _pageParameter;
            }
        }

        private SbtPageStatusList _pageStatusList = null;
        private SbtPageStatus _currentPageStatus = null;
        public SbtPageStatus CurrentPageStatus
        {
            get
            {
                //======= 1. 如果已经赋了值，则直接返回 =======
                if (_currentPageStatus != null)
                    return _currentPageStatus;

                //====== 2. 得到PageStatusList ==========
                if (_pageStatusList == null)
                {
                    //======== 2. 判断session中的页面状态情况 =========
                    _pageStatusList = (SbtPageStatusList)Session["pageStatusListMEFTMG"];

                    //======= 3. 如果session中无值，则创建一个值放到session中 ======
                    if (_pageStatusList == null)
                    {
                        _pageStatusList = new SbtPageStatusList();
                        Session["pageStatusListMEFTMG"] = _pageStatusList;
                    }
                }

                //========== 3. 得到页面状态 ============
                return _pageStatusList.GetPageStatus(Page.Request.Url.ToString());
                //return _pageStatusList.GetPageStatus(Page.Request.Path);
                //return _pageStatusList.GetPageStatus(Page.Request.RawUrl);
            }
        }

        public void ClearAllPageStatus()
        {
            Session["pageStatusListMEFTMG"] = null;
        }

        /// <summary>
        /// 在PreInit事件中动态加载主题
        /// </summary>
        /// <param name="e">事件</param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //string sForceThemeUsing = ForceThemeUsing;
            //if (sForceThemeUsing != "")
            //    Theme = sForceThemeUsing;
            //else
            Theme = this.CurrentUser.ThemePreference;
            //StyleSheetTheme = this.CurrentUser.ThemePreference;
        }

        /// <summary>
        /// 在OnLoad事件中进行权限检查
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            //=========== 1.当前访问路径的获取及规整  ================


            //AbsolutePath	"/DialMBSInspector.Web/ngresource_device/device_pc/device_pc_modify.aspx"	string

            string sCurrentUrl = Request.Url.AbsolutePath;

            string sVirtualPath = AppPath.GetCurrentVirtualPath();

            if (sVirtualPath != "")
                sCurrentUrl = "~" + sCurrentUrl.Substring(sVirtualPath.Length + 1);



            //========== 2.检查当前页面是否为权限项 ====================

            TbSysMenu tblRightMenu = TbSysMenu__Lib.Instance.GetMenuRecordByMenuLink(sCurrentUrl);

            if (tblRightMenu != null)
            {
                if (!CurrentUser.HasPopedom(tblRightMenu.MenuCode))
                {
                    //====== 无访问权限跳转回登录页 =============

                    //DebugLogger.LogDebugMessage("无效的访问:" + tblRightMenu.MenuCode + "\r\n" + Request.Url.AbsoluteUri);

                    this.Response.Redirect("~/framework/admin/admin_go.aspx");  
                    return;
                }
            }

            base.OnLoad(e);
        }


        public static string ForceThemeUsing
        {
            get
            {
                string sTheme = (string)HttpContext.Current.Session["forceThemeMEFTMG"];
                if (sTheme == null)
                    return "";
                return sTheme;
            }

            set
            {
                HttpContext.Current.Session["forceThemeMEFTMG"] = value;
            }
        }

        #region NaviTab控制
        private NVTNaviTabController _naviTabController = null;
        /// <summary>
        /// 当前的用户
        /// </summary>
        public NVTNaviTabController NaviTabController
        {
            get
            {
                if (_naviTabController == null)
                {
                    _naviTabController = (NVTNaviTabController)Session["naviTabControllerMEFTMG"];
                    if (_naviTabController == null)
                    {
                        _naviTabController = new NVTNaviTabController();
                        Session["naviTabControllerMEFTMG"] = _naviTabController;
                    }
                }

                _naviTabController.PG__thisPage = this;

                return _naviTabController;
            }
        }

        Literal __divTabForHolderPlace = new Literal();

        protected override void OnInit(EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                Control oneControl = this.Controls[i];
                if (oneControl is HtmlForm)
                {
                    __divTabForHolderPlace.Text = "";
                    __divTabForHolderPlace.EnableViewState = false;
                    oneControl.Controls.AddAt(0, __divTabForHolderPlace);
                    break;
                }
            }
            base.OnInit(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            __divTabForHolderPlace.Text = this.NaviTabController.PG__ToHtmlString();

            if (__divTabForHolderPlace.Text != "")
            {
                HtmlLink link = new HtmlLink();
                link.Href = this.NaviTabController.BarStyleFilePath;
                link.Attributes.Add("type", "text/css");
                link.Attributes.Add("rel", "stylesheet");
                Page.Header.Controls.Add(link);
            }

            base.OnPreRender(e);
        }

        #endregion NaviTab控制

        /// <summary>
        /// 获得客户端访问IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            string sClientIP = "";
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null
                    && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    sClientIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else
                {
                    sClientIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                }
            }
            return sClientIP;
        }


        #region 页面数据保存

        /// <summary>
        /// 保存页面数据
        /// </summary>
        public void SavePageData()
        {
            PageParameter.SetCustomParamObject(this.Request.Url.AbsoluteUri, this);
        }

        /// <summary>
        /// 清除页面数据
        /// </summary>
        public void ClearPageData()
        {
            PageParameter.SetCustomParamObject(this.Request.Url.AbsoluteUri, null);
        }


        /// <summary>
        /// 加载页面数据
        /// </summary>
        /// <returns></returns>
        public bool LoadPageData()
        {
            string sKey = this.Request.Url.AbsoluteUri;

            SbtPageBase pgSave = PageParameter.GetCustomParamObject(sKey) as SbtPageBase;

            if (pgSave == null)
                return false;

            LoadControlsValue(pgSave.Form.Controls);

            //foreach (Control ctl in pgSave.Form.Controls)
            //{

            //    string sCtlTypeName = ctl.GetType().Name;

            //    switch (sCtlTypeName)
            //    {
            //        case "TextBox":
            //            TextBox edtSave = ctl as TextBox;
            //            TextBox edtCurrent = this.FindControl(ctl.ClientID) as TextBox;
            //            edtCurrent.Text = edtSave.Text;
            //            break;
            //        case "DropDownList":
            //            DropDownList ddlbSave = ctl as DropDownList;
            //            DropDownList ddlbCurrent = this.FindControl(ctl.ClientID) as DropDownList;
            //            ddlbCurrent.SelectedValue = ddlbSave.SelectedValue;
            //            break;
            //        case "Hidden":
            //            HiddenField hidSave = ctl as HiddenField;
            //            HiddenField hidCurrent = this.FindControl(ctl.ClientID) as HiddenField;
            //            hidCurrent.Value = hidSave.Value;
            //            break;
            //        case "CheckBox":
            //            CheckBox ckbSave = ctl as CheckBox;
            //            CheckBox ckbCurrent = this.FindControl(ctl.ClientID) as CheckBox;
            //            ckbCurrent.Checked = ckbSave.Checked;
            //            break;
            //        default:
            //            Control ctlCurrent = this.FindControl(ctl.ClientID);
            //            if (ctlCurrent == null)
            //                break;

            //            if (ctl.Controls.Count > 0)
            //            {
            //                foreach (Control ctlChild in ctl.Controls)
            //                {

            //                }
            //            }

            //            break;
            //    }
            //}

            return true;
        }


        public void LoadControlsValue(ControlCollection ctlColl)
        {
            foreach (Control ctl in ctlColl)
            {
                string sCtlTypeName = ctl.GetType().Name;

                switch (sCtlTypeName)
                {
                    case "TextBox":
                        TextBox edtSave = ctl as TextBox;
                        TextBox edtCurrent = this.FindControl(ctl.UniqueID) as TextBox;
                        edtCurrent.Text = edtSave.Text;
                        break;
                    case "DropDownList":
                        DropDownList ddlbSave = ctl as DropDownList;
                        DropDownList ddlbCurrent = this.FindControl(ctl.UniqueID) as DropDownList;
                        ddlbCurrent.SelectedValue = ddlbSave.SelectedValue;
                        break;
                    case "Hidden":
                        HiddenField hidSave = ctl as HiddenField;
                        HiddenField hidCurrent = this.FindControl(ctl.UniqueID) as HiddenField;
                        hidCurrent.Value = hidSave.Value;
                        break;
                    case "CheckBox":
                        CheckBox ckbSave = ctl as CheckBox;
                        CheckBox ckbCurrent = this.FindControl(ctl.UniqueID) as CheckBox;
                        ckbCurrent.Checked = ckbSave.Checked;
                        break;
                    default:
                        Control ctlCurrent = this.FindControl(ctl.UniqueID);
                        if (ctlCurrent == null)
                            break;

                        if (ctl.Controls.Count > 0)
                        {
                            LoadControlsValue(ctl.Controls);
                        }

                        break;
                }
            }
        }


        #endregion

    }
}
