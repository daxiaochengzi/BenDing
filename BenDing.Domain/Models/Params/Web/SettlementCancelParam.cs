using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;

namespace BenDing.Domain.Models.Params.Web
{/// <summary>
/// 取消结算基层参数
/// </summary>
   public class SettlementCancelParam
    {  /// <summary>
        /// 业务
        /// </summary>
        public string BusinessId { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public UserInfoDto User { get; set; }
    }
}
