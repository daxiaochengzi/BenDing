using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.Params.Resident
{/// <summary>
 /// 
 /// </summary>
    [XmlRootAttribute("ROW", IsNullable = false)]
    public  class LeaveHospitalSettlementCancelParam
    {/// <summary>
     /// 住院号
     /// </summary>
        [XmlElementAttribute("PI_ZYH", IsNullable = false)]
        public string MedicalInsuranceHospitalizationNo { get; set; }
        /// <summary>
        /// 结算单据号
        /// </summary>
        [XmlElementAttribute("PI_DJH", IsNullable = false)]
        public string SettlementNo { get; set; }
        /// <summary>
        /// 取消度取消程度(1取消结算2删除资料)病人办理出院后，若需要取消：应先调用接口取消结算，再调用接口删除资料
        /// </summary>
        [XmlElementAttribute("PI_QXCD", IsNullable = false)]
        public string CancelLimit { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        [XmlElementAttribute("PI_JBR", IsNullable = false)]
        public string Operators { get; set; }
    }
}
