using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Web
{
   public class HisHospitalizationPreSettlementDto: InpatientInfoDto
    {/// <summary>
    /// 预结算经办人
    /// </summary>
        public string PreSettlementOperator { get; set; }
    }
}
