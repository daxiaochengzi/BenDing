using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.JsonEntity
{/// <summary>
 /// 
 /// </summary>
    public  class InpatientDiagnosisDataDto
    {

        /// <summary>
        /// 诊断名称
        /// </summary>
        [JsonProperty(PropertyName = "诊断名称")]
        public string DiseaseName { get; set; }
        /// <summary>
        /// 诊断编码
        /// </summary>
        [JsonProperty(PropertyName = "诊断编码")]
        public string DiseaseCoding { get; set; }
        /// <summary>
        /// 是否主诊断
        /// </summary>
        [JsonProperty(PropertyName = "是否主诊断")]
        public string IsMainDiagnosis { get; set; }
        /// <summary>
        ///  诊断医保名称
        /// </summary>
        [JsonProperty(PropertyName = "诊断医保名称")] 
        public string ProjectName { get; set; }
        /// <summary>
        ///  诊断医保编码
        /// </summary>
        [JsonProperty(PropertyName = "诊断医保编码")]
        public string ProjectCode { get; set; }
        // /// <summary>
        // /// 是否主诊断
        // /// </summary>
        // [JsonProperty(PropertyName = "是否主诊断")]
        // public string IsMainDiagnosis { get; set; }
        ///// <summary>
        ///// 诊断医保编码
        ///// </summary>
        //[JsonProperty(PropertyName = "诊断医保编码")]
        //public string DiagnosisMedicalInsuranceCode { get; set; }
        ///// <summary>
        ///// 诊断医保编码
        ///// </summary>
        //[JsonProperty(PropertyName = "诊断医保名称")]
        //public string DiagnosisMedicalInsuranceName { get; set; }
    }
}
