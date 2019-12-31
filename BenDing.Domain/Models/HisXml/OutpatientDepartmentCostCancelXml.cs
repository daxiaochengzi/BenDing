using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.HisXml
{/// <summary>
/// 门诊结算取消
/// </summary>
   public class OutpatientDepartmentCostCancelXml
    {

        /// <summary>
        /// 结算单号
        /// </summary>
        [XmlElement("aaz216", IsNullable = false)]
        public string SettlementNo { get; set; }
    }
}
