using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
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

public partial class import_execl_import_openid_import : System.Web.UI.Page
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

        string sUploadFileName = "borrow_export.csv";

        string sFullUploadFileName = sUploadPath + "\\" + sUploadFileName;

        fulUpload.PostedFile.SaveAs(sFullUploadFileName);


        //============ 2.文件检查 =================
        CsvPacket csvConfig = new CsvPacket();
        csvConfig.ReadFromFile(sFullUploadFileName);
        File.Delete(sFullUploadFileName);

        BIPDataSet ds = csvConfig.GetDataSet();

        string sOutputMessage = "";

        BorrowerService borrowerService = new BorrowerService();

        List<Borrower> BorrowerAll = borrowerService.GetAll();
        BankCardService bankcardService = new BankCardService();
        IdCardInformationService idcardinformationService = new IdCardInformationService();
        int j = 0;
        for (int i = 1; i <= ds.GetRecordCount(); i++)
        {
            string sPhone = ds.GetItemString(i, 0);
            string sOpenid = ds.GetItemString(i, 1);
            Borrower borrower = BorrowerAll.Find(o => o.Aliases == sPhone);

            if (borrower==null)
            {
                sOutputMessage += "第" + (i + 1) + "行,:" + sPhone + "在数据库中不存在！<br/>";
                j++;
                continue;
            }
            else if (sPhone.Length != 11)
            {
                sOutputMessage += "第" + (i + 1) + "行,电话号码" + sPhone + "格式不正确<br/>";
                j++;
                continue;
            }

            borrower.WeiXinId = sOpenid;
            borrowerService.Update(borrower);


        }
        lblErrMessage.Text = sOutputMessage;
        sOutputMessage += "总共" + ds.GetRecordCount() + "条数据,成功导入" + (ds.GetRecordCount() - j) + "条";
        lblErrMessage.Text = sOutputMessage;
        SbtAppLogger.LogAction("批量导入用户信息", "导入" + ds.GetRecordCount() + "个用户");
    }
}