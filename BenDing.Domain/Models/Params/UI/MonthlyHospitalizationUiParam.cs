using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BenDing.Domain.Models.Enums;

namespace BenDing.Domain.Models.Params.UI
{/// <summary>
/// 
/// </summary>
   public class MonthlyHospitalizationUiParam:UiInIParam
    {/// <summary>
     /// 人员类别(1:职工,2:居民)
     /// </summary>
     
        public PeopleType PeopleType { get; set; }

        ///// <summary>
        ///// 汇总类别 20-单病种住院月结 21-精神病住院结算22-门诊诊查费结
        ///// </summary>
        /////
        //[Display(Name = "汇总类别")]
        //[Required(ErrorMessage = "{0}不能为空!!!")]
        //public string SummaryType { get; set; }
        /// <summary>
        /// 报销开始日期(yyyyMMdd)
        /// </summary>
        [Display(Name = "报销开始日期")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string StartTime { get; set; }
        /// <summary>
        /// 报销结束时间(yyyyMMdd)
        /// </summary>
        [Display(Name = "报销结束时间")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string EndTime { get; set; }
        /// <summary>
        /// 结算xml
        /// </summary>
        public  string SettlementJson { get; set; }
    }
}
