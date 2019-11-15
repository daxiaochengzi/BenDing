using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.Web
{
   public class UpdateHospitalizationFeeParam
    {   /// <summary>
    /// id
    /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 项目批次号
        /// </summary>
        public  string BatchNumber { get; set; }
        /// <summary>
        /// 交易Id
        /// </summary>
        public  string TransactionId { get; set; }
        /// <summary>
        /// 上传金额
        /// </summary>
        public decimal UploadAmount { get; set; }
    }
}
