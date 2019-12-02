using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NFine.Web.Model
{
    public class UserInfo
    {/// <summary>
     /// 用户姓名
     /// </summary>
        [Display(Name = "用户姓名")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string UserPwd { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public  DateTime? CreateTime { get; set; }=null;
    }
}