using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.JsonEntity
{
   public class LeaveHospitalDiagnosisDataJsonDto
    {/// <summary>
     /// 出院诊断名称
     /// </summary>
        [JsonProperty(PropertyName = "出院诊断名称")]
        public string DiagnosisName { get; set; }
        /// <summary>
        /// 出院诊断编码
        /// </summary>
        [JsonProperty(PropertyName = "出院诊断编码")]
        public string DiseaseCoding { get; set; }
        /// <summary>
        /// 主诊断标志
        /// </summary>
        [JsonProperty(PropertyName = "主诊断标志")]
        public string IsMainDiagnosis { get; set; }
        /// <summary>
        /// 治疗结果
        /// </summary>
        [JsonProperty(PropertyName = "治疗结果")]
        public string TreatmentOutcome { get; set; }
        /// <summary>
        /// 诊断医保编码
        /// </summary>
        [JsonProperty(PropertyName = "出院医保诊断编码")]
        public string DiagnosisMedicalInsuranceCode { get; set; }
        /// <summary>
        /// 出院医保诊断名称 
        /// </summary>
        [JsonProperty(PropertyName = "出院医保诊断名称")]
        public string DiagnosisMedicalInsuranceName { get; set; }
    }
}
