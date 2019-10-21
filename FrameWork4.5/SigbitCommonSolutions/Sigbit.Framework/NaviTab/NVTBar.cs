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
    /// Bar样式
    /// </summary>
    public enum NVTBarStyle
    {
        /// <summary>
        /// 默认样式
        /// </summary>
        Default,
        /// <summary>
        /// 蓝色情怀
        /// </summary>
        BlueFeeling,
        /// <summary>
        /// 粉色爱恋
        /// </summary>
        PinkLove,
        /// <summary>
        /// 绿色心情
        /// </summary>
        GreenMood,
        /// <summary>
        /// 黑色时尚
        /// </summary>
        BlackFashion
    }

    class NVTBar
    {
        /// <summary>
        /// 按URL的哈希
        /// </summary>
        private Hashtable _htURL = new Hashtable();

        /// <summary>
        /// 按顺序的TAB项
        /// </summary>
        private ArrayList _arrItem = new ArrayList();

        private string _barName = "";
        /// <summary>
        /// Bar名称
        /// </summary>
        public string BarName
        {
            get { return _barName; }
            set { _barName = value; }
        }

        private string _currentPageURL = "";
        /// <summary>
        /// 当前页面的链接
        /// </summary>
        public string CurrentPageURL
        {
            get { return _currentPageURL; }
            set { _currentPageURL = value; }
        }

        /// <summary>
        /// 根据索引获取TabItem
        /// </summary>
        /// <param name="nIndex">索引</param>
        /// <returns>TabItem</returns>
        public NVTTabItem GetTabItem(int nIndex)
        {
            return (NVTTabItem)_arrItem[nIndex];
        }

        /// <summary>
        /// 根据链接获取TabItem
        /// </summary>
        /// <param name="sURL">链接</param>
        /// <returns>TabItem</returns>
        public NVTTabItem GetTabItem(string sURL)
        {
            NVTTabItem tabItem = (NVTTabItem)_htURL[sURL];
            if (tabItem == null)
                throw new Exception("NVTBar.GetTabItem() : 找不到对应的URL - " + sURL);
            return tabItem;
        }

        /// <summary>
        /// 获取TAB索引
        /// </summary>
        /// <param name="sTabURL">TAB链接</param>
        /// <returns>存在返回索引，否则返回-1</returns>
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
        /// 链接是否已经存在
        /// </summary>
        /// <param name="sURL">链接</param>
        /// <returns>是否已经存在</returns>
        public bool URLExists(string sURL)
        {
            if (_htURL[sURL] == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 增加一个TAB项，
        /// 如果增加的TAB项的TabCategory已赋值，则清除相同TabCategory的
        /// TAB项，并在清除的位置加入TAB项，用于实现Bar条中某些页面只能出现一个TAB的功能。
        /// </summary>
        /// <param name="tabItemToBeAdded">TAB项</param>
        public void AddTabItem(NVTTabItem tabItemToBeAdded)
        {
            string sTabURL=tabItemToBeAdded.TabURL;

            //如已存在，则更新Tab标题
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
        /// 根据TAB类型删除TabItem
        /// </summary>
        /// <param name="sTabCategory">TAB类型</param>
        /// <returns>最后删除的TabItem的位置</returns>
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
        /// 根据链接删除TabItem
        /// </summary>
        /// <param name="sURL">链接</param>
        public void RemoveTabItemByURL(string sURL)
        {
            RemoveTabItemByURL(sURL,true);
        }

        /// <summary>
        /// 根据链接删除TabItem
        /// </summary>
        /// <param name="sURL">链接</param>
        /// <param name="IsDelSub">是否删除子TabItem</param>
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
        /// 根据索引删除TabItem
        /// </summary>
        /// <param name="nIndex">索引</param>
        public void RemoveTabItemByIndex(int nIndex)
        {
            RemoveTabItemByIndex(nIndex, true);
        }

        /// <summary>
        /// 根据索引删除TabItem
        /// </summary>
        /// <param name="nIndex">索引</param>
        /// <param name="IsDelSub">是否删除子TabItem</param>
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
        /// 根据索引删除TabItem并返回当前页地址
        /// </summary>
        /// <param name="nIndex">要删除的TabItem索引</param>
        /// <returns>当前页地址</returns>
        public string RemoveAndReturn(int nIndex)
        {
            NVTTabItem tabItem = GetTabItem(nIndex);
            string sURL = tabItem.TabURL;

            _htURL.Remove(sURL);
            _arrItem.RemoveAt(nIndex);
            RemoveTabItemByParentURL(sURL);

            #region 小李的代码

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


            CurrentPageURL = GetTabItem(_arrItem.Count - 1).TabURL;    //Zick Update 按最后一个TAB来展现

            return CurrentPageURL;
        }

        /// <summary>
        /// 根据父级链接删除TabItem
        /// </summary>
        /// <param name="sURL">父级链接</param>
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
        /// 删除所有可关闭的TAB
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
        /// 删除链接、名称、类型中包含设定键值的可关闭的TAB
        /// </summary>
        /// <param name="sRemoveKey">设定键值</param>
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
        /// 生成HTML字符串
        /// </summary>
        /// <param name="sBarStyleDirPath">Bar样式目录路径</param>
        /// <returns>生成的HTML字符串</returns>
        public string ToHtmlString(string sBarStyleDirPath)
        {
            //===============1.判断当前TAB是否设置===============
            if (!URLExists(this.CurrentPageURL))
                return "";

            //===============2.设置Bar样式===============
            StringBuilder sbRet = new StringBuilder();
            sbRet.Append("<table cellspacing='0' cellpadding='0' class='TableBar'><tr>");
            sbRet.Append("<td id='TDTabLeft' class='TDTabLeft'><img class='ImgTabLeft' src='" + sBarStyleDirPath + "images/tab_scrollleft.gif' onmousedown='StartScrollLeft()' onmouseup='EndTabScroll()' /></td>");
            sbRet.Append("<td><div id='DivNaviTab' class='DivTab'><table id='TableNaviTab' class='TableTab' cellspacing='0' cellpadding='0'><tr>");
            sbRet.Append("<td class='TDTabStart'></td>");

            //===============3.当前和普通TAB模板===============
            string sCurrTemplate = @"<td title=""{0}"" class=""CurrentTab"" align=""center""><span class='CurrentTabSpan'>{1}</span>{2}</td>";
            string sCommTemplate = @"<td title=""{0}"" class=""CommonTab"" align=""center"" onmouseover=""this.className='HoverTab'"" onmouseout=""this.className='CommonTab'"" ><a class='TabTitleA' href=""{1}"" target=""_self"">{2}</a>{3}</td>";

            //===============4.获取所有TAB===============
            int nCurrent = 0;
            for (int i = 0; i < _arrItem.Count; i++)
            {
                NVTTabItem tabItem = (NVTTabItem)_arrItem[i];

                //===============5.判断TAB是否过长===============
                string sSourceTitle = tabItem.TabTitle;
                string sTitle = sSourceTitle;
                if (HZStringProc.HZByteLength(sTitle) > 12)
                {
                    sTitle = HZStringProc.SubstringBytes(sTitle, 0, 10) + "..";
                }

                //===============6.判断TAB是否可关闭===============
                string sCloseTemplate = "";
                if (tabItem.CanClose)
                {
                    sCloseTemplate = "<a href='../../framework/main/tab_delete.aspx?tab_index=" + i.ToString() + "&bar_name=" + this.BarName + "' class='TabClose'><img src='" + sBarStyleDirPath + "images/tab_close.gif' /></a>";
                }


                //===============7.判断使用那个模板===============
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

            //===============8.设置Bar样式===============
            sbRet.Append("<td class='TDTabEnd'></td></tr></table></div></td><td class='TDTabRight' id='TDTabRight'>");
            sbRet.Append("<img class='ImgTabLeft' src='" + sBarStyleDirPath + "images/tab_scrollright.gif'' onmousedown='StartScrollRight()' onmouseup='EndTabScroll()' /></td></tr></table>");
            sbRet.Append("<script language='javascript' type='text/javascript' src='../../module/navi_tab/styles/tab.js'></script>");
            sbRet.Append("<script language='javascript' type='text/javascript'>ShowTabScroll(" + nCurrent + ");</script>");

            //===============9.返回生成的HTML字符串===============
            return sbRet.ToString();
        }
    }
}
