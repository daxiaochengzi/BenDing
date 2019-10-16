using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.DifferentPlaces
{
  public  class DifferentPlacesDoctorsAdviceUploadParam
    {/// <summary>
     /// 参保地统筹地区编码
     /// </summary>
        public string baa008 { get; set; }
        /// <summary>
        /// 就诊登记号
        /// </summary>
        public string aaz217 { get; set; }
        /// <summary>
        /// 个人编号
        /// </summary>
        public string aac001 { get; set; }
        /// <summary>
        /// 经办人姓名
        /// </summary>
        public string bkc131 { get; set; }
        public DifferentPlacesDoctorsAdviceZyyzmx zyyzmx { get; set; }
    }

    public class DifferentPlacesDoctorsAdviceZyyzmx
    {
        public DifferentPlacesDoctorsAdvicedatarow DataRow { get; set; }

    }
    public class DifferentPlacesDoctorsAdvicedatarow
    {
        public  List<DifferentPlacesDoctorsAdviceRow>  Row { get; set; }

    }
    public class DifferentPlacesDoctorsAdviceRow
    {
        
        /// <summary>
        /// 医嘱记录序号
        /// </summary>
        public string bkc127 { get; set; }
        /// <summary>
        /// 医嘱内容
        /// </summary>
        public string ake099 { get; set; }
        /// <summary>
        /// 医嘱时间
        /// </summary>
        public string bkc128 { get; set; }
        /// <summary>
        /// 医生编码
        /// </summary>
        public string bkc048 { get; set; }
        /// <summary>
        /// 医生姓名
        /// </summary>
        public string bkc049 { get; set; }

    }
}
