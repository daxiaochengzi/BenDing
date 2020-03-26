using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Base;

namespace BenDing.Domain.Models.Params.UI
{
  public  class GetOutpatientPlanBirthSettlementUiParam: UiBaseDataParam
    {
        ///<summary>
        /// 身份标识
        /// </summary>
        [Display(Name = "身份标识")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string IdentityMark { get; set; }
        /// <summary>
        ///身份证号或者个人编号
        /// </summary>
        [Display(Name = "身份证号或者个人编号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string AfferentSign { get; set; }
        /// <summary>
        /// 账户支付
        /// </summary>
        public decimal AccountPayment { get; set; }
        ///// <summary>
        ///// 诊断
        ///// </summary>
        //public List<InpatientDiagnosisDto> DiagnosisList { get; set; } = null;
    }
}
