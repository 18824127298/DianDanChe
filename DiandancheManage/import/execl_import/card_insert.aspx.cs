using CheDaiBaoWeChatModel;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
using Sigbit.Common;
using Sigbit.Framework;
using Sigbit.Net.BIPPacket;
using Sigbit.Net.CsvPacket;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class import_execl_import_card_insert : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (fulUpload.PostedFile == null)
        {
            lblErrMessage.Text = "请上传文件";
            lblErrMessage.Visible = true;
            return;
        }

        //============= 1.上传文件 ================

        string sUploadPath = MapPath("");

        string sUploadFileName = "card_insert.csv";

        string sFullUploadFileName = sUploadPath + "\\" + sUploadFileName;

        fulUpload.PostedFile.SaveAs(sFullUploadFileName);


        //============ 2.文件检查 =================
        CsvPacket csvConfig = new CsvPacket();
        csvConfig.ReadFromFile(sFullUploadFileName);
        File.Delete(sFullUploadFileName);

        BIPDataSet ds = csvConfig.GetDataSet();

        string sOutputMessage = "";

        CardService cardService = new CardService();
        AgentService agentService = new AgentService();
        SupplierService supplierService = new SupplierService();
        List<Supplier> supplierList = supplierService.GetAll();
        List<Card> cardList = cardService.GetAll();
        List<Agent> agentList = agentService.GetAll();
        Hashtable htPhone = new Hashtable();

        int j = 0;
        for (int i = 1; i <= ds.GetRecordCount(); i++)
        {
            string sCardBrand = ds.GetItemString(i, 0);
            string sCardStatus = ds.GetItemString(i, 1);
            string sSupplier = ds.GetItemString(i, 2);
            string sCarNumber = ds.GetItemString(i, 3);
            string sCardNumber = ds.GetItemString(i, 4);
            string sIsRecharge = ds.GetItemString(i, 5);
            string sCardDiscount = ds.GetItemString(i, 6);
            string sCardRoyalty = ds.GetItemString(i, 7);
            string sPhone = ds.GetItemString(i, 8);

            if (htPhone.Contains(sCardNumber))
            {
                sOutputMessage += "第" + (i + 1) + "行,卡号" + sCardNumber + "在您的execl文档中出现了两次<br/>";
                j++;
                continue;
            }

            Card newCard = new Card();

            Card card = cardList.Find(o => o.CardNumber == sCardNumber);
            if (card != null)
            {
                sOutputMessage += "第" + (i + 1) + "行,卡号" + sCardNumber + "已存在<br/>";
                j++;
                continue;
            }

            if (!string.IsNullOrEmpty(sPhone))
            {
                Agent agent = agentList.Find(o => o.Phone == sPhone);
                if (agent == null)
                {
                    sOutputMessage += "第" + (i + 1) + "行,客户" + sPhone + "找不到该手机号的代理商<br/>";
                    j++;
                    continue;
                }
                newCard.AgentId = agent.Id;
            }

            if (!string.IsNullOrEmpty(sSupplier))
            {
                Supplier supplier = supplierList.Find(o => o.Number == sSupplier);
                if (supplier == null)
                {
                    sOutputMessage += "第" + (i + 1) + "行,主账号" + sSupplier + "找不到该主账号的物流公司<br/>";
                    j++;
                    continue;
                }
                newCard.SupplierId = supplier.Id;
            }

            newCard.CardBrand = (CardBrand)Enum.Parse(typeof(CardBrand), sCardBrand);
            newCard.CardStatus = (CardStatus)Enum.Parse(typeof(CardStatus), sCardStatus);
            newCard.CarNumber = sCarNumber;
            newCard.CardNumber = sCardNumber;
            newCard.IsRecharge = sIsRecharge == "1" ? true : false;
            newCard.CardDiscount = string.IsNullOrEmpty(sCardDiscount) ? 1 : ConvertUtil.ToDecimal(sCardDiscount);
            newCard.CardRoyalty = ConvertUtil.ToInt(sCardRoyalty);
            cardService.Insert(newCard);
            htPhone.Add(sCardNumber, newCard);

        }
        lblErrMessage.Text = sOutputMessage;
        sOutputMessage += "总共" + ds.GetRecordCount() + "条数据,成功导入" + (ds.GetRecordCount() - j) + "条";
        lblErrMessage.Text = sOutputMessage;
        SbtAppLogger.LogAction("批量导入卡号信息", "导入" + ds.GetRecordCount() + "个卡号");
    }


}