using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.JsonEntity
{
   public class OutpatientInfoJsonDto
    {/// <summary>
     /// 姓名
     /// </summary>
        [JsonProperty(PropertyName = "姓名")]
        public string PatientName { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        [JsonProperty(PropertyName = "身份证号码")]
        public string IdCardNo { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [JsonProperty(PropertyName = "性别")]
        public string PatientSex { get; set; }
        /// <summary>
        /// 业务ID
        /// </summary>
        [JsonProperty(PropertyName = "业务ID")]
        public string BusinessId { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        [JsonProperty(PropertyName = "门诊号")]
        public string OutpatientNumber { get; set; }
        /// <summary>
        /// 就诊日期
        /// </summary>
        [JsonProperty(PropertyName = "就诊日期")]
        public string VisitDate { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        [JsonProperty(PropertyName = "科室")]
        public string DepartmentName { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        [JsonProperty(PropertyName = "科室编码")]
        public string DepartmentId { get; set; }
        /// <summary>
        /// 诊断医生
        /// </summary>
        [JsonProperty(PropertyName = "诊断医生")]
        public string DiagnosticDoctor { get; set; }
        /// <summary>
        /// 诊断疾病编码
        /// </summary>
        [JsonProperty(PropertyName = "诊断疾病编码")]
        public string DiagnosticDiseaseCode { get; set; }
        /// <summary>
        /// 诊断疾病名称
        /// </summary>
        [JsonProperty(PropertyName = "诊断疾病名称")]
        public string DiagnosticDiseaseName { get; set; }
        /// <summary>
        /// 主要病情描述
        /// </summary>
        [JsonProperty(PropertyName = "主要病情描述")]
        public string DiseaseDesc { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        [JsonProperty(PropertyName = "经办人")]
        public string Operator { get; set; }
        /// <summary>
        /// 就诊总费用
        /// </summary>
        [JsonProperty(PropertyName = "就诊总费用")]
        public decimal MedicalTreatmentTotalCost { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty(PropertyName = "备注")]
        public string Remark { get; set; }
        /// <summary>
        /// 接诊状态
        /// </summary>
        [JsonProperty(PropertyName = "接诊状态")]
        public int ReceptionStatus { get; set; }
    }
}
