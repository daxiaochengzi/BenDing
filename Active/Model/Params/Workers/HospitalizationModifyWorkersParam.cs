using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Workers
{/// <summary>
    /// 住院资料修改  
    /// </summary>
    class HospitalizationModifyWorkersParam
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
        /// 入院日期  
        /// </summary>
        public string Pi_ryrq { get; set; }
        /// <summary>
        /// 入院主要诊断疾病ICD-10编码  
        /// </summary>
        public string Pi_icd10 { get; set; }
        /// <summary>
        /// 入院诊断疾病ICD-10编码  
        /// </summary>
        public string Pi_icd10_2 { get; set; }
        /// <summary>
        /// 入院诊断疾病ICD-10编码  
        /// </summary>
        public string Pi_icd10_3 { get; set; }
        /// <summary>
        /// 入院诊断  
        /// </summary>
        public string Pi_ryzd { get; set; }
        /// <summary>
        /// 住院病区  
        /// </summary>
        public string Pi_zybq { get; set; }
        /// <summary>
        /// 床位号  
        /// </summary>
        public string Pi_cwh { get; set; }
        /// <summary>
        /// 医院住院号  
        /// </summary>
        public string Pi_yyzyh { get; set; }
    }
}
