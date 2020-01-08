using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.JsonEntity
{/// <summary>
/// his医院预结算json
/// </summary>
   public class HisHospitalizationPreSettlementJsonDto
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
        public List<HisHospitalizationPreSettlementDataJsonDto> PreSettlementData { get; set; }
    }
    public class HisHospitalizationPreSettlementDataJsonDto
    {/// <summary>
     /// 离院时间
     /// </summary>
        [JsonProperty(PropertyName = "结束日期")]
        public string EndDate { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        [JsonProperty(PropertyName = "经办人")]
        public string Operator { get; set; }
    }
}
