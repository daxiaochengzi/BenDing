using BenDing.Domain.Models.Dto.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Base
{ 
   public class WorkerBaseParam
    { /// <summary>
      /// 医疗机构号
      /// </summary>
        public string OrganizationCode { get; set; }
        /// <summary>
        /// 医保住院号
        /// </summary>
        public string MedicalInsuranceHospitalizationNo { get; set; }
        /// <summary>
        /// 行政区域
        /// </summary>
        public string AdministrativeArea { get; set; }
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
