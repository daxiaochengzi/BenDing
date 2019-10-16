using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.DifferentPlaces
{
   public class UserInfoDifferentDto:IniDto
    {/// <summary>
    /// 个人账户余额
    /// </summary>
        public string po_grzhye { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string PO_LXDH { get; set; }
        /// <summary>
        /// 实足年龄
        /// </summary>
        public string PO_SZNL { get; set; }
        /// <summary>
        /// 特殊人员身份
        /// </summary>
        ///
        public string PO_TSRYSF { get; set; }
        /// <summary>
        /// 职工类别
        /// </summary>
        public string PO_ZGLB { get; set; }
        /// <summary>
        /// 统筹支付余额
        /// </summary>
        public string PO_TCZFYE { get; set; }
        /// <summary>
        /// 险种类型310:城镇职工基本医疗保险342：城乡居民基本医疗保险根据获取的险种类型，调用对应的职工或者居民接口办理入院。
        /// </summary>
        public string PO_XZLX { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string PO_XB { get; set; }
        

        /// <summary>
        /// 行政区划
        /// </summary>
        public string PO_XZQH { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string PO_LXDZ { get; set; }
     
        /// <summary>
        /// 报销状态说明
        /// </summary>
        public string P0_YBBXZTSM { get; set; }
        /// <summary>
        /// 报销状态
        /// </summary>
        public string PO_YBTSZT { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string PO_XM { get; set; }


        /// <summary>
        /// 参保状态
        /// </summary>
        public string PO_CBZT { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string PO_SFZH { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public string PO_CSNY { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>

        public string PO_DWMC { get; set; }

        /// <summary>
        /// 个人社保号（即个人编号）
        /// </summary>
        public string PO_GRSHBZH { get; set; }
      
       
        //
       
    }
}
