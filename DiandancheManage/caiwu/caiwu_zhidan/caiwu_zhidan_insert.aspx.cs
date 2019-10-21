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
using Sigbit.Data;
using Sigbit.Framework;
using Sigbit.Web.WebControlUtil;
using System.Collections.Generic;
using System.Text;
using Sigbit.Web;
using CheDaiBaoWeChatService.Service;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatModel;
public partial class caiwu_caiwu_zhidan_caiwu_zhidan_insert : SbtPageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {


        //========== 1. 初始化界面 ==========
        if (IsPostBack)
            return;


        BorrowerId.Value = Request.QueryString["BorrowerId"];

        if (Request.QueryString["Action"] != null)
        {
            Response.ContentType = "text/plain";
            string Action = Request.QueryString["Action"];
            switch (Action)
            {
                case "GetBorrower":
                    Response.Write(GetBorrower(Request.QueryString["Borrower"]));
                    break;
            }
            Response.End();
        }


    }

    public string GetBorrowerName()
    {
        string sSQL = "select Aliases from Borrower where Id=" + StringUtil.QuotedToDBStr(BorrowerId.Value);
        return ConvertUtil.ToString(DataHelper.Instance.ExecuteScalar(sSQL));
    }


    /// <summary>
    /// 根据关键字搜索
    /// </summary>
    /// <param name="BorrowerId"></param>
    /// <returns></returns>
    public string GetBorrower(string Borrower)
    {
        string sSQL = "select Id as id , Aliases+'('+FullName+')' as text from Borrower where CHARINDEX("
            + StringUtil.QuotedToDBStr(Borrower)
            + ",Aliases)>0 union select Id as id , Aliases+'('+FullName+')' as text from Borrower where CHARINDEX(" +
            StringUtil.QuotedToDBStr(Borrower) + ", FullName)>0";
        DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);
        return DataTableToJson(ds.Tables[0]);
    }

    public static string DataTableToJson(DataTable dt)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("[");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            jsonBuilder.Append("{");
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                jsonBuilder.Append("\"");
                jsonBuilder.Append(dt.Columns[j].ColumnName);
                jsonBuilder.Append("\":\"");
                jsonBuilder.Append(dt.Rows[i][j].ToString());
                jsonBuilder.Append("\",");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("},");
        }
        if (dt.Rows.Count != 0)
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        jsonBuilder.Append("]");
        return jsonBuilder.ToString();
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        // 数据校验
        if (BorrowerId.Value == "")
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "请选择要进行充值的用户！";
            BorrowerId.Focus();
            return;
        }


        if (edtAmount.Text.Trim() == "")
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "金额不能为空！";
            edtAmount.Focus();
            return;
        }


        if (edtRemark.Text == "")
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "备注不能为空！";
            edtRemark.Focus();
            return;
        }


        int nInputMoney = 0;
        bool isInt = int.TryParse(edtAmount.Text.Trim(), out nInputMoney);
        if (!isInt)
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "请输入正确格式的金额！";
            edtAmount.Focus();
            return;
        }
        nInputMoney = nInputMoney * 100;
        if (nInputMoney <= 0)
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "请输入大于0的金额！";
            edtAmount.Focus();
            return;
        }

        RechargeService rechargeService = new RechargeService();
         Random r = new Random();
        int nNewRechargeID = rechargeService.Insert(new Recharge()
        {
            BorrowerId = ConvertUtil.ToInt(BorrowerId.Value),
            ActualRechargeFee = ConvertUtil.ToDecimal(nInputMoney * 0.003),
            Amount = ConvertUtil.ToDecimal(nInputMoney),
            RechargeMode = RechargeMode.后台充值,
            RechargeTime = DateTimeUtil.Now,
            Creator = CurrentUser.RealName,
            OrderNumber = DateTime.Now.ToString("yyyyMMddHHmmss") + r.Next(0, 99999),
            RechargeRemark = edtRemark.Text.Trim()
        });

        BorrowerService borrowService = new BorrowerService();
        Borrower borrower = borrowService.GetById(ConvertUtil.ToInt(BorrowerId.Value));
        OperaService operaService = new OperaService();
        operaService.Insert(new Opera()
        {
            BorrowerId = Convert.ToInt32(CurrentUser.RecordData.ThirdPartyCode),
            OperaType = OperaType.后台添加充值数据,
            RelationId = nNewRechargeID,
            Remark = string.Format("后台线下充值，充值会员：{0}，充值金额：{1}，操作人ID：{2}",
                borrower.Aliases, nInputMoney, Convert.ToInt32(CurrentUser.RecordData.ThirdPartyCode))
        });

         

        SbtAppLogger.LogAction("后台线下充值", string.Format("给用户{0}后台线下充值,充值金额{1}元,备注:{2};",
            borrower.Aliases, nInputMoney, edtRemark.Text.Trim()));

        SbtMessageBoxPage msgPage = new SbtMessageBoxPage(this);
        msgPage.MessageText = "后台充值成功，充值金额:" + edtAmount.Text.Trim() + "元";
        msgPage.ReturnUrl = this.Request.Url.AbsoluteUri;

        msgPage.Show();
    }
}