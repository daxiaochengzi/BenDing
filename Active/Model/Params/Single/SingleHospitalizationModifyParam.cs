using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Single
{/// <summary>
 /// 单病种 精神病住院资料修改
 /// </summary>
    public class SingleHospitalizationModifyParam
    { /// <summary>
        /// 就诊记录号  
        /// </summary>
        public string PI_AAZ217 { get; set; }
        /// <summary>
        /// 出入院标志  1-在院 3- 取消入院
        /// </summary>
        public string PI_CKC544 { get; set; }
        /// <summary>
        /// 诊断日期  
        /// </summary>
        public string PI_CKC537 { get; set; }
        /// <summary>
        /// 入院主要诊断疾病ICD-10编码  
        /// </summary>
        public string PI_AKC193 { get; set; }
        /// <summary>
        /// 入院诊断疾病ICD-10编码  
        /// </summary>
        public string PI_AKC194 { get; set; }
        /// <summary>
        /// 入院诊断疾病ICD-10编码  
        /// </summary>
        public string PI_AKC195 { get; set; }
        /// <summary>
        /// 入院诊断疾病名称  
        /// </summary>
        public string PI_CKC540 { get; set; }
        /// <summary>
        /// 住院科室  
        /// </summary>
        public string PI_AKF001 { get; set; }
        /// <summary>
        /// 床位号  
        /// </summary>
        public string PI_AKE020 { get; set; }
        /// <summary>
        /// 住院号  
        /// </summary>
        public string PI_AKC190 { get; set; }
    }
}
