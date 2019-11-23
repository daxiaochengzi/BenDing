using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BenDing.Domain.Models.Params.Web
{/// <summary>
/// 
/// </summary>
   public class UpdateMedicalInsuranceResidentSettlementParam
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 自费金额
        /// </summary>
        public decimal SelfPayFee { get; set; }
        /// <summary>
        /// 报销金额
        /// </summary>
        public decimal ReimbursementExpenses { get; set; }

    }
}
