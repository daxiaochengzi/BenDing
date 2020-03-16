using BenDing.Domain.Models.Enums;
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
        /// 结算交易id
        /// </summary>
        public string SettlementTransactionId { get; set; }
       
        /// <summary>
        /// 取消交易id
        /// </summary>

        public string CancelTransactionId { get; set; }
        /// <summary>
        /// 预结算交易id
        /// </summary>
        public string PreSettlementTransactionId { get; set; }
        /// <summary>
        /// 医保状态
        /// </summary>
        public MedicalInsuranceState MedicalInsuranceState { get; set; }
        /// <summary>
        /// 是否更新His状态
        /// </summary>
         
        public bool IsHisUpdateState { get; set; }
        /// <summary>
        /// 刷卡流水
        /// </summary>
        public  string WorkersStrokeCardNo { get; set; }
        /// <summary>
        /// 刷卡信息
        /// </summary>
        public string WorkersStrokeCardInfo { get; set; }
        /// <summary>
        /// 职工取消结算备注
        /// </summary>

        public string CancelSettlementRemarks { get; set; }

    }
}
