using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BenDing.Domain.Models.Dto.Base;

namespace BenDing.Domain.Models.Params.UI
{
  public  class QueryPrescriptionDetailUiParam: PaginationDto
    {/// <summary>
     /// 用户id
     /// </summary>
        //[JsonProperty(PropertyName = "操作人员ID")]
        [XmlElement("用户ids", IsNullable = false)]
        [Display(Name = "用户id")]
        [Required(ErrorMessage = "{0}不能为空!!!")]

        public string UserId { get; set; }
        /// <summary>
        /// 业务id
        /// </summary>
        //[JsonProperty(PropertyName = "业务ID")]
        [Display(Name = "业务ID")]
        [Required(ErrorMessage = "{0}不能为空!!!")]

        public string BusinessId { get; set; }
    }
}
