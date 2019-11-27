using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using BenDing.Domain.Models.Dto;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Base;
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
        private IWebBasicRepository _webServiceBasic;
        private IHisSqlRepository _hisSqlRepository;
        private IWebServiceBasicService _webServiceBasicService;
        private IMedicalInsuranceSqlRepository _medicalInsuranceSqlRepository;
        private ISystemManageRepository _systemManage;
        private IResidentMedicalInsuranceRepository _residentMedicalInsurance;
        private IResidentMedicalInsuranceService _residentService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="insuranceRepository"></param>
        /// <param name="webServiceBasicService"></param>
        /// <param name="medicalInsuranceSqlRepository"></param>
        /// <param name="hisSqlRepository"></param>
        /// <param name="manageRepository"></param>
        /// <param name="iresidentService"></param>
        /// <param name="webServiceBasic"></param>
        public BenDingController(IResidentMedicalInsuranceRepository insuranceRepository,
            IWebServiceBasicService webServiceBasicService,
            IMedicalInsuranceSqlRepository medicalInsuranceSqlRepository,
            IHisSqlRepository hisSqlRepository,
            ISystemManageRepository manageRepository,
            IResidentMedicalInsuranceService iresidentService,
            IWebBasicRepository webServiceBasic
            )
        {
            _webServiceBasicService = webServiceBasicService;
            _medicalInsuranceSqlRepository = medicalInsuranceSqlRepository;
            _residentMedicalInsurance = insuranceRepository;
            _hisSqlRepository = hisSqlRepository;
            _systemManage = manageRepository;
            _residentService = iresidentService;
            _webServiceBasic = webServiceBasic;
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

                var data = await _hisSqlRepository.QueryICD10(param);
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
        public async Task<ActionResult> GetInformation(GetInformationUiParam param)
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
                        DirectoryType = param.DirectoryType
                    };
                    var inputInpatientInfoData = await _webServiceBasicService.SaveInformation(userBase, inputInpatientInfo);
                    if (inputInpatientInfoData.Any())
                    {
                        y.Data = inputInpatientInfoData;
                    }
                }
            }), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询HIS系统中科室、医师、病区、床位的基本信息
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> QueryInformationInfo(GetInformationUiParam param)
        {
            return Json(await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {
                var userBase = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
                if (userBase != null)
                {
                    var queryParam = new InformationParam()
                    {
                        OrganizationCode = userBase.OrganizationCode,
                        DirectoryType = param.DirectoryType
                    };
                    var data = await _hisSqlRepository.QueryInformationInfo(queryParam);
                    y.Data = data;
                }
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
               var data = await _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(param);
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
               var infoData = new GetInpatientInfoParam()
               {
                   User = verificationCode,
                   BusinessId = param.BusinessId,
                   InfoParam = inputInpatientInfo,
               };
               var inpatientData = await _webServiceBasicService.GetInpatientInfo(infoData);
               if (!string.IsNullOrWhiteSpace(inpatientData.BusinessId))
               {//获取医保个人信息
                   await _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });

                   var userBase = await _residentMedicalInsurance.GetUserInfo(new ResidentUserInfoParam()
                   {
                       IdentityMark = "1",
                       InformationNumber = param.IdCardNo,
                   });
                   var data = inpatientData;

                   data.MedicalInsuranceResidentInfo = userBase;
                   y.Data = data;

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
                var infoData = new GetInpatientInfoParam()
                {
                    User = verificationCode,
                    BusinessId = param.BusinessId,
                    InfoParam = inputInpatientInfo,
                };
                var inpatientData = await _webServiceBasicService.GetInpatientInfo(infoData);
                if (!string.IsNullOrWhiteSpace(inpatientData.BusinessId))
                {

                    var diagnosisData = new List<InpatientDiagnosisDto>();
                    diagnosisData.Add(new InpatientDiagnosisDto()
                    {
                        DiagnosisName = inpatientData.AdmissionMainDiagnosis,
                        DiagnosisCode = inpatientData.AdmissionMainDiagnosisIcd10,
                        IsMainDiagnosis = true
                    });
                    if (!string.IsNullOrWhiteSpace(inpatientData.LeaveHospitalMainDiagnosisIcd10))
                    {
                        diagnosisData.Add(new InpatientDiagnosisDto()
                        {
                            DiagnosisName = inpatientData.LeaveHospitalMainDiagnosis,
                            DiagnosisCode = inpatientData.LeaveHospitalMainDiagnosisIcd10,
                            IsMainDiagnosis = false
                        });
                    }


                    y.Data = diagnosisData;

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

                var verificationCode = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
                if (verificationCode != null)
                {
                    var inpatientData = await _hisSqlRepository.QueryInpatientInfo(new QueryInpatientInfoParam() { BusinessId = param.BusinessId });

                    var InpatientInfoDetail = new InpatientInfoDetailParam()
                    {
                        AuthCode = verificationCode.AuthCode,
                        HospitalizationNo = inpatientData.HospitalizationNo,
                        BusinessId = inpatientData.BusinessId,
                        StartTime = inpatientData.AdmissionDate,
                        EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        State = "0"
                    };
                    var data = await _webServiceBasicService.GetInpatientInfoDetail(verificationCode,
                        InpatientInfoDetail, param.BusinessId);
                    y.Data = data;

                }

            }), JsonRequestBehavior.AllowGet);

        }
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
        /// <summary>
        /// 基层入院登记取消
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> HospitalizationRegisterCancel(BaseUiBusinessIdDataParam param)
        {
            return Json(await new ApiJsonResultData().RunWithTryAsync(async y =>
            {
                var dd = new ResidentUserInfoParam();
                dd.IdentityMark = "1";
                dd.InformationNumber = "111";
                var userBase = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
                var strXmlIntoParam = XmlSerializeHelper.XmlSerialize(dd);
                var strXmlBackParam = XmlSerializeHelper.XmlSerialize(dd);
                var saveXmlData = new SaveXmlData();
                saveXmlData.OrganizationCode = userBase.OrganizationCode;
                saveXmlData.AuthCode = userBase.AuthCode;
                saveXmlData.BusinessId = param.BusinessId;
                saveXmlData.TransactionId = param.BusinessId;
                saveXmlData.MedicalInsuranceBackNum = "CXJB004";
                saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
                saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
                saveXmlData.MedicalInsuranceCode = "22";
                saveXmlData.UserId = param.UserId;
                await _webServiceBasic.HIS_InterfaceListAsync("38", JsonConvert.SerializeObject(saveXmlData), userBase.UserId);

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
                var queryData = await _hisSqlRepository.QueryCatalog(param);
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
                var queryData = await _medicalInsuranceSqlRepository.QueryProjectDownload(param);


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
                    await _medicalInsuranceSqlRepository.MedicalInsurancePairCode(param);
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
                param.State = 0;
                var queryData = await _medicalInsuranceSqlRepository.DirectoryComparisonManagement(param);
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
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> ProjectDownload(ResidentProjectDownloadParam param)
        {
            var resultData = await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {
                //医保登陆
                await _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                var data = await _residentService.ProjectDownload(param);
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

                if (param.DiagnosisList != null && param.DiagnosisList.Any())
                {   //主诊断
                    var mainDiagnosis = param.DiagnosisList.FirstOrDefault(c => c.IsMainDiagnosis == true);
                    if (mainDiagnosis == null) throw new Exception("主诊断不能为空!!!");
                    param.AdmissionMainDiagnosisIcd10 = mainDiagnosis.DiagnosisCode;
                    param.AdmissionMainDiagnosis = mainDiagnosis.DiagnosisName;
                    //次诊断
                    var nextDiagnosis = param.DiagnosisList.Where(c => c.IsMainDiagnosis == false).ToList();
                    int num = 1;
                    foreach (var item in nextDiagnosis)
                    {
                        if (num == 1) param.DiagnosisIcd10Two = item.DiagnosisCode;
                        if (num == 2) param.DiagnosisIcd10Three = item.DiagnosisCode;
                        num++;
                    }
                    var userBase = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
                    var inputInpatientInfo = new InpatientInfoParam()
                    {
                        AuthCode = userBase.AuthCode,
                        OrganizationCode = userBase.OrganizationCode,
                        IdCardNo = param.IdCardNo,
                        StartTime = Convert.ToDateTime(param.StartTime).ToString("yyyy-MM-dd HH:mm:ss"),
                        EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        State = "0",
                    };
                    var infoData = new GetInpatientInfoParam()
                    {
                        User = userBase,
                        BusinessId = param.BusinessId,
                        InfoParam = inputInpatientInfo,
                        IsSave = false,
                    };
                    var inpatientData = await _webServiceBasicService.GetInpatientInfo(infoData);
                    if (!string.IsNullOrWhiteSpace(inpatientData.BusinessId))
                    {  //医保登录
                        await _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                        param.Operators = param.UserId;
                        //入院登记
                        await _residentMedicalInsurance.HospitalizationRegister(param, userBase);
                    }
                    else
                    {
                        infoData.IsSave = true;
                        //保存病人信息
                        await _webServiceBasicService.GetInpatientInfo(infoData);
                    }

                }
                else
                {
                    throw new Exception("诊断不能为空!!!");
                }



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
                var data = await _webServiceBasicService.QueryMedicalInsuranceDetail(param);
                y.Data = data;

            });
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 居民入院登记修改
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> HospitalizationModify([FromBody]HospitalizationModifyUiParam param)
        {
            var resultData = await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {  //his登陆
                var verificationCode = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
                //医保登陆
                await _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                if (param.DiagnosisList != null && param.DiagnosisList.Any())
                {   //主诊断
                    //医保修改
                    var modifyParam = new HospitalizationModifyParam()
                    {
                        AdmissionDate = param.AdmissionDate,
                        BedNumber = param.BedNumber,
                        BusinessId = param.BusinessId,
                        FetusNumber = param.FetusNumber,
                        HouseholdNature = param.HouseholdNature,
                        MedicalInsuranceHospitalizationNo = param.MedicalInsuranceHospitalizationNo,
                        TransactionId = param.TransactionId,
                        UserId = param.UserId,
                        InpatientDepartmentCode = param.InpatientDepartmentCode,
                        HospitalizationNo = CommonHelp.GuidToStr(param.BusinessId)
                    };
                    var mainDiagnosis = param.DiagnosisList.FirstOrDefault(c => c.IsMainDiagnosis == true);
                    if (mainDiagnosis == null) throw new Exception("主诊断不能为空!!!");
                    modifyParam.AdmissionMainDiagnosisIcd10 = mainDiagnosis.DiagnosisCode;
                    modifyParam.AdmissionMainDiagnosis = mainDiagnosis.DiagnosisName;

                    //次诊断
                    var nextDiagnosis = param.DiagnosisList.Where(c => c.IsMainDiagnosis == false).ToList();
                    int num = 1;
                    foreach (var item in nextDiagnosis)
                    {
                        if (num == 1) modifyParam.DiagnosisIcd10Two = item.DiagnosisCode;
                        if (num == 2) modifyParam.DiagnosisIcd10Three = item.DiagnosisCode;
                        num++;
                    }


                    await _residentMedicalInsurance.HospitalizationModify(modifyParam, verificationCode);
                }
                else
                {
                    throw new Exception("诊断不能为空!!!");
                }
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
                var queryData = await _hisSqlRepository.QueryHospitalizationFee(param);
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
        /// 处方上传
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> PrescriptionUpload([FromBody]PrescriptionUploadUiParam param)
        {
            var resultData = await new ApiJsonResultData().RunWithTryAsync(async y =>
            {
                var userBase = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
                //医保登录
                await _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                //处方上传
                var data = await _residentMedicalInsurance.PrescriptionUpload(param, userBase);
                if (!string.IsNullOrWhiteSpace(data.Msg))
                {
                    throw new Exception(data.Msg);
                }
                else
                {
                    if (data.Num > 0)
                    {
                        y.Data = "处方上传成功" + data.Num + " 条,失败" + (data.Count - data.Num) + " 条";
                    }
                }
            });
            return Json(resultData);
        }
        /// <summary>
        /// 处方自动上传
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> PrescriptionUploadAutomatic(PrescriptionUploadAutomaticUiParam param)
        {
            var resultData = await new ApiJsonResultData().RunWithTryAsync(async y =>
            {
                var userBase = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
                //医保登录
                await _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                await _residentService.PrescriptionUploadAutomatic(new PrescriptionUploadAutomaticParam()
                { IsTodayUpload = param.IsTodayUpload }, userBase);

            });
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除处方数据
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> DeletePrescriptionUpload(BaseUiBusinessIdDataParam param)
        {
            var resultData = await new ApiJsonResultData().RunWithTryAsync(async y =>
            {
                var userBase = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
                //获取医保病人信息
                var residentDataParam = new QueryMedicalInsuranceResidentInfoParam()
                {
                    BusinessId = param.BusinessId,
                    OrganizationCode = userBase.OrganizationCode,
                };
                //获取病人明细
                var queryData = await _hisSqlRepository.InpatientInfoDetailQuery
                              (new InpatientInfoDetailQueryParam() { BusinessId = param.BusinessId });
                var residentData = await _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(residentDataParam);
                if (queryData.Any())
                {
                    //获取已上传数据、
                    var uploadDataId = queryData.Where(c => c.UploadMark == 1).Select(d => d.Id).ToList();
                    var batchNumberList = queryData.Where(c => c.UploadMark == 1).GroupBy(d => d.BatchNumber).Select(b => b.Key).ToList();
                    if (batchNumberList != null && batchNumberList.Any())
                    {
                        var batchNumberArray = string.Join(",", batchNumberList.ToArray());
                        var deleteParam = new DeletePrescriptionUploadParam()
                        {
                            BatchNumber = string.Join(",", batchNumberList.ToArray()),
                            MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo
                        };
                        //医保登录
                        await _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                        await _residentMedicalInsurance.DeletePrescriptionUpload(deleteParam, uploadDataId, userBase);
                    }




                }



            });
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 医保处方明细查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> QueryPrescriptionDetail(BaseUiBusinessIdDataParam param)
        {
            var resultData = await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {
                //var queryData = await _medicalInsuranceSqlRepository.QueryProjectDownload(param);
                //var data = new 
                //{
                //    data = queryData.Values.FirstOrDefault(),
                //    count = queryData.Keys.FirstOrDefault()
                //};
                //获取操作人员信息

                var userBase = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
                var queryResidentParam = new QueryMedicalInsuranceResidentInfoParam()
                {
                    BusinessId = param.BusinessId,
                    OrganizationCode = userBase.OrganizationCode
                };
                //医保登录
                //await _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                //获取医保病人信息
                var residentData = await _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
                var data = await _residentMedicalInsurance.QueryPrescriptionDetail(new QueryPrescriptionDetailParam() 
                          { MedicalInsuranceHospitalizationNo= residentData .MedicalInsuranceHospitalizationNo});
                y.Data = data;
            });
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 医保住院费用预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> HospitalizationPresettlement(HospitalizationPresettlementUiParam param)
        {
            var resultData = await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {   //获取操作人员信息

                var userBase = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
                var queryResidentParam = new QueryMedicalInsuranceResidentInfoParam()
                {
                    BusinessId = param.BusinessId,
                    OrganizationCode = userBase.OrganizationCode
                };
                //获取医保病人信息
                var residentData = await _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
                //医保登录
                await _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                var presettlementParam = new HospitalizationPresettlementParam()
                {
                    MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo,
                    LeaveHospitalDate = DateTime.Now.ToString("yyyyMMdd"),
                };

                var data = await _residentMedicalInsurance.HospitalizationPresettlement(presettlementParam, userBase);
                y.Data = data;
            });
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 医保出院费用结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> LeaveHospitalSettlement(LeaveHospitalSettlementUiParam param)
        {
            var resultData = await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {   //获取操作人员信息

                var userBase = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
                var queryResidentParam = new QueryMedicalInsuranceResidentInfoParam()
                {
                    BusinessId = param.BusinessId,
                    OrganizationCode = userBase.OrganizationCode
                };
                //获取医保病人信息
                var residentData = await _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
                if (residentData != null)
                {
                    if (!string.IsNullOrWhiteSpace(residentData.TransactionId))
                    {
                        throw new Exception("当前病人已办理结算");
                    }
                    else
                    {
                        var inpatientInfoParam = new QueryInpatientInfoParam() { BusinessId = param.BusinessId };
                        //获取住院病人
                        var InpatientInfoData = await _hisSqlRepository.QueryInpatientInfo(inpatientInfoParam);
                        //医保登录
                        await _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                        var presettlementParam = new LeaveHospitalSettlementParam()
                        {
                            MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo,
                            // LeaveHospitalDate = (!string.IsNullOrWhiteSpace(InpatientInfoData.LeaveHospitalDate)) == true ? Convert.ToDateTime(InpatientInfoData.LeaveHospitalDate).ToString("yyyyMMdd") : DateTime.Now.ToString("yyyyMMdd"),
                            //LeaveHospitalMainDiagnosisIcd10 = InpatientInfoData.LeaveHospitalMainDiagnosisIcd10,
                            //LeaveHospitalDiagnosisIcd10Two = InpatientInfoData.LeaveHospitalSecondaryDiagnosisIcd10,
                            //LeaveHospitalMainDiagnosis = InpatientInfoData.LeaveHospitalMainDiagnosis,
                           
                            LeaveHospitalDate = DateTime.Now.ToString("yyyyMMdd"),
                            LeaveHospitalMainDiagnosisIcd10 = InpatientInfoData.AdmissionMainDiagnosisIcd10,
                            //LeaveHospitalDiagnosisIcd10Two = InpatientInfoData.LeaveHospitalSecondaryDiagnosisIcd10,
                            LeaveHospitalMainDiagnosis = InpatientInfoData.AdmissionSecondaryDiagnosis,
                            UserId = CommonHelp.GuidToStr(userBase.UserId),
                            LeaveHospitalInpatientState = param.LeaveHospitalInpatientState

                        };
                        var InfoParam = new LeaveHospitalSettlementInfoParam()
                        {
                            user = userBase,
                            BusinessId = param.BusinessId,
                            Id = residentData.Id,
                            InsuranceNo = residentData.InsuranceNo,
                        };
                        var data = await _residentMedicalInsurance.LeaveHospitalSettlement(presettlementParam, InfoParam);
                        y.Data = data;
                    }

                }

            });
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询医保出院费用结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> QueryLeaveHospitalSettlement(QueryHospitalizationPresettlementUiParam param)
        {
            var resultData = await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {   //获取操作人员信息

                var userBase = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
                var queryResidentParam = new QueryMedicalInsuranceResidentInfoParam()
                {
                    BusinessId = param.BusinessId,
                    OrganizationCode = userBase.OrganizationCode
                };
                //医保登录
                await _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                //获取医保病人信息
                var residentData = await _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
                if (residentData != null)
                {
                    var data = await _residentMedicalInsurance.QueryLeaveHospitalSettlement(new QueryLeaveHospitalSettlementParam() { MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo });
                    y.Data = data;

                }

            });
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 取消医保出院费用结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> LeaveHospitalSettlementCancel(LeaveHospitalSettlementCancelUiParam param)
        {
            var resultData = await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {   //获取操作人员信息

                var userBase = await _webServiceBasicService.GetUserBaseInfo(param.UserId);
                var queryResidentParam = new QueryMedicalInsuranceResidentInfoParam()
                {
                    BusinessId = param.BusinessId,
                    OrganizationCode = userBase.OrganizationCode
                };
                //医保登录
                await _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                //获取医保病人信息
                var residentData = await _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
                if (residentData != null)
                {
                    var settlementCancelParam = new LeaveHospitalSettlementCancelParam()
                    {
                        MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo,
                        SettlementNo = residentData.SettlementNo,
                        Operators = CommonHelp.GuidToStr(userBase.UserId),
                        CancelLimit = param.CancelLimit,
                    };
                    var cancelParam = new LeaveHospitalSettlementCancelInfoParam()
                    {
                        BusinessId = param.BusinessId,
                        Id = residentData.Id,
                        User = userBase,
                    };
                    await _residentMedicalInsurance.LeaveHospitalSettlementCancel(settlementCancelParam, cancelParam);


                }

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
