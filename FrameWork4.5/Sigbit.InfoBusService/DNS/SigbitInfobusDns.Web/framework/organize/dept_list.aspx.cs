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

using System.Text;

using Sigbit.Framework;
using Sigbit.Data;
using Sigbit.Common;

public partial class framework_organize_dept_list : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        TreeNode rootNode = tvDeptTree.Nodes[0];

        TbUserDept tblRootDept = new TbUserDept();
        tblRootDept.DeptLevel = 0;

        ExpandNode(rootNode, tblRootDept);
    }

    private void ExpandNode(TreeNode tvNode, TbUserDept tblDept)
    {
        if (tblDept.DeptLevel != 0)
        {
            tvNode.ImageUrl = "~/images/menu_icon/dept.gif";
            tvNode.Text = tblDept.DeptName;
            tvNode.NavigateUrl = "dept_edit.aspx?dpt_uid=" + tblDept.DeptId;
            tvNode.Target = "dept_main";
            tvNode.Expanded = false;
        }

        TbUserDeptList childDepts = tblDept.GetChildDepts();
        if (childDepts.Count > 0)
        {
            tvNode.SelectAction = TreeNodeSelectAction.Expand;
        }

        for (int i = 0; i < childDepts.Count; i++)
        {
            TbUserDept childDept = childDepts.GetDept(i);
            TreeNode childTVNode = new TreeNode();
            tvNode.ChildNodes.Add(childTVNode);
            ExpandNode(childTVNode, childDept);
        }
    }
}
