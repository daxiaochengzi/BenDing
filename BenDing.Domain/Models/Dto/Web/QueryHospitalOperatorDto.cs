using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Web
{
   public class QueryHospitalOperatorDto
    {
        /// <summary>
        /// 基层账户
        /// </summary>
        public string HisUserAccount { get; set; }
        /// <summary>
        /// 基层密码
        /// </summary>
        public string HisUserPwd { get; set; }
        /// <summary>
        /// 厂商编号
        /// </summary>
        public  string ManufacturerNumber { get; set; }
    }
}
