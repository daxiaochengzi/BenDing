using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Resident;
using BenDing.Domain.Models.Params.UI;
using BenDing.Domain.Models.Params.Web;
using BenDing.Domain.Xml;
using BenDing.Repository.Interfaces.Web;
using BenDing.Service.Interfaces;
using Newtonsoft.Json;

namespace NFine.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class BenDingController : BaseAsyncController
    {
        private IDataBaseHelpRepository _baseHelpRepository;
        private IWebServiceBasicService _webServiceBasicService;
        private IBaseSqlServerRepository _dataBaseSqlServerService;
        private ISystemManageRepository _systemManage;
        private IResidentMedicalInsuranceRepository _residentMedicalInsurance;
        private IResidentMedicalInsuranceService _residentService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="insuranceRepository"></param>
        /// <param name="iWebServiceBasicService"></param>
        /// <param name="iDataBaseSqlServerService"></param>
        /// <param name="iBaseHelpRepository"></param>
        public BenDingController(IResidentMedicalInsuranceRepository insuranceRepository,
            IWebServiceBasicService iWebServiceBasicService,
            IBaseSqlServerRepository iDataBaseSqlServerService,
            IDataBaseHelpRepository iBaseHelpRepository,
            ISystemManageRepository iManageRepository,
            IResidentMedicalInsuranceService IresidentService
            )
        {
            _webServiceBasicService = iWebServiceBasicService;
            _dataBaseSqlServerService = iDataBaseSqlServerService;
            _residentMedicalInsurance = insuranceRepository;
            _baseHelpRepository = iBaseHelpRepository;
            _systemManage = iManageRepository;
            _residentService = IresidentService;
        }
        #region 基层接口
        /// <summary>
        /// 获取登陆信息
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> GetLoginInfo(QueryHospitalOperatorParam param)
        {
            return Json(await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {

                var data = await _systemManage.QueryHospitalOperator(param);
                if (string.IsNullOrWhiteSpace(data.HisUserAccount))
                {
                    throw new Exception("当前用户未授权,基层账户信息,请重新授权!!!");
                }
                else
                {
                    var inputParam = new UserInfoParam()
                    {
                        UserName = data.HisUserAccount,
                        Pwd = data.HisUserPwd,
                        ManufacturerNumber = data.ManufacturerNumber,
                    };
                    string inputParamJson = JsonConvert.SerializeObject(inputParam, Formatting.Indented);
                    var verificationCode = await _webServiceBasicService.GetVerificationCode("01", inputParamJson);
                    y.Data = verificationCode;
                }

            }), JsonRequestBehavior.AllowGet);

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
                var userBase = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
                if (userBase != null)
                {
                    var inputInpatientInfo = new CatalogParam()
                    {
                        AuthCode = userBase.AuthCode,
                        CatalogType = param.CatalogType,
                        OrganizationCode = userBase.OrganizationCode,
                        Nums = 1000,
                    };


                    var inputInpatientInfoData = await _webServiceBasicService.GetCatalog(userBase, inputInpatientInfo);
                    if (inputInpatientInfoData.Any())
                    {
                        y.Data = inputInpatientInfoData;
                    }
                }



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
                var userBase = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
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
                var userBase = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
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
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> QueryIcd10(QueryICD10UiParam param)
        {
            return Json(await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {

                var data = await _baseHelpRepository.QueryICD10(param);
                y.Data = data;
            }));
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
                var userBase = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
                var data = await _webServiceBasicService.GetOrg(userBase, param.Name);
                y.Data = "更新机构" + data + "条";
            }), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取HIS系统中科室、医师、病区、床位的基本信息
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> GetInformation(UiInIParam param)
        {
            return Json(await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {
                var userBase = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
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
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> GetInpatientInfo(InpatientInfoUiParam param)
        {
            return Json(await new ApiJsonResultData().RunWithTryAsync(async y =>
           {
               var verificationCode = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
               var inputInpatientInfo = new InpatientInfoParam()
               {
                   AuthCode = verificationCode.AuthCode,
                   OrganizationCode = verificationCode.OrganizationCode,
                   IdCardNo = param.IdCardNo,
                   StartTime = param.StartTime,
                   EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                   State = "0"
               };

               string inputInpatientInfoJson =
                   JsonConvert.SerializeObject(inputInpatientInfo, Formatting.Indented);
               var inputInpatientInfoData = await _webServiceBasicService
                   .GetInpatientInfo(verificationCode, inputInpatientInfoJson,false);
               if (inputInpatientInfoData.Any())
               {//获取医保个人信息
                   await _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                   var userBase = new ResidentUserInfoDto();
                   userBase = await _residentMedicalInsurance.GetUserInfo(new ResidentUserInfoParam()
                   {
                       IdentityMark = "1",
                       InformationNumber = param.IdCardNo,
                   });
                   var data = inputInpatientInfoData.FirstOrDefault(c => c.BusinessId == param.BusinessId);
                   if (data != null)
                   {
                       data.MedicalInsuranceResidentInfo = userBase;
                       y.Data = data;
                   }
               }
           }));
        }
        /// <summary>
        /// 获取病人诊断
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> GetInpatientDiagnosis(InpatientInfoUiParam param)
        {
            return Json(await new ApiJsonResultData().RunWithTryAsync(async y =>
            {
                var verificationCode = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
                
                var inputInpatientInfo = new InpatientInfoParam()
                {
                    AuthCode = verificationCode.AuthCode,
                    OrganizationCode = verificationCode.OrganizationCode,
                    IdCardNo = param.IdCardNo,
                    StartTime = param.StartTime,
                    EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    State = "0"
                };

                string inputInpatientInfoJson =
                    JsonConvert.SerializeObject(inputInpatientInfo, Formatting.Indented);
                var inputInpatientInfoData = await _webServiceBasicService
                    .GetInpatientInfo(verificationCode, inputInpatientInfoJson, false);
                if (inputInpatientInfoData.Any())
                {
                    var data = inputInpatientInfoData.FirstOrDefault(c => c.BusinessId == param.BusinessId);
                    if (data != null)
                    {
                       
                        var diagnosisData = new List<InpatientDiagnosisDto> ();
                        diagnosisData.Add(new InpatientDiagnosisDto()
                        {
                            DiagnosisName = data.AdmissionMainDiagnosis,
                            DiagnosisCode = data.AdmissionMainDiagnosisIcd10,
                            IsMainDiagnosis = true
                        });
                        if (!string.IsNullOrWhiteSpace(data.LeaveHospitalMainDiagnosisIcd10))
                        {
                            diagnosisData.Add(new InpatientDiagnosisDto()
                            {
                                DiagnosisName = data.LeaveHospitalMainDiagnosis,
                                DiagnosisCode = data.LeaveHospitalMainDiagnosisIcd10,
                                IsMainDiagnosis = false
                            });
                        }

                        
                        y.Data = diagnosisData;
                    }
                }
            }));
        }
        /// <summary>
        /// 获取住院病人明细费用
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> GetInpatientInfoDetail(GetInpatientInfoDetailParam param)
        {
            return Json(await new ApiJsonResultData().RunWithTryAsync(async y =>
            {
                var inpatientInList = new List<InpatientInfoDto>();
                var verificationCode = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
                if (verificationCode != null)
                {
                    var inputInpatientInfo = new InpatientInfoParam()
                    {
                        AuthCode = verificationCode.AuthCode,
                        OrganizationCode = verificationCode.OrganizationCode,
                        IdCardNo = "512527196604306139",
                        StartTime = "2019-11-01 14:41:00",
                        EndTime = "2020-12-01 11:09:00",
                        State = "0"
                    };
                    string inputInpatientInfoJson =
                        JsonConvert.SerializeObject(inputInpatientInfo, Formatting.Indented);
                    inpatientInList = await _webServiceBasicService.GetInpatientInfo(verificationCode, inputInpatientInfoJson,true);
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
                    var data = await _webServiceBasicService.GetInpatientInfoDetail(verificationCode, InpatientInfoDetail,param.BusinessId);
                    y.Data = data;

                }
            }), JsonRequestBehavior.AllowGet);

        }
        ///// <summary>
        ///// 获取门诊病人
        ///// </summary>
        ///// <returns></returns>
        //[System.Web.Mvc.HttpGet]
        //public async Task<ActionResult> GetOutpatientPerson()
        //{
        //    return Json(await new ApiJsonResultData().RunWithTryAsync(async y =>
        //   {
        //       var verificationCode = await _webServiceBasicService.GetUserBaseInfo("");
        //       if (verificationCode != null)
        //       {
        //           var outPatient = new OutpatientParam()
        //           {
        //               验证码 = verificationCode.AuthCode,
        //               机构编码 = verificationCode.OrganizationCode,
        //               身份证号码 = "511526199610225518",
        //               开始时间 = "2019-04-27 11:09:00",
        //               结束时间 = "2020-04-27 11:09:00",

        //           };
        //           var inputInpatientInfoData = await _webServiceBasicService.GetOutpatientPerson(verificationCode, outPatient);
        //           if (inputInpatientInfoData.Any())
        //           {
        //               y.Data = inputInpatientInfoData;
        //           }
        //       }

        //   }), JsonRequestBehavior.AllowGet);
        //}
        /// <summary>
        /// 获取门诊病人明细
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> GetOutpatientDetailPerson(UiInIParam param)
        {
            return Json(await new ApiJsonResultData().RunWithTryAsync(async y =>
          {
              var verificationCode = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
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
        /// <summary>
        /// 医保信息回写至基层系统
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> SaveXmlData(UiInIParam param)
        {
            return Json(await new ApiJsonResultData().RunWithTryAsync(async y =>
            {
                var userBase = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
                var dataInfo = new ResidentUserInfoParam()
                {
                    IdentityMark = "1",
                    InformationNumber = "512501195802085180"
                };
                var strXmls = XmlSerializeHelper.XmlSerialize(dataInfo);
                var data = new SaveXmlData();
                data.OrganizationCode = userBase.OrganizationCode;
                data.AuthCode = userBase.AuthCode;
                data.BusinessId = "FFE6ADE4D0B746C58B972C7824B8C9DF";
                data.TransactionId = Guid.Parse("05D3BE09-1ED6-484F-9807-1462101F02BF").ToString("N");
                data.MedicalInsuranceBackNum = Guid.Parse("05D3BE09-1ED6-484F-9807-1462101F02BF").ToString("N");
                data.BackParam = CommonHelp.EncodeBase64("utf-8", strXmls);
                data.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmls);
                data.MedicalInsuranceCode = "21";
                data.UserId = userBase.UserId;
                //await _webServiceBasicService.SaveXmlData(data);
                var xmlData = new XmlData();
                xmlData.业务ID = data.BusinessId;
                xmlData.医保交易码 = "23";
                xmlData.发起交易的动作ID = data.TransactionId;
                xmlData.验证码 = data.AuthCode;
                xmlData.操作人员ID = data.UserId;
                xmlData.机构ID = data.OrganizationCode;
                await _webServiceBasicService.GetXmlData(xmlData);

            }), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 医保对码
        /// <summary>
        /// 基层端三大目录查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> QueryCatalog(QueryCatalogUiParam param)
        {
            var resultData = await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {
                var queryData = await _baseHelpRepository.QueryCatalog(param);
                var data = new
                {
                    data = queryData.Values.FirstOrDefault(),
                    count = queryData.Keys.FirstOrDefault()
                };
                y.Data = data;

            });
           
            return Json(resultData, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 医保中心项目查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> QueryProjectDownload(QueryProjectUiParam param)
        {
            var resultData = await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {
                var queryData = await _baseHelpRepository.QueryProjectDownload(param);
               

                var data = new
                {
                    data = queryData.Values.FirstOrDefault(),
                    count = queryData.Keys.FirstOrDefault()
                };
                y.Data = data;
            });
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 医保对码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> MedicalInsurancePairCode([FromBody]MedicalInsurancePairCodesUiParam param)
        {
            var resultData = await new ApiJsonResultData().RunWithTryAsync(async y =>
            {
                if (param.PairCodeList == null)
                {
                    throw new Exception("对码数据不能为空");
                }

                var userBase = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
                if (userBase != null && string.IsNullOrWhiteSpace(userBase.OrganizationCode) == false)
                {
                    param.OrganizationCode = userBase.OrganizationCode;
                    param.OrganizationName = userBase.OrganizationName;
                    await _dataBaseSqlServerService.MedicalInsurancePairCode(param);
                    y.Data = param;
                }
            });
            return Json(resultData);
        }
        /// <summary>
        ///对照目录中心管理 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> DirectoryComparisonManagement(DirectoryComparisonManagementUiParam param)
        {
            var resultData = await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {
                var verificationCode = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
                param.OrganizationCode = verificationCode.OrganizationCode;
                var queryData = await _dataBaseSqlServerService.DirectoryComparisonManagement(param);
                var data = new
                {
                    data = queryData.Values.FirstOrDefault(),
                    count = queryData.Keys.FirstOrDefault()
                };
                y.Data = data;
            });
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 目录对码页面
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult MedicalDirectoryPairCode(MedicalDirectoryCodePairUiParam param)
        {
            //参数可查询医保中心目录
            ViewBag.DirectoryName = param.ProjectName;
            ViewBag.DirectoryCode = param.ProjectCode;
            ViewBag.DirectoryCategoryCode = param.ProjectCodeType;
            ViewBag.empid = param.UserId;
            return View();
        }

        /// <summary>
        /// 三大目录对码信息回写至基层系统
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> ThreeCataloguePairCodeUpload(UiInIParam param)
        {
            var resultData = await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {
                var verificationCode = await _webServiceBasicService.GetUserBaseInfo(param.UserId);

                var data = await _webServiceBasicService.ThreeCataloguePairCodeUpload(verificationCode);

                y.Data = "[" + data + "] 条回写成功!!!";
            });
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 居民医保
        /// <summary>
        /// 获取居民医保信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> GetUserInfo(ResidentUserInfoParam param)
        {
            var resultData = await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {
                //医保登陆
                await _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                var userBase = await _residentMedicalInsurance.GetUserInfo(param);
                y.Data = userBase;
            });
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 项目下载
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> ProjectDownload([FromBody]ResidentProjectDownloadParam param)
        {
            var resultData = await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {
                //医保登陆
                await _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                var data = await _residentService.ProjectDownload(new ResidentProjectDownloadParam());
                y.Data = data;
            });
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 居民入院登记
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> HospitalizationRegister([FromBody]ResidentHospitalizationRegisterParam param)
        {
            var resultData = await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {
                var userBase = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
                await _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });

                param.Operators = param.UserId;

                await _residentMedicalInsurance.HospitalizationRegister(param, userBase);
                var inputInpatientInfo = new InpatientInfoParam()
                {
                    AuthCode = userBase.AuthCode,
                    OrganizationCode = userBase.OrganizationCode,
                    IdCardNo = param.IdCardNo,
                    StartTime = param.StartTime,
                    EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    State = "0",
                };
                string inputInpatientInfoJson =
                    JsonConvert.SerializeObject(inputInpatientInfo, Formatting.Indented);
                await _webServiceBasicService.GetInpatientInfo(userBase, inputInpatientInfoJson,true);

            });
            return Json(resultData);
        }
        /// <summary>
        /// 居民入院登记查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> QueryMedicalInsurance(QueryMedicalInsuranceUiParam param)
        {
            var resultData = await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {
                var userBase = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
                var queryData = await _baseHelpRepository.QueryMedicalInsurance(userBase, param.BusinessId);
                if (!string.IsNullOrWhiteSpace(queryData.AdmissionInfoJson))
                {
                    var data = JsonConvert.DeserializeObject<QueryMedicalInsuranceDetailDto>(queryData.AdmissionInfoJson);
                    data.Id = queryData.Id;
                    y.Data = data;
                }


            });
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// ICD10诊断录入弹层
        /// </summary>
        /// <param name="UserId">操作员ID</param>
        /// <returns></returns>
        public ActionResult DiagnosisIframe(string UserId)
        {
            ViewBag.title = "录入ICD-10诊断";
            ViewBag.empid = UserId;
            return View();
        }

        /// <summary>
        /// 居民入院登记修改
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> HospitalizationModify([FromBody]HospitalizationModifyParam param)
        {
            var resultData = await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {  //his登陆
                var verificationCode = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
                //医保登陆
                await _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                //医保修改
                await _residentMedicalInsurance.HospitalizationModify(param, verificationCode);
            });
            return Json(resultData);
        }
        /// <summary>
        /// 处方上传
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> PrescriptionUpload([FromBody]PrescriptionUploadUiParam param)
        {
            var resultData = await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {
                var userBase = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
                await _residentMedicalInsurance.PrescriptionUpload(param, userBase);
            });
            return Json(resultData);
        }
        /// <summary>
        /// 住院清单查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> QueryHospitalizationFee(QueryHospitalizationFeeUiParam param)
        {
            var resultData = await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {
                var queryData = await _baseHelpRepository.QueryHospitalizationFee(param);
                var data = new
                {
                    data = queryData.Values.FirstOrDefault(),
                    count = queryData.Keys.FirstOrDefault()
                };
                y.Data = data;
            });
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 医保连接
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> HospitalizationRegister(QueryHospitalOperatorParam param)
        {
            var resultData = await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {

                var data = await _systemManage.QueryHospitalOperator(param);
                if (string.IsNullOrWhiteSpace(data.MedicalInsuranceAccount))
                {
                    throw new Exception("当前用户未授权,医保账户信息,请重新授权!!!");
                }
                else
                {
                    await _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });

                }

            });
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
