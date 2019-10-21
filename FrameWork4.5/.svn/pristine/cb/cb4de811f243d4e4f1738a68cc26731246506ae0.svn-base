using System;
using System.Collections.Generic;
using System.Text;

using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sigbit.Framework
{
    /// <summary>
    /// 保存数据页
    /// </summary>
    public class SbtSavePage
    {
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <returns></returns>
        public bool LoadData(SbtPageBase curPage)
        {
            string sKey = curPage.Request.Url.AbsoluteUri;

            SbtPageBase pgSave = curPage.PageParameter.GetCustomParamObject(sKey) as SbtPageBase;

            if (pgSave == null)
                return false;


            foreach (Control ctl in pgSave.Form.Controls)
            {
                switch (ctl.GetType().Name)
                {
                    case "TextBox":
                        TextBox edtSave = ctl as TextBox;
                        TextBox edtCurrent = curPage.FindControl(ctl.ClientID) as TextBox;
                        edtCurrent.Text = edtSave.Text;
                        break;
                    case "DropDownList":
                        DropDownList ddlbSave = ctl as DropDownList;
                        DropDownList ddlbCurrent = curPage.FindControl(ctl.ClientID) as DropDownList;
                        ddlbCurrent.SelectedValue = ddlbSave.SelectedValue;
                        break;
                    case "Hidden":
                        HiddenField hidSave = ctl as HiddenField;
                        HiddenField hidCurrent = curPage.FindControl(ctl.ClientID) as HiddenField;
                        hidCurrent.Value = hidSave.Value;
                        break;
                    default:
                        break;
                }
            }

            return true;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public void SaveData(SbtPageBase curPage)
        {
            curPage.PageParameter.SetCustomParamObject(curPage.Request.Url.AbsoluteUri, curPage);
        }

        /// <summary>
        /// 清除数据
        /// </summary>
        public void ClearData(SbtPageBase curPage)
        {
            curPage.PageParameter.SetCustomParamObject(curPage.Request.Url.AbsoluteUri, null);
        }
    }
}
