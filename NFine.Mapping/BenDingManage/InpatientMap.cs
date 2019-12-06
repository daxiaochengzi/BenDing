using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain._03_Entity.BenDingManage;

namespace NFine.Mapping.BenDingManage
{
  public  class InpatientMap: EntityTypeConfiguration<InpatientEntity>
    {
        public InpatientMap()
        {
            this.ToTable("Inpatient");
            this.HasKey(t => t.Id);
        }
    }
}
