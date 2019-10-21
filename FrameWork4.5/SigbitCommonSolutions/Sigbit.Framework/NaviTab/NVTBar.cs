using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Diagnostics;

using Sigbit.Common.WordProcess;
using System.Web;

namespace Sigbit.Framework.NaviTab
{
    /// <summary>
    /// Bar��ʽ
    /// </summary>
    public enum NVTBarStyle
    {
        /// <summary>
        /// Ĭ����ʽ
        /// </summary>
        Default,
        /// <summary>
        /// ��ɫ�黳
        /// </summary>
        BlueFeeling,
        /// <summary>
        /// ��ɫ����
        /// </summary>
        PinkLove,
        /// <summary>
        /// ��ɫ����
        /// </summary>
        GreenMood,
        /// <summary>
        /// ��ɫʱ��
        /// </summary>
        BlackFashion
    }

    class NVTBar
    {
        /// <summary>
        /// ��URL�Ĺ�ϣ
        /// </summary>
        private Hashtable _htURL = new Hashtable();

        /// <summary>
        /// ��˳���TAB��
        /// </summary>
        private ArrayList _arrItem = new ArrayList();

        private string _barName = "";
        /// <summary>
        /// Bar����
        /// </summary>
        public string BarName
        {
            get { return _barName; }
            set { _barName = value; }
        }

        private string _currentPageURL = "";
        /// <summary>
        /// ��ǰҳ�������
        /// </summary>
        public string CurrentPageURL
        {
            get { return _currentPageURL; }
            set { _currentPageURL = value; }
        }

        /// <summary>
        /// ����������ȡTabItem
        /// </summary>
        /// <param name="nIndex">����</param>
        /// <returns>TabItem</returns>
        public NVTTabItem GetTabItem(int nIndex)
        {
            return (NVTTabItem)_arrItem[nIndex];
        }

        /// <summary>
        /// �������ӻ�ȡTabItem
        /// </summary>
        /// <param name="sURL">����</param>
        /// <returns>TabItem</returns>
        public NVTTabItem GetTabItem(string sURL)
        {
            NVTTabItem tabItem = (NVTTabItem)_htURL[sURL];
            if (tabItem == null)
                throw new Exception("NVTBar.GetTabItem() : �Ҳ�����Ӧ��URL - " + sURL);
            return tabItem;
        }

        /// <summary>
        /// ��ȡTAB����
        /// </summary>
        /// <param name="sTabURL">TAB����</param>
        /// <returns>���ڷ������������򷵻�-1</returns>
        public int GetIndex(string sTabURL)
        {
            for (int i = 0; i < _arrItem.Count; i++)
            {
                NVTTabItem tabItem = GetTabItem(i);
                if (tabItem.TabURL == sTabURL)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// �����Ƿ��Ѿ�����
        /// </summary>
        /// <param name="sURL">����</param>
        /// <returns>�Ƿ��Ѿ�����</returns>
        public bool URLExists(string sURL)
        {
            if (_htURL[sURL] == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// ����һ��TAB�
        /// ������ӵ�TAB���TabCategory�Ѹ�ֵ���������ͬTabCategory��
        /// TAB����������λ�ü���TAB�����ʵ��Bar����ĳЩҳ��ֻ�ܳ���һ��TAB�Ĺ��ܡ�
        /// </summary>
        /// <param name="tabItemToBeAdded">TAB��</param>
        public void AddTabItem(NVTTabItem tabItemToBeAdded)
        {
            string sTabURL=tabItemToBeAdded.TabURL;

            //���Ѵ��ڣ������Tab����
            if (URLExists(sTabURL))
            {
                int nIndex=GetIndex(sTabURL);
                if (nIndex != -1)
                {
                    NVTTabItem tabItem = (NVTTabItem)_htURL[sTabURL];
                    tabItem.TabTitle = tabItemToBeAdded.TabTitle;

                    RemoveTabItemByIndex(nIndex, false);

                    _htURL[sTabURL] = tabItem;
                    _arrItem.Insert(nIndex, tabItem);
                }
                return;
            }

            int nAddPos = -1;
            if (tabItemToBeAdded.TabCategory != "")
                nAddPos = RemoveCategory(tabItemToBeAdded.TabCategory);

            if (nAddPos == -1)
            {
                _arrItem.Add(tabItemToBeAdded);
                _htURL[sTabURL] = tabItemToBeAdded;
            }
            else
            {
                _arrItem.Insert(nAddPos, tabItemToBeAdded);
                _htURL[sTabURL] = tabItemToBeAdded;
            }
        }

        /// <summary>
        /// ����TAB����ɾ��TabItem
        /// </summary>
        /// <param name="sTabCategory">TAB����</param>
        /// <returns>���ɾ����TabItem��λ��</returns>
        private int RemoveCategory(string sTabCategory)
        {
            Debug.Assert(sTabCategory != "");

            int nLastRemovePos = -1;

            for (int i = _arrItem.Count - 1; i >= 0; i--)
            {
                NVTTabItem item = GetTabItem(i);
                if (item.TabCategory == sTabCategory)
                {
                    _htURL.Remove(item.TabURL);
                    _arrItem.RemoveAt(i);

                    nLastRemovePos = i;
                }
            }

            return nLastRemovePos;
        }

        /// <summary>
        /// ��������ɾ��TabItem
        /// </summary>
        /// <param name="sURL">����</param>
        public void RemoveTabItemByURL(string sURL)
        {
            RemoveTabItemByURL(sURL,true);
        }

        /// <summary>
        /// ��������ɾ��TabItem
        /// </summary>
        /// <param name="sURL">����</param>
        /// <param name="IsDelSub">�Ƿ�ɾ����TabItem</param>
        public void RemoveTabItemByURL(string sURL,bool IsDelSub)
        {
            if (!URLExists(sURL))
            {
                return;
            }
            NVTTabItem tabItem = GetTabItem(sURL);

            _htURL.Remove(sURL);
            int nIndex = GetIndex(sURL);
            _arrItem.RemoveAt(nIndex);
            if (IsDelSub)
            {
                RemoveTabItemByParentURL(sURL);
            }
        }

        /// <summary>
        /// ��������ɾ��TabItem
        /// </summary>
        /// <param name="nIndex">����</param>
        public void RemoveTabItemByIndex(int nIndex)
        {
            RemoveTabItemByIndex(nIndex, true);
        }

        /// <summary>
        /// ��������ɾ��TabItem
        /// </summary>
        /// <param name="nIndex">����</param>
        /// <param name="IsDelSub">�Ƿ�ɾ����TabItem</param>
        public void RemoveTabItemByIndex(int nIndex, bool IsDelSub)
        {
            NVTTabItem tabItem = GetTabItem(nIndex);
            string sURL = tabItem.TabURL;

            _htURL.Remove(sURL);
            _arrItem.RemoveAt(nIndex);
            if (IsDelSub)
            {
                RemoveTabItemByParentURL(sURL);
            }
        }

        /// <summary>
        /// ��������ɾ��TabItem�����ص�ǰҳ��ַ
        /// </summary>
        /// <param name="nIndex">Ҫɾ����TabItem����</param>
        /// <returns>��ǰҳ��ַ</returns>
        public string RemoveAndReturn(int nIndex)
        {
            NVTTabItem tabItem = GetTabItem(nIndex);
            string sURL = tabItem.TabURL;

            _htURL.Remove(sURL);
            _arrItem.RemoveAt(nIndex);
            RemoveTabItemByParentURL(sURL);

            #region С��Ĵ���

            //if (sURL == CurrentPageURL)
            //{
            //    if (tabItem.ParentURL == "")
            //    {
            //        NVTTabItem tabItemNew = GetTabItem(0);
            //        CurrentPageURL = tabItemNew.TabURL;
            //    }
            //    else
            //    {
            //        CurrentPageURL = tabItem.ParentURL;
            //    }
            //}

            #endregion


            CurrentPageURL = GetTabItem(_arrItem.Count - 1).TabURL;    //Zick Update �����һ��TAB��չ��

            return CurrentPageURL;
        }

        /// <summary>
        /// ���ݸ�������ɾ��TabItem
        /// </summary>
        /// <param name="sURL">��������</param>
        public void RemoveTabItemByParentURL(string sURL)
        {
            if (sURL != "")
            {
                for (int i = 0; i < _arrItem.Count; i++)
                {
                    NVTTabItem tabItem = GetTabItem(i);
                    if (tabItem.ParentURL == sURL)
                    {
                        _arrItem.RemoveAt(i);
                        _htURL.Remove(tabItem.TabURL);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// ɾ�����пɹرյ�TAB
        /// </summary>
        public void RemoveAllTabs()
        {
            for (int i = _arrItem.Count - 1; i >= 0; i--)
            {
                NVTTabItem item = GetTabItem(i);
                if (item.CanClose)
                {
                    RemoveTabItemByIndex(i);
                }
            }
        }

        /// <summary>
        /// ɾ�����ӡ����ơ������а����趨��ֵ�Ŀɹرյ�TAB
        /// </summary>
        /// <param name="sRemoveKey">�趨��ֵ</param>
        public void RemoveTab(string sRemoveKey)
        {
            for (int i = _arrItem.Count - 1; i >= 0; i--)
            {
                NVTTabItem item = GetTabItem(i);
                if (item.CanClose)
                {
                    if (item.IsMatchKey(sRemoveKey))
                        RemoveTabItemByIndex(i);
                }
            }
        }

        /// <summary>
        /// ����HTML�ַ���
        /// </summary>
        /// <param name="sBarStyleDirPath">Bar��ʽĿ¼·��</param>
        /// <returns>���ɵ�HTML�ַ���</returns>
        public string ToHtmlString(string sBarStyleDirPath)
        {
            //===============1.�жϵ�ǰTAB�Ƿ�����===============
            if (!URLExists(this.CurrentPageURL))
                return "";

            //===============2.����Bar��ʽ===============
            StringBuilder sbRet = new StringBuilder();
            sbRet.Append("<table cellspacing='0' cellpadding='0' class='TableBar'><tr>");
            sbRet.Append("<td id='TDTabLeft' class='TDTabLeft'><img class='ImgTabLeft' src='" + sBarStyleDirPath + "images/tab_scrollleft.gif' onmousedown='StartScrollLeft()' onmouseup='EndTabScroll()' /></td>");
            sbRet.Append("<td><div id='DivNaviTab' class='DivTab'><table id='TableNaviTab' class='TableTab' cellspacing='0' cellpadding='0'><tr>");
            sbRet.Append("<td class='TDTabStart'></td>");

            //===============3.��ǰ����ͨTABģ��===============
            string sCurrTemplate = @"<td title=""{0}"" class=""CurrentTab"" align=""center""><span class='CurrentTabSpan'>{1}</span>{2}</td>";
            string sCommTemplate = @"<td title=""{0}"" class=""CommonTab"" align=""center"" onmouseover=""this.className='HoverTab'"" onmouseout=""this.className='CommonTab'"" ><a class='TabTitleA' href=""{1}"" target=""_self"">{2}</a>{3}</td>";

            //===============4.��ȡ����TAB===============
            int nCurrent = 0;
            for (int i = 0; i < _arrItem.Count; i++)
            {
                NVTTabItem tabItem = (NVTTabItem)_arrItem[i];

                //===============5.�ж�TAB�Ƿ����===============
                string sSourceTitle = tabItem.TabTitle;
                string sTitle = sSourceTitle;
                if (HZStringProc.HZByteLength(sTitle) > 12)
                {
                    sTitle = HZStringProc.SubstringBytes(sTitle, 0, 10) + "..";
                }

                //===============6.�ж�TAB�Ƿ�ɹر�===============
                string sCloseTemplate = "";
                if (tabItem.CanClose)
                {
                    sCloseTemplate = "<a href='../../framework/main/tab_delete.aspx?tab_index=" + i.ToString() + "&bar_name=" + this.BarName + "' class='TabClose'><img src='" + sBarStyleDirPath + "images/tab_close.gif' /></a>";
                }


                //===============7.�ж�ʹ���Ǹ�ģ��===============
                if (tabItem.TabURL == this.CurrentPageURL)
                {
                    sbRet.Append(string.Format(sCurrTemplate, sSourceTitle, sTitle, sCloseTemplate));
                    nCurrent = i;
                }
                else
                {
                    sbRet.Append(string.Format(sCommTemplate, sSourceTitle, tabItem.TabURL, sTitle, sCloseTemplate));
                }
            }

            //===============8.����Bar��ʽ===============
            sbRet.Append("<td class='TDTabEnd'></td></tr></table></div></td><td class='TDTabRight' id='TDTabRight'>");
            sbRet.Append("<img class='ImgTabLeft' src='" + sBarStyleDirPath + "images/tab_scrollright.gif'' onmousedown='StartScrollRight()' onmouseup='EndTabScroll()' /></td></tr></table>");
            sbRet.Append("<script language='javascript' type='text/javascript' src='../../module/navi_tab/styles/tab.js'></script>");
            sbRet.Append("<script language='javascript' type='text/javascript'>ShowTabScroll(" + nCurrent + ");</script>");

            //===============9.�������ɵ�HTML�ַ���===============
            return sbRet.ToString();
        }
    }
}
