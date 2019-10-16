using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params
{/// <summary>
 /// 特殊疾病认定
 /// </summary>
    public class IdentificationSpecialDiseasesParam
    {  /// <summary>
        /// 身份标志
        /// </summary>
        public string PI_SFBZ { get; set; }
        /// <summary>
        /// 传入标志
        /// </summary>
        public string PI_CRBZ { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string PI_AAE006 { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string PI_AAE005 { get; set; }
        /// <summary>
        /// 监护人姓名
        /// </summary>
        public string PI_AKE100 { get; set; }
        /// <summary>
        /// 监护人联系电话
        /// </summary>
        public string PI_AAE036 { get; set; }
        /// <summary>
        /// 监护人身份证号
        /// </summary>
        public string PI_AKE102 { get; set; }
        /// <summary>
        /// 认定意见
        /// </summary>
        public string PI_CKZ736 { get; set; }
        /// <summary>
        /// 认定时间
        /// </summary>
        public string PI_AAE810 { get; set; }
        /// <summary>
        /// 申报医院编号_1
        /// </summary>
        public string PI_AAZ107_1 { get; set; }
        /// <summary>
        /// 申报医院编号_2
        /// </summary>
        public string PI_AAZ107_2 { get; set; }
        /// <summary>
        /// 申报医院编号_3
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
        /// <summary>
        /// 特殊疾病编号
        /// </summary>
        public string PI_CKZ735 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<IdentificationSpecialDiseasesRowParam> PI_DYXX { get; set; }
    }
  public class IdentificationSpecialDiseasesRowParam
    {        /// <summary>
        /// 特殊疾病编号
        /// </summary>
        public string PI_CKZ735 { get; set; }
        /// <summary>
        /// 待遇享受开始年月
        /// </summary>
        public string PI_AAE041 { get; set; }
        /// <summary>
        /// 待遇享受结束年月
        /// </summary>
        public string PI_AAE042 { get; set; }
    }
}
