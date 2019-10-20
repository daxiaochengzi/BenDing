
using System;
using System.Collections.Generic;
using System.Text;
using BenDing.Domain.Models.Dto.Web;

namespace BenDing.Domain.Models.Params.UI
{
   public class SingleResidentInfoDownloadUIParam
    {
        public string EmpID { get; set; }
        public List<SingleResidentInfoDto> DownloadData { get; set; }
    }
}
