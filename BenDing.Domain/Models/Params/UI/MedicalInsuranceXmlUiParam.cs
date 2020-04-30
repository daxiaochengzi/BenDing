using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.UI
{
   public class MedicalInsuranceXmlUiParam
    {
        /// <summary>
        /// 业务ID
        /// </summary>
       
        public string BusinessId { get; set; }
       
        /// <summary>
        /// 操作人员ID
        /// </summary>

      
        public string UserId { get; set; }
        /// <summary>
        /// 结算号
        /// </summary>
        
        public string SettlementNo { get; set; }
        public string TransKey { get; set; }
        
    }
}
