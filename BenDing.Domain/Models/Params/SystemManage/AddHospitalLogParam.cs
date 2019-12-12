using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;

namespace BenDing.Domain.Models.Params.SystemManage
{/// <summary>
/// 添加操作日志
/// </summary>
  public  class AddHospitalLogParam
    {/// <summary>
     /// 关联id
     /// </summary>
        public Guid? RelationId { get; set; } = Guid.Empty;
        /// <summary>
        /// 入参或者旧数据
        /// </summary>
        public string JoinOrOldJson { get; set; }
        /// <summary>
        /// 回参或者新数据
        /// </summary>
        public string ReturnOrNewJson { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
       
        /// <summary>
        /// 用户id
        /// </summary>
        public UserInfoDto User { get; set; }
    }
}
