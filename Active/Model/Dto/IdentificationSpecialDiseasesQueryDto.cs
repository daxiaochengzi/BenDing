using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto
{/// <summary>
/// 特殊疾病查询
/// </summary>
   public  class IdentificationSpecialDiseasesQueryDto : IniDto
    {

        /// <summary>
        /// 认定业务流水号
        /// </summary>
        public string PO_AKC603 { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string PO_AAC002 { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string PO_AAC003 { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string PO_AAC006 { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string PO_AAE006 { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string PO_AAE005 { get; set; }
        /// <summary>
        /// 监护人姓名
        /// </summary>
        public string PO_AKE100 { get; set; }
        /// <summary>
        /// 监护人联系电话
        /// </summary>
        public string PO_AKE101 { get; set; }
        /// <summary>
        /// 监护人身份证号
        /// </summary>
        public string PO_AKE102 { get; set; }
        /// <summary>
        /// 认定意见
        /// </summary>
        public string PO_CKZ736 { get; set; }
        /// <summary>
        /// 认定医院编号
        /// </summary>
        public string PO_AAZ107 { get; set; }
        /// <summary>
        /// 认定医院名称
        /// </summary>
        public string PO_CKB519 { get; set; }
        /// <summary>
        /// 经办时间
        /// </summary>
        public string PO_AAE036 { get; set; }
        /// <summary>
        /// 认定时间
        /// </summary>
        public string PO_AAE810 { get; set; }
        /// <summary>
        /// 认定时的人员身份
        /// </summary>
        public string PO_CKA549 { get; set; }
        /// <summary>
        /// 特殊疾病编号
        /// </summary>
        public string PO_CKZ735 { get; set; }
        /// <summary>
        /// 特殊疾病名称
        /// </summary>
        public string PO_JBMC { get; set; }
        /// <summary>
        /// 待遇享受开始年月
        /// </summary>
        public string PO_AAE041 { get; set; }
        /// <summary>
        /// 待遇享受结束年月
        /// </summary>
        public string PO_AAE042 { get; set; }
    }
}
