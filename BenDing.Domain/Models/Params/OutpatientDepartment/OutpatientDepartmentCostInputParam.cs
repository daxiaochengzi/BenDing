using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BenDing.Domain.Models.Dto.Web;

namespace BenDing.Domain.Models.Params.OutpatientDepartment
{


    /// <summary>
    /// 门诊结算
    /// </summary>
    [XmlRoot("ROW", IsNullable = false)]
    public class OutpatientDepartmentCostInputParam
    {
        /// <summary>
        /// 身份标志  身份证号或个人编号
        /// </summary>

        [XmlElementAttribute("PI_SFBZ", IsNullable = false)]
        public string IdentityMark { get; set; }
        /// <summary>
        /// 传入标志 1为公民身份号码 2为个人编号
        /// </summary>
        [Display(Name = "传入标志")]
        [XmlElementAttribute("PI_CRBZ", IsNullable = false)]
        public string AfferentSign{ get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        [XmlElementAttribute("PI_JBR", IsNullable = false)]
        public string Operators { get; set; }
        /// <summary>
        /// 门诊诊查费总费用
        /// </summary>
        [XmlElementAttribute("PI_ZCFY", IsNullable = false)]
        public decimal AllAmount { get; set; }

    }

}
