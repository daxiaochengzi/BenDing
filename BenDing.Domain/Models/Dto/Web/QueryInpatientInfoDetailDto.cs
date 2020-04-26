using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Web
{/// <summary>
/// 
/// </summary>
   public class QueryInpatientInfoDetailDto: InpatientInfoDetailDto
    { /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人员
        /// </summary>
        public string CreateUserId { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public  int DataSort { get; set; }
        ///// <summary>
        ///// 处方固定码
        ///// </summary>
        //public  string RecipeCodeFixedEncoding { get; set; }
        ///// <summary>
        ///// 开单医生固定编码
        ///// </summary>
        //public string BillDoctorIdFixedEncoding { get; set; }
        /// <summary>
        /// 调整差值
        /// </summary>
        public  decimal AdjustmentDifferenceValue { get; set; }
        /// <summary>
        /// 业务id
        /// </summary>
        public string BusinessId { get; set; }
        /// <summary>
        /// 上传状态
        /// </summary>
        public int UploadMark { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNumber { get; set; }
        /// <summary>
        /// 审核标志
        /// </summary>
        public int ApprovalMark { get; set; }
        /// <summary>
        /// 审核人员
        /// </summary>
        public string ApprovalUserName { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public string ApprovalTime { get; set; }

    }
}
