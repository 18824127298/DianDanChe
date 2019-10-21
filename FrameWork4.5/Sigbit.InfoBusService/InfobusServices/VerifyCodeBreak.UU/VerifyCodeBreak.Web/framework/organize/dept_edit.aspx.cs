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

public partial class framework_organize_dept_edit : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        string sDeptId = ConvertUtil.ToString(Request["dpt_uid"]);
        if (sDeptId == "")
            return;

        TbUserDept tblDept = new TbUserDept();
        tblDept.DeptId = sDeptId;
        tblDept.Fetch();

        lblDeptName.Text = tblDept.DeptName;
        edtDeptName.Text = tblDept.DeptName;
        edtListOrder.Text = tblDept.ListOrder.ToString();
        edtRemarks.Text = tblDept.Remarks;
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
        string sDeptId = ConvertUtil.ToString(Request["dpt_uid"]);
        if (sDeptId == "")
            return;

        TbUserDept tblDept = new TbUserDept();
        tblDept.DeptId = sDeptId;
        tblDept.Fetch();

        tblDept.DeptName = sDeptName;
        tblDept.ListOrder = nListOrder;
        tblDept.Remarks = edtRemarks.Text;

        tblDept.Update();

        //========= 3. 刷新显示 =============
        PageParameter.StringParam[0] = "部门/成员单位信息已保存修改";
        Response.Redirect("dept_update_message.aspx");
    }

    protected void btnCreateNewDept_Click(object sender, EventArgs e)
    {
        string sDeptId = ConvertUtil.ToString(Request["dpt_uid"]);
        string sUrl = "dept_new.aspx?dpt_uid=" + sDeptId;
        Response.Redirect(sUrl);
    }

    protected void btnDeleteDept_Click(object sender, EventArgs e)
    {
        //========= 1. 数据校验，判断有无下级部门 =============
        string sDeptId = ConvertUtil.ToString(Request["dpt_uid"]);
        TbUserDept tblDept = new TbUserDept();
        tblDept.DeptId = sDeptId;
        tblDept.Fetch();

        TbUserDeptList childList = tblDept.GetChildDepts();
        if (childList.Count > 0)
        {
            lblDeleteMsg.Visible = true;
            lblDeleteMsg.Text = "有下级部门，不能直接删除该部门/成员单位。"
                    + "在删除本级部门之前，请先删除下级部门。";
            return;
        }

        int nUserCountOfDept = TbUser.GetUserCountOfDept(sDeptId);
        if (nUserCountOfDept >= 1)
        {
            lblDeleteMsg.Visible = true;
            lblDeleteMsg.Text = "部门中有用户，不能直接删除该部门/成员单位。"
                    + "在删除本级部门之前，请先删除用户或将用户移到其它部门。";
            return;
        }

        //============ 2. 删除 ===============
        tblDept.Delete();

        //========= 3. 刷新显示 =============
        PageParameter.StringParam[0] = "已删除部门/成员单位“" + tblDept.DeptName + "”";
        Response.Redirect("dept_update_message.aspx");
    }
}
