using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto
{/// <summary>
/// 获取用户信息
/// </summary>
  public class UserInfoDto:IniDto
    {/// <summary>
     /// 个人编码
     /// </summary>
        public string PO_GRSHBZH { get; set; }
        /// <summary>
        /// 行政区划
        /// </summary>
        public string PO_XZQH { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string PO_XM { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string PO_XB { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public string PO_CSNY { get; set; }
        /// <summary>
        /// 险种类型310:城镇职工基本医疗保险342：城乡居民基本医疗保险根据获取的险种类型，调用对应的职工或者居民接口办理入院。
        /// </summary>
        public string PO_XZLX { get; set; }
        /// <summary>
        /// 参保状态
        /// </summary>
        public string PO_CBZT { get; set; }
        /// <summary>
        /// 报销状态
        /// </summary>
        public string PO_YBTSZT { get; set; }
        /// <summary>
        /// 报销状态说明
        /// </summary>
        public string P0_YBBXZTSM { get; set; }
        /// <summary>
        /// 人员分类
        /// </summary>
        public  string PO_RYFL { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string PO_LXDZ { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string PO_LXDH { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string PO_SFZH { get; set; }
        /// <summary>
        /// 实足年龄
        /// </summary>
        public string PO_SZNL { get; set; }
        /// <summary>
        /// 社区名称
        /// </summary>
        public string PO_SQMC { get; set; }
        /// <summary>
        /// 居民医保账户余额
        /// </summary>
        public string PO_JBZHYE { get; set; }
        /// <summary>
        /// 职工医保账户余额
        /// </summary>
        public string PO_ZGZHYE { get; set; }
        /// <summary>
        /// 门特余额
        /// </summary>
        public string PO_MTYE { get; set; }
        /// <summary>
        /// 建卡贫困户标志
        /// </summary>
        public string PO_PKHBZ { get; set; }
        /// <summary>
        /// 重点救助对象类别
        /// </summary>
        public string PO_MZJZLB { get; set; }
        /// <summary>
        /// 重点优抚对象类别
        /// </summary>
        public string PO_MZYFLB { get; set; }
        /// <summary>
        /// 民政特殊人员认定地
        /// </summary>
        public string PO_MZRDD { get; set; }
        /// <summary>
        /// 统筹支付余额
        /// </summary>
        public string PO_TCZFYE { get; set; }
        
     
    }
}
