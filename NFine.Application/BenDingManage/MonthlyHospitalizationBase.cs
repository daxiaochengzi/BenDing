using System;
using System.Collections.Generic;
using System.Linq;
using BenDing.Domain.Models.Dto.Web;
using NFine.Domain._03_Entity.BenDingManage;
using NFine.Domain._04_IRepository.BenDingManage;
using NFine.Repository.BenDingManage;

namespace NFine.Application.BenDingManage
{/// <summary>
/// 门诊月结
/// </summary>
  public  class MonthlyHospitalizationBase
    {
        private IMonthlyHospitalizationRepository service = new MonthlyHospitalizationRepository();

        public List<MonthlyHospitalizationEntity> GetList()
        {
            return service.IQueryable().ToList();
        }
        public MonthlyHospitalizationEntity GetForm(Guid keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(Guid keyValue)
        {
            service.Delete(t => t.Id == keyValue);
        }

        public void Insert(MonthlyHospitalizationEntity inpatientEntity, UserInfoDto user)
        {
            inpatientEntity.Create(user);
            service.Insert(inpatientEntity);
        }

        public void Modify(MonthlyHospitalizationEntity inpatientEntity, UserInfoDto user,Guid id)
        {
            inpatientEntity.Modify(id, user.UserId);
            service.Update(inpatientEntity);
        }

        public int ExecuteSqlCommand(string strSql)
        {
          return  service.ExecuteSqlCommand(strSql);
        }
    }
}
