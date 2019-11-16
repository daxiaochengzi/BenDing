using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Resident
{
   public class ResidentProjectDownloadJsonDto
    {
        public ResidentProjectDownloadRowDataJsonDto RowData { get; set; }
    }

    public class ResidentProjectDownloadRowDataJsonDto
    {
        public List<ResidentProjectDownloadRowDataRowDto> Row { get; set; }
    }



}
