using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.DifferentPlaces
{
   public class HospitalizationRegisterDifferentDto:IniDto
    {/// <summary>
     /// 个人账户余额
     /// </summary>
        public string po_grzhye { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string po_zyh { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public string po_csrq { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string po_gmsfz { get; set; }
        /// <summary>
        /// 参保身份
        /// </summary>
        public string po_cbsf { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string po_xb { get; set; }
        /// <summary>
        /// 险种类型
        /// </summary>
        public string po_xzlx { get; set; }
        /// <summary>
        /// 特殊人员类别
        /// </summary>
        public string po_tsrylb { get; set; }
        /// <summary>
        /// 参保人所属行政区划代码
        /// </summary>
        public string po_xzqh { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string po_xm { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string po_dwmc { get; set; }
        /// <summary>
        /// 单位编号
        /// </summary>
        public string po_dwbh { get; set; }
        /// <summary>
        /// 享受待遇标识
        /// </summary>
        public string po_xsdybs { get; set; }
        /// <summary>
        /// 不享受待遇原因
        /// </summary>
        public string po_bxsdyyy { get; set; }


    }
}
