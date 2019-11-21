using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Resident
{/// <summary>
/// 获取所有的住院病人
/// </summary>
   public class QueryAllHospitalizationPatientsDto
    {/// <summary>
    /// 组织机构
    /// </summary>
        public string OrganizationCode { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string HospitalizationNo { get; set; }
        /// <summary>
        /// 业务id
        /// </summary>
        public string BusinessId { get; set; }
        
    }
}
