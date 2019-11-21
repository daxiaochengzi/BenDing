using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Web
{/// <summary>
/// 查询医保明细信息
/// </summary>
   public class QueryMedicalInsuranceDetailInfoDto: QueryMedicalInsuranceDetailDto
    {/// <summary>
    /// 医保住院号
    /// </summary>
        public string MedicalInsuranceHospitalizationNo { get; set; }
    }
}
