using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Web
{
    
    /// <summary>
    /// 
    /// </summary>
    public class IniDto
    {/// <summary>
     /// 过程返回值(为1时正常，否则不正常)
     /// </summary>
        [JsonProperty(PropertyName = "PO_FHZ")]
      
        public string ReturnState { get; set; }
        /// <summary>
        /// 系统错误信息
        /// </summary>
        [JsonProperty(PropertyName = "PO_MSG")]
       
        public string Msg { get; set; }

        
    }
}
