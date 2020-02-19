using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Base;

namespace BenDing.Domain.Models.Params.UI
{
   public class QueryHospitalizationFeeUiParam: PaginationDto
    {   /// <summary>
    /// 业务id
    /// </summary>
        [Display(Name = "业务id")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string BusinessId { get; set; }

        /// <summary>
        /// 上传状态查询
        /// </summary>

        public int UploadMark { get; set; }
    }
}
