using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BenDing.Domain.Models.Dto.Resident;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.OutpatientDepartment
{
   
    public class QueryOutpatientDepartmentCostDto
    {
        /// <summary>
        /// 身份证号
        /// </summary>
        [JsonProperty(PropertyName = "PO_AAC002")]
      
        public string IdCardNo { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        [JsonProperty(PropertyName = "PO_AAC003")]
     
        public string PatientName { get; set; }
        /// <summary>
        /// 结算人群
        /// </summary>
       
        [JsonProperty(PropertyName = "PO_CKA549")]
        public string SettlementCrowd { get; set; }

        /// <summary>
        ///挂号医院
        /// </summary>
        [JsonProperty(PropertyName = "PO_CKB519")]
       
        public string RegisterHospital { get; set; }

        /// <summary>
        /// 合计金额
        /// </summary>
       
        [JsonProperty(PropertyName = "PO_ZFY")]
      
        public decimal AllAmount { get; set; }

        /// <summary>
        /// 个人自付金额
        /// </summary>

        [JsonProperty(PropertyName = "PO_XJZ")]
      
        public decimal SelfPayFeeAmount { get; set; }

        /// <summary>
        /// 报销金额
        /// </summary>

        [JsonProperty(PropertyName = "PO_GHBZ")]
      
        public decimal ReimbursementExpensesAmount { get; set; }
        /// <summary>
        /// 报销说明备注
        /// </summary>
        [JsonProperty(PropertyName = "PO_XJZF")]
     
        public string Remark { get; set; }
    }
   
}
