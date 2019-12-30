﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Workers
{/// <summary>
/// 
/// </summary>
  public  class WorkerHospitalizationPreSettlementDto
    {/// <summary>
     /// 发生费用金额
     /// </summary>
      
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 基本统筹支付
        /// </summary>
      
        public decimal BasicOverallPay { get; set; }
        /// <summary>
        /// 补充医疗保险支付金额
        /// </summary>
       
        public decimal SupplementPayAmount { get; set; }
        /// <summary>
        /// 专项基金支付金额
        /// </summary>
        public decimal SpecialFundPayAmount { get; set; }
        /// <summary>
        /// 公务员补贴
        /// </summary>
        public decimal CivilServantsSubsidies { get; set; }
        /// <summary>
        /// 公务员补助
        /// </summary>
        public decimal CivilServantsSubsidy { get; set; }
        /// <summary>
        ///  其它支付金额
        /// </summary>

        public decimal OtherPaymentAmount { get; set; }
        /// <summary>
        /// 账户支付
        /// </summary>
        public decimal AccountPayment { get; set; }
        /// <summary>
        /// 现金支付
        /// </summary>

        public decimal CashPayment { get; set; }
        /// <summary>
        /// 起付金额
        /// </summary>
       
        public decimal PaidAmount { get; set; }
        /// <summary>
        /// 单据号
        /// </summary>
      
        public string DocumentNo { get; set; }

        /// <summary>
        ///  备注
        /// </summary>

        public string Remark { get; set; }
    }
}