using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Base
{
  public  class GetPostUrlDto
    {
        public decimal orderAmt { get; set; }
        public string token { get; set; }
    }
}
