using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Web
{
   public class QueryMedicalInsuranceDto
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 办理住院信息
        /// </summary>

        public string AdmissionInfoJson { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public  string HisHospitalizationId { get; set; }
        /// <summary>
        /// 医保住院号
        /// </summary>
        public  string MedicalInsuranceHospitalizationNo { get; set; }
        /// <summary>
        /// 医保类型
        /// </summary>
        public  int InsuranceType { get; set; }
    }
}
