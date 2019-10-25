using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Web
{
  public  class UiIniParam
    {
        public string UserId { get; set; }
        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizationCode { get; set; }
        //XmlIgnoreAttribute
        /// <summary>
        /// 验证码
        /// </summary>
        public string AuthCode { get; set; }
    }
}
