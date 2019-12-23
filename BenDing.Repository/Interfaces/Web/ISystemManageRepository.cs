using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenDing.Domain.Models.Dto.SystemManage;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Enums;
using BenDing.Domain.Models.Params.SystemManage;
using BenDing.Domain.Models.Params.Web;

namespace BenDing.Repository.Interfaces.Web
{
    public interface ISystemManageRepository
    {  /// <summary>
        /// 获取所有的操作人员
        /// </summary>
        /// <returns></returns>
        List<QueryHospitalOperatorAll> QueryHospitalOperatorAll();
        /// <summary>
        /// 医院等级设置
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        void AddHospitalOrganizationGrade(HospitalOrganizationGradeParam param);

        /// <summary>
        /// 操作员登陆信息查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        QueryHospitalOperatorDto QueryHospitalOperator(QueryHospitalOperatorParam param);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>

        void AddHospitalOperator(AddHospitalOperatorParam param);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        HospitalOrganizationGradeDto QueryHospitalOrganizationGrade(string param);
        int AddHospitalLog(AddHospitalLogParam param);
    }
}
