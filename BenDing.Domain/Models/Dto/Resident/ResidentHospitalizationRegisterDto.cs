﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;

namespace BenDing.Domain.Models.Dto.Resident
{
  /// <summary>
     /// 入院登记
     /// </summary>
        public class ResidentHospitalizationRegisterDto : IniDto
        {/// <summary>
            /// 社保住院号
            /// </summary>
            public string PO_ZYH { get; set; }
            /// <summary>
            /// 审批编号
            /// </summary>
            public string PO_SPBH { get; set; }
            /// <summary>
            /// 本年统筹可支付金额
            /// </summary>
            public string PO_BNTCKZFJE { get; set; }
            /// <summary>
            /// 本年已住院次数
            /// </summary>
            public string PO_BNYZYCS { get; set; }
        }
}
