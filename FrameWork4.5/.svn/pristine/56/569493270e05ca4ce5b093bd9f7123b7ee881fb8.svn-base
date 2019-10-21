using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;

using Sigbit.Common;

namespace Sigbit.Framework.NaviTab
{
    /// <summary>
    /// TabBar控制类
    /// </summary>
    public class NVTNaviTabController
    {
        #region 页面相关的属性和操作
        private SbtPageBase _PG__thisPage = null;
        /// <summary>
        /// 当前页面
        /// </summary>
        public SbtPageBase PG__thisPage
        {
            get { return _PG__thisPage; }
            set { _PG__thisPage = value; }
        }

        /// <summary>
        /// 生成HTML字符串
        /// </summary>
        /// <returns>HTML字符串</returns>
        public string PG__ToHtmlString()
        {
            CurrentBar.CurrentPageURL = PG__thisPage.Request.Url.PathAndQuery;
            return CurrentBar.ToHtmlString(BarStyleDirPath);
        }

        private NVTBarStyle _barStyle = NVTBarStyle.Default;
        /// <summary>
        /// Bar条样式
        /// </summary>
        public NVTBarStyle BarStyle
        {
            get { return _barStyle; }
            set { _barStyle = value; }
        }

        /// <summary>
        /// Bar条样式目录路径
        /// </summary>
        public string BarStyleDirPath
        {
            get
            {
                string sStyle = ConvertUtil.EnumToString(BarStyle);
                return "../../module/navi_tab/styles/" + sStyle + "/";
            }
        }

        /// <summary>
        /// Bar条样式文件路径
        /// </summary>
        public string BarStyleFilePath
        {
            get
            {
                string sStyle = ConvertUtil.EnumToString(BarStyle);
                return "~/module/navi_tab/styles/" + sStyle + "/tab.css";
            }
        }
        #endregion 页面相关的属性和操作

        #region 容器
        private NVTBarContainer _barContainer = null;
        /// <summary>
        /// Bar条的容器
        /// </summary>
        internal NVTBarContainer BarContainer
        {
            get
            {
                if (_barContainer == null)
                    _barContainer = new NVTBarContainer();
                return _barContainer;
            }
        }

        /// <summary>
        /// 当前的Bar条
        /// </summary>
        internal NVTBar CurrentBar
        {
            get
            {
                return this.BarContainer.CurrentBar;
            }
        }
        #endregion 容器

        #region 支持函数
        /// <summary>
        /// 得到一个相对地址的绝对地址
        /// </summary>
        /// <param name="sRelativeURL">相对地址</param>
        /// <returns>绝对地址</returns>
        /// <remarks>该绝对地址的得到是和当前页面相融合而得的</remarks>
        private string GetAbsoluteURL(string sRelativeURL)
        {
            string sRet = "";
            if (sRelativeURL != "")
            {
                Uri pathUri = new Uri(PG__thisPage.Request.Url, sRelativeURL);

                sRet = pathUri.PathAndQuery;
            }
            return sRet;
        }
        #endregion 支持函数

        #region 主页的TAB显示和操作
        /// <summary>
        /// 按本页面的标题显示TAB
        /// </summary>
        public void ShowTabBar()
        {
            ShowTabBar("");
        }

        /// <summary>
        /// 设置本页面的标题并显示TAB
        /// </summary>
        public void ShowTabBar(string sTitle)
        {
            if (sTitle == "")
                sTitle = PG__thisPage.Title;

            NVTTabItem tabItem = new NVTTabItem();
            tabItem.TabTitle = sTitle;
            tabItem.TabURL = PG__thisPage.Request.Url.PathAndQuery;
            tabItem.ParentURL = "";
            tabItem.CanClose = false;
            tabItem.TabCategory = "";

            this.CurrentBar.AddTabItem(tabItem);
        }

        /// <summary>
        /// 增加一个TAB（不能关的TAB）
        /// </summary>
        /// <param name="sURL">链接</param>
        /// <param name="sTitle">标题</param>
        public void AddTab(string sURL, string sTitle)
        {
            AddTab(sURL, sTitle, false, "","");
        }

        /// <summary>
        /// 增加一个TAB
        /// </summary>
        /// <param name="sURL">链接</param>
        /// <param name="sTitle">标题</param>
        /// <param name="IsCanClose">是否可关闭</param>
        public void AddTab(string sURL, string sTitle, bool IsCanClose)
        {
            AddTab(sURL, sTitle, IsCanClose,"","");
        }

        /// <summary>
        /// 增加一个TAB
        /// </summary>
        /// <param name="sURL">链接</param>
        /// <param name="sTitle">标题</param>
        /// <param name="IsCanClose">是否可关闭</param>
        /// <param name="sParentURL">父级链接</param>
        public void AddTab(string sURL, string sTitle, bool IsCanClose, string sParentURL)
        {
            AddTab(sURL, sTitle, IsCanClose, sParentURL,"");
        }

        /// <summary>
        /// 增加一个TAB
        /// </summary>
        /// <param name="sURL">链接</param>
        /// <param name="sTitle">标题</param>
        /// <param name="IsCanClose">是否可关闭</param>
        /// <param name="sParentURL">父级链接</param>
        /// <param name="sTabCategory">TAB类型</param>
        public void AddTab(string sURL, string sTitle, bool IsCanClose, string sParentURL,string sTabCategory)
        {
            string sAbsoluteURL = GetAbsoluteURL(sURL);

            NVTTabItem tabItem = new NVTTabItem();
            tabItem.TabTitle = sTitle;
            tabItem.TabURL = sAbsoluteURL;
            tabItem.ParentURL = GetAbsoluteURL(sParentURL);
            tabItem.CanClose = IsCanClose;
            tabItem.TabCategory = sTabCategory;

            this.CurrentBar.AddTabItem(tabItem);
        }

        /// <summary>
        /// 以TAB的形式打开一个新的页面(能关的TAB)
        /// </summary>
        /// <param name="sURL">链接</param>
        /// <param name="sTitle">标题</param>
        public void OpenInNewTab(string sURL, string sTitle)
        {
            OpenInNewTab(sURL, sTitle, "","");
        }

        /// <summary>
        /// 以TAB的形式打开一个新的页面(能关的TAB)
        /// </summary>
        /// <param name="sURL">链接</param>
        /// <param name="sTitle">标题</param>
        /// <param name="sParentURL">父级链接</param>
        public void OpenInNewTab(string sURL, string sTitle, string sParentURL)
        {
            OpenInNewTab(sURL, sTitle, sParentURL,"");
        }

        /// <summary>
        /// 以TAB的形式打开一个新的页面(能关的TAB)
        /// </summary>
        /// <param name="sURL">链接</param>
        /// <param name="sTitle">标题</param>
        /// <param name="sParentURL">父级链接</param>
        /// <param name="sTabCategory">TAB类型</param>
        public void OpenInNewTab(string sURL, string sTitle, string sParentURL, string sTabCategory)
        {
            string sAbsoluteURL = GetAbsoluteURL(sURL);

            NVTTabItem tabItem = new NVTTabItem();
            tabItem.TabTitle = sTitle;
            tabItem.TabURL = sAbsoluteURL;
            tabItem.ParentURL = GetAbsoluteURL(sParentURL);
            tabItem.CanClose = true;
            tabItem.TabCategory = sTabCategory;

            this.CurrentBar.AddTabItem(tabItem);
            PG__thisPage.Response.Redirect(sURL);
        }

        /// <summary>
        /// 删除所有可关闭的TAB
        /// </summary>
        public void RemoveAllTabs()
        {
            this.CurrentBar.RemoveAllTabs();
        }

        /// <summary>
        /// 删除链接、名称、类型中包含设定键值的可关闭的TAB
        /// </summary>
        /// <param name="sRemoveKey">设定键值</param>
        public void RemoveTab(string sRemoveKey)
        {
            this.CurrentBar.RemoveTab(sRemoveKey);
        }

        /// <summary>
        /// 根据索引删除TabItem并返回当前页地址
        /// </summary>
        /// <param name="nIndex">索引</param>
        public string RemoveAndReturn(int nIndex)
        {
            return this.CurrentBar.RemoveAndReturn(nIndex);
        }
        #endregion 主页的TAB显示和操作

        #region 新页面的TAB操作
        /// <summary>
        /// 将本页显示在Bar条中
        /// </summary>
        public void AppendSelfToBar()
        {
            AppendSelfToBar("", "","");
        }

        /// <summary>
        /// 将本页显示在Bar条中
        /// </summary>
        /// <param name="sTitle">指定标题</param>
        public void AppendSelfToBar(string sTitle)
        {
            AppendSelfToBar(sTitle, "","");
        }

        /// <summary>
        /// 将本页显示在Bar条中
        /// </summary>
        /// <param name="sTitle">指定标题</param>
        /// <param name="sParentURL">父级链接</param>
        public void AppendSelfToBar(string sTitle, string sParentURL)
        {
            AppendSelfToBar(sTitle, sParentURL, "");
        }

        /// <summary>
        /// 将本页显示在Bar条中
        /// </summary>
        /// <param name="sTitle">指定标题</param>
        /// <param name="sParentURL">父级链接</param>
        /// <param name="sTabCategory">TAB类型</param>
        public void AppendSelfToBar(string sTitle, string sParentURL, string sTabCategory)
        {
            if (sTitle == "")
                sTitle = PG__thisPage.Title;

            NVTTabItem tabItem = new NVTTabItem();
            tabItem.TabTitle = sTitle;
            tabItem.TabURL = PG__thisPage.Request.Url.PathAndQuery;
            tabItem.ParentURL = GetAbsoluteURL(sParentURL);
            tabItem.CanClose = true;
            tabItem.TabCategory = sTabCategory;

            this.CurrentBar.AddTabItem(tabItem);
        }

        /// <summary>
        /// 从Bar条中删除自己并跳转到相应页面
        /// </summary>
        public void RemoveSelfFromBar()
        {
            RemoveSelfFromBar(true);
        }

        /// <summary>
        /// 从Bar条中删除自己
        /// </summary>
        /// <param name="bRedirect">是否跳转到相应页面</param>
        public void RemoveSelfFromBar(bool bRedirect)
        {
            string sDelURL = this.PG__thisPage.Request.Url.PathAndQuery;
            if (bRedirect)
            {
                int nIndex = this.CurrentBar.GetIndex(sDelURL);

                //【2014.10.16 Zick Update】
                if (nIndex == -1)    
                    return;
                string sURL = RemoveAndReturn(nIndex);
                this.PG__thisPage.Response.Redirect(sURL);
            }
            else
            {
                this.CurrentBar.RemoveTabItemByURL(sDelURL);
            }
        }

        /// <summary>
        /// 设置并跳转到当前页面
        /// </summary>
        /// <param name="sURL">当前页面的链接</param>
        public void SetCurrentPage(string sURL)
        {
            sURL = GetAbsoluteURL(sURL);
            if (this.CurrentBar.URLExists(sURL))
            {
                this.CurrentBar.CurrentPageURL = sURL;
                this.PG__thisPage.Response.Redirect(sURL);
            }
        }
        #endregion 新页面的TAB操作

        #region Bar条操作
        /// <summary>
        /// 切换到特定的Bar条
        /// </summary>
        /// <param name="sBarName">Bar条的名称</param>
        public void SwitchToBar(string sBarName)
        {
            this.BarContainer.CurrentBarName = sBarName;
        }

        /// <summary>
        /// 切换到特定的Bar条
        /// </summary>
        public void SwitchToBar()
        {
            SwitchToBar("");
        }

        /// <summary>
        /// 清空Bar条
        /// </summary>
        /// <param name="sBarName">Bar条的名称</param>
        public void ClearBar(string sBarName)
        {
            this.BarContainer.ClearBar(sBarName);
        }

        /// <summary>
        /// 清空Bar条
        /// </summary>
        public void ClearBar()
        {
            ClearBar("");
        }

        #endregion Bar条操作
    }
}
