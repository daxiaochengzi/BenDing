using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Params.OutpatientDepartment
{ /// <summary>
    /// 门诊结算
    /// </summary>
    [XmlRoot("ROW", IsNullable = false)]
    public class MonthlyHospitalizationCancelParam
    {

        [XmlElementAttribute("PO_BAE007", IsNullable = false)]
        [JsonProperty(PropertyName = "PO_BAE007")]
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
