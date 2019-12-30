using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Base;

namespace BenDing.Domain.Models.Params.Resident
{
  public  class LeaveHospitalSettlementUiParam:UiBaseDataParam
    { /// <summary>
      /// 出院日期
      /// </summary>
        [Display(Name = "出院日期")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public  string LeaveHospitalDate { get; set; }
        /// <summary>
        /// 出院病人状态(1康复，2转院，3死亡，4其他)
        /// </summary>
        [Display(Name = "出院病人状态")]
        [Required(ErrorMessage = "{0}不能为空!!!")]

        public string LeaveHospitalInpatientState { get; set; }
        /// <summary>
        /// 离院经办人
        /// </summary>
        [Display(Name = "离院经办人")]
        [Required(ErrorMessage = "{0}不能为空!!!")]

        public string LeaveHospitalOperator { get; set; }

        /// <summary>
        /// 诊断
        /// </summary>

        public List<InpatientDiagnosisDto> DiagnosisList { get; set; } = null;


    }
}
