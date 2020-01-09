using System;
using System.Web.Http;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Web;
using BenDing.Domain.Xml;
using BenDing.Repository.Interfaces.Web;
using BenDing.Service.Interfaces;
using Newtonsoft.Json;

namespace NFine.Web.Controllers
{
    /// <summary>
    /// 本鼎后台系统管理
    /// </summary>
    public class SystemManagesController : ApiController
    {
        private readonly IHisSqlRepository _baseHelpRepository;
        private readonly IWebServiceBasicService _webServiceBasicService;
        private readonly IMedicalInsuranceSqlRepository _dataBaseSqlServerService;
        private readonly ISystemManageRepository _systemManage;
        private readonly IResidentMedicalInsuranceRepository _residentMedicalInsurance;
       /// <summary>
       /// 
       /// </summary>
       /// <param name="insuranceRepository"></param>
       /// <param name="iWebServiceBasicService"></param>
       /// <param name="iDataBaseSqlServerService"></param>
       /// <param name="iBaseHelpRepository"></param>
       /// <param name="iManageRepository"></param>
        public SystemManagesController(IResidentMedicalInsuranceRepository insuranceRepository,
            IWebServiceBasicService iWebServiceBasicService,
            IMedicalInsuranceSqlRepository iDataBaseSqlServerService,
            IHisSqlRepository iBaseHelpRepository,
            ISystemManageRepository iManageRepository
        )
        {
            _webServiceBasicService = iWebServiceBasicService;
            _dataBaseSqlServerService = iDataBaseSqlServerService;
            _residentMedicalInsurance = insuranceRepository;
            _baseHelpRepository = iBaseHelpRepository;
            _systemManage = iManageRepository;
        }
        /// <summary>
        /// 添加基层his账户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData AddHospitalOperator(AddHospitalOperatorParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry( y =>
             {

                     if (string.IsNullOrWhiteSpace(param.ManufacturerNumber))
                     {
                         throw new Exception("厂商编号不能为空!!!");
                     }
                     var inputParam = new UserInfoParam()
                     {
                         UserName = param.UserAccount,
                         Pwd = param.UserPwd,
                         ManufacturerNumber = param.ManufacturerNumber,
                     };
                     string inputParamJson = JsonConvert.SerializeObject(inputParam, Formatting.Indented);
                     var verificationCode = _webServiceBasicService.GetVerificationCode("01", inputParamJson);
                     if (verificationCode != null)
                     {
                         param.OrganizationCode = verificationCode.OrganizationCode;
                         param.HisUserName = verificationCode.UserName;
                     }

                     _systemManage.AddHospitalOperator(param);
                 



             });
        }

        /// <summary>
        /// 添加医保账户与基层his账户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData AddHospitalOrganizationGrade([FromBody]HospitalOrganizationGradeParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry( y =>
                 {
                     _systemManage.AddHospitalOrganizationGrade(param);
                 });
        }


    }
}


