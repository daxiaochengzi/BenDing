using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params
{/// <summary>
 /// 住院资料修改
 /// </summary>
    public class HospitalizationModifyParam
    {/// <summary>
        /// 住院号
        /// </summary>
        public string PI_ZHY { get; set; }
        /// <summary>
        /// 入院日期
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
        /// 医院住院号
        /// </summary>
        public string PI_YYZYH { get; set; }
        /// <summary>
        /// 胎儿数
        /// </summary>
        public string PI_TES { get; set; }
        /// <summary>
        /// 户口性质
        /// </summary>
        public string PI_HKXZ { get; set; }
    }
}
