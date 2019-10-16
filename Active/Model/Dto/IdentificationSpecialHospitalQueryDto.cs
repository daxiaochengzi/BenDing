using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto
{
   public class IdentificationSpecialHospitalQueryDto:IniDto
    {
        /// <summary>
        /// 申报医院编号_1
        /// </summary>
        public string PO_AAZ107_1 { get; set; }
        /// <summary>
        /// 医院名称1
        /// </summary>
        public string PO_CKB519_1 { get; set; }
        /// <summary>
        /// 申报医院编号2
        /// </summary>
        public string PO_AAZ107_2 { get; set; }
        /// <summary>
        /// 医院名称2
        /// </summary>
        public string PO_CKB519_2 { get; set; }
        /// <summary>
        /// 申报医院编号3
        /// </summary>
        public string PO_AAZ107_3 { get; set; }
        /// <summary>
        /// 医院名称3
        /// </summary>
        public string PO_CKB519_3 { get; set; }
        /// <summary>
        /// 市外三级医院名称1
        /// </summary>
        public string PI_AKB021_1 { get; set; }
        /// <summary>
        /// 市外三级医院名称2
        /// </summary>
        public string PI_AKB021_2 { get; set; }
        /// <summary>
        /// 市外三级医院名称3
        /// </summary>
        public string PI_AKB021_3 { get; set; }
        
    }
}
