using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.JsonEntity
{/// <summary>
 ///  his医院结算json
 /// </summary>
    public class HisHospitalizationSettlementJsonDto
    {

        /// <summary>
        /// 诊断信息
        /// </summary>
        [JsonProperty(PropertyName = "诊断信息")]
        public List<LeaveHospitalDiagnosisDataJsonDto> DiagnosisJson { get; set; }
        /// <summary>
        /// 基础信息
        /// </summary>
        [JsonProperty(PropertyName = "基础信息")]
        public List<PatientLeaveHospitalInfoDataJsonDto>  LeaveHospitalInfoData { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class PatientLeaveHospitalInfoDataJsonDto
    {/// <summary>
        /// 出院日期
        /// </summary>
        [JsonProperty(PropertyName = "出院日期")]
        public string LeaveHospitalDate { get; set; }
        /// <summary>
        /// 出院科室编码
        /// </summary>
        [JsonProperty(PropertyName = "出院科室编码")]
        public string LeaveHospitalDepartmentId { get; set; }
        /// <summary>
        /// 出院科室名称
        /// </summary>
        [JsonProperty(PropertyName = "出院科室名称")]
        public string LeaveHospitalDepartmentName { get; set; }
        /// <summary>
        /// 出院床位
        /// </summary>
        [JsonProperty(PropertyName = "出院床位")]
        public string LeaveHospitalBedNumber { get; set; }
        /// <summary>
        /// 诊断医生
        /// </summary>
        [JsonProperty(PropertyName = "出院诊断医生")]
        public string LeaveHospitalDiagnosticDoctor { get; set; }
        /// <summary>
        /// 出院经办人
        /// </summary>
        [JsonProperty(PropertyName = "出院经办人")]
        public string LeaveHospitalOperator { get; set; }
        /// <summary>
        /// 合计金额
        /// </summary>
        [JsonProperty(PropertyName = "明细费用总额")]
        public decimal AllAmount { get; set; }
    }
}
