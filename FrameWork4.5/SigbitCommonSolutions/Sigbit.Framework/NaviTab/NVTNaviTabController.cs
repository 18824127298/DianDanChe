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
    /// TabBar������
    /// </summary>
    public class NVTNaviTabController
    {
        #region ҳ����ص����ԺͲ���
        private SbtPageBase _PG__thisPage = null;
        /// <summary>
        /// ��ǰҳ��
        /// </summary>
        public SbtPageBase PG__thisPage
        {
            get { return _PG__thisPage; }
            set { _PG__thisPage = value; }
        }

        /// <summary>
        /// ����HTML�ַ���
        /// </summary>
        /// <returns>HTML�ַ���</returns>
        public string PG__ToHtmlString()
        {
            CurrentBar.CurrentPageURL = PG__thisPage.Request.Url.PathAndQuery;
            return CurrentBar.ToHtmlString(BarStyleDirPath);
        }

        private NVTBarStyle _barStyle = NVTBarStyle.Default;
        /// <summary>
        /// Bar����ʽ
        /// </summary>
        public NVTBarStyle BarStyle
        {
            get { return _barStyle; }
            set { _barStyle = value; }
        }

        /// <summary>
        /// Bar����ʽĿ¼·��
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
        /// Bar����ʽ�ļ�·��
        /// </summary>
        public string BarStyleFilePath
        {
            get
            {
                string sStyle = ConvertUtil.EnumToString(BarStyle);
                return "~/module/navi_tab/styles/" + sStyle + "/tab.css";
            }
        }
        #endregion ҳ����ص����ԺͲ���

        #region ����
        private NVTBarContainer _barContainer = null;
        /// <summary>
        /// Bar��������
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
        /// ��ǰ��Bar��
        /// </summary>
        internal NVTBar CurrentBar
        {
            get
            {
                return this.BarContainer.CurrentBar;
            }
        }
        #endregion ����

        #region ֧�ֺ���
        /// <summary>
        /// �õ�һ����Ե�ַ�ľ��Ե�ַ
        /// </summary>
        /// <param name="sRelativeURL">��Ե�ַ</param>
        /// <returns>���Ե�ַ</returns>
        /// <remarks>�þ��Ե�ַ�ĵõ��Ǻ͵�ǰҳ�����ں϶��õ�</remarks>
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
        #endregion ֧�ֺ���

        #region ��ҳ��TAB��ʾ�Ͳ���
        /// <summary>
        /// ����ҳ��ı�����ʾTAB
        /// </summary>
        public void ShowTabBar()
        {
            ShowTabBar("");
        }

        /// <summary>
        /// ���ñ�ҳ��ı��Ⲣ��ʾTAB
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
        /// ����һ��TAB�����ܹص�TAB��
        /// </summary>
        /// <param name="sURL">����</param>
        /// <param name="sTitle">����</param>
        public void AddTab(string sURL, string sTitle)
        {
            AddTab(sURL, sTitle, false, "","");
        }

        /// <summary>
        /// ����һ��TAB
        /// </summary>
        /// <param name="sURL">����</param>
        /// <param name="sTitle">����</param>
        /// <param name="IsCanClose">�Ƿ�ɹر�</param>
        public void AddTab(string sURL, string sTitle, bool IsCanClose)
        {
            AddTab(sURL, sTitle, IsCanClose,"","");
        }

        /// <summary>
        /// ����һ��TAB
        /// </summary>
        /// <param name="sURL">����</param>
        /// <param name="sTitle">����</param>
        /// <param name="IsCanClose">�Ƿ�ɹر�</param>
        /// <param name="sParentURL">��������</param>
        public void AddTab(string sURL, string sTitle, bool IsCanClose, string sParentURL)
        {
            AddTab(sURL, sTitle, IsCanClose, sParentURL,"");
        }

        /// <summary>
        /// ����һ��TAB
        /// </summary>
        /// <param name="sURL">����</param>
        /// <param name="sTitle">����</param>
        /// <param name="IsCanClose">�Ƿ�ɹر�</param>
        /// <param name="sParentURL">��������</param>
        /// <param name="sTabCategory">TAB����</param>
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
        /// ��TAB����ʽ��һ���µ�ҳ��(�ܹص�TAB)
        /// </summary>
        /// <param name="sURL">����</param>
        /// <param name="sTitle">����</param>
        public void OpenInNewTab(string sURL, string sTitle)
        {
            OpenInNewTab(sURL, sTitle, "","");
        }

        /// <summary>
        /// ��TAB����ʽ��һ���µ�ҳ��(�ܹص�TAB)
        /// </summary>
        /// <param name="sURL">����</param>
        /// <param name="sTitle">����</param>
        /// <param name="sParentURL">��������</param>
        public void OpenInNewTab(string sURL, string sTitle, string sParentURL)
        {
            OpenInNewTab(sURL, sTitle, sParentURL,"");
        }

        /// <summary>
        /// ��TAB����ʽ��һ���µ�ҳ��(�ܹص�TAB)
        /// </summary>
        /// <param name="sURL">����</param>
        /// <param name="sTitle">����</param>
        /// <param name="sParentURL">��������</param>
        /// <param name="sTabCategory">TAB����</param>
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
        /// ɾ�����пɹرյ�TAB
        /// </summary>
        public void RemoveAllTabs()
        {
            this.CurrentBar.RemoveAllTabs();
        }

        /// <summary>
        /// ɾ�����ӡ����ơ������а����趨��ֵ�Ŀɹرյ�TAB
        /// </summary>
        /// <param name="sRemoveKey">�趨��ֵ</param>
        public void RemoveTab(string sRemoveKey)
        {
            this.CurrentBar.RemoveTab(sRemoveKey);
        }

        /// <summary>
        /// ��������ɾ��TabItem�����ص�ǰҳ��ַ
        /// </summary>
        /// <param name="nIndex">����</param>
        public string RemoveAndReturn(int nIndex)
        {
            return this.CurrentBar.RemoveAndReturn(nIndex);
        }
        #endregion ��ҳ��TAB��ʾ�Ͳ���

        #region ��ҳ���TAB����
        /// <summary>
        /// ����ҳ��ʾ��Bar����
        /// </summary>
        public void AppendSelfToBar()
        {
            AppendSelfToBar("", "","");
        }

        /// <summary>
        /// ����ҳ��ʾ��Bar����
        /// </summary>
        /// <param name="sTitle">ָ������</param>
        public void AppendSelfToBar(string sTitle)
        {
            AppendSelfToBar(sTitle, "","");
        }

        /// <summary>
        /// ����ҳ��ʾ��Bar����
        /// </summary>
        /// <param name="sTitle">ָ������</param>
        /// <param name="sParentURL">��������</param>
        public void AppendSelfToBar(string sTitle, string sParentURL)
        {
            AppendSelfToBar(sTitle, sParentURL, "");
        }

        /// <summary>
        /// ����ҳ��ʾ��Bar����
        /// </summary>
        /// <param name="sTitle">ָ������</param>
        /// <param name="sParentURL">��������</param>
        /// <param name="sTabCategory">TAB����</param>
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
        /// ��Bar����ɾ���Լ�����ת����Ӧҳ��
        /// </summary>
        public void RemoveSelfFromBar()
        {
            RemoveSelfFromBar(true);
        }

        /// <summary>
        /// ��Bar����ɾ���Լ�
        /// </summary>
        /// <param name="bRedirect">�Ƿ���ת����Ӧҳ��</param>
        public void RemoveSelfFromBar(bool bRedirect)
        {
            string sDelURL = this.PG__thisPage.Request.Url.PathAndQuery;
            if (bRedirect)
            {
                int nIndex = this.CurrentBar.GetIndex(sDelURL);

                //��2014.10.16 Zick Update��
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
        /// ���ò���ת����ǰҳ��
        /// </summary>
        /// <param name="sURL">��ǰҳ�������</param>
        public void SetCurrentPage(string sURL)
        {
            sURL = GetAbsoluteURL(sURL);
            if (this.CurrentBar.URLExists(sURL))
            {
                this.CurrentBar.CurrentPageURL = sURL;
                this.PG__thisPage.Response.Redirect(sURL);
            }
        }
        #endregion ��ҳ���TAB����

        #region Bar������
        /// <summary>
        /// �л����ض���Bar��
        /// </summary>
        /// <param name="sBarName">Bar��������</param>
        public void SwitchToBar(string sBarName)
        {
            this.BarContainer.CurrentBarName = sBarName;
        }

        /// <summary>
        /// �л����ض���Bar��
        /// </summary>
        public void SwitchToBar()
        {
            SwitchToBar("");
        }

        /// <summary>
        /// ���Bar��
        /// </summary>
        /// <param name="sBarName">Bar��������</param>
        public void ClearBar(string sBarName)
        {
            this.BarContainer.ClearBar(sBarName);
        }

        /// <summary>
        /// ���Bar��
        /// </summary>
        public void ClearBar()
        {
            ClearBar("");
        }

        #endregion Bar������
    }
}
