using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Base
{/// <summary>
/// 获取诊断
/// </summary>
   public class DiagnosisData
    {
        /// <summary>
        /// 入院主要诊断疾病ICD-10编码
        /// </summary>
        public string AdmissionMainDiagnosisIcd10 { get; set; }
        /// <summary>
        /// 入院诊断疾病ICD-10编码
        /// </summary>

        public string DiagnosisIcd10Two { get; set; }
        /// <summary>
        /// 入院诊断疾病ICD-10编码three
        /// </summary>

        public string DiagnosisIcd10Three { get; set; }
        /// <summary>
        /// 诊断描述
        /// </summary>
        public  string DiagnosisDescribe { get; set; }
    }
}
