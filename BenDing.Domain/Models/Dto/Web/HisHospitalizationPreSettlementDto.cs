using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Enums;

namespace BenDing.Domain.Models.Dto.Web
{
   public class HisHospitalizationPreSettlementDto: InpatientInfoDto
    {/// <summary>
    /// 经办人
    /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 医保类型
        /// </summary>
        public string InsuranceType { get; set; }
        /// <summary>
        /// 医保状态
        /// </summary>
        public MedicalInsuranceState MedicalInsuranceState { get; set; }
        /// <summary>
        /// 结算单据号
        /// </summary>
        public  string SettlementNo { get; set; }
        /// <summary>
        /// 是否生育入院登记
        /// </summary>
        public int IsBirthHospital { get; set; }
       /// <summary>
       /// 身份标识
       /// </summary>
        public string IdentityMark { get; set; }
        /// <summary>
        /// 传输标志
        /// </summary>
        public  string AfferentSign { get; set; }
        /// <summary>
        /// 诊断
        /// </summary>
        public List<InpatientDiagnosisDto> DiagnosisList { get; set; }
    }
}
