using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Base;
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
        public UiBaseDataParam UiParam { get; set; }
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }
        ///<summary>
        /// 身份标识
        /// </summary>
      
        public string IdentityMark { get; set; }
        /// <summary>
        ///身份证号或者个人编号
        /// </summary>
       
        public string AfferentSign { get; set; }
        /// <summary>
        /// 结算xml数据
        /// </summary>
        public string SettlementXml { get; set; }

        /// <summary>
        /// 账户余额
        /// </summary>

        public string AccountBalance { get; set; }
        /// <summary>
        /// 保险类型
        /// </summary>
        public string InsuranceType { get; set; }

    }
}
