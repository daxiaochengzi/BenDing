using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.UI
{/// <summary>
/// 
/// </summary>
   public class WorKerHospitalizationRegisterUiParam:UiBaseDataParam
    { ///<summary>
      /// 身份标识 (个人IC卡号或身份证号)
      /// </summary>

        [Display(Name = "身份标识")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string IdentityMark { get; set; }
        /// <summary>
        ///传入标志(1表示卡号,2身份证号,3为个人编号)
        /// </summary>

        [Display(Name = "传入标志")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string AfferentSign { get; set; }
        /// <summary>
        /// 医疗类别(普通住院 21 ；工伤住院41 )
        /// </summary>
        [Display(Name = " 医疗类别")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string MedicalCategory { get; set; }
        /// <summary>
        /// 结算json
        /// </summary>
        public string SettlementJson { get; set; }
        /// <summary>
        /// 诊断
        /// </summary>

        public List<InpatientDiagnosisDto> DiagnosisList { get; set; } = null;
    }
}
