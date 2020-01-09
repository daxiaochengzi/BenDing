using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Enums;

namespace BenDing.Domain.Models.Dto.SystemManage
{
  public  class HospitalOrganizationGradeDto
    { /// <summary>
    /// 行政区域
    /// </summary>
        public string AdministrativeArea { get; set; }
        /// <summary>
        /// 医院等级
        /// </summary>
        public OrganizationGrade OrganizationGrade { get; set; }
        /// <summary>
        /// 医保账户
        /// </summary>
        public string MedicalInsuranceAccount { get; set; }
        /// <summary>
        /// 医保密码
        /// </summary>
        public  string MedicalInsurancePwd { get; set; }
    }
}
