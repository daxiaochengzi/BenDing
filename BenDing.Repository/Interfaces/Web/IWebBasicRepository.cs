using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;


namespace BenDing.Repository.Interfaces.Web
{
  public  interface IWebBasicRepository
    {
        Task<BasicResultDto> HIS_InterfaceListAsync(string tradeCode, string inputParameter, string operatorId);
    }
}
