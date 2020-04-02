using BenDing.Domain.Models.Dto.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.Resident
{/// <summary>
/// 
/// </summary>
  public  class LeaveHospitalSettlementCancelInfoParam
    {/// <summary>
    /// 住院病人id
    /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 业务id
        /// </summary>
        public string BusinessId { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfoDto User { get; set; }
      
    }
}
