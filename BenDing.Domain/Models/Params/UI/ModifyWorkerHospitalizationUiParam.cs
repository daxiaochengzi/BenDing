using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Base;

namespace BenDing.Domain.Models.Params.UI
{/// <summary>
/// 
/// </summary>
   public class ModifyWorkerHospitalizationUiParam: UiBaseDataParam
    {
        /// <summary>
        /// 入院日期(格式为YYYYMMDD)
        /// </summary>
        public string AdmissionDate { get; set; }

        /// <summary>
        /// 病区
        /// </summary>
        public string InpatientArea { get; set; }

        /// <summary>
        /// 床位号
        /// </summary>

        public string BedNumber { get; set; }
        /// <summary>
        /// 医院住院号
        /// </summary>

        public string HospitalizationNo { get; set; }

        /// <summary>
        /// 经办人
        /// </summary>
        public string Operators { get; set; }
        /// <summary>
        /// 诊断
        /// </summary>
      
        public List<InpatientDiagnosisDto> DiagnosisList { get; set; } = null;
    }
}
