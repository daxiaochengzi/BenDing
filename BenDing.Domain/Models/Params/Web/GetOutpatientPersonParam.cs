using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.UI;

namespace BenDing.Domain.Models.Params.Web
{/// <summary>
/// 获取门诊病人信息
/// </summary>
   public class GetOutpatientPersonParam
    {/// <summary>
    /// 
    /// </summary>
        public UserInfoDto User { get; set; }
        /// <summary>
        /// 是否保存
        /// </summary>
        public  bool IsSave { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public GetOutpatientUiParam UiParam { get; set; }

        /// <summary>
        /// 回参json
        /// </summary>
        public string ReturnJson { get; set; }
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }
       

    }
}
