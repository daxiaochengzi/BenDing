using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto
{/// <summary>
 /// 特殊疾病用药下载 
 /// </summary>
    public class IdentificationSpecialDownloadDto : IniDto
    {
        public IdentificationSpecialDownloadRowData ROWDATA { get; set; }
    }

    public class IdentificationSpecialDownloadRowData
    {
        public IdentificationSpecialDownloadRowDataList ROW { get; set; }

    }
    public class IdentificationSpecialDownloadRowDataList
    {
        public string PO_CKZ735 { get; set; }
        public string PO_AKE001 { get; set; }
        public string PO_AKE002 { get; set; }

    }

    public class IdentificationSpecialDownloadListDto : IniDto
    {
        public IdentificationSpecialDownloadRowDataLists ROWDATA { get; set; }
    }
    public class IdentificationSpecialDownloadRowDataLists
    {
        public List<IdentificationSpecialDownloadRowDataList> ROW { get; set; }

    }

}
