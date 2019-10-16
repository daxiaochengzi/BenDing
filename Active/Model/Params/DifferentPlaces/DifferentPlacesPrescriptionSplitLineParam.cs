using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.DifferentPlaces
{
  public  class DifferentPlacesPrescriptionSplitLineParam
    {
        /// <summary>
        /// 参保人统筹区代码
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
        /// 处方号
        /// </summary>
        public string aae072 { get; set; }
        /// <summary>
        /// 收费开始时间
        /// </summary>
        public string aae030 { get; set; }
        /// <summary>
        /// 收费终止时间
        /// </summary>
        public string aae031 { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public string bke019 { get; set; }

    }
}
