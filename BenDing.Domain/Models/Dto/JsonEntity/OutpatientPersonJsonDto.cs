using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.JsonEntity
{/// <summary>
/// 门诊病人
/// </summary>
  public  class OutpatientPersonJsonDto
    {
        [JsonProperty(PropertyName = "基础信息")]
        public OutpatientPersonBaseJsonDto OutpatientPersonBase { get; set; }
        /// <summary>
        /// 诊断列表
        /// </summary>
        [JsonProperty(PropertyName = "诊断信息")]
        public List<InpatientDiagnosisDataDto> DiagnosisList { get; set; }
    }
    public class OutpatientPersonBaseJsonDto
    {/// <summary>
     /// 病人姓名
     /// </summary>
        [JsonProperty(PropertyName = "病人姓名")]
        public string PatientName { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        [JsonProperty(PropertyName = "身份证")]
        public  string IdCardNo { get; set; }
        /// <summary>
        /// 医生姓名
        /// </summary>
        [JsonProperty(PropertyName = "医生姓名")]
        public  string DoctorName { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        [JsonProperty(PropertyName = "科室名称")]
        public string DepartmentName { get; set; }

        /// <summary>
        /// 门诊号
        /// </summary>
        [JsonProperty(PropertyName = "门诊号")]
        public string OutpatientNumber { get; set; }
        /// <summary>
        /// 收费日期
        /// </summary>
        [JsonProperty(PropertyName = "收费日期")]
        public  string VisitDate { get; set; }

        /// <summary>
        /// 明细总额
        /// </summary>
        [JsonProperty(PropertyName = "明细总额")]
        public string MedicalTreatmentTotalCost { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        [JsonProperty(PropertyName = "经办人")]
        public string Operator { get; set; }
        /// <summary>
        /// 结算编号
        /// </summary>
        [JsonProperty(PropertyName = "结算编号")]
        public  string SettlementNo { get; set; }
    }

  
}
