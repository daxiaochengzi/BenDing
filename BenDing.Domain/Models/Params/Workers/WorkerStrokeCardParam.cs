using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Base;
using BenDing.Domain.Models.Dto.Web;

namespace BenDing.Domain.Models.Params.Workers
{/// <summary>
/// 
/// </summary>
  public  class WorkerStrokeCardParam: WorkerBaseParam
    {/// <summary>
     /// 端口
     /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public  string  Pwd { get; set; }
        /// <summary>
        /// 合计金额(2位小数)
        /// </summary>
        public  string  AllAmount { get; set; }
        /// <summary>
        /// 卡类别(1门诊,2住院)
        /// </summary>
        public string CardType { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        public string Operators { get; set; }
        /// <summary>
        /// Id        /// </summary>
        public Guid Id { get; set; }

    }
}
