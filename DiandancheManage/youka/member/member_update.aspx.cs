using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
using Sigbit.Common;
using Sigbit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class youka_member_member_update : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        int nId = ConvertUtil.ToInt(Request["id"]);

        if (nId != 0)
        {
            PageParameter.SetCustomParamObject("id", nId);
        }
        MemberService memberService = new MemberService();
        Member member = memberService.GetById(nId);

        Phone.Text = member.Phone;
        Count.Text = member.Count.ToString();
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        MemberService memberService = new MemberService();
        Member member = new Member();
        member.Id = ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id"));
        member.Count = ConvertUtil.ToInt(Count.Text);
        memberService.Update(member);

        Response.Redirect("../member/member_list.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../member/member_list.aspx");
    }
}