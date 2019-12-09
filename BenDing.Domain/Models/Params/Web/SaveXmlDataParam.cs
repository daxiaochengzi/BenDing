using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;

namespace BenDing.Domain.Models.Params.Web
{/// <summary>
 /// 医保信息回写至基层系统
 /// </summary>
    public class SaveXmlDataParam
    {
        /// <summary>
        /// 用户
        /// </summary>
        public UserInfoDto User { get; set; }
        /// <summary>
        /// his唯一交易码
        /// </summary>
        public string TransactionId { get; set; }
        /// <summary>
        /// 医保交易码
        /// </summary>
        public  string MedicalInsuranceCode { get; set; }
        /// <summary>
        /// 业务id
        /// </summary>
        public  string BusinessId { get; set; }
    }
}
