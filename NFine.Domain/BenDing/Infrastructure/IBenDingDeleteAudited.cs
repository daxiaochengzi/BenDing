using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.BenDing.Infrastructure
{
   public interface IBenDingDeleteAudited
    {
        /// <summary>
        /// 逻辑删除标记
        /// </summary>
        bool IsDelete { get; set; }

        /// <summary>
        /// 删除实体的用户
        /// </summary>
        string DeleteUserId { get; set; }

        /// <summary>
        /// 删除实体时间
        /// </summary>
        DateTime? DeleteTime { get; set; }
        /// <summary>
        /// id
        /// </summary>

        Guid Id { get; set; }
    }
}
