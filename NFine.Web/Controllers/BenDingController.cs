using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Resident;
using BenDing.Domain.Models.Params.Web;
using BenDing.Domain.Xml;
using BenDing.Repository.Interfaces.Web;
using BenDing.Service.Interfaces;
using Newtonsoft.Json;
using NFine.Web.Model;

namespace NFine.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class BenDingController : AsyncController
    {
        private IDataBaseHelpRepository _baseHelpRepository;
        private IWebServiceBasicService _webServiceBasicService;
        private IBaseSqlServerRepository _dataBaseSqlServerService;
        private IResidentMedicalInsuranceRepository _residentMedicalInsurance;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iGrammarNewService"></param>
        /// <param name="iWebServiceBasicService"></param>
        /// <param name="iDataBaseSqlServerService"></param>
        public BenDingController(IResidentMedicalInsuranceRepository insuranceRepository,
            IWebServiceBasicService iWebServiceBasicService,
            IBaseSqlServerRepository iDataBaseSqlServerService,
            IDataBaseHelpRepository iBaseHelpRepository
            )
        {
            _webServiceBasicService = iWebServiceBasicService;
            _dataBaseSqlServerService = iDataBaseSqlServerService;
            _residentMedicalInsurance = insuranceRepository;
            _baseHelpRepository = iBaseHelpRepository;
        }
        #region 基层接口
        // [HttpGet]
        // public string Data( string dd)
        //{

        //    return _webServiceBasicService.TestIf().ToString();

        //    //return Ok();

        //}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> GetUserBaseInfos()
        {
            var resultData = await new ApiJsonResultData().RunWithTryAsync(async y =>
             {
                 var userBase = await GetUserBaseInfo();
                 y.Data = userBase;

             });
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
        //[HttpGet]
        //public ApiJsonResultData GetUserBaseInfoss(string dd)
        //{
        //    return new ApiJsonResultData().RunWithTry(y =>
        //    {

        //        y.Data = dd;

        //    });
        //}
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        public ActionResult Test()
        {
            return View();
        }
        /// <summary>
        /// 获取三大目录
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> GetCatalog(UiCatalogParam param)
        {
            var resultData = await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {
                var userBase = await GetUserBaseInfo();
                if (userBase != null)
                {
                    var inputInpatientInfo = new CatalogParam()
                    {
                        AuthCode = userBase.AuthCode,
                        CatalogType = param.CatalogType,
                        OrganizationCode = userBase.OrganizationCode,
                        Nums = 500,
                    };


                    var inputInpatientInfoData = await _webServiceBasicService.GetCatalog(userBase, inputInpatientInfo);
                    if (inputInpatientInfoData.Any())
                    {
                        y.Data = inputInpatientInfoData;
                    }
                }

                //var data = await webService.ExecuteSp(param.Params);

            });
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除三大目录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> DeleteCatalog(UiCatalogParam param)
        {
            return Json(await new ApiJsonResultData().RunWithTryAsync(async y =>
            {
                var userBase = await GetUserBaseInfo();
                var data = await _webServiceBasicService.DeleteCatalog(userBase, Convert.ToInt16(param.CatalogType));
                y.Data = data;


            }));
        }
        /// <summary>
        /// 获取ICD10
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> GetIcd10(UiCatalogParam param)
        {
            return Json(await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {
                var userBase = await GetUserBaseInfo();
                var data = await _webServiceBasicService.GetICD10(userBase, new CatalogParam()
                {
                    OrganizationCode = userBase.OrganizationCode,
                    AuthCode = userBase.AuthCode,
                    Nums = 1000,
                    CatalogType = param.CatalogType
                });
                y.Data = data;


            }), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询ICD10
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> QueryICD10(QueryICD10UiParam param)
        {
            return Json(await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {

                var data = await _baseHelpRepository.QueryICD10(param);
                y.Data = data;
            }), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 更新机构
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> GetOrg(OrgParam param)
        {
            return Json(await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {
                var userBase = await GetUserBaseInfo();
                var data = await _webServiceBasicService.GetOrg(userBase, param.Name);
                y.Data = "更新机构" + data + "条";
            }), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取HIS系统中科室、医师、病区、床位的基本信息
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> GetInformation()
        {
            return Json(await new ApiJsonResultData().RunWithTryAsync(async y =>
            {
                var userBase = await GetUserBaseInfo();
                if (userBase != null)
                {
                    var inputInpatientInfo = new InformationParam()
                    {
                        AuthCode = userBase.AuthCode,
                        OrganizationCode = userBase.OrganizationCode,
                        DirectoryType = "1"
                    };
                    //string inputInpatientInfoJson = JsonConvert.SerializeObject(inputInpatientInfo, Formatting.Indented);

                    var inputInpatientInfoData = await _webServiceBasicService.GetInformation(userBase, inputInpatientInfo);
                    if (inputInpatientInfoData.Any())
                    {
                        y.Data = inputInpatientInfoData;
                    }
                }

                //var data = await webService.ExecuteSp(param.Params);

            }), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 住院病人信息查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> QueryMedicalInsuranceResidentInfo([FromBody] QueryMedicalInsuranceResidentInfoParam param)
        {
            return Json(await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
           {
               var data = await _dataBaseSqlServerService.QueryMedicalInsuranceResidentInfo(param);
               y.Data = data;

           }));
        }
        /// <summary>
        /// 获取住院病人
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> GetInpatientInfo()
        {
            return Json(await new ApiJsonResultData().RunWithTryAsync(async y =>
           {
               var verificationCode = await GetUserBaseInfo();
               if (verificationCode != null)
               {
                   var inputInpatientInfo = new InpatientInfoParam()
                   {
                       AuthCode = verificationCode.AuthCode,
                       OrganizationCode = verificationCode.OrganizationCode,
                       IdCardNo = "512501195802085180",
                       StartTime = "2018-04-27 11:09:00",
                       EndTime = "2020-04-27 11:09:00",
                       State = "0"
                   };
                   string inputInpatientInfoJson =
                       JsonConvert.SerializeObject(inputInpatientInfo, Formatting.Indented);

                   var inputInpatientInfoData = await _webServiceBasicService
                       .GetInpatientInfo(verificationCode, inputInpatientInfoJson);
                   if (inputInpatientInfoData.Any())
                   {
                       y.Data = inputInpatientInfoData;
                   }
               }



           }), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取住院病人明细费用
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> GetInpatientInfoDetail()
        {
            return Json(await new ApiJsonResultData().RunWithTryAsync(async y =>
            {
                var inpatientInList = new List<InpatientInfoDto>();
                var verificationCode = await GetUserBaseInfo();
                if (verificationCode != null)
                {
                    var inputInpatientInfo = new InpatientInfoParam()
                    {
                        AuthCode = verificationCode.AuthCode,
                        OrganizationCode = verificationCode.OrganizationCode,
                        IdCardNo = "511523198701122345",
                        StartTime = "2019-04-27 11:09:00",
                        EndTime = "2020-04-27 11:09:00",
                        State = "0"
                    };
                    string inputInpatientInfoJson =
                        JsonConvert.SerializeObject(inputInpatientInfo, Formatting.Indented);
                    inpatientInList = await _webServiceBasicService.GetInpatientInfo(verificationCode, inputInpatientInfoJson);
                }
                if (inpatientInList.Any())
                {
                    var inpatientIni = inpatientInList.FirstOrDefault();
                    var InpatientInfoDetail = new InpatientInfoDetailParam()
                    {
                        AuthCode = verificationCode.AuthCode,
                        HospitalizationNo = inpatientIni.HospitalizationNo,
                        BusinessId = inpatientIni.BusinessId,
                        StartTime = inpatientIni.AdmissionDate,
                        EndTime = "2020-04-27 11:09:00",
                        State = "0"
                    };
                    var data = await _webServiceBasicService.GetInpatientInfoDetail(verificationCode, InpatientInfoDetail);
                    y.Data = data;

                }
            }), JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 获取门诊病人
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> GetOutpatientPerson()
        {
            return Json(await new ApiJsonResultData().RunWithTryAsync(async y =>
           {
               var verificationCode = await GetUserBaseInfo();
               if (verificationCode != null)
               {
                   var outPatient = new OutpatientParam()
                   {
                       验证码 = verificationCode.AuthCode,
                       机构编码 = verificationCode.OrganizationCode,
                       身份证号码 = "511526199610225518",
                       开始时间 = "2019-04-27 11:09:00",
                       结束时间 = "2020-04-27 11:09:00",

                   };
                   var inputInpatientInfoData = await _webServiceBasicService.GetOutpatientPerson(verificationCode, outPatient);
                   if (inputInpatientInfoData.Any())
                   {
                       y.Data = inputInpatientInfoData;
                   }
               }

           }), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取门诊病人明细
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> GetOutpatientDetailPerson()
        {
            return Json(await new ApiJsonResultData().RunWithTryAsync(async y =>
          {
              var verificationCode = await GetUserBaseInfo();
              if (verificationCode != null)
              {
                  var outPatient = new OutpatientParam()
                  {
                      验证码 = verificationCode.AuthCode,
                      机构编码 = verificationCode.OrganizationCode,
                      身份证号码 = "511526199610225518",
                      开始时间 = "2019-04-27 11:09:00",
                      结束时间 = "2020-04-27 11:09:00",

                  };
                  var inputInpatientInfoData = await _webServiceBasicService.GetOutpatientPerson(verificationCode, outPatient);
                  if (inputInpatientInfoData.Any())
                  {
                      var inputInpatientInfoFirst = inputInpatientInfoData.FirstOrDefault();
                      var outpatientDetailParam = new OutpatientDetailParam()
                      {
                          验证码 = verificationCode.AuthCode,
                          门诊号 = inputInpatientInfoFirst.门诊号,
                          业务ID = inputInpatientInfoFirst.业务ID
                      };
                      var inputInpatientInfoDatas = await _webServiceBasicService.
                          GetOutpatientDetailPerson(verificationCode, outpatientDetailParam);
                      y.Data = inputInpatientInfoDatas;
                  }
              }

              //var data = await webService.ExecuteSp(param.Params);

          }), JsonRequestBehavior.AllowGet);
        }
        [System.Web.Mvc.NonAction]
        private async Task<UserInfoDto> GetUserBaseInfo()
        {
            var inputParam = new UserInfoParam()
            {
                UserName = "liqian",
                Pwd = "123",
                ManufacturerNumber = "510303001",
            };
            string inputParamJson = JsonConvert.SerializeObject(inputParam, Formatting.Indented);
            var verificationCode = await _webServiceBasicService.GetVerificationCode("01", inputParamJson);
            return verificationCode;
        }
        #endregion
        #region 居民医保
        /// <summary>
        /// 获取居民医保信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> GetUserInfo(ResidentUserInfoParam param)
        {
            var resultData = await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {

                var login = BaseConnect.Connect();
                if (login == 1)
                {
                    var userBase =await _residentMedicalInsurance.GetUserInfo(param);
                    y.Data = userBase;
                }
                else
                {
                    y.AddErrorMessage("登陆失败!!!");
                }
            });
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 项目下载
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> ProjectDownload(ResidentProjectDownloadParam param)
        {
            var resultData = await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {
                
                var login = 1;
                if (login == 1)
                {
                    var userBase = await _residentMedicalInsurance.ProjectDownload(new ResidentProjectDownloadParam());
                   
                    if (userBase.Row!=null && userBase.Row.Any())
                    {
                       
                        await _baseHelpRepository.ProjectDownload(new UserInfoDto() { UserId = "E075AC49FCE443778F897CF839F3B924" }, userBase.Row);
                    }


                    // y.Data = userBase;
                }
                else
                {
                    y.AddErrorMessage("登陆失败!!!");
                }



            });
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 入院登记
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> HospitalizationRegister(ResidentUserInfoParam param)
        {
            var resultData = await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {

                var login = BaseConnect.Connect();
                if (login == 1)
                {
                    var paramIni = new ResidentHospitalizationRegisterParam();
                    var userBase = await _residentMedicalInsurance.HospitalizationRegister(paramIni);
                    y.Data = userBase;
                }
                else
                {
                    y.AddErrorMessage("登陆失败!!!");
                }



            });
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
