using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Web
{
  public  class HospitalThreeCatalogBatchUploadDto
    {
        public Guid Id { get; set; }
        public string DirectoryCode { get; set; }
        public  string FixedEncoding { get; set; }
       
    }
}
