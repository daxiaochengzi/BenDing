using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Single
{/// <summary>
 /// 单病种 精神病住院登记
 /// </summary>
    public class SingleHospitalizationRegisterParam
    {/// <summary>
     /// 身份标志 个人编号或身份证号
     /// </summary>
        public string PI_SFBZ { get; set; }
        /// <summary>
        /// 传入标志  1为公民身份号码 2为个人编号
        /// </summary>
        public string PI_CRBZ { get; set; }
        /// <summary>
        /// 就诊类别 18-单病种住院报销;28-单病种危急重症住院报销(三甲医院居民医疗不降低报销比例);19-精神病住院报销
        /// </summary>
        public string PI_AKA130 { get; set; }
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
        /// <summary>
        /// 经办人  
        /// </summary>
        public string PI_AAE011 { get; set; }
    }
}
