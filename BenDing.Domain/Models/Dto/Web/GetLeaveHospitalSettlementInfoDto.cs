using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Params.Resident;

namespace BenDing.Domain.Models.Dto.Web
{
   public class GetLeaveHospitalSettlementInfoDto
    {/// <summary>
    /// 入参
    /// </summary>
        public LeaveHospitalSettlementParam ParamData { get; set; }
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCardNo { get; set; }
    }
}
