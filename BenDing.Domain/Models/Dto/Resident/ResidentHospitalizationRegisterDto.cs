using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Resident
{
  /// <summary>
     /// 入院登记
     /// </summary>
        public class ResidentHospitalizationRegisterDto : IniDto
        {/// <summary>
         /// 社保住院号
         /// </summary>
        [JsonProperty(PropertyName = "PO_ZYH")]
        public string MedicalInsuranceInpatientNo { get; set; }
        /// <summary>
        /// 审批编号
        /// </summary>
        [JsonProperty(PropertyName = "PO_SPBH")]
        public string ApprovalNumber { get; set; }
        /// <summary>
        /// 本年统筹可支付金额
        /// </summary>
        [JsonProperty(PropertyName = "PO_BNTCKZFJE")]
        public string MedicalInsuranceYearBalance { get; set; }
        /// <summary>
        /// 本年已住院次数
        /// </summary>
        [JsonProperty(PropertyName = "PO_BNYZYCS")]
        public string YearInpatientsNumber { get; set; }
        
        }
}
