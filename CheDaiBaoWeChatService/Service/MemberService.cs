using CheDaiBaoWeChatModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatService.Service
{
    public partial class MemberService
    {
        public Member Registration(Member model)
        {
            MemberService memberService = new MemberService();
            SmsService smsService = new SmsService();
            EncryptionService encryptionService = new EncryptionService();

            List<Member> borrowerOfRecommendedIdentifierList = memberService.GetAll();

            model.Phone = (model.Phone + "").Trim();
            model.Code = (model.Code + "").Trim();

            #region 服务端验证

            if (!smsService.ValidateMobileCode(model.Code, model.Phone))
            {
                throw new Exception("验证码错误或者失效，请重新填写或者获取验证码");
            }

            if (memberService.Search(new Member() { Phone = model.Phone }).Any())
            {
                throw new Exception("已经有相同的手机号码");
            }


            #endregion


            Member member = new Member();

            #region 生成推荐人标示符

            string recommendedIdentifier = Guid.NewGuid().ToString();


            #endregion

            member.Guid = Guid.NewGuid().ToString();
            member.Phone = model.Phone;
            member.OpenId = model.OpenId;
            member.MemberLevel = 1;
            int NewBorrowId = memberService.Insert(member);
            member = memberService.GetById(NewBorrowId);
            return member;
        }


    }
}
