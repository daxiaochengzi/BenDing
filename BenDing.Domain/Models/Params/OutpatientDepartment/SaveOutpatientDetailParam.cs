using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;

namespace BenDing.Domain.Models.Params.OutpatientDepartment
{
  public  class SaveOutpatientDetailParam
    {/// <summary>
    /// 用户信息
    /// </summary>
        public UserInfoDto User { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>

        public string OutpatientNo { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>

        public string OrganizationCode { get; set; }
        /// <summary>
        /// OrganizationName
        /// </summary>

        public string OrganizationName { get; set; }
       
    }
}
