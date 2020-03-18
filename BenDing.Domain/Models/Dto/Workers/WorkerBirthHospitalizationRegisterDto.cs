using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Workers
{
   public class WorkerBirthHospitalizationRegisterDto: IniDto
    {/// <summary>
     /// 社保住院号
     /// </summary>
        [JsonProperty(PropertyName = "PO_ZYH")]
        public string MedicalInsuranceInpatientNo { get; set; }
    }
}
