using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.Params.OutpatientDepartment
{
    /// <summary>
    /// 门诊费用查询
    /// </summary>
    [XmlRoot("ROW", IsNullable = false)]
    public class QueryOutpatientDepartmentCostParam
    {
        /// <summary>
        /// 流水号PI_AAC001
        /// </summary>

        [XmlElementAttribute("PI_BAE007", IsNullable = false)]
        public string DocumentNo { get; set; }
           /// <summary>
           /// 证件编号
           /// </summary>
        [XmlElementAttribute("PI_AAC001", IsNullable = false)]
        public  string IdCardNo { get; set; }
    }
}
