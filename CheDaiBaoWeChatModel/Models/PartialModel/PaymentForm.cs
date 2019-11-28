﻿using CheDaiBaoCommonService.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatModel.Models
{
    public partial class PaymentForm : BaseModel
    {

        /// <summary>
        /// 加油站Id 
        /// </summary>         
        [OriginalField]
        [Display(Name = "加油站Id")]
        public Int32? GasStationId { get; set; }

        /// <summary>
        /// 会员的Id
        /// </summary>         
        [OriginalField]
        [Display(Name = "会员的Id")]
        public Int32? MemberId { get; set; }

        /// <summary>
        /// 国标价价格
        /// </summary>         
        [OriginalField]
        [Display(Name = "国标价价格")]
        public Decimal? TotalNationalPrice { get; set; }

        /// <summary>
        /// 加油站价格
        /// </summary>         
        [OriginalField]
        [Display(Name = "加油站价格")]
        public Decimal? GasStationAmount { get; set; }

        /// <summary>
        /// 实际支付价格
        /// </summary>         
        [OriginalField]
        [Display(Name = "实际支付价格")]
        public Decimal? ActualAmount { get; set; }

        /// <summary>
        /// 是否已审核
        /// </summary>         
        [OriginalField]
        [Display(Name = "是否已审核")]
        public Boolean? IsAudit { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>         
        [OriginalField]
        [Display(Name = "订单号")]
        public string OrderNumber { get; set; }

        /// <summary>
        /// 支付的时间
        /// </summary>         
        [OriginalField]
        [Display(Name = "支付的时间")]
        public DateTime? PayTime { get; set; }

        /// <summary>
        /// 审核的时间
        /// </summary>         
        [OriginalField]
        [Display(Name = "审核的时间")]
        public DateTime? AuditTime { get; set; }

        /// <summary>
        /// 手续费
        /// </summary>         
        [OriginalField]
        [Display(Name = "手续费")]
        public Decimal? ServiceFee { get; set; }

        /// <summary>
        /// 微信订单号
        /// </summary>         
        [OriginalField]
        [Display(Name = "微信订单号")]
        public string TransactionId { get; set; }

        /// <summary>
        /// 升数
        /// </summary>         
        [OriginalField]
        [Display(Name = "升数")]
        public Decimal? RiseNumber { get; set; }

        /// <summary>
        /// 加油的编号Id
        /// </summary>         
        [OriginalField]
        [Display(Name = "加油的编号Id")]
        public Int32? OilGunId { get; set; }

        /// <summary>
        /// 支付的方式
        /// </summary>         
        [OriginalField]
        [Display(Name = "支付的方式")]
        public PayMode? PayMode { get; set; }

        /// <summary>
        ///是否已打印
        /// </summary>         
        [OriginalField]
        [Display(Name = "是否已打印")]
        public Boolean? IsPrint { get; set; }
    }

}