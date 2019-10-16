using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params
{/// <summary>
 /// 特殊疾病下载
 /// </summary>
    public class IdentificationSpecialDownloadParam
    { /// <summary>
        /// 查询条件：病种代码  
        /// </summary>
        public string PI_CKZ735 { get; set; }
        /// <summary>
        /// 查询条件：最近更新日期  
        /// </summary>
        public string PI_AAE036 { get; set; }
        /// <summary>
        /// 查询类别  
        /// </summary>
        public string PI_CXLB { get; set; }
        /// <summary>
        /// 指定每页最大结果集条数  
        /// </summary>
        public string PI_PAGESIZE { get; set; }
        /// <summary>
        /// 指定查询第几页  
        /// </summary>
        public string PI_PAGE { get; set; }
    }
}
