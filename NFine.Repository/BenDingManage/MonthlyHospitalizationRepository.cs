using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Data;
using NFine.Domain._03_Entity.BenDingManage;
using NFine.Domain._04_IRepository.BenDingManage;

namespace NFine.Repository.BenDingManage
{
   public class MonthlyHospitalizationRepository:RepositoryBase<MonthlyHospitalizationEntity>, IMonthlyHospitalizationRepository
    {
    }
}
