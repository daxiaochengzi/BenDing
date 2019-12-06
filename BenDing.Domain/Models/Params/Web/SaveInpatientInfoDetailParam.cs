using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;

namespace BenDing.Domain.Models.Params.Web
{
  public  class SaveInpatientInfoDetailParam
    {/// <summary>
    /// 数据
    /// </summary>
        public List<InpatientInfoDetailDto> DataList { get; set; }
        /// <summary>
        /// 住院id
        /// </summary>
        public string HospitalizationId { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public UserInfoDto User { get; set; }
    }
}
