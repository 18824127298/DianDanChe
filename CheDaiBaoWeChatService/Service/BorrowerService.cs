using CheDaiBaoCommonService.Data;
using CheDaiBaoWeChatModel;
using CheDaiBaoWeChatModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatService.Service
{
    public partial class BorrowerService
    {

        public Borrower Registration(Borrower model)
        {
            BorrowerService borrowerService = new BorrowerService();
            SmsService smsService = new SmsService();
            EncryptionService encryptionService = new EncryptionService();

            List<Borrower> borrowerOfRecommendedIdentifierList = borrowerService.GetAll();

            model.Aliases = model.Aliases ?? model.Phone;
            model.Phone = (model.Phone + "").Trim();
            model.LoginKey = (model.LoginKey + "").Trim();
            model.Code = (model.Code + "").Trim();

            #region 服务端验证

            if (!smsService.ValidateMobileCode(model.Code, model.Phone))
            {
                throw new Exception("验证码错误或者失效，请重新填写或者获取验证码");
            }
            if (borrowerService.Search(new Borrower() { Aliases = model.Aliases }).Any())
            {
                throw new Exception("已经有相同的用户名");
            }

            if (borrowerService.Search(new Borrower() { Phone = model.Phone }).Any())
            {
                throw new Exception("已经有相同的手机号码");
            }

            //if (!model.Agree)
            //{
            //    throw new Exception("须同意并勾选服务条款才可进行注册");
            //}


            #endregion


            Borrower borrower = new Borrower();

            #region 生成推荐人标示符

            string recommendedIdentifier = Guid.NewGuid().ToString();


            #endregion

            borrower.Guid = Guid.NewGuid().ToString();
            borrower.Aliases = model.Aliases ?? model.Phone;
            borrower.Phone = model.Phone;
            borrower.IsValidatePhone = 1;
            borrower.IDType = IDType.身份证;
            borrower.CustomerServiceId = 0;
            borrower.UnionId = model.UnionId;
            borrower.WeiXinId = model.WeiXinId;
            borrower.IsSalesman = false;
            int NewBorrowId = borrowerService.Insert(borrower);
            borrower = borrowerService.GetById(NewBorrowId);
            return borrower;
        }


        //获取风控微信的openid
        public List<string> GetFengKong()
        {
            string sql = "select b.WeiXinId from sbt_user su join Borrower b on su.mobilephone = b.Phone where su.dept_id = 'izm'";
            List<string> lstFengKong = SqlConnections.GetOpenConnection().Query<string>(sql).ToList();
            return lstFengKong;
        }


        //获取风控手机号信息
        public List<string> GetPhone()
        {
            string sql = "select mobilephone from sbt_user su where su.dept_id = 'izm'";
            List<string> lstFengKong = SqlConnections.GetOpenConnection().Query<string>(sql).ToList();
            return lstFengKong;
        }
    }
}
