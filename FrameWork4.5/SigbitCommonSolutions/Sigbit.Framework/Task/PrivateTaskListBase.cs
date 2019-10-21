using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Sigbit.Framework.Task
{
    /// <summary>
    /// 装载多个任务类（Task）的集合类。
    /// </summary>
    public class PrivateTaskListBase : ArrayList
    {
        class PrivateTaskListConst
        {
            public const string SHOW_ICON = "[%SHOW_ICON%]";

            public const string SHOW_TITLE = "[%SHOW_TITLE%]";

            public const string SHOW_CONTENT = "[%SHOW_CONTENT%]";

            public const string LINK_URL = "[%LINK_URL%]";
        }



        private string _showIcon = "";
        /// <summary>
        /// 显示的图标
        /// </summary>
        public string ShowIcon
        {
            get { return _showIcon; }
            set { _showIcon = value; }
        }


        private string _showTitle = "";
        /// <summary>
        /// 显示的标题
        /// </summary>
        public string ShowTitle
        {
            get { return _showTitle; }
            set { _showTitle = value; }
        }




        private string _linkUrl = "";
        /// <summary>
        /// 链接
        /// </summary>
        public string LinkUrl
        {
            get { return _linkUrl; }
            set { _linkUrl = value; }
        }


        private string _showContent = "";
        /// <summary>
        /// 显示内容
        /// </summary>
        public string ShowContent
        {
            get { return _showContent; }
            set { _showContent = value; }
        }


        private ArrayList _taskList = new ArrayList();
        /// <summary>
        /// 任务列表
        /// </summary>
        public ArrayList TaskList
        {
            get { return _taskList; }
            set { _taskList = value; }
        }

        /// <summary>
        /// 保存任务列表到文件中
        /// </summary>
        /// <param name="sFileName">文件名称</param>
        public void DumpToFile(string sFileName)
        {
            if (sFileName != "")
            {

            }
        }

        /// <summary>
        /// 将其它任务列表合并进来
        /// </summary>
        /// <param name="sTaskList">要合并的任务列表</param>
        public void MergeTaskList(ArrayList sTaskList)
        {
            for (int i = 0; i < sTaskList.Count; i++)
            {
                if (!TaskList.Contains((PrivateTask)sTaskList[i]))
                {
                    TaskList.Add((PrivateTask)sTaskList[i]);
                }
            }
        }


        private string _showTemplate = @"
<div style='position: relative; margin-bottom: 10px;'>
	<table cellspacing='1' class='contentTable' width='100%'>
   		<thead>
        	<tr>
            	<td nowrap='nowrap' height='22px'>
                	<img src='[%SHOW_ICON%]' style='float:left' />
                	<span style='float:left'>&nbsp;[%SHOW_TITLE%]</span>
                    [%LINK_URL%]
                        <img style='float: right;border:0px' src='../../../images/arrow_icon/green_arrow.gif' alt='' border='0' />
                    </a>&nbsp;
                </td>
            </tr>
        </thead>
       	<tbody>
            <tr>
              	<td style='height: 50px;padding:10px'>
                    [%SHOW_CONTENT%]
                </td>
            </tr>
        </tbody>
    </table>
</div>";

        /// <summary>
        /// 显示模板
        /// </summary>
        protected string ShowTempalte
        {
            get
            {
                return _showTemplate;
            }
            set
            {
                _showTemplate = value;
            }
        }


        /// <summary>
        /// 得到显示到页面所需的HTML字符串
        /// </summary>
        /// <returns>HTML字符串</returns>
        public virtual string ToDisplayHTML()
        {

            string sRetHTML = this.ShowTempalte;

            string sLinkUrl = "";
            if (this.LinkUrl != "")
                sLinkUrl = "<a href='" + this.LinkUrl + "' />";
            sRetHTML = sRetHTML.Replace(PrivateTaskListConst.LINK_URL, sLinkUrl);

            sRetHTML = sRetHTML.Replace(PrivateTaskListConst.SHOW_CONTENT, this.ShowContent);

            sRetHTML = sRetHTML.Replace(PrivateTaskListConst.SHOW_ICON, this.ShowIcon);

            sRetHTML = sRetHTML.Replace(PrivateTaskListConst.SHOW_TITLE, this.ShowTitle);

            return sRetHTML;
        }

        /// <summary>
        ///  按时间、紧迫度等重新排序，用于显示
        /// </summary>
        public void Reorder()
        {

        }
    }
}
