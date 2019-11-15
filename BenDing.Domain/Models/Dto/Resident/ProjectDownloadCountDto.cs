﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BenDing.Domain.Models.Dto.Web;

namespace BenDing.Domain.Models.Dto.Resident
{
    [XmlRoot("ROW", IsNullable = false)]
    public class ProjectDownloadCountDto : IniDto
    {/// <summary>
    /// 总条数
    /// </summary>
        [XmlElementAttribute("PO_CNT", IsNullable = false)]
        public Int64 PO_CNT { get; set; } 
    } 
    
}
