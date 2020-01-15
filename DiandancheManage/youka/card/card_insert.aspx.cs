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

public partial class youka_card_card_insert : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        AgentService agentService = new AgentService();
        List<Agent> agentList = agentService.GetAll();
        foreach (Agent agent in agentList)
        {
            ListItem li = new ListItem();
            li.Value = agent.Id.ToString();
            li.Text = agent.Name;
            ddlAgent.Items.Insert(0, li);
        }
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        CardService cardService = new CardService();
        Card card = new Card();
        cardService.Insert(new Card()
        {
            AgentId = ConvertUtil.ToInt(ddlAgent.SelectedValue),
            CardDiscount = ConvertUtil.ToDecimal(CardDiscount.Text),
            CardLevel = ConvertUtil.ToInt(CardLevel.Text),
            CardNumber = CardNumber.Text,
            CardRoyalty = ConvertUtil.ToInt(CardRoyalty.Text)
        });

        Response.Redirect("../card/card_list.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../card/card_list.aspx");
    }
}