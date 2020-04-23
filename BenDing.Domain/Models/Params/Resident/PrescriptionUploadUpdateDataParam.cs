using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;

namespace BenDing.Domain.Models.Params.Resident
{
   public class PrescriptionUploadUpdateDataParam
    {
        public PrescriptionUploadParam UploadData { get; set; }
        public UserInfoDto User { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public  string ProjectBatch { get; set; }
        /// <summary>
        /// 业务id
        /// </summary>
        public  string BusinessId { get; set; }

    }
}
