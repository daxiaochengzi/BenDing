using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Params.Resident;

namespace BenDing.Domain.Models.Params.UI
{
    /// <summary>
    /// 
    /// </summary>
   public class PrescriptionUploadUiParam:UiInIParam
    {/// <summary>
     /// 根据病人业务id上传
     /// </summary>
        [Display(Name = "业务id")]
      
        public string BusinessId { get; set; }

        /// <summary>
        /// 上传数据
        /// </summary>
        public PrescriptionUploadParam UploadData { get; set; } = null;
        /// <summary>
        /// 批次号
        /// </summary>
        public string ProjectBatch { get; set; }
        /// <summary>
        /// 根据数据id上传
        /// </summary>
        public List<string> DataIdList { get; set; } = null;
       
    }
}
