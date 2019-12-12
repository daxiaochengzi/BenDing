using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BenDing.Domain.Models.Dto.Web;

namespace BenDing.Domain.Models.Params.OutpatientDepartment
{/// <summary>
/// 门诊月汇总取消
/// </summary>
    public class CancelMonthlyHospitalizationParam
    {/// <summary>
        /// 入参
        /// </summary>
        public CancelMonthlyHospitalizationParticipationParam Participation { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfoDto User { get; set; }
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("ROW", IsNullable = false)]
    public  class CancelMonthlyHospitalizationParticipationParam
    {/// <summary>
     /// 汇总单据号
     /// </summary>
        [XmlElementAttribute("PO_BAE007", IsNullable = false)]
        public string DocumentNo { get; set; }

        /// <summary>
        /// 人员类别
        /// </summary>
        [XmlElementAttribute("PI_CKA549", IsNullable = false)]
        public string PeopleType { get; set; }

        /// <summary>
        /// 汇总类别 20-单病种住院月结 21-精神病住院结算22-门诊诊查费结
        /// </summary>
        ///
        [XmlElementAttribute("PI_CKE544", IsNullable = false)]
        public string SummaryType { get; set; }
    }
}
