using CheDaiBaoWeChatModel;
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

public partial class youka_membercard_membercard_update : SbtPageBase
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

        foreach (CardBrand pd in Enum.GetValues(typeof(CardBrand)))
        {
            //myway对象必须放在foreach中，因为如果放在外层，只定义一个Way对象，对该对象重复赋值
            //指向的是同一块内存区域，最终获得的List值是3个 “汽车 3” 
            ListItem li = new ListItem();
            li.Value = pd.GetHashCode().ToString(); //1 2 3
            li.Text = Enum.GetName(typeof(CardBrand), pd);  //或者pd.ToString()  飞机，轮船，汽车
            ddlCardBrand.Items.Insert(0, li);
        }

        MemberCardService memberCardService = new MemberCardService();
        MemberCard memberCard = memberCardService.GetById(nId);

        MemberService memberService = new MemberService();
        Member member = memberService.GetById(memberCard.MemberId.Value);

        AgentService agentService = new AgentService();
        Agent agent = agentService.GetById(memberCard.AgentId.Value);

        Phone.Text = member.Phone;
        CardNumber.Text = memberCard.CardNumber;
        Discount.Text = memberCard.Discount.Value.ToString("N2");
        CardLevel.Text = memberCard.CardLevel.ToString();
        ddlCardBrand.SelectedValue = ((int)memberCard.CardBrand).ToString();
        Agent.Text = agent.Name;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        MemberCardService memberCardService = new MemberCardService();
        MemberCard memberCard = memberCardService.Search(new MemberCard() { IsValid = true }).Where(o => o.CardNumber == CardNumber.Text.Trim() && o.Id != ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id"))).SingleOrDefault();
        if (memberCard != null)
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "卡号不能重复";
            return;
        }

        AgentService agentService = new AgentService();
        Agent agent = agentService.Search(new Agent() { IsValid = true }).Where(o => o.Name == Agent.Text).SingleOrDefault();
        if (agent == null)
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "代理商未录入";
            return;
        }


        MemberCard newMemberCard = memberCardService.GetById(ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id")));
        newMemberCard.CardNumber = CardNumber.Text;
        newMemberCard.Discount = Convert.ToDecimal(Discount.Text);
        newMemberCard.CardLevel = Convert.ToInt32(CardLevel.Text);
        newMemberCard.CardBrand = (CardBrand)Convert.ToInt32(ddlCardBrand.SelectedValue);
        newMemberCard.AgentId = agent.Id;
        memberCardService.Update(newMemberCard);

        Response.Redirect("membercard_list.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("membercard_list.aspx");
    }
}