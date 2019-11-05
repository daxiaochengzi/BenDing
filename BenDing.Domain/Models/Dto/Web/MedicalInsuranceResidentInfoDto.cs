using System;
using System.Collections.Generic;
using System.Text;

namespace BenDing.Domain.Models.Dto.Web
{
 public   class MedicalInsuranceResidentInfoDto
    {
        /// <summary>
        /// id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 内容json
        /// </summary>
        public string AdmissionInfoJson { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string HisHospitalizationId { get; set; }
       
    }
}
