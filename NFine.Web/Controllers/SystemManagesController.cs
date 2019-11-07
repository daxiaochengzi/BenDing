using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.UI;
using BenDing.Domain.Models.Params.Web;
using BenDing.Domain.Xml;
using BenDing.Repository.Interfaces.Web;
using BenDing.Service.Interfaces;
using Newtonsoft.Json;

namespace NFine.Web.Controllers
{
    /// <summary>
    /// 本鼎系统管理
    /// </summary>
    public class SystemManagesController : Controller
    {
        private IDataBaseHelpRepository _baseHelpRepository;
        private IWebServiceBasicService _webServiceBasicService;
        private IBaseSqlServerRepository _dataBaseSqlServerService;
        private ISystemManageRepository _systemManage;
        private IResidentMedicalInsuranceRepository _residentMedicalInsurance;
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
            IBaseSqlServerRepository iDataBaseSqlServerService,
            IDataBaseHelpRepository iBaseHelpRepository,
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
        /// 添加医保账户与基层his账户
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> AddHospitalOperator(AddHospitalOperatorParam param)
        {
            return Json(await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {
                if (param.IsHis)
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
                    var verificationCode = await _webServiceBasicService.GetVerificationCode("01", inputParamJson);
                    if (verificationCode != null)
                    {
                        param.OrganizationCode = verificationCode.OrganizationCode;
                        param.HisUserName = verificationCode.UserName;
                    }

                    await _systemManage.AddHospitalOperator(param);
                }
                else
                {
                    var login = MedicalInsuranceDll.ConnectAppServer_cxjb(param.UserAccount, param.UserPwd);
                    if (login != 1)
                    {
                        throw new Exception("医保登陆失败,请核对账户与密码!!!");
                    }
                    else
                    {
                        await _systemManage.AddHospitalOperator(param);
                    }
                }


            }));
        }

        /// <summary>
        /// 添加医保账户与基层his账户
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> AddHospitalOrganizationGrade([FromBody]HospitalOrganizationGradeParam param)
        {
            return Json(await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
                {
                    await _systemManage.AddHospitalOrganizationGrade(param);
                }));
        }


    }
}


