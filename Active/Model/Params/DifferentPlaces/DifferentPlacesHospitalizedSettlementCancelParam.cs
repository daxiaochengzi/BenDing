using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.DifferentPlaces
{
   public class DifferentPlacesHospitalizedSettlementCancelParam
    {
        /// <summary>
        /// 参保地统筹区划代码
        /// </summary>
        public string baa008 { get; set; }
        /// <summary>
        /// 就诊记录号
        /// </summary>
        public string aaz217 { get; set; }
        /// <summary>
        /// 结算记录号
        /// </summary>
        public string aaz216 { get; set; }
        /// <summary>
        /// 个人编码
        /// </summary>
        public string aac001 { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string aac002 { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string aac003 { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        public string bkc131 { get; set; }
    }
}
