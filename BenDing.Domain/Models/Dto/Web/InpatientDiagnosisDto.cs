using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Web
{
   public class InpatientDiagnosisDto
    {/// <summary>
    /// 诊断名称
    /// </summary>
        public string DiseaseName { get; set; }
        /// <summary>
        /// 诊断编码
        /// </summary>
        public string DiseaseCoding { get; set; }
        /// <summary>
        /// 是否主诊断
        /// </summary>
        public bool IsMainDiagnosis { get; set; } = false;
        /// <summary>
        ///  诊断医保编码
        /// </summary>
    
        public string ProjectCode { get; set; }
       
     
    }
}
