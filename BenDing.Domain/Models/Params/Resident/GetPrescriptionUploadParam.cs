using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Resident;

namespace BenDing.Domain.Models.Params.Resident
{
  public  class GetPrescriptionUploadParam
    {
        public List<PrescriptionUploadParam> UploadList { get; set; }

        public RetrunPrescriptionUploadDto RetrunUpload { get; set; }
    }
}
