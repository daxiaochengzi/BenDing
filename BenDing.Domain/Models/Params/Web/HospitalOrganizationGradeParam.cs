using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Params.UI;

namespace BenDing.Domain.Models.Params.Web
{
  public  class HospitalOrganizationGradeParam:UiInIParam
    {/// <summary>
    /// 组织机构等级
    /// </summary>
        public int OrganizationGrade { get; set; }
        /// <summary>
        /// 医院id
        /// </summary>
        public  string HospitalId { get; set; }
       
    }
}
