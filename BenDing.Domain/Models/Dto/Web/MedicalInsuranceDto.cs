using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BenDing.Domain.Models.Dto.Web
{
   public class MedicalInsuranceDto
    {/// <summary>
     /// 基层His住院Id
     /// </summary>
        public string HisHospitalizationId { get; set;}
        /// <summary>
        /// 医保住院号
        /// </summary>
        public string MedicalInsuranceHospitalizationNo { get; set; }
        /// <summary>
        /// 医保卡号
        /// </summary>
        public string InsuranceNo { get; set; }
        /// <summary>
        /// 医保年度余额
        /// </summary>
        public decimal MedicalInsuranceYearBalance { get; set; }
      /// <summary>
      /// 报账费用
      /// </summary>
        public decimal ReimbursementExpenses { get; set; }
        /// <summary>
        /// 自费费用
        /// </summary>
        public decimal SelfPayFee { get; set; }
        /// <summary>
        /// 其他信息
        /// </summary>
        public string OtherInfo { get; set; }
        /// <summary>
        /// 入院信息
        /// </summary>
        public string AdmissionInfoJson { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
    }
}
