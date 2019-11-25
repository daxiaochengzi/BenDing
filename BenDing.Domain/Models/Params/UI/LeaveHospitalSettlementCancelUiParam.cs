﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.UI
{/// <summary>
/// 
/// </summary>
 public   class LeaveHospitalSettlementCancelUiParam: HospitalizationPresettlementUiParam
    { /// <summary>
      /// 取消度取消程度(1取消结算2删除资料)病人办理出院后，若需要取消：应先调用接口取消结算，再调用接口删除资料
      /// </summary>
        [Display(Name = "取消度取消程度")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string CancelLimit { get; set; }
    }
}
