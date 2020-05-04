using BenDing.Domain.Models.Dto.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Resident
{
   public class AdmissionRegistrationCancelInfoDto
    {
        public InpatientInfoDto InpatientInfo { get; set; }
        /// <summary>
        /// 是否生育住院
        /// </summary>
        public int IsBirthHospital { get; set; }
        /// <summary>
        /// 保险类别
        /// </summary>
        public string InsuranceType { get; set; }
    }
}
