using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.BenDing.Infrastructure
{
   public interface IBenDingModificationAudited
    {
        Guid Id { get; set; }
        string UpdateUserId { get; set; }
        DateTime? UpdateTime { get; set; }
    }
}
