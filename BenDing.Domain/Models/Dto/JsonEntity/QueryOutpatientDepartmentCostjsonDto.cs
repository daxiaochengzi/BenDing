using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.JsonEntity
{
   public class QueryOutpatientDepartmentCostjsonDto
    {/// <summary>
        /// 身份证号
        /// </summary>
    
        public string IdCardNo { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
      
        public string PatientName { get; set; }
        /// <summary>
        /// 结算人群
        /// </summary>
      
        public string SettlementCrowd { get; set; }

        /// <summary>
        ///挂号医院
        /// </summary>
     
        public string RegisterHospital { get; set; }

        /// <summary>
        /// 合计金额
        /// </summary>
   
        public decimal AllAmount { get; set; }

        /// <summary>
        /// 个人自付金额
        /// </summary>
      
        public decimal SelfPayFeeAmount { get; set; }

        /// <summary>
        /// 报销金额
        /// </summary>
        public decimal ReimbursementExpensesAmount { get; set; }
        /// <summary>
        /// 报销说明备注
        /// </summary>
        public string Remark { get; set; }
    }
}
