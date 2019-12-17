using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BenDing.Domain.Models.Dto.Web;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Params.OutpatientDepartment
{ /// <summary>
    /// 门诊结算取消
    /// </summary>

    public class CancelOutpatientDepartmentCostParam
    {/// <summary>
        /// 入参
        /// </summary>
        public CancelOutpatientDepartmentCostParticipationParam Participation { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfoDto User { get; set; }
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 业务Id
        /// </summary>
        public  string BusinessId { get; set; }
    }

   /// <summary>
   /// 
   /// </summary>
    [XmlRoot("ROW", IsNullable = false)]
    public  class CancelOutpatientDepartmentCostParticipationParam
    {
        [XmlElementAttribute("PI_BAE007", IsNullable = false)]
        public string DocumentNo { get; set; }
    }
}
