using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Web
{
   public class BasicResultDto
    {
        public string Result { get; set; }
        public List<dynamic> Msg { get; set; }
    }
}
