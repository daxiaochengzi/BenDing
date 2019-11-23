using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.Params.Resident
{/// <summary>
/// 批次入参
/// </summary>
    [XmlRootAttribute("ROW", IsNullable = false)]
    public class BatchConfirmParam
    {/// <summary>
     /// 医保住院号
     /// </summary>
        [XmlElementAttribute("PI_ZYH", IsNullable = false)]
        public string MedicalInsuranceHospitalizationNo { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        [XmlElementAttribute("PI_JBR", IsNullable = false)]
        public string Operators { get; set; }
        /// <summary>
        /// 批次号 多个批次号通过"，"连接
        /// </summary>
        [XmlElementAttribute("PI_PCH", IsNullable = false)]
        public string BatchNumber { get; set; }
        /// <summary>
        /// 确认类型 1确认，2删除
        /// </summary>
        [XmlElementAttribute("PI_QRLX", IsNullable = false)]
        public int  ConfirmType { get; set; }
    }
}
