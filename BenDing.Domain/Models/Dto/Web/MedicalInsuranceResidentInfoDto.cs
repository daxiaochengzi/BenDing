using System;
using System.Collections.Generic;
using System.Text;

namespace BenDing.Domain.Models.Dto.Web
{/// <summary>
/// 医保病人信息
/// </summary>
 public   class MedicalInsuranceResidentInfoDto
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// his住院id
        /// </summary>
        public string HisHospitalizationId  { get; set; }
        /// <summary>
        /// 社保卡号
        /// </summary>
        public string InsuranceNo { get; set; }
        /// <summary>
        /// 医保年度余额
        /// </summary>
        public string MedicalInsuranceYearBalance { get; set; }
        /// <summary>
        /// 医保住院号
        /// </summary>
        public string MedicalInsuranceHospitalizationNo { get; set; }
        /// <summary>
        /// 自付费用
        /// </summary>
        public decimal SelfPayFee { get; set; }
        /// <summary>
        /// 入院json信息
        /// </summary>
        public string AdmissionInfoJson { get; set; }
        /// <summary>
        /// 报账费用
        /// </summary>
        public  decimal ReimbursementExpenses { get; set; }
        /// <summary>
        /// 其它信息
        /// </summary>
        public string OtherInfo { get; set; }
        /// <summary>
        /// 组织机构编号
        /// </summary>
        public string OrganizationCode { get; set; }
        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string OrganizationName { get; set; }
        /// <summary>
        /// 医保类型(居民，职工,异地)
        /// </summary>
        public  string InsuranceType { get; set; }
        /// <summary>
        /// 医保状态 (0:办理入院，1:办理结算出院)
        /// </summary>

        public int MedicalInsuranceState { get; set; }



    }
}
