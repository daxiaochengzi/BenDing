using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params
{
  public   class IdentificationSpecialChangParam
    {
        /// <summary>
        /// 身份标志
        /// </summary>
        public string PI_SFBZ { get; set; }
        /// <summary>
        /// 传入标志
        /// </summary>
        public string PI_CRBZ { get; set; }
        /// <summary>
        /// 新的申报医院编号_1
        /// </summary>
        public string PI_AAZ107_1 { get; set; }
        /// <summary>
        /// 新的申报医院编号_2
        /// </summary>
        public string PI_AAZ107_2 { get; set; }
        /// <summary>
        /// 新的申报医院编号_3
        /// </summary>
        public string PI_AAZ107_3 { get; set; }
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
