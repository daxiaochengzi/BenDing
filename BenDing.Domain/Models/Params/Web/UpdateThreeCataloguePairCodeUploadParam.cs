using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;

namespace BenDing.Domain.Models.Params.Web
{
   public class UpdateThreeCataloguePairCodeUploadParam
   {
       /// <summary>
       /// 项目编码
       /// </summary>
       public List<string> ProjectCodeList { get; set; } = null;
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfoDto User { get; set; }
        
    }
}
