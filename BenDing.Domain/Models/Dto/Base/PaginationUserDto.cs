using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Base
{/// <summary>
/// 
/// </summary>
   public class PaginationUserDto
   {
       public int Limit { get; set; }

       public int Page { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        //[JsonProperty(PropertyName = "UserId")]
        [XmlIgnore]
        [JsonIgnore]
        public string UserId { get; set; }
       /// <summary>
       /// 组织机构
       /// </summary>
        public string OrganizationCode { get; set; }
        //    /// <summary>
        //    /// 每页行数
        //    /// </summary>
        //    [Display(Name = "每页行数")]
        //    [Required(ErrorMessage = "{0}不能为空!!!")]
        //    public int rows { get; set; }
        //    /// <summary>
        //    /// 当前页
        //    /// </summary>
        //    public int page { get; set; }
        //    /// <summary>
        //    /// 排序列
        //    /// </summary>
        //    public string sidx { get; set; }
        //    /// <summary>
        //    /// 排序类型
        //    /// </summary>
        //    public string sord { get; set; }
        //    /// <summary>
        //    /// 总记录数
        //    /// </summary>
        //    public int records { get; set; }

        //    /// <summary>
        //    /// 总页数
        //    /// </summary>
        //    public int total
        //    {
        //        get
        //        {
        //            if (records > 0)
        //            {
        //                return records % this.rows == 0 ? records / this.rows : records / this.rows + 1;
        //            }
        //            else
        //            {
        //                return 0;
        //            }
        //        }
        //    }
    }
}
