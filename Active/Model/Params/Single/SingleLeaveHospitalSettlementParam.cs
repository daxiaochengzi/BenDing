using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Single
{/// <summary>
 /// 单病种费用结算
 /// </summary>
    public class SingleLeaveHospitalSettlementParam
    {
        /// <summary>
        /// 住院号  
        /// </summary>
        public string PI_AAZ217 { get; set; }
        /// <summary>
        /// 出院经办人    
        /// </summary>
        public string PI_JBR { get; set; }
        /// <summary>
        /// 出院日期  
        /// </summary>
        public string PI_CYRQ { get; set; }
        /// <summary>
        /// 出院情况  	出院情况（1康复，2转院，3死亡，4其他）
        /// </summary>
        public string PI_CYQK { get; set; }
        /// <summary>
        /// 出院主要诊断疾病ICD-10编码  
        /// </summary>
        public string PI_ICD10 { get; set; }
        /// <summary>
        /// 出院次要诊断疾病ICD-10编码  
        /// </summary>
        public string PI_ICD10_2 { get; set; }
        /// <summary>
        /// 出院次要诊断疾病ICD-10编码  
        /// </summary>
        public string PI_ICD10_3 { get; set; }
        /// <summary>
        /// 出院主要诊断（确诊疾病）  
        /// </summary>
        public string PI_CYZD { get; set; }
    }
}
