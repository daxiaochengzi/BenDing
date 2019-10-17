using BenDingWeb.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BenDingWeb.WebServiceBasic
{
   public interface IWebServiceBasic
    {
        Task<BasicResultDto> HIS_InterfaceListAsync(string tradeCode, string inputParameter, string operatorId);
    }
}
