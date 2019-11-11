using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.Resident
{
  public  class QueryMedicalInsurancePairCodeParam
    {
        /// <summary>
        /// 基层his三大目录编码
        /// </summary>
        public List<string>  DirectoryCodeList { get; set; }
        /// <summary>
        /// 组织机构
        /// </summary>
        public  string OrganizationCode { get; set; }
    }
}
