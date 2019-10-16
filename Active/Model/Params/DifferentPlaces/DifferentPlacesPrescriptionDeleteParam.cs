using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.DifferentPlaces
{
  public  class DifferentPlacesPrescriptionDeleteParam
    {
        /// <summary>
        /// 参保地统筹区代码
        /// </summary>
        public string baa008 { get; set; }
        /// <summary>
        /// 就诊记录号*
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
        /// 处方序号
        /// </summary>
        public PrescriptionDeleteCfxh Cfxh { get; set; }

        /// <summary>
        /// 处方日期
        /// </summary>
        public string bkc040 { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        public string bkc131 { get; set; }
    }

    public class PrescriptionDeleteCfxh
    {
        public PrescriptionDeleteDatarow DataRow { get; set; }
    }
    public class PrescriptionDeleteDatarow
    {
        public PrescriptionDeleteRow Row { get; set; }
    }
    public class PrescriptionDeleteRow
    {/// <summary>
    /// 项目序号
    /// </summary>
        public Int32 bke019 { get; set; }
    }
}
