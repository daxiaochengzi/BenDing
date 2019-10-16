using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params
{/// <summary>
 /// 入院登记
 /// </summary>
    public class HospitalizationRegisterParam
    {
      
            ///<summary>
            /// 身份标识
            /// </summary>
            public string PI_SFBZ { get; set; }
            /// <summary>
            /// 传入标志
            /// </summary>
            public string PI_CRBZ { get; set; }
            /// <summary>
            /// 医疗类别11：普通入院23: 市内转院住院14: 大病门诊15: 大病住院22: 急诊入院71：顺产72：剖宫产41：病理剖宫产
            /// </summary>
            public string PI_YLLB { get; set; }
            /// <summary>
            /// 胎儿数
            /// </summary>
            public string PI_TES { get; set; }
            /// <summary>
            /// 户口性质
            /// </summary>
            public string PI_HKXZ { get; set; }
        /// <summary>
        /// 入院日期  (格式为YYYYMMDD)
        /// </summary>
          public string PI_RYRQ { get; set; }
        /// <summary>
        /// 入院主要诊断疾病ICD-10编码
        /// </summary>
        public string PI_ICD10 { get; set; }
            /// <summary>
            /// 入院诊断疾病ICD-10编码
            /// </summary>
            public string PI_ICD10_2 { get; set; }
            /// <summary>
            /// 入院诊断疾病ICD-10编码
            /// </summary>
            public string PI_ICD10_3 { get; set; }
            /// <summary>
            /// 入院诊断疾病名称
            /// </summary>
            public string PI_RYZD { get; set; }
            /// <summary>
            /// 住院科室
            /// </summary>
            public string PI_ZYBQ { get; set; }
            /// <summary>
            /// 床位号
            /// </summary>
            public string PI_CWH { get; set; }
            /// <summary>
            /// 医院住院号
            /// </summary>
            public string PI_YYZYH { get; set; }
            /// <summary>
            /// 经办人
            /// </summary>
            public string PI_JBR { get; set; }
      
           
    }
}
