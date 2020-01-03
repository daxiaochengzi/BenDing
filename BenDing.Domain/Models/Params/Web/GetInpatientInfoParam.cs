using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;

namespace BenDing.Domain.Models.Params.Web
{/// <summary>
/// 获取病人查询
/// </summary>
  public  class GetInpatientInfoParam
    {
        /// <summary>
        /// 是否保存数据
        /// </summary>
        public  bool IsSave { get; set; }
        /// <summary>
        /// 业务
        /// </summary>
        public  string BusinessId { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public UserInfoDto User { get; set; }
       
    } 
}
