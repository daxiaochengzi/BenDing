using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.DifferentPlaces
{
   public class DifferentPlacesCardManagementDto:IniDto
    {
    
     /// <summary>
     /// 个人账户支付
     /// </summary>
        public string po_zhzfje { get; set; }
        /// <summary>
        /// 现金支付
        /// </summary>
        public string po_zfzfje { get; set; }
        /// <summary>
        /// 划卡流水号
        /// </summary>
        public string po_hklsh { get; set; }
      
    }
}
