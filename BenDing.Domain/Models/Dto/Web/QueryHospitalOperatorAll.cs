using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Web
{
   public class QueryHospitalOperatorAll
    {
        /// <summary>
        /// 操作人员id
        /// </summary>
        public string HisUserId { get; set; }
        /// <summary>
        /// 操作员姓名
        /// </summary>
        public  string HisUserName { get; set; }
        /// <summary>
        /// 固定编码
        /// </summary>
        public string FixedEncoding { get; set; }
    }
}
