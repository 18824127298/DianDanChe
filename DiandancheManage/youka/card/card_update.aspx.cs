using CheDaiBaoWeChatModel;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
using Sigbit.Common;
using Sigbit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class youka_card_card_update : SbtPageBase
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
        AgentService agentService = new AgentService();
        List<Agent> agentList = agentService.GetAll();
        foreach (Agent agent in agentList)
        {
            ListItem li = new ListItem();
            li.Value = agent.Id.ToString();
            li.Text = agent.Name;
            ddlAgent.Items.Insert(1, li);
        }
        MemberService memberService = new MemberService();
        SupplierService supplierService = new SupplierService();

        Array values = Enum.GetValues(typeof(CardBrand));
        if (values.Length > 0)
        {
            foreach (int item in values)
            {
                ListItem li = new ListItem();
                li.Value = item.ToString();
                li.Text = Enum.GetName(typeof(CardBrand), item);
                ddlCardBrand.Items.Insert(0, li);
            }
        }

        Array CardStatusValues = Enum.GetValues(typeof(CardStatus));
        if (values.Length > 0)
        {
            foreach (int item in CardStatusValues)
            {
                ListItem li = new ListItem();
                li.Value = item.ToString();
                li.Text = Enum.GetName(typeof(CardStatus), item);
                ddlCardStatus.Items.Insert(0, li);
            }
        }
        CardService cardService = new CardService();
        Card card = cardService.GetById(nId);
        CardNumber.Text = card.CardNumber;
        CardDiscount.Text = card.CardDiscount.ToString();
        CardLevel.Text = card.CardLevel.ToString();
        CardRoyalty.Text = card.CardRoyalty.ToString();
        ddlAgent.SelectedValue = card.AgentId.ToString();
        Member.Text = string.IsNullOrEmpty(card.MemberId.ToString()) ? "" : memberService.GetById(card.MemberId.Value).Phone;
        SupplierNumber.Text = string.IsNullOrEmpty(card.SupplierId.ToString()) ? "" : supplierService.GetById(card.SupplierId.Value).Number;
        CarNumber.Text = card.CarNumber;
        ddlIsRecharge.SelectedValue = card.IsRecharge.Value.ToString();
        ddlCardBrand.SelectedValue = ConvertUtil.ToString((int)card.CardBrand);
        ddlCardStatus.SelectedValue = ConvertUtil.ToString((int)card.CardStatus);
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        CardService cardService = new CardService();
        Card card = cardService.GetById(ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id")));
        MemberService memberService = new MemberService();
        AgentService agentService = new AgentService();
        SupplierService supplierService = new SupplierService();
        List<Supplier> supplierList = supplierService.GetAll();
        List<Card> cardList = cardService.GetAll();
        List<Member> memberList = memberService.GetAll();

        if (card.CardNumber != CardNumber.Text)
        {
            Card newcard = cardList.Find(o => o.CardNumber == CardNumber.Text);
            if (newcard != null)
            {
                Response.Write("<script language='javascript'>alert('卡号不能重复！')</script>");
                return;
            }
        }
        if (!string.IsNullOrEmpty(ddlAgent.SelectedValue))
        {
            card.AgentId = ConvertUtil.ToInt(ddlAgent.SelectedValue);
        }
        if (!string.IsNullOrEmpty(Member.Text))
        {
            Member member = memberList.Find(o => o.Phone == Member.Text);
            if (member != null)
            {
                card.MemberId = member.Id;
            }
            else
            {
                Response.Write("<script language='javascript'>alert('找不到该会员！')</script>");
                return;
            }
        }

        if (!string.IsNullOrEmpty(SupplierNumber.Text))
        {
            Supplier supplier = supplierList.Find(o => o.Number == SupplierNumber.Text);
            if (supplier != null)
            {
                card.SupplierId = supplier.Id;
            }
            else
            {
                Response.Write("<script language='javascript'>alert('找不到有该主账号的物流公司！')</script>");
                return;
            }
        }
        card.CardDiscount = ConvertUtil.ToDecimal(CardDiscount.Text);
        card.CardLevel = ConvertUtil.ToInt(CardLevel.Text);
        card.CardNumber = CardNumber.Text;
        card.CardRoyalty = ConvertUtil.ToInt(CardRoyalty.Text);
        card.CardBrand = (CardBrand)ConvertUtil.ToInt(ddlCardBrand.SelectedValue);
        card.CardStatus = (CardStatus)ConvertUtil.ToInt(ddlCardStatus.SelectedValue);
        card.CarNumber = CarNumber.Text;
        card.IsRecharge = ConvertUtil.ToBool(ddlIsRecharge.SelectedValue);

        cardService.Update(card);
        Response.Redirect("../card/card_list.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../card/card_list.aspx");
    }

}