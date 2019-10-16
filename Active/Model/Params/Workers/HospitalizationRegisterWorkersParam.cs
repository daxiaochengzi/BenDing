using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Workers
{   /// <summary>
    /// 住院登记  
    /// </summary>
    public class HospitalizationRegisterWorkersParam
    {
        /// <summary>
        /// 个人IC卡号或身份证号  
        /// </summary>
        public string Pi_sfbz { get; set; }
        /// <summary>
        /// 为’1’表示卡号,’2’身份证号,3为个人编号  
        /// </summary>
        public string Pi_crbz { get; set; }
        /// <summary>
        /// 行政区划 
        /// </summary>
        public string Pi_xzqh { get; set; }
        /// <summary>
        /// 医疗类别(普通住院 21 ；工伤住院41 )  
        /// </summary>
        public string Pi_yllb { get; set; }
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
        /// <summary>
        /// 经办人  
        /// </summary>
        public string Pi_jbr { get; set; }
    }
}
