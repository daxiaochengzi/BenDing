using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Code;

namespace NFine.Domain.BenDing.Infrastructure
{
 
   public class IBenDingEntity<TEntity>
    {
        public void Create(string userId)
        {
            var entity = this as IBenDingCreationAudited;
            entity.Id = Common.GuId();
            entity.CreateUserId = userId;
            entity.CreateTime = DateTime.Now;
        }
        public void Modify(string id, string userId)
        {
            var entity = this as IBenDingModificationAudited;
            entity.Id = id;
            entity.UpdateUserId = userId;
            entity.UpdateTime = DateTime.Now;
        }
        public void Remove(string id, string userId)
        {
            var entity = this as IBenDingDeleteAudited;
            entity.Id = id;
            entity.DeleteUserId = userId;
            entity.DeleteTime = DateTime.Now;
            entity.IsDelete= true;
        }
    }
}
