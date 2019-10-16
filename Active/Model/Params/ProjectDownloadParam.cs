using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params
{/// <summary>
/// 医保项目下载
/// </summary>
  public  class ProjectDownloadParam
    { ///<summary>
        /// 查询条件：最近更新日期
        /// </summary>
        public string PI_AAE036 { get; set; }
        /// <summary>
        /// 查询条件：收费项目编码
        /// </summary>
        public string PI_AKE001 { get; set; }
        /// <summary>
        /// 收费项目类别
        /// </summary>
        public string PI_CKE897 { get; set; }
        // <summary>
        /// 指定每页最大结果集条数
        /// </summary>
        public string PI_PAGESIZE { get; set; }
        /// <summary>
        /// 指定查询第几页
        /// </summary>
        public string PI_PAGE { get; set; }
        /// <summary>
        /// 查询类别
        /// </summary>
        public string PI_CXLB { get; set; }
    }
}
