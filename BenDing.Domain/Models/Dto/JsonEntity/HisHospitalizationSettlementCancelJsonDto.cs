using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.JsonEntity
{
   public class HisHospitalizationSettlementCancelJsonDto
    {
        [JsonProperty(PropertyName = "基础信息")]
        public HisHospitalizationSettlementCancelInfoJsonDto InfoData { get; set; }
    }
    public class HisHospitalizationSettlementCancelInfoJsonDto
    {
        /// <summary>
        /// 结算编号
        /// </summary>
        [JsonProperty(PropertyName = "结算编号")]
        public string SettlementNo { get; set; }
        /// <summary>
        /// 就诊编号
        /// </summary>
        [JsonProperty(PropertyName = "就诊编号")]
        public string DiagnosisNo { get; set; }
        /// <summary>
        ///取消经办人
        /// </summary>
        [JsonProperty(PropertyName = "经办人")]
        public string CancelOperator { get; set; }
    }
}
