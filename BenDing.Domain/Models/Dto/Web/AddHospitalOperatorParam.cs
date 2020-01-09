using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Web
{
   public class AddHospitalOperatorParam
    {/// <summary>
     /// 用户id
     /// </summary>
        [Display(Name = "用户id")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string UserId { get; set; }

        /// <summary>
        /// 操作员账户
        /// </summary>
        [Display(Name = "操作员账户")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string UserAccount { get; set; }
        /// <summary>
        /// 操作员密码
        /// </summary>
        [Display(Name = "操作员密码")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string UserPwd { get; set; }
        /// <summary>
        /// 组织机构
        /// </summary>
        public  string OrganizationCode { get; set; }
        /// <summary>
        /// 厂商编号
        /// </summary>
        public string ManufacturerNumber { get; set; }
       /// <summary>
       /// 用户姓名
       /// </summary>
        public  string HisUserName { get; set; }
    }
}
