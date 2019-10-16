using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.DifferentPlaces
{
   public class DifferentLeaveHospitalReturnParam
    {
        /// <summary>
        /// 参保人统筹地区编码
        /// </summary>
        public string baa008 { get; set; }
        /// <summary>
        /// 就诊记录号
        /// </summary>
        public string aaz217 { get; set; }
        /// <summary>
        /// 个人编号
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
        /// 出院经办人
        /// </summary>
        public string bkc131 { get; set; }
     

    }
}
