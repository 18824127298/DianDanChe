using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using CheDaiBaoCommonService.Data;


namespace CheDaiBaoWeChatModel.Models
{
    [Serializable]
    public partial class LoanApply : BaseModel
    {
        /// <summary>
        /// 会员Id
        /// </summary>
        [OriginalField]
        [Display(Name = "会员Id")]
        public int BorrowerId { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [OriginalField]
        [Display(Name = "手机号")]
        public string CreditPhone { get; set; }


        /// <summary>
        /// 借款金额
        /// </summary>
        [OriginalField]
        [Display(Name = "借款金额")]
        public Decimal? CreditAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 借款期限
        /// </summary>
        [OriginalField]
        [Display(Name = "借款期限")]
        public Int32? Deadline
        {
            get;
            set;
        }

        /// <summary>
        /// 借款描述
        /// </summary>
        [OriginalField]
        [Display(Name = "借款描述")]
        public string CreditDescription
        {
            get;
            set;
        }


        /// <summary>
        /// 商户名称
        /// </summary>
        [OriginalField]
        [Display(Name = "商户名称")]
        public string BusinessName
        {
            get;
            set;
        }

        /// <summary>
        /// 招聘点名称
        /// </summary>
        [OriginalField]
        [Display(Name = "招聘点名称")]
        public string RecruitmentName
        {
            get;
            set;
        }



        /// <summary>
        /// 入职站点名称
        /// </summary>
        [OriginalField]
        [Display(Name = "入职站点名称")]
        public string EntrySiteName
        {
            get;
            set;
        }


        /// <summary>
        /// 站点地址
        /// </summary>
        [OriginalField]
        [Display(Name = "站点地址")]
        public string SiteAddress
        {
            get;
            set;
        }

        /// <summary>
        /// 站长电话
        /// </summary>
        [OriginalField]
        [Display(Name = "站长电话")]
        public string StationMasterPhone
        {
            get;
            set;
        }

        /// <summary>
        /// 品牌
        /// </summary>
        [OriginalField]
        [Display(Name = "品牌")]
        public string Brand
        {
            get;
            set;
        }

        /// <summary>
        /// 型号
        /// </summary>
        [OriginalField]
        [Display(Name = "型号")]
        public string CheType
        {
            get;
            set;
        }


        /// <summary>
        /// 分期总额
        /// </summary>
        [OriginalField]
        [Display(Name = "分期总额")]
        public Decimal? TotalAmountStage
        {
            get;
            set;
        }


        /// <summary>
        /// 首付
        /// </summary>
        [OriginalField]
        [Display(Name = " 首付")]
        public Decimal? DownPayments
        {
            get;
            set;
        }

        /// <summary>
        /// 住宅电话
        /// </summary>
        [OriginalField]
        [Display(Name = "住宅电话")]
        public string ResidentialPhone
        {
            get;
            set;
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        [OriginalField]
        [Display(Name = "邮箱")]
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// 教育程度
        /// </summary>
        [OriginalField]
        [Display(Name = "教育程度")]
        public string EducationLevel
        {
            get;
            set;
        }

        /// <summary>
        /// 婚姻状况
        /// </summary>
        [OriginalField]
        [Display(Name = "婚姻状况")]
        public string MaritalStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 子女数目
        /// </summary>
        [OriginalField]
        [Display(Name = "子女数目")]
        public Int32? ChildrenNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 来市时间
        /// </summary>
        [OriginalField]
        [Display(Name = "来市时间")]
        public string ComingTime
        {
            get;
            set;
        }

        /// <summary>
        /// 居住状况
        /// </summary>
        [OriginalField]
        [Display(Name = "居住状况")]
        public string LivingConditions
        {
            get;
            set;
        }

        /// <summary>
        /// 现居住地址
        /// </summary>
        [OriginalField]
        [Display(Name = "现居住地址")]
        public string ResidenceAddress
        {
            get;
            set;
        }

        /// <summary>
        /// 上一份工作单位名称
        /// </summary>
        [OriginalField]
        [Display(Name = "上一份工作单位名称")]
        public string LastWorkName
        {
            get;
            set;
        }

        /// <summary>
        /// 工作内容
        /// </summary>
        [OriginalField]
        [Display(Name = "工作内容")]
        public string JobContent
        {
            get;
            set;
        }

        /// <summary>
        /// 月工资
        /// </summary>
        [OriginalField]
        [Display(Name = "月工资")]
        public Decimal? MonthlyWage
        {
            get;
            set;
        }

        /// <summary>
        /// 工作时间
        /// </summary>
        [OriginalField]
        [Display(Name = "工作时间")]
        public string WorkingYear
        {
            get;
            set;
        }

        /// <summary>
        /// 总工作年限
        /// </summary>
        [OriginalField]
        [Display(Name = "总工作年限")]
        public string WorkingSeniority
        {
            get;
            set;
        }


        /// <summary>
        /// 支出
        /// </summary>
        [OriginalField]
        [Display(Name = "支出")]
        public Decimal? Expenditure
        {
            get;
            set;
        }

        /// <summary>
        /// 生活费用
        /// </summary>
        [OriginalField]
        [Display(Name = "生活费用")]
        public Decimal? LivingCost
        {
            get;
            set;
        }

        /// <summary>
        /// 房租
        /// </summary>
        [OriginalField]
        [Display(Name = "房租")]
        public Decimal? Rent
        {
            get;
            set;
        }

        /// <summary>
        /// 芝麻信用
        /// </summary>
        [OriginalField]
        [Display(Name = "芝麻信用")]
        public Decimal? SesameCredit
        {
            get;
            set;
        }

        /// <summary>
        /// 是否花呗逾期
        /// </summary>
        [OriginalField]
        [Display(Name = "是否花呗逾期")]
        public int IsFlowersOverdue
        {
            get;
            set;
        }

        /// <summary>
        /// 花呗逾期金额
        /// </summary>
        [OriginalField]
        [Display(Name = "花呗逾期金额")]
        public Decimal? FlowersOverdueAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 是否借呗逾期
        /// </summary>
        [OriginalField]
        [Display(Name = "是否借呗逾期")]
        public int IsBorrowOverdue
        {
            get;
            set;
        }


        /// <summary>
        /// 借呗逾期金额
        /// </summary>
        [OriginalField]
        [Display(Name = "借呗逾期金额")]
        public Decimal? BorrowOverdueAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 是否在银行贷款
        /// </summary>
        [OriginalField]
        [Display(Name = "是否在银行贷款")]
        public int IsBankLoan
        {
            get;
            set;
        }

        /// <summary>
        /// 银行贷款余额
        /// </summary>
        [OriginalField]
        [Display(Name = "银行贷款余额")]
        public Decimal? BankLoanBalance
        {
            get;
            set;
        }


        /// <summary>
        /// 是否银行逾期
        /// </summary>
        [OriginalField]
        [Display(Name = "是否银行逾期")]
        public int IsBorrowOverdueAmount
        {
            get;
            set;
        }


        /// <summary>
        /// 银行逾期金额
        /// </summary>
        [OriginalField]
        [Display(Name = "银行逾期金额")]
        public Decimal? BankOverdueAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 是否在P2P/小贷公司借款
        /// </summary>
        [OriginalField]
        [Display(Name = "是否在P2P/小贷公司借款")]
        public int IsPtpLoan
        {
            get;
            set;
        }

        /// <summary>
        /// P2P贷款余额
        /// </summary>
        [OriginalField]
        [Display(Name = "P2P贷款余额")]
        public Decimal? PtpLoanBalance
        {
            get;
            set;
        }


        /// <summary>
        /// 是否P2P逾期
        /// </summary>
        [OriginalField]
        [Display(Name = "是否P2P逾期")]
        public int IsPtpOverdueAmount
        {
            get;
            set;
        }


        /// <summary>
        /// 银行P2P逾期金额
        /// </summary>
        [OriginalField]
        [Display(Name = "银行P2P逾期金额")]
        public Decimal? PtpOverdueAmount
        {
            get;
            set;
        }


        /// <summary>
        /// 网贷、银行贷款申请未通过情况说明
        /// </summary>
        [OriginalField]
        [Display(Name = "网贷、银行贷款申请未通过情况说明")]
        public string FailurePassReason
        {
            get;
            set;
        }

        /// <summary>
        /// 是否办理银行信用卡
        /// </summary>
        [OriginalField]
        [Display(Name = "是否办理银行信用卡")]
        public int IsCreditcardLoan
        {
            get;
            set;
        }

        /// <summary>
        /// 信用卡已用金额
        /// </summary>
        [OriginalField]
        [Display(Name = "信用卡已用金额")]
        public Decimal? CreditcardLoanBalance
        {
            get;
            set;
        }


        /// <summary>
        /// 是否信用卡逾期
        /// </summary>
        [OriginalField]
        [Display(Name = "是否信用卡逾期")]
        public int IsCreditcardOverdueAmount
        {
            get;
            set;
        }


        /// <summary>
        /// 信用卡逾期金额
        /// </summary>
        [OriginalField]
        [Display(Name = "信用卡逾期金额")]
        public Decimal? CreditcardOverdueAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 客户分类
        /// </summary>
        [OriginalField]
        [Display(Name = "客户分类")]
        public string CustomerClassification
        {
            get;
            set;
        }

        /// <summary>
        /// 配偶
        /// </summary>
        [OriginalField]
        [Display(Name = "配偶")]
        public string Spouse
        {
            get;
            set;
        }

        /// <summary>
        /// 是否知晓分期
        /// </summary>
        [OriginalField]
        [Display(Name = "是否知晓分期")]
        public int IsSpouseKnowStages
        {
            get;
            set;
        }

        /// <summary>
        /// 联系电话
        /// </summary>
        [OriginalField]
        [Display(Name = "联系电话")]
        public string ContactPhone
        {
            get;
            set;
        }

        /// <summary>
        /// 联系地址
        /// </summary>
        [OriginalField]
        [Display(Name = "联系地址")]
        public string ContactAddress
        {
            get;
            set;
        }


        /// <summary>
        /// 父母
        /// </summary>
        [OriginalField]
        [Display(Name = "父母")]
        public string Parents
        {
            get;
            set;
        }

        /// <summary>
        /// 父母是否知晓分期
        /// </summary>
        [OriginalField]
        [Display(Name = "父母是否知晓分期")]
        public int IsParentsKnowStages
        {
            get;
            set;
        }

        /// <summary>
        /// 父母联系电话
        /// </summary>
        [OriginalField]
        [Display(Name = "父母联系电话")]
        public string ParentsContactPhone
        {
            get;
            set;
        }

        /// <summary>
        /// 兄弟
        /// </summary>
        [OriginalField]
        [Display(Name = "兄弟")]
        public string Brothers
        {
            get;
            set;
        }

        /// <summary>
        /// 兄弟是否知晓分期
        /// </summary>
        [OriginalField]
        [Display(Name = "兄弟是否知晓分期")]
        public int IsBrothersKnowStages
        {
            get;
            set;
        }

        /// <summary>
        /// 兄弟联系电话
        /// </summary>
        [OriginalField]
        [Display(Name = "兄弟联系电话")]
        public string BrothersContactPhone
        {
            get;
            set;
        }


        /// <summary>
        /// 朋友
        /// </summary>
        [OriginalField]
        [Display(Name = "朋友")]
        public string Friend
        {
            get;
            set;
        }

        /// <summary>
        /// 朋友是否知晓分期
        /// </summary>
        [OriginalField]
        [Display(Name = "朋友是否知晓分期")]
        public int IsFriendKnowStages
        {
            get;
            set;
        }

        /// <summary>
        /// 朋友联系电话
        /// </summary>
        [OriginalField]
        [Display(Name = "朋友联系电话")]
        public string FriendContactPhone
        {
            get;
            set;
        }


        /// <summary>
        /// 亲戚
        /// </summary>
        [OriginalField]
        [Display(Name = "亲戚")]
        public string Relative
        {
            get;
            set;
        }

        /// 与亲戚之间的关系
        /// </summary>
        [OriginalField]
        [Display(Name = "与亲戚之间的关系")]
        public string RelativeRelationShip
        {
            get;
            set;
        }

        /// <summary>
        /// 亲戚是否知晓分期
        /// </summary>
        [OriginalField]
        [Display(Name = "亲戚是否知晓分期")]
        public int IsRelativeKnowStages
        {
            get;
            set;
        }

        /// <summary>
        /// 朋友联系电话
        /// </summary>
        [OriginalField]
        [Display(Name = "亲戚联系电话")]
        public string RelativeContactPhone
        {
            get;
            set;
        }


        /// <summary>
        /// 备注
        /// </summary>
        [OriginalField]
        [Display(Name = "备注")]
        public string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 风控备注
        /// </summary>
        [OriginalField]
        [Display(Name = "风控备注")]
        public string FengKongRemark
        {
            get;
            set;
        }

        /// <summary>
        /// 审核时间
        /// </summary>
        [OriginalField]
        [Display(Name = "审核时间")]
        public DateTime? AuditTime
        {
            get;
            set;
        }

        /// <summary>
        /// 审核人
        /// </summary>
        [OriginalField]
        [Display(Name = "审核人")]
        public string Auditor
        {
            get;
            set;
        }

        /// <summary>
        /// 审核备注
        /// </summary>
        [OriginalField]
        [Display(Name = "审核备注")]
        public string AuditorRemark
        {
            get;
            set;
        }

        /// <summary>
        /// 起息日
        /// </summary>
        [OriginalField]
        [Display(Name = "起息日")]
        public DateTime? InterestDate
        {
            get;
            set;
        }
        /// <summary>
        /// 结清日
        /// </summary>
        [OriginalField]
        [Display(Name = "结清日")]
        public DateTime? ClosingDate
        {
            get;
            set;
        }
        /// <summary>
        /// 还款状态
        /// </summary>
        [OriginalField]
        [Display(Name = "还款状态")]
        public CreditStatus? RepaymentStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 预计还款日
        /// </summary>
        [OriginalField]
        [Display(Name = "预计结清日")]
        public DateTime? ExpectedRepayment
        {
            get;
            set;
        }

        /// <summary>
        /// 第二次审核结果 
        /// </summary>         
        [OriginalField]
        [Display(Name = "第二次审核结果")]
        public Boolean? SecondAuditResult { get; set; }

        /// <summary>
        /// 第二次审核人 
        /// </summary>         
        [OriginalField]
        [Display(Name = "第二次审核人")]
        public string SecondAuditor { get; set; }

        /// <summary>
        /// 第二次审核时间 
        /// </summary>         
        [OriginalField]
        [Display(Name = "第二次审核时间")]
        public DateTime? SecondAuditTime { get; set; }

        /// <summary>
        ///  第二次审核备注 
        /// </summary>         
        [OriginalField]
        [Display(Name = "第二次审核备注")]
        public string SecondRemark { get; set; }

        /// <summary>
        ///  业务员标识 
        /// </summary>         
        [OriginalField]
        [Display(Name = "业务员标识")]
        public int SalesmanId { get; set; }



        /// <summary>
        /// 提前还款违约金 
        /// </summary>         
        [OriginalField]
        [Display(Name = "提前还款违约金")]
        public Decimal? BreachAmount { get; set; }

        /// <summary>
        ///类型(正常还款=1,提前还款=2,逾期还款=3)
        /// </summary>         
        [OriginalField]
        [Display(Name = "类型")]
        public RepaymentPlanMode? RepaymentPlanMode { get; set; }

        /// <summary>
        ///  是否允许提还 
        /// </summary>         
        [OriginalField]
        [Display(Name = "是否允许提还")]
        public Boolean? IsMentionBack { get; set; }

        /// <summary>
        ///  多少个月内提还收取两百加一个月手续费 
        /// </summary>         
        [OriginalField]
        [Display(Name = "月内")]
        public int WithinMonth { get; set; }


        /// <summary>
        /// 月供
        /// </summary>         
        [OriginalField]
        [Display(Name = "月供")]
        public Decimal? MonthlyPayment { get; set; }

        /// <summary>
        /// 批处理时间
        /// </summary>         
        [OriginalField]
        [Display(Name = "批处理时间")]
        public DateTime? BatchDate { get; set; }


        /// <summary>
        ///  同盾网 
        /// </summary>         
        [OriginalField]
        [Display(Name = "同盾网")]
        public string TongDunWang { get; set; }


        /// <summary>
        ///  电核 
        /// </summary>         
        [OriginalField]
        [Display(Name = "电核")]
        public string ElectricCore { get; set; }



        /// <summary>
        ///  回访 
        /// </summary>         
        [OriginalField]
        [Display(Name = "回访")]
        public string VisitRecord { get; set; }


        /// <summary>
        ///  是否做骑手 
        /// </summary>         
        [OriginalField]
        [Display(Name = "是否做骑手")]
        public Boolean? IsRider { get; set; }


        /// <summary>
        ///  其他贷款说明 
        /// </summary>         
        [OriginalField]
        [Display(Name = "其他贷款说明")]
        public string OtherLoan { get; set; }


        /// <summary>
        /// 租金费率
        /// </summary>         
        [OriginalField]
        [Display(Name = "租金费率")]
        public Decimal? RentRate { get; set; }

        /// <summary>
        ///  是否已放款 
        /// </summary>         
        [OriginalField]
        [Display(Name = "是否已放款")]
        public Boolean? IsLending { get; set; }


        /// <summary>
        /// 放款的时间
        /// </summary>         
        [OriginalField]
        [Display(Name = "放款的时间")]
        public DateTime? LendingDate { get; set; }

        /// <summary>
        ///  智查分 
        /// </summary>         
        [OriginalField]
        [Display(Name = "智查分")]
        public string ZhiCha { get; set; }

        /// <summary>
        ///  贷前分 
        /// </summary>         
        [OriginalField]
        [Display(Name = "贷前分")]
        public string DaiQian { get; set; }

        /// <summary>
        /// 步骤
        /// </summary>
        [OriginalField]
        [Display(Name = "步骤")]
        public Int32? Step
        {
            get;
            set;
        }

        /// <summary>
        ///  撤单的备注 
        /// </summary>         
        [OriginalField]
        [Display(Name = "撤单的备注")]
        public string CancellationRemark { get; set; }


        /// <summary>
        /// 允许提还时间
        /// </summary>         
        [OriginalField]
        [Display(Name = "允许提还时间")]
        public DateTime? MentionBackTime { get; set; }

        /// <summary>
        /// 押金
        /// </summary>         
        [OriginalField]
        [Display(Name = "押金")]
        public Decimal? Deposit { get; set; }

        /// <summary>
        /// 电单车租金
        /// </summary>         
        [OriginalField]
        [Display(Name = "电单车租金")]
        public Decimal? BicyclesRent { get; set; }

        /// <summary>
        ///所属的公司 
        /// </summary>         
        [OriginalField]
        [Display(Name = "所属的公司")]
        public Company? Company { get; set; }


        /// <summary>
        ///贷款的类型
        /// </summary>         
        [OriginalField]
        [Display(Name = "贷款的类型")]
        public LoanType? LoanType { get; set; }


        /// <summary>
        ///  电单车编号 
        /// </summary>         
        [OriginalField]
        [Display(Name = "电单车编号")]
        public string BicycleNumber { get; set; }

        /// <summary>
        /// 押金
        /// </summary>         
        [OriginalField]
        [Display(Name = "押金")]
        public Decimal? unDeposit { get; set; }


        /// <summary>
        ///  是否有接听 
        /// </summary>         
        [OriginalField]
        [Display(Name = "是否有接听")]
        public Boolean? IsAnswer { get; set; }

        /// <summary>
        ///  是否有异常 
        /// </summary>         
        [OriginalField]
        [Display(Name = "是否有异常")]
        public Boolean? IsAbnormal { get; set; }

        /// <summary>
        ///  商户是否同意 
        /// </summary>         
        [OriginalField]
        [Display(Name = "商户是否同意")]
        public Boolean? IsMarchantAgree { get; set; }

        /// <summary>
        ///   商户审核人
        /// </summary>         
        [OriginalField]
        [Display(Name = "商户审核人")]
        public string MerchantAuditor { get; set; }

        /// <summary>
        ///   风控审核意见
        /// </summary>         
        [OriginalField]
        [Display(Name = "风控审核意见")]
        public string AuditOpinion { get; set; }

        /// <summary>
        ///   预付租金月数
        /// </summary>         
        [OriginalField]
        [Display(Name = "预付租金月数")]
        public Int32? AdvanceMonth { get; set; }


        /// <summary>
        ///   保险费
        /// </summary>         
        [OriginalField]
        [Display(Name = "保险费")]
        public Decimal? Insurance { get; set; }
    }
}
