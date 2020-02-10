using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.JsonEntity.DifferentPlaces
{
   public class CanCelDifferentPlacesHospitalizationRegisterJsonDto
    {/// <summary>
    /// 经办时间
    /// </summary>
        [JsonProperty(PropertyName = "po_jbsj")]
        public string OperationTime { get; set;  }
        
    }
}
