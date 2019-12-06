using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;
using NFine.Code;

namespace NFine.Domain.BenDing.Infrastructure
{
 
   public class IBenDingEntity<TEntity>
    {
        public void Create(UserInfoDto user)
        {
            var entity = this as IBenDingCreationAudited;
            if (entity != null)
            {

                  
                entity.CreateUserId = user.UserId;
                entity.CreateTime = DateTime.Now;
                entity.OrganizationName = user.OrganizationName;
                entity.OrganizationCode = user.OrganizationCode;


            }

            
        }
        public void Modify(Guid id, string userId)
        {
            var entity = this as IBenDingModificationAudited;
            entity.Id = id;
            entity.UpdateUserId = userId;
            entity.UpdateTime = DateTime.Now;
        }
        public void Remove(Guid id, string userId)
        {
            var entity = this as IBenDingDeleteAudited;
            entity.Id = id;
            entity.DeleteUserId = userId;
            entity.DeleteTime = DateTime.Now;
            entity.IsDelete= true;
        }
    }
}
