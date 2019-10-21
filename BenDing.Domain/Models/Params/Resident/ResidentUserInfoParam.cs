using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.Resident
{
  public  class ResidentUserInfoParam
    {
        /// <summary>
        /// 身份标志身份证号或个人编号
        /// </summary>
        public string PI_SFBZ { get; set; }
        /// <summary>
        /// 1为公民身份号码 2为个人编号
        /// </summary>
        public string PI_CRBZ { get; set; }
    }
}
