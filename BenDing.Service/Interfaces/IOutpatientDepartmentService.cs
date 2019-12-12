using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.OutpatientDepartment;
using BenDing.Domain.Models.Params.OutpatientDepartment;
using BenDing.Domain.Models.Params.Web;

namespace BenDing.Service.Interfaces
{
  public  interface IOutpatientDepartmentService
    {
        OutpatientDepartmentCostInputDto OutpatientDepartmentCostInput(GetOutpatientPersonParam param);
    }
}
