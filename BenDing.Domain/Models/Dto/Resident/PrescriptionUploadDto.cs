using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BenDing.Domain.Models.Dto.Web;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Resident
{
    [XmlRoot("ROW", IsNullable = false)]
    public class PrescriptionUploadDto: IniDto
    {/// <summary>
    /// 批次号
    /// </summary>
        [JsonProperty(PropertyName = "PO_PCH")]
        public string ProjectBatch { get; set; }
    }
}
