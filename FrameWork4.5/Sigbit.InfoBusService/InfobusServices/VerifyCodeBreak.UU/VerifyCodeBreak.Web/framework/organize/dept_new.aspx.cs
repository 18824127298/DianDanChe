using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Sigbit.Common;
using Sigbit.Framework;

public partial class framework_organize_dept_new : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        string sDeptId = ConvertUtil.ToString(Request["dpt_uid"]);
        if (sDeptId == "")
        {
            lblDeptName.Text = "无";
        }
        else
        {
            TbUserDept tblDept = new TbUserDept();
            tblDept.DeptId = sDeptId;
            tblDept.Fetch();

            lblDeptName.Text = tblDept.DeptName;
        }
        edtDeptName.Text = "";
        edtListOrder.Text = "100";
        edtRemarks.Text = "";
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. 数据校验 ===========
        //========== 1.1 名称 =============
        string sDeptName = edtDeptName.Text.Trim().Replace("/", "");
        if (sDeptName == "")
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "请指定部门/成员单位名称";
            edtDeptName.Focus();
            return;
        }

        //=========== 1.2 显示顺序 ============
        int nListOrder = ConvertUtil.ToInt(edtListOrder.Text, 0);
        if (nListOrder < 0)
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "显示顺序不能为负数";
            edtListOrder.Focus();
            return;
        }

        //======== 2. 数据更新 ===============
        string sParentDeptId = ConvertUtil.ToString(Request["dpt_uid"]);

        TbUserDept tblParent = new TbUserDept();
        tblParent.DeptId = sParentDeptId;
        if (tblParent.DeptId == "")
        {
            tblParent.DeptLevel = 0;
            tblParent.FullDeptName = "";
            tblParent.DeptName = "";
        }
        else
            tblParent.Fetch();

        TbUserDept tblDept = new TbUserDept();
        tblDept.DeptId = tblParent.DeptId + RandUtil.NewString(3, RandStringType.Lower);
        tblDept.DeptName = sDeptName;
        tblDept.ThirdParyCode = "";
        if (tblParent.FullDeptName == "")
            tblDept.FullDeptName = sDeptName;
        else
            tblDept.FullDeptName = tblParent.FullDeptName + "/" + sDeptName;
        tblDept.DeptLevel = tblParent.DeptLevel + 1;
        tblDept.HasChild = "N";
        tblDept.CreateTime = DateTimeUtil.Now;
        tblDept.Creator = CurrentUser.UserName;
        tblDept.IsActive = "Y";
        tblDept.ListOrder = nListOrder;
        tblDept.Remarks = edtRemarks.Text;

        tblDept.Insert();

        //========= 3. 刷新显示 =============
        PageParameter.StringParam[0] = "新增部门/成员单位信息成功，部门名称为“"
                    + tblDept.DeptName + "”";
        Response.Redirect("dept_update_message.aspx");
    }
}
