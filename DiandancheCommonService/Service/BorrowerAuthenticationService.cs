using CheDaiBaoCommonService.Models;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiandancheCommonService.Service
{
    public class BorrowerAuthenticationService
    {
        public Borrower GetAuthenticatedBorrower()
        {
            BorrowerService borrowerService = new BorrowerService();
            WeiXinModel WeiXin = (WeiXinModel)System.Web.HttpContext.Current.Session["WeiXin"];
            Borrower borrower = borrowerService.GetAll().Find(o => o.WeiXinId == WeiXin.OpenId);
            if (borrower != null)
            {
                borrower.WeiXin = new WeiXin();
                borrower.WeiXin.UnionId = WeiXin.UnionId;
                borrower.WeiXin.HeadImgurl = WeiXin.HeadImgurl;
                borrower.WeiXin.NickName = WeiXin.NickName;
                borrower.WeiXin.OpenId = WeiXin.OpenId;
                borrower.WeiXin.Token = WeiXin.Token;
            }
            return borrower;
        }

    }
}