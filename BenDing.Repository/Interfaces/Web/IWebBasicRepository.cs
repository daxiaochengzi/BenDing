using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Web;


namespace BenDing.Repository.Interfaces.Web
{
    public interface IWebBasicRepository
    {
        BasicResultDto HIS_InterfaceList(string tradeCode, string inputParameter);
        ResultDataDto HIS_Interface(string tradeCode, string inputParameter);
        void SaveXmlData(SaveXmlDataParam param);
    }
}
