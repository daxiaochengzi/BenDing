using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Web
{
   public class HisHospitalizationPreSettlementDto: InpatientInfoDto
    {/// <summary>
    /// 经办人
    /// </summary>
        public string Operator { get; set; }
        
     
        /// <summary>
        /// 诊断
        /// </summary>
        public List<InpatientDiagnosisDto> DiagnosisList { get; set; }
    }
}
