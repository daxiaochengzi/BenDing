using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Web
{/// <summary>
/// 用户信息
/// </summary>
  
    public class UserInfoDto
    {    /// <summary>
         /// 验证码
         /// </summary>
      
        public string AuthCode { get; set; }
        /// <summary>
        /// 职员ID
        /// </summary>
      
        public string UserId { get; set; }
        /// <summary>
        /// 职员姓名
        /// </summary>
        
        public string UserName { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
      
        public string OrganizationCode { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
       
        public string OrganizationName { get; set; }
        ///// <summary>
        ///// 管辖区划
        ///// </summary>

        //public List<string> RegionCodeList { get; set; } = null;
        /// <summary>
        /// 医保交易码
        /// </summary>
        public  string TransKey { get; set; }
    }
}
