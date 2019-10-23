using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Web
{
  public  class ICD10InfoDto
    {
        [JsonProperty(PropertyName = "疾病编码")]
        public string DiseaseCoding { get; set; }
        [JsonProperty(PropertyName = "病种名称")]
        public string DiseaseName { get; set; }
        [JsonProperty(PropertyName = "助记码")]
        public string MnemonicCode { get; set; }
        [JsonProperty(PropertyName = "备注")]
        public string Remark { get; set; }
        [JsonProperty(PropertyName = "创建时间")]
        public string Icd10CreateTime { get; set; }
        [JsonProperty(PropertyName = "疾病ID")]
        public string DiseaseId { get; set; }
    }
}
