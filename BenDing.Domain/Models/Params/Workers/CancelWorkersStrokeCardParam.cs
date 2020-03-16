using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.Params.Workers
{
    [XmlRoot("ROW", IsNullable = false)]
    public class CancelWorkersStrokeCardParam
    {/// <summary>
        /// 划卡流水号
        /// </summary>
        [XmlElement("PI_HKLSH", IsNullable = false)]
        public string WorkersStrokeCardNo { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        [XmlElement("PI_JBR", IsNullable = false)]
        public string Operate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement("PI_AAE013", IsNullable = false)]
        public string Remarks { get; set; }
    }
}
