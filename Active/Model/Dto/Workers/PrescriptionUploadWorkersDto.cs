using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.Workers
{/// <summary>
    /// 处方项目传输  
    /// </summary>
    public class PrescriptionUploadWorkersDto:IniDto
    {/// <summary>
        /// 批次号(本次上传)  
        /// </summary>
        public string Po_pch { get; set; }
    }
}
