using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BenDing.Domain.Models.Params.Web
{/// <summary>
/// 
/// </summary>
   public class UpdateMedicalInsuranceResidentSettlementParam
    {/// <summary>
    /// Id
    /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 自费金额
        /// </summary>
        public decimal SelfPayFeeAmount { get; set; }
        /// <summary>
        /// 报销金额
        /// </summary>
        public decimal ReimbursementExpensesAmount { get; set; }
        /// <summary>
        /// 医保总费用
        /// </summary>
        public decimal MedicalInsuranceAllAmount { get; set; }
        /// <summary>
        ///其他信息
        /// </summary>
        public string OtherInfo { get; set; }
        /// <summary>
        /// 单据号
        /// </summary>
        public string SettlementNo { get; set; }
        /// <summary>
        /// 交易号
        /// </summary>
        public string TransactionId { get; set; }
        /// <summary>
        /// 取消人员
        /// </summary>
        public string CancelUserId { get; set; }
        /// <summary>
        /// 取消交易id
        /// </summary>

        public string CancelTransactionId { get; set; }
        /// <summary>
        /// 是否预结算
        /// </summary>
        public bool IsPresettlement { get; set; }

    }
}
