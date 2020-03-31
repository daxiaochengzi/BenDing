using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Base;

namespace BenDing.Domain.Models.Params.SystemManage
{
   public class QueryMedicalInsuranceIcd10UiParam: PaginationDto
    {/// <summary>
    /// 
    /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 疾病编码
        /// </summary>
        public string ProjectCode { get; set; }
    }
}
