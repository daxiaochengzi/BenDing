using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Domain._03_Entity.BenDingManage;
using NFine.Domain._04_IRepository.BenDingManage;
using NFine.Repository.BenDingManage;
using NFine.Repository.SystemManage;
using NFine.Code;
using NFine.Domain.Params;

namespace NFine.Application.BenDingManage
{
  public  class MonthlyHospitalizationApp
    {
        private IMonthlyHospitalizationRepository service = new MonthlyHospitalizationRepository();
        public List<MonthlyHospitalizationEntity> GetList(Pagination pagination, DoorDiagnosisMonthlyParam time)
        {
            var expression = ExtLinq.True<MonthlyHospitalizationEntity>();
            if (!string.IsNullOrEmpty(time.SettlementStartTime))
            {
                var startTime = Convert.ToDateTime(time.SettlementStartTime + " 00:00:00.000");
                expression = expression.And(t => t.StartTime >= startTime);
                if (!string.IsNullOrEmpty(time.SettlementEndTime))
                {
                    var endTime = Convert.ToDateTime(time.SettlementEndTime + " 00:00:00.000");
                    expression = expression.And(t => t.EndTime <= endTime);


                }
            }

            return service.FindList(expression, pagination);
        }
        public MonthlyHospitalizationEntity GetForm(string keyValue)
        {
            return service.FindEntity(Guid.Parse(keyValue));
        }
        public void DeleteForm(string keyValue, string userId)
        {
            var entity= service.FindEntity(keyValue);
            entity.IsDelete = true;
            entity.Modify(Guid.Parse(keyValue), userId);
            service.Update(entity);
           
        }
        public void SubmitForm(MonthlyHospitalizationEntity areaEntity, string keyValue, UserInfoDto user)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                areaEntity.Modify(Guid.Parse(keyValue), user.UserId);
                service.Update(areaEntity);
            }
            else
            {
                areaEntity.Create(user);
                service.Insert(areaEntity);
            }
        }
    }
}
