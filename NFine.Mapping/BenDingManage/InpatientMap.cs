using System.Data.Entity.ModelConfiguration;
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
