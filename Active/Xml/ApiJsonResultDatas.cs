using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Xml
{
  
    public class ApiJsonResultDatas
    {

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>

        public object Data { get; set; }

        public bool Code { get; set; }

      
    }

}
