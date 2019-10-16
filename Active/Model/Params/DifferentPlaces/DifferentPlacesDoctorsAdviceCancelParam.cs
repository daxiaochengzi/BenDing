using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.DifferentPlaces
{
   public class DifferentPlacesDoctorsAdviceCancelParam
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
        public DifferentPlacesDoctorsAdviceCancelZyyzmx Zyyzmx { get; set; }
    }

    public class DifferentPlacesDoctorsAdviceCancelZyyzmx
    {
        public DifferentPlacesDoctorsAdviceCancelDataRow DataRow { get; set; }
    }
    public class DifferentPlacesDoctorsAdviceCancelDataRow
    {
        public  List<DifferentPlacesDoctorsAdviceCancelRow>  Row { get; set; }
    }
    public class DifferentPlacesDoctorsAdviceCancelRow
    {  
        /// <summary>
        /// 医嘱记录序号
        /// </summary>
        public string bkc127 { get; set; }
        /// <summary>
        /// 医嘱作废时间
        /// </summary>
        public string bkc129 { get; set; }
        /// <summary>
        /// 作废原因
        /// </summary>
        public string bkc130 { get; set; }
        /// <summary>
        /// 作废经办人姓名
        /// </summary>
        public string bkc132 { get; set; }
    }
}
