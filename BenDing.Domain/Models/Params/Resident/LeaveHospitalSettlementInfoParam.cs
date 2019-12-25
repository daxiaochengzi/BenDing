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
  public  class LeaveHospitalSettlementInfoParam
    {   /// <summary>
    /// 操作人员
    /// </summary>
        public UserInfoDto User { get; set; }
        
        /// <summary>
        /// 医保病人id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 医保卡号
        /// </summary>
        public string InsuranceNo { get; set; }
        /// <summary>
        /// 业务id
        /// </summary>
        public  string BusinessId { get; set; }
        /// <summary>
        /// 病人姓名
        /// </summary>
        public  string IdCardNo { get; set; }
        
    } 
}
