using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Base;

namespace BenDing.Domain.Models.Params.UI
{/// <summary>
/// 
/// </summary>
  public  class HospitalizationPreSettlementUiParam : UiBaseDataParam
    {/// <summary>
        /// 71：顺产: 72: 剖宫产
        /// </summary>
       
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string MedicalCategory { get; set; }

        /// <summary>
        /// 胎儿数
        /// </summary>
      
        public string FetusNumber { get; set; }
        /// <summary>
        /// 诊断
        /// </summary>

        public List<InpatientDiagnosisDto> DiagnosisList { get; set; } = null;

    }
}
