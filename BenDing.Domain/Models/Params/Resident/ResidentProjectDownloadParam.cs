using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.Resident
{/// <summary>
    /// 医保项目下载
    /// </summary>
    public class ResidentProjectDownloadParam
    {///<summary>
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
        //[Display(Name = "收费项目类别")]
        //[Required(ErrorMessage = "{0}不能为空!!!")]
        //[StringLength(1, ErrorMessage = "收费项目类别输入过长，不能超过1位")]
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
        /// 查询类别 1获取总条数PO_CNT， 2实际数据
        /// </summary>
        //[Display(Name = "查询类别")]
        //[Required(ErrorMessage = "{0}不能为空!!!")]
        public string PI_CXLB { get; set; }
    }
}
