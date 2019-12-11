using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain._03_Entity.BenDingManage;
using NFine.Domain._04_IRepository.BenDingManage;
using NFine.Repository.BenDingManage;
using NFine.Repository.SystemManage;
using BenDing.Domain.Models.Dto.Web;

namespace NFine.Application.BenDingManage
{
  public  class InpatientBase
    {
        private IInpatientRepository service = new InpatientRepository();

        public List<InpatientEntity> GetList()
        {
            return service.IQueryable().ToList();
        }
        public InpatientEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(Guid keyValue)
        {
                service.Delete(t => t.Id == keyValue);
        }

        public void Insert(InpatientEntity inpatientEntity, UserInfoDto user)
        {
             inpatientEntity.Create(user);
             service.Insert(inpatientEntity);
        }

 
        //public void SubmitForm(InpatientEntity areaEntity, string keyValue)
        //{
        //    if (!string.IsNullOrEmpty(keyValue))
        //    {
        //        areaEntity.Modify(keyValue);
        //        service.Update(areaEntity);
        //    }
        //    else
        //    {
        //        areaEntity.Create();
        //        service.Insert(areaEntity);
        //    }
        //}

    }
}
