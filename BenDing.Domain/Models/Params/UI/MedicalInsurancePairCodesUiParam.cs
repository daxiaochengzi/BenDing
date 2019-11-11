using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.UI
{
   public class MedicalInsurancePairCodesUiParam
   {
       /// <summary>
       /// 对码数据
       /// </summary>

       public List<MedicalInsurancePairCodeUiParam> PairCodeList { get; set; } = null;
    
        /// <summary>
        /// 用户id
        /// </summary>
        [Display(Name = "UserId")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string UserId { get; set; }
        /// <summary>
        /// 组织机构编码
        /// </summary>
        public string OrganizationCode { get; set; }
        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string OrganizationName { get; set; }
    }
}
