using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Enums;
using BenDing.Domain.Models.Params.UI;

namespace BenDing.Domain.Models.Params.Web
{
  public  class HospitalOrganizationGradeParam:UiInIParam
    {/// <summary>
     /// 组织机构等级
     /// </summary>
        [Display(Name = "组织机构等级")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public OrganizationGrade OrganizationGrade { get; set; }
        /// <summary>
        /// 医院id
        /// </summary>
        [Display(Name = "医院id")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public  string HospitalId { get; set; }
        /// <summary>
        /// 操作员账户
        /// </summary>
        [Display(Name = "医保账户")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string MedicalInsuranceAccount { get; set; }
        /// <summary>
        /// 操作员密码
        /// </summary>
        [Display(Name = "医保账户")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string MedicalInsurancePwd { get; set; }
        /// <summary>
        /// 行政区划
        /// </summary>
        public string AdministrativeArea { get; set; }


    }
}
