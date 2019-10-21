using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Sigbit.Data;

namespace Sigbit.Web.WebControlUtil
{
    public class WCUDataGrid
    {
        /// <summary>
        /// 返回指定DataGrid选中的编号集合
        /// </summary>
        /// <param name="dg"></param>
        /// <param name="CheckBoxName">CheckBox的名字</param>
        /// <param name="IDIndex">编号字段在Grid中的序号</param>
        /// <returns></returns>
        public static ArrayList GetDataGridSelectID(DataGrid dg, string CheckBoxName, int IDIndex)
        {
            ArrayList Result = new ArrayList();
            //string id = "";
            foreach (DataGridItem li in dg.Items)
            {
                if (((CheckBox)li.FindControl(CheckBoxName)).Checked)
                {
                    Result.Add(li.Cells[0].Text.ToString());
                }
            }
            return Result;
        }

        /// <summary>
        /// 返回指定DataGrid选中的编号集合
        /// </summary>
        /// <param name="dg">DataGrid</param>
        /// <returns></returns>
        public static ArrayList GetDataGridSelectID(DataGrid dg)
        {
            return GetDataGridSelectID(dg, "CheckBoxId", 0);
        }

        /// <summary>
        /// 获取DataGrid中的排序语句
        /// </summary>
        /// <param name="DefaultSortField"></param>
        /// <returns></returns>
        public static string DataGridGetOrderSQL(StateBag ViewState, string CurrentSortField, string DefaultSortField)
        {
            string sSQL;
            if (ViewState["strSort"] == null)
            {
                ViewState["strSort"] = DefaultSortField;
                ViewState["SortOrder"] = "0";
            }
            else
            {

                if (CurrentSortField == (string)ViewState["strSort"])
                {
                    if ((string)ViewState["SortOrder"] == "0")
                    {
                        ViewState["SortOrder"] = "1";
                    }
                    else
                    {
                        ViewState["SortOrder"] = "0";
                    }
                }
                else
                {
                    ViewState["strSort"] = CurrentSortField;
                    ViewState["SortOrder"] = "0";
                }
            }

            sSQL = "order by " + CurrentSortField;
            if ((string)ViewState["SortOrder"] == "0")
            {
                sSQL = sSQL + " ASC";
            }
            else
            {
                sSQL = sSQL + " DESC";
            }
            return sSQL;
        }
    }
}
