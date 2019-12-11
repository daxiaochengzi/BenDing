using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.OutpatientDepartment
{/// <summary>
/// 
/// </summary>
   public class MonthlyHospitalizationDto
    {
        [JsonProperty(PropertyName = "PO_BAE007")]
        public string DocumentNo { get; set; }
        /// <summary>
        /// 报销人次
        /// </summary>
        [JsonProperty(PropertyName = "PO_AKB079")]
        public int  ReimbursementPeopleNum { get; set; }
        /// <summary>
        /// 报销合计金额
        /// </summary>
        [JsonProperty(PropertyName = "PO_AKB065")]
        public decimal ReimbursementAllAmount { get; set; }
    }
}
