using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BenDing.Domain.Models.Dto.Web;

namespace BenDing.Domain.Models.Params.OutpatientDepartment
{ /// <summary>
  /// 门诊月结汇总
  /// </summary>
    public class MonthlyHospitalizationParam
    {/// <summary>
        /// 入参
        /// </summary>
        public MonthlyHospitalizationParticipationParam Participation { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfoDto User { get; set; }

    }
    /// <summary>
    /// 入参
    /// </summary>
    [XmlRoot("ROW", IsNullable = false)]
    public class MonthlyHospitalizationParticipationParam
    {
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
        /// <summary>
        /// 报销开始日期
        /// </summary>
        [XmlElementAttribute("PI_KSRQ", IsNullable = false)]
        public string StartTime { get; set; }
        /// <summary>
        /// 报销结束时间
        /// </summary>
        [XmlElementAttribute("PI_JSRQ", IsNullable = false)]
        public string EndTime { get; set; }

    }
}
