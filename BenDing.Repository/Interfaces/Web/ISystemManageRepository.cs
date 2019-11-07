using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Enums;
using BenDing.Domain.Models.Params.Web;

namespace BenDing.Repository.Interfaces.Web
{
   public interface ISystemManageRepository
    {  /// <summary>
        /// 获取所有的操作人员
        /// </summary>
        /// <returns></returns>
        Task<List<QueryHospitalOperatorAll>> QueryHospitalOperatorAll();
        /// <summary>
        /// 医院等级设置
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task AddHospitalOrganizationGrade(HospitalOrganizationGradeParam param);

        /// <summary>
        /// 操作员登陆信息查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<QueryHospitalOperatorDto> QueryHospitalOperator(QueryHospitalOperatorParam param);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>

        Task AddHospitalOperator(AddHospitalOperatorParam param);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<OrganizationGrade> QueryHospitalOrganizationGrade(string param);
    }
}
