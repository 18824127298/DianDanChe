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

public partial class youka_membercard_agent_insert : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        AgentService agentService = new AgentService();
        Agent agent = new Agent();
        agent.Name = Name.Text;
        agent.Phone = Phone.Text;
        int Id = agentService.Insert(agent);

        OperaService operaService = new OperaService();
        operaService.Insert(new Opera()
        {
            BorrowerId = Id,
            FullName = CurrentUser.RealName,
            OperaType = OperaType.后台新增代理商,
            RelationId = ConvertUtil.ToInt(CurrentUser.UserUid),
            Remark = "后台新增加油站点",
        });

        Response.Redirect("agent_list.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("agent_list.aspx");
    }
}