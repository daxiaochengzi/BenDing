using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.DifferentPlaces
{
   public class DifferentPlacesCardAuthenticationTransactionParam
   {
        //<aac002>身份证号码(社会保障号码)</aac002>
        //<aac003>姓名</aac003>
        //<aaz500>社会保障卡卡号</aaz500>
        //<bkb005> Psam卡编码</bkb005>
        //<aaz501>卡的识别码</aaz501>
        //<aab034>行政区划</aab034>
        //<aae120>是否校验PSAM登记信息标志</aae120>
        //<aae140>险种类型</aae140>
        //<baa008>参保地统筹区编码</baa008>
       /// <summary>
       /// 身份证号码(社会保障号码)
        /// </summary>
        public string aac002 { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string aac003 { get; set; }
        /// <summary>
        /// 社会保障卡卡号
        /// </summary>
        public string aaz500 { get; set; }
        /// <summary>
        /// Psam卡编码
        /// </summary>
        public string bkb005 { get; set; }
        /// <summary>
        /// 卡的识别码
        /// </summary>
        public string aaz501 { get; set; }
        /// <summary>
        /// 行政区划
        /// </summary>
        public string aab034 { get; set; }
        /// <summary>
        /// 是否校验PSAM登记信息标志
        /// </summary>
        public string aae120 { get; set; }
        /// <summary>
        /// 险种类型
        /// </summary>
        public string aae140 { get; set; }
        /// <summary>
        /// 参保地统筹区编码
        /// </summary>
        public string baa008 { get; set; }
    }
}
