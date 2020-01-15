using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatModel
{
    public enum BusinessType
    {
        人身照 = 1,
        客户的信息照 = 2,
        作证的资料 = 3,
        文件 = 6
    }

    /// <summary>
    /// 身份类型
    /// </summary>
    public enum IDType
    {
        身份证 = 1,
        组织机构代码 = 2,
        护照 = 3
    }
    public enum OccupationCategory
    {
        /// <summary>
        /// 房产中介经纪人
        /// </summary>
        HouseAgency = 1,
        /// <summary>
        /// 担保公司客户经理
        /// </summary>
        BondingCompany = 2,
        /// <summary>
        /// 小贷公司客户经理
        /// </summary>
        SmallLoan = 3,
        /// <summary>
        /// 自由职业经纪人
        /// </summary>
        FreeOccupation = 4,
        /// <summary>
        /// 其他
        /// </summary>
        Other = 0
    }
    /// <summary>
    /// 审核状态
    /// </summary>
    public enum AuditStatusType
    {
        Apply = 0,
        Pass = 1,
        Fail = 2
    }
    /// <summary>
    /// 经理级别
    /// </summary>
    public enum WorkLevelType
    {
        Common = 1,
        Senior = 2,
        MoreSenior = 3
    }
    /// <summary>
    /// 项目特长
    /// </summary>
    public enum ProjectExpertiseType
    {
        House = 1,
        Car = 2,
        Consumption = 3,
        Pledge = 4,
        Credit = 5,
        Management = 6
    }
    /// <summary>
    /// 类型 
    /// </summary>
    public enum GodType
    {
        投资人 = 1,
        借款人 = 2,
        担保公司 = 3,
        平台 = 4,
        体验标帐号 = 5,
    }
    /// <summary>
    /// 注册类型
    /// </summary>
    public enum RegisterType
    {
        /// <summary>
        /// 普通会员
        /// </summary>
        Common = 0,
        /// <summary>
        /// 客户经理
        /// </summary>
        CustomerManager = 1
    }

    /// <summary>
    /// 注册渠道
    /// </summary>
    public enum RegChannel
    {
        网站 = 1,
        微信 = 2,
        微信专属 = 3,
        //-----ceshidaimaduan-----
        微信活动 = 5,
        微信房贷 = 6,
        广州移动频道 = 7,
        国际贸易活动委员 = 8,
        聚享吧专属 = 9,
        红薪宝 = 10,
        车贷宝 = 18
    }


    /// <summary>
    /// 借款申请类型
    /// </summary>
    public enum ApplyType
    {
        /// <summary>
        /// 为自己申请
        /// </summary>
        Self = 0,
        /// <summary>
        /// 代理申请人申请
        /// </summary>
        Proxy = 1
    }
    /// <summary>
    /// 状态 
    /// </summary>
    public enum CreditStatus
    {
        未审核 = 1,
        审核未通过 = 3,
        还款中 = 5,
        还款完成 = 6,
        已初审 = 8,
        撤单 = 11
    }

    /// <summary>
    /// 借款方式
    /// </summary>
    public enum CreditType
    {
        一次性还本付息 = 1,
        等额本息 = 2,
        按月付息到期还本 = 3
    }

    /// <summary>
    ///自动投标方式 
    /// </summary>
    public enum AutoBidType
    {
        /// <summary>
        /// 余额
        /// </summary>
        Balance = 0,
        /// <summary>
        /// 最大投资金额
        /// </summary>
        MaxBidMoney = 1,
        /// <summary>
        /// 余额百分比
        /// </summary>
        BalancePercent = 2,
        /// <summary>
        /// 保留余额
        /// </summary>
        RetainBalance = 3
    }

    /// <summary>
    /// 类型 
    /// </summary>
    public enum CreditMode
    {
        借款标 = 1,
        体验标 = 3,
        活动标 = 4,
        存钱标 = 5,
        I7活动专属标 = 6,
        I7活动预约标 = 7,
        红薪宝借款标 = 8,
        车贷宝借款标 = 9
    }


    /// <summary>
    /// 类型 
    /// </summary>
    public enum RepaymentPlanMode
    {
        正常还款 = 1,
        提前还款 = 2,
        逾期还款 = 3
    }

    /// <summary>
    /// 资金类型
    /// </summary>
    public enum FeeType
    {
        充值 = 1,
        提现 = 2,
        提现手续费 = 3,

        推荐费 = 6,
        支付 = 8,
        退费 = 9,

        后台充值 = 11,
        后台提现 = 12,

        第三方收取的充值手续费 = 14,
        第三方收取的提现手续费 = 15,

        本金 = 23,

        逾期借款利息 = 25,
        月供 = 26,

        提前还款违约金 = 30,
        利息 = 32,

        平台佣金 = 100,

        平台打款 = 101,

        租金 = 102,
        押金 = 103,

        押金回退 = 19918
    }

    /// <summary>
    /// 充值接口 
    /// </summary>
    public enum RechargeMode
    {
        后台充值 = 1,
        微信充值 = 2
    }

    /// <summary>
    /// 支付接口 
    /// </summary>
    public enum PayMode
    {
        微信公众号支付 = 1
    }

    /// <summary>
    /// 操作类型 
    /// </summary>
    public enum OperaType
    {
        注册 = 1,
        登录 = 2,
        实名认证 = 3,
        手机验证 = 4,
        充值 = 5,
        提现 = 6,
        验证银行卡 = 7,
        添加银行卡 = 8,
        修改密码 = 9,
        修改手机 = 10,
        删除银行卡 = 11,
        投标 = 12,
        修改提现密码 = 13,
        找回密码 = 15,
        申请借款 = 16,
        充值回调 = 17,
        添加推荐人 = 18,
        注册送奖金 = 19,
        清空推荐人 = 20,

        短信注册会员 = 120,
        短信手机认证 = 121,
        短信密码找回 = 122,
        短信修改密码 = 123,
        短信修改手机 = 124,
        短信发送新手机验证码 = 125,
        短信提现 = 126,
        短信删除银行帐号 = 127,
        短信增加银行帐号 = 128,
        短信自动还款通知 = 129,
        短信提前还款通知 = 130,
        短信通知客户验证银行卡 = 131,
        短信还款提醒 = 132,
        短信用户注册回访 = 133,

        后台审核银行卡并汇款 = 201,
        后台添加充值数据 = 202,
        后台充值 = 203,
        后台添加提现数据 = 204,
        后台提现 = 205,
        补体验标 = 206,
        审核借款 = 207,
        一键创建体验标 = 208,
        创建借款标 = 209,
        后台创建会员 = 210,
        后台审核基金 = 211,
        取消客服 = 212,
        后台修改管理员信息 = 213,
        后台修改用户信息 = 214,
        添加客服 = 215,


        自动计息 = 301,
        自动还款 = 302,
        批处理 = 303,

        网站异常 = 500,

        邮箱验证发送 = 800,
        邮箱验证接收 = 801,
        邮箱修改 = 802,

        驳回申请原因 = 196,
        押金回退操作 = 19918,
        后台新增加油站点 = 191115,
        后台修改加油站点 = 19118,
        后台新增代理商 = 1912
    }

    /// <summary>
    /// 快速备注类型
    /// </summary>
    public enum AuditType
    {
        后台验证银行卡 = 1,
        后台审核提现 = 2,
        后台审核充值 = 3,
        后台审核基金 = 4,
        后台提现备注 = 6,
        后台充值备注 = 7
    }

    /// <summary>
    /// 借款用途
    /// </summary>
    public enum Usefulness
    {
        赎楼借款 = 1,
        红本抵押 = 2,
        车辆抵押 = 3,
        企业借款 = 4,
        企业信用借款 = 5,
        企业抵押借款 = 6,
        体验标 = 7,
        工薪信贷 = 11,
        经营信贷 = 12,
        房供信贷 = 13,
        车供信贷 = 14,
        理财计划 = 15,
    }

    /// <summary>
    /// 借款期限单位
    /// </summary>
    public enum DateType
    {
        天 = 1,
        月 = 2
    }

    /// <summary>
    /// 短信类型
    /// </summary>
    public enum SendSmsTemplate
    {
        短信验证码 = 1,
        语音验证码 = 2,
        回款通知 = 3,
        //正常还款=4,
        新标通知 = 5,
        流标 = 6,
        银行卡验证成功 = 7,
        银行卡验证失败 = 8,
    }


    /// <summary>
    /// 广告类型
    /// </summary>
    public enum ADType
    {
        首页轮播图 = 1,
        友情链接 = 2,
        手机轮播图 = 3,
        首页悬浮轮播图 = 4,
        众筹首页轮播图 = 5,
        合作伙伴 = 6,
        首页轮播图大图 = 7
    }

    /// <summary>
    /// 提现类型
    /// </summary>
    public enum WithdrawalMode
    {
        会员自提 = 6,
        后台提现 = 205,
        红薪宝后台自动提现 = 8,
        车贷宝后台自动提现 = 9
    }

    /// <summary>
    /// 借款搜索条件
    /// </summary>
    public enum CreditListStatus
    {
        最近5天还款,
        今天还款,
        逾期标借款
    }

    /// <summary>
    /// 借款详情页面样式类型
    /// </summary>
    public enum CreditStatusStyleClass
    {
        InBidding = 3,
        CompleteBidding = 4,
        InRepaying = 5,
        CompleteRepaying = 6,
        CancelledProject = 11,
        FinishedAllRepaying = 20
    }

    /// <summary>
    /// 日志客户端类型
    /// </summary>
    public enum ClientDevices
    {
        电脑版本 = 1,
        移动设备版本 = 2
    }


    /// <summary>
    /// 众筹模式
    /// </summary>
    public enum CrowdfundType
    {
        购买众筹 = 1,
        股权众筹 = 2
    }
    /// <summary>
    /// 众筹状态
    /// </summary>
    public enum CrowdfundStatus
    {
        编辑中 = 1,
        等待审核 = 2,
        审核失败 = 3,
        众筹中 = 4,
        众筹成功 = 5,
        众筹失败 = 6
    }


    /// <summary>
    /// 项目阶段
    /// </summary>
    public enum CrowdfundStage
    {
        请选择项目阶段 = 0,
        概念阶段 = 1,
        产品研发中 = 2,
        已正式发布 = 3,
        已经盈利 = 4
    }

    /// <summary>
    /// 团队成员类型
    /// </summary>
    public enum TeamMemberType
    {
        创建者 = 1,
        团队成员 = 2,
        顾问导师 = 3,
    }

    /// <summary>
    /// 回报类型
    /// </summary>
    public enum RepayType
    {
        实物回报 = 1,
        虚拟回报 = 2,
    }

    /// <summary>
    /// 订单状态
    /// </summary>
    public enum OrderStatus
    {
        未支付 = 0,
        已支付 = 1,
        已发货 = 2,
        已收货 = 3,
        完成 = 4,
        作废 = 5
    }

    /// <summary>
    /// 申请查看项目
    /// </summary>
    public enum WantSeeStatus
    {
        等待审核 = 0,
        同意 = 1,
        不同意 = 2,
    }
    /// <summary>
    /// 项目查看类型
    /// </summary>
    public enum SeeType
    {
        同意查看,
        不同意查看,
        等待审核,
        申请查看,
        请登录,
        不是天使投资人
    }
    /// <summary>
    /// 天使状态
    /// </summary>
    public enum AngelInfoStatus
    {
        等待审核 = 0,
        天使认证通过 = 1,
        天使认证失败 = 2,
        申请中 = 9,
    }

    public enum CreatorType
    {
        客户经理 = 1,
        区域经理 = 2,
        风控专员 = 3
    }

    public enum Company
    {
        翼速 = 0,
        李思静 = 1
    }

    public enum LoanType
    {
        以租代购 = 0,
        租客 = 1
    }

    public enum CardBrand
    {
        中国石化 = 1,
        中油BP = 2,
        中国石油 = 3
    }

    public enum BounsType
    {
        注册优惠券 = 1,
        推荐优惠券 = 2
    }

    /// <summary>
    /// 奖金状态
    /// </summary>
    public enum BounsStatus
    {
        未使用 = 1,
        使用中 = 2,
        已用完 = 3,
        过期失效 = 4,
        未激活 = 5
    }

    /// <summary>
    /// 奖金使用类型
    /// </summary>
    public enum BounsUseType
    {
        支付 = 1,
        充值 = 2
    }

    /// <summary>
    /// 奖金使用类型
    /// </summary>
    public enum CardStatus
    {
        正常 = 1,
        已销卡 = 2,
        已挂失 = 3
    }
}
