using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Base;

namespace BenDing.Domain.Models.Params.Web
{/// <summary>
 /// 医保信息回写至基层系统
 /// </summary>
    public class SaveXmlDataParam: UserBaseParam
    {
        /// <summary>
        /// 业务id
        /// </summary>
        public  string BusinessId { get; set; }
        /// <summary>
        /// 医保交易码
        /// </summary>
        public  string MedicalInsuranceCode { get; set; }
        /// <summary>
        /// 医保返回编码
        /// </summary>
        public  string MedicalInsuranceBackNum { get; set; }
        /// <summary>
        /// 返回参数
        /// </summary>
        public  string BackParam { get; set; }

    }
}
