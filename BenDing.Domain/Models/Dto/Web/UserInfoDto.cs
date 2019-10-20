using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Web
{/// <summary>
/// 用户信息
/// </summary>
    public class UserInfoDto
    {
        public string 验证码 { get; set; }
        public string 职员ID { get; set; }
        public string 职员名称 { get; set; }
        public string 机构编码 { get; set; }
        public string 机构名称 { get; set; }
        public string 管辖区域 { get; set; }
    }
}
