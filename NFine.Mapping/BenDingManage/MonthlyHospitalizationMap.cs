using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Domain._03_Entity.BenDingManage;

namespace NFine.Mapping.BenDingManage
{
   public class MonthlyHospitalizationMap: EntityTypeConfiguration<MonthlyHospitalizationEntity>
    {
        public MonthlyHospitalizationMap()
        {
            this.ToTable("MonthlyHospitalization");
            this.HasKey(t => t.Id);
        }
    }
}
