using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.DifferentPlaces
{
   public class DifferentPlacesCardManagementParam
    {

        /// <summary>
        /// 行政区划
        /// </summary>
        public string pi_xzqh { get; set; }
        /// <summary>
        /// 费用总额
        /// </summary>
        public string pi_fyze { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string pi_cardid { get; set; }
        /// <summary>
        /// 划卡类别
        /// </summary>
        public string pi_hklb { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        public string pi_jbr { get; set; }

    }
}
