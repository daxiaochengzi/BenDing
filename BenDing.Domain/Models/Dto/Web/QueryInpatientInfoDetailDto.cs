using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Web
{
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
        /// <summary>
        /// 处方固定码
        /// </summary>
        public  string RecipeCodeFixedEncoding { get; set; }
        /// <summary>
        /// 开单医生固定编码
        /// </summary>
        public string BillDoctorIdFixedEncoding { get; set; }
        /// <summary>
        /// 调整差值
        /// </summary>
        public  decimal AdjustmentDifferenceValue { get; set; }


    }
}
