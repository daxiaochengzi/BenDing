using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.Resident
{
  public  class LeaveHospitalSettlementUiParam
    {
        /// <summary>
        /// 业务id
        /// </summary>
        [Display(Name = "业务id")]
        [Required(ErrorMessage = "{0}不能为空!!!")]

        public string BusinessId { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        [Display(Name = "用户id")]
        [Required(ErrorMessage = "{0}不能为空!!!")]

        public string UserId { get; set; }

        ///// <summary>
        ///// 出院病人状态(1康复，2转院，3死亡，4其他)
        ///// </summary>
        //[Display(Name = "出院病人状态")]
        //[Required(ErrorMessage = "{0}不能为空!!!")]

        //public string LeaveHospitalInpatientState { get; set; }


    }
}
