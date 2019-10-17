using BenDingWeb.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BenDingWeb.Models.Params.UI
{
   public class SingleResidentInfoDownloadUIParam
    {
        public string EmpID { get; set; }
        public List<SingleResidentInfoDto> DownloadData { get; set; }
    }
}
