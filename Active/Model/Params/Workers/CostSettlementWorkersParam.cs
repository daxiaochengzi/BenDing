using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Workers
{
  public  class CostSettlementWorkersParam
    {/// <summary>
        /// 医疗机构号  
        /// </summary>
        public string Pi_fwjgh { get; set; }
        /// <summary>
        /// 住院号  
        /// </summary>
        public string Pi_zyh { get; set; }
        /// <summary>
        /// 行政区划  
        /// </summary>
        public string Pi_xzqh { get; set; }
        /// <summary>
        /// 是否计住院次数  
        /// </summary>
        public string Pi_cszl { get; set; }
        /// <summary>
        /// 操作员  
        /// </summary>
        public string Pi_czy { get; set; }
        /// <summary>
        /// 出院日期  
        /// </summary>
        public string Pi_cyrq { get; set; }
        /// <summary>
        /// 出院情况（1康复，2转院，3死亡，4其他）  
        /// </summary>
        public string Pi_cyqk { get; set; }
        /// <summary>
        /// 出院主要诊断疾病ICD-10编码  
        /// </summary>
        public string Pi_icd10 { get; set; }
        /// <summary>
        /// 出院诊断疾病ICD-10编码  
        /// </summary>
        public string Pi_icd10_2 { get; set; }
        /// <summary>
        /// 出院诊断疾病ICD-10编码  
        /// </summary>
        public string Pi_icd10_3 { get; set; }
        /// <summary>
        /// 出院诊断（确诊疾病）  
        /// </summary>
        public string Pi_cyzd { get; set; }
    }
}
