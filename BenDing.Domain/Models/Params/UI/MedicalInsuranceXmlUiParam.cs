﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.UI
{
   public class MedicalInsuranceXmlUiParam
    {
        /// <summary>
        /// 业务ID
        /// </summary>
       
        public string BusinessId { get; set; }
        /// <summary>
        /// 
        /// </summary>
     
        public string TransactionId { get; set; }
       
        /// <summary>
        /// 操作人员ID
        /// </summary>

      
        public string UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
      

     
        /// <summary>
        /// 
        /// </summary>
        
        public string HealthInsuranceNo { get; set; }
    }
}
