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
    [XmlRootAttribute("PO_GHXX", IsNullable = false)]
    public class QueryOutpatientDepartmentCostDto
    {
        [XmlElementAttribute("ROW")]
        [JsonProperty(PropertyName = "Row")]
        public List<QueryOutpatientDepartmentCostDataRowDto> Row { get; set; }
    }
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class QueryOutpatientDepartmentCostDataRowDto
    {/// <summary>
     /// 身份证号
     /// </summary>
        [XmlElementAttribute("PO_AAC002", IsNullable = false)]
        public string IdCardNo { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        [XmlElementAttribute("PO_AAC003", IsNullable = false)]
        public string PatientName { get; set; }
        /// <summary>
        /// 结算人群
        /// </summary>
        [XmlElementAttribute("PO_CKA549", IsNullable = false)]
        public string SettlementCrowd { get; set; }

        /// <summary>
        ///挂号医院
        /// </summary>
        [XmlElementAttribute("PO_CKB519", IsNullable = false)]
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
