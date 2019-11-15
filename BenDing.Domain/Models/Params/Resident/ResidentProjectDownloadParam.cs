using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.UI;

namespace BenDing.Domain.Models.Params.Resident
{/// <summary>
 /// 医保项目下载
 /// </summary>
    [XmlRoot("ROW", IsNullable = false)]
    public class ResidentProjectDownloadParam: UiInIParam
    {///<summary>
     /// 查询条件：最近更新日期   [XmlElementAttribute("PI_AAE036", IsNullable = false)]
     /// </summary>
        [XmlElementAttribute("PI_AAE036", IsNullable = false)]
        public string UpdateTime { get; set; }

        /// <summary>
        /// 查询条件：收费项目编码
        /// </summary>
        [XmlElementAttribute("PI_AKE001", IsNullable = false)]
        public string ProjectCode { get; set; }

        /// <summary>
        /// 收费项目类别
        /// </summary>
        //[Display(Name = "收费项目类别")]
        //[Required(ErrorMessage = "{0}不能为空!!!")]
        //[StringLength(1, ErrorMessage = "收费项目类别输入过长，不能超过1位")]ProjectCodeTyp
        [XmlElementAttribute("PI_CKE897", IsNullable = false)]
        public string ProjectCodeType { get; set; }
        /// <summary>
        /// 条数 (最多每页500条)
        /// </summary>
        [XmlElementAttribute("PI_PAGESIZE", IsNullable = false)]
        [Display(Name = "条数")]
        [Range(typeof(int), "1", "500")]
        public int Limit { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        [XmlElementAttribute("PI_PAGE", IsNullable = false)]
        public int Page { get; set; }
        /// <summary>
        /// 查询类别 (1:获取总条数;2:实际数据)
        /// </summary>
        [Display(Name = "查询类别")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        [XmlElementAttribute("PI_CXLB", IsNullable = false)]
        public int QueryType { get; set; }
    }
}
