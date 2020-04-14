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
  public  class OutpatientPlanBirthSettlementUiParam: UiBaseDataParam
    {
        ///<summary>
        /// 身份标识 (身份证号或者个人编号)
        /// </summary>
        [Display(Name = "身份标识")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string IdentityMark { get; set; }
        /// <summary>
        ///传入标志
        /// </summary>
        [Display(Name = "传入标志")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string AfferentSign { get; set; }
        /// <summary>
        /// 账户支付
        /// </summary>
        public string AccountPayment { get; set; }
        /// <summary>
        /// 是否生育
        /// </summary>
        public int IsBirthHospital { get; set; }
        /// <summary>
        /// 结算xml
        /// </summary>
        public  string SettlementJson { get; set; }
        /// <summary>
        /// 账户余额
        /// </summary>
        public string AccountBalance { get; set; }
        /// <summary>
        /// 保险类型
        /// </summary>
        public  string InsuranceType { get; set; }
    }
}
