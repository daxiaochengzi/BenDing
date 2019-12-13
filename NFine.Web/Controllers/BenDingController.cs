﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BenDing.Domain.Models.Dto.OutpatientDepartment;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Enums;
using BenDing.Domain.Models.Params.Base;
using BenDing.Domain.Models.Params.OutpatientDepartment;
using BenDing.Domain.Models.Params.Resident;
using BenDing.Domain.Models.Params.UI;
using BenDing.Domain.Models.Params.Web;
using BenDing.Domain.Xml;
using BenDing.Repository.Interfaces.Web;
using BenDing.Repository.Providers.Web;
using BenDing.Service.Interfaces;
using Newtonsoft.Json;

namespace NFine.Web.Controllers
{
    /// <summary>
    /// 本鼎接口
    /// </summary>
    public class BenDingController : ApiController
    {
        private readonly IWebBasicRepository _webServiceBasic;
        private readonly IHisSqlRepository _hisSqlRepository;
        private readonly IWebServiceBasicService _webServiceBasicService;
        private readonly IMedicalInsuranceSqlRepository _medicalInsuranceSqlRepository;
        private readonly ISystemManageRepository _systemManage;
        private readonly IResidentMedicalInsuranceRepository _residentMedicalInsurance;
        private readonly IResidentMedicalInsuranceService _residentService;
        private readonly IOutpatientDepartmentService _outpatientDepartmentService;
        private readonly IOutpatientDepartmentRepository _outpatientDepartmentRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="insuranceRepository"></param>
        /// <param name="webServiceBasicService"></param>
        /// <param name="medicalInsuranceSqlRepository"></param>
        /// <param name="hisSqlRepository"></param>
        /// <param name="manageRepository"></param>
        /// <param name="residentMedicalInsuranceService"></param>
        /// <param name="webServiceBasic"></param>
        /// <param name="outpatientDepartmentService"></param>
        /// <param name="outpatientDepartmentRepository"></param>
        public BenDingController(IResidentMedicalInsuranceRepository insuranceRepository,
            IWebServiceBasicService webServiceBasicService,
            IMedicalInsuranceSqlRepository medicalInsuranceSqlRepository,
            IHisSqlRepository hisSqlRepository,
            ISystemManageRepository manageRepository,
            IResidentMedicalInsuranceService residentMedicalInsuranceService,
            IWebBasicRepository webServiceBasic,
            IOutpatientDepartmentService outpatientDepartmentService,
            IOutpatientDepartmentRepository outpatientDepartmentRepository
            )
        {
            _webServiceBasicService = webServiceBasicService;
            _medicalInsuranceSqlRepository = medicalInsuranceSqlRepository;
            _residentMedicalInsurance = insuranceRepository;
            _hisSqlRepository = hisSqlRepository;
            _systemManage = manageRepository;
            _residentService = residentMedicalInsuranceService;
            _webServiceBasic = webServiceBasic;
            _outpatientDepartmentService = outpatientDepartmentService;
            _outpatientDepartmentRepository = outpatientDepartmentRepository;
        }
        #region 基层接口
        /// <summary>
        /// 获取登陆信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData GetLoginInfo([FromUri]QueryHospitalOperatorParam param)
        {
            return new ApiJsonResultData(ModelState, new UserInfoDto()).RunWithTry(y =>
           {

               var data = _systemManage.QueryHospitalOperator(param);
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
                   var userBase = _webServiceBasicService.GetVerificationCode("01", inputParamJson);
                   y.Data = userBase;
               }

           });


        }
        /// <summary>
        /// 获取三大目录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData GetCatalog([FromUri]UiCatalogParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
             {
                 var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                 if (userBase != null)
                 {
                     var inputInpatientInfo = new CatalogParam()
                     {
                         AuthCode = userBase.AuthCode,
                         CatalogType = param.CatalogType,
                         OrganizationCode = userBase.OrganizationCode,
                         Nums = 1000,
                     };
                     var inputInpatientInfoData = _webServiceBasicService.GetCatalog(userBase, inputInpatientInfo);
                     if (inputInpatientInfoData.Any())
                     {
                         y.Data = inputInpatientInfoData;
                     }
                 }



             });

        }
        /// <summary>
        /// 删除三大目录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData DeleteCatalog([FromUri]UiCatalogParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                var data = _webServiceBasicService.DeleteCatalog(userBase, Convert.ToInt16(param.CatalogType));
                y.Data = data;


            });
        }
        /// <summary>
        /// 获取ICD10
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData GetIcd10([FromUri]UiCatalogParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                var data = _webServiceBasicService.GetIcd10(userBase, new CatalogParam()
                {
                    OrganizationCode = userBase.OrganizationCode,
                    AuthCode = userBase.AuthCode,
                    Nums = 1000,
                    CatalogType = param.CatalogType
                });
                y.Data = data;


            });
        }
        /// <summary>
        /// 查询ICD10
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData QueryIcd10([FromBody]QueryICD10UiParam param)
        {
            return new ApiJsonResultData(ModelState, new QueryICD10InfoDto()).RunWithTry(y =>
             {

                 var data = _hisSqlRepository.QueryICD10(param);
                 y.Data = data;
             });
        }
        /// <summary>
        /// 更新机构
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData GetOrg([FromUri]OrgParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                var data = _webServiceBasicService.GetOrg(userBase, param.Name);
                y.Data = "更新机构" + data + "条";
            });
        }
        /// <summary>
        /// 获取HIS系统中科室、医师、病区、床位的基本信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData GetInformation([FromUri]GetInformationUiParam param)
        {
            return new ApiJsonResultData(ModelState, new InformationDto()).RunWithTry(y =>
           {
               var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
               if (userBase != null)
               {
                   var inputInpatientInfo = new InformationParam()
                   {
                       AuthCode = userBase.AuthCode,
                       OrganizationCode = userBase.OrganizationCode,
                       DirectoryType = param.DirectoryType
                   };
                   var inputInpatientInfoData = _webServiceBasicService.SaveInformation(userBase, inputInpatientInfo);
                   if (inputInpatientInfoData.Any())
                   {
                       y.Data = inputInpatientInfoData;
                   }
               }
           });
        }
        /// <summary>
        /// 查询HIS系统中科室、医师、病区、床位的基本信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData QueryInformationInfo([FromUri]GetInformationUiParam param)
        {
            return new ApiJsonResultData(ModelState, new QueryInformationInfoDto()).RunWithTry(y =>
           {
               var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
               if (userBase != null)
               {
                   var queryParam = new InformationParam()
                   {
                       OrganizationCode = userBase.OrganizationCode,
                       DirectoryType = param.DirectoryType
                   };
                   var data = _hisSqlRepository.QueryInformationInfo(queryParam);
                   y.Data = data;
               }
           });
        }
        /// <summary>
        /// 住院病人信息查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData QueryMedicalInsuranceResidentInfo([FromBody] QueryMedicalInsuranceResidentInfoParam param)
        {
            return new ApiJsonResultData(ModelState, new MedicalInsuranceResidentInfoDto()).RunWithTry(y =>
           {
               var data = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(param);
               y.Data = data;

           });
        }
        /// <summary>
        /// 获取住院病人
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData GetInpatientInfo([FromBody]InpatientInfoUiParam param)
        {
            return new ApiJsonResultData(ModelState, new ResidentUserInfoDto()).RunWithTry(y =>
           {
               var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
               var infoData = new GetInpatientInfoParam()
               {
                   User = userBase,
                   BusinessId = param.BusinessId,
                   IsSave = true
               };
               var inpatientData = _webServiceBasicService.GetInpatientInfo(infoData);

               //if (!string.IsNullOrWhiteSpace(inpatientData.BusinessId))
               //{//获取医保个人信息
               //    _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });

               //    var residentUserBase = _residentMedicalInsurance.GetUserInfo(new ResidentUserInfoParam()
               //    {
               //        IdentityMark = "1",
               //        InformationNumber = inpatientData.IdCardNo,
               //    });
               //    var data = inpatientData;

               //    data.MedicalInsuranceResidentInfo = residentUserBase;
               //    y.Data = data;

               //}
           });
        }
        /// <summary>
        /// 获取病人诊断
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData GetInpatientDiagnosis([FromBody]InpatientInfoUiParam param)
        {
            return new ApiJsonResultData(ModelState, new InpatientDiagnosisDto()).RunWithTry(y =>
            {
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);

                var infoData = new GetInpatientInfoParam()
                {
                    User = userBase,
                    BusinessId = param.BusinessId,
                };
                var inpatientData = _webServiceBasicService.GetInpatientInfo(infoData);
                if (inpatientData.DiagnosisList != null && inpatientData.DiagnosisList.Any())
                {
                    y.Data = inpatientData.DiagnosisList;
                }
            });
        }
        /// <summary>
        /// 获取住院病人明细费用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData GetInpatientInfoDetail([FromUri]GetInpatientInfoDetailParam param)
        {
            return new ApiJsonResultData(ModelState, new InpatientInfoDetailDto()).RunWithTry(y =>
           {

               var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
               if (userBase != null)
               {
                   var data = _webServiceBasicService.GetInpatientInfoDetail(userBase, param.BusinessId);
                   y.Data = data;

               }

           });

        }
        /// <summary>
        /// 获取门诊病人
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData GetOutpatient([FromUri] GetOutpatientUiParam param)
        {
            return new ApiJsonResultData(ModelState, new BaseOutpatientInfoDto()).RunWithTry(y =>
            {
                var baseUser = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                var paramIni = new GetOutpatientPersonParam();
                if (baseUser != null)
                {
                    paramIni.User = baseUser;
                    paramIni.IsSave = false;
                    paramIni.UiParam = param;
                    var data = _webServiceBasicService.GetOutpatientPerson(paramIni);
                    y.Data = data;

                }

            });
        }

        /// <summary>
        /// 获取门诊病人明细
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData GetOutpatientDetail([FromUri]BaseUiBusinessIdDataParam param)
        {
            return new ApiJsonResultData(ModelState, new BaseOutpatientInfoDto()).RunWithTry(y =>
          {
              var queryData = _hisSqlRepository.QueryOutpatient(new QueryOutpatientParam() { BusinessId = param.BusinessId });
              if (queryData != null)
              {
                  var baseUser = _webServiceBasicService.GetUserBaseInfo(param.UserId);

                  var outpatientDetailParam = new OutpatientDetailParam()
                  {
                      AuthCode = baseUser.AuthCode,
                      OutpatientNo = queryData.OutpatientNumber,
                      BusinessId = param.BusinessId
                  };
                  var data = _webServiceBasicService.GetOutpatientDetailPerson(baseUser, outpatientDetailParam);
                  y.Data = data;

              }
          });
        }
        /// <summary>
        /// 医保信息回写至基层系统
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData SaveXmlData([FromUri]UiInIParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                //var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                //var dataInfo = new ResidentUserInfoParam()
                //{
                //    IdentityMark = "1",
                //    InformationNumber = "512501195802085180"
                //};
                //var strXmls = XmlSerializeHelper.XmlSerialize(dataInfo);
                //var data = new SaveXmlData();
                //data.OrganizationCode = userBase.OrganizationCode;
                //data.AuthCode = userBase.AuthCode;
                //data.BusinessId = "FFE6ADE4D0B746C58B972C7824B8C9DF";
                //data.TransactionId = Guid.Parse("05D3BE09-1ED6-484F-9807-1462101F02BF").ToString("N");
                //data.MedicalInsuranceBackNum = Guid.Parse("05D3BE09-1ED6-484F-9807-1462101F02BF").ToString("N");
                //data.BackParam = CommonHelp.EncodeBase64("utf-8", strXmls);
                //data.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmls);
                //data.MedicalInsuranceCode = "21";
                //data.UserId = userBase.UserId;
                //// _webServiceBasicService.SaveXmlData(data);
                //var xmlData = new XmlData();
                //xmlData.业务ID = data.BusinessId;
                //xmlData.医保交易码 = "23";
                //xmlData.发起交易的动作ID = data.TransactionId;
                //xmlData.验证码 = data.AuthCode;
                //xmlData.操作人员ID = data.UserId;
                //xmlData.机构ID = data.OrganizationCode;
                //_webServiceBasicService.GetXmlData(xmlData);

            });
        }
        /// <summary>
        /// 基层入院登记取消
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData HospitalizationRegisterCancel([FromUri]BaseUiBusinessIdDataParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                var dd = new ResidentUserInfoParam();
                dd.IdentityMark = "1";
                dd.InformationNumber = "111";
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
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
                _webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData));

            });
        }
        #endregion
        #region 医保对码
        /// <summary>
        /// 基层端三大目录查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData QueryCatalog([FromUri]QueryCatalogUiParam param)
        {
            return new ApiJsonResultData(ModelState, new QueryCatalogDto()).RunWithTry(y =>
           {
               var queryData = _hisSqlRepository.QueryCatalog(param);
               var data = new
               {
                   data = queryData.Values.FirstOrDefault(),
                   count = queryData.Keys.FirstOrDefault()
               };
               y.Data = data;

           });



        }
        /// <summary>
        /// 医保中心项目查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData QueryProjectDownload([FromUri]QueryProjectUiParam param)
        {
            return new ApiJsonResultData(ModelState, new ResidentProjectDownloadRow()).RunWithTry(y =>
            {
                var queryData = _medicalInsuranceSqlRepository.QueryProjectDownload(param);
                var data = new
                {
                    data = queryData.Values.FirstOrDefault(),
                    count = queryData.Keys.FirstOrDefault()
                };
                y.Data = data;
            });

        }
        /// <summary>
        /// 医保对码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData MedicalInsurancePairCode([FromBody]MedicalInsurancePairCodesUiParam param)
        {
            return new ApiJsonResultData(ModelState, new MedicalInsurancePairCodesUiParam()).RunWithTry(y =>
           {
               if (param.PairCodeList == null)
               {
                   throw new Exception("对码数据不能为空");
               }

               var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
               if (userBase != null && string.IsNullOrWhiteSpace(userBase.OrganizationCode) == false)
               {
                   param.OrganizationCode = userBase.OrganizationCode;
                   param.OrganizationName = userBase.OrganizationName;
                   _medicalInsuranceSqlRepository.MedicalInsurancePairCode(param);
                   _webServiceBasicService.ThreeCataloguePairCodeUpload(
                        new UpdateThreeCataloguePairCodeUploadParam()
                        {
                            User = userBase,
                            ProjectCodeList = param.PairCodeList.Select(c => c.ProjectCode).ToList()
                        }
                    );

               }
           });

        }
        /// <summary>
        ///对照目录中心管理 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData DirectoryComparisonManagement([FromUri]DirectoryComparisonManagementUiParam param)
        {
            return new ApiJsonResultData(ModelState, new DirectoryComparisonManagementDto()).RunWithTry(y =>
             {
                 var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                 param.OrganizationCode = userBase.OrganizationCode;
                 param.State = 0;
                 var queryData = _medicalInsuranceSqlRepository.DirectoryComparisonManagement(param);
                 var data = new
                 {
                     data = queryData.Values.FirstOrDefault(),
                     count = queryData.Keys.FirstOrDefault()
                 };
                 y.Data = data;
             });
        }
        /// <summary>
        /// 三大目录对码信息回写至基层系统
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData ThreeCataloguePairCodeUpload([FromUri]UiInIParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);

                var data = _webServiceBasicService.ThreeCataloguePairCodeUpload(
                    new UpdateThreeCataloguePairCodeUploadParam()
                    {
                        User = userBase,
                        ProjectCodeList = new List<string>()
                    }
                    );

                y.Data = "[" + data + "] 条数据回写成功!!!";
            });

        }
        #endregion
        #region 居民住院医保
        /// <summary>
        /// 获取居民医保信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData GetUserInfo([FromBody]ResidentUserInfoParam param)
        {
            return new ApiJsonResultData(ModelState, new ResidentUserInfoDto()).RunWithTry(y =>
             {
                //医保登陆
                _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                 var userBase = _residentMedicalInsurance.GetUserInfo(param);
                 y.Data = userBase;
             });

        }
        /// <summary>
        /// 项目下载
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData ProjectDownload([FromUri]ResidentProjectDownloadParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                //医保登陆
                _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                var data = _residentService.ProjectDownload(param);
                y.Data = data;
            });

        }
        /// <summary>
        /// 居民入院登记
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData HospitalizationRegister([FromBody]ResidentHospitalizationRegisterUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                var iniParam = new ResidentHospitalizationRegisterParam();
                if (param.DiagnosisList != null && param.DiagnosisList.Any())
                {   //主诊断
                    var mainDiagnosisList = param.DiagnosisList.Where(c => c.IsMainDiagnosis = true)
                        .Select(d => d.DiagnosisCode).Take(3).ToList();
                    if (mainDiagnosisList.Any() == false) throw new Exception("主诊断不能为空!!!");
                    iniParam.AdmissionMainDiagnosisIcd10 = CommonHelp.DiagnosisStr(mainDiagnosisList);
                    //第二诊断
                    var nextDiagnosisList = param.DiagnosisList.Where(c => c.IsMainDiagnosis = false)
                        .Select(d => d.DiagnosisCode).ToList();
                    if (mainDiagnosisList.Any())
                    {
                        var diagnosisIcd10Two = nextDiagnosisList.Take(3).ToList();
                        iniParam.DiagnosisIcd10Two = CommonHelp.DiagnosisStr(diagnosisIcd10Two);
                        if (nextDiagnosisList.Count > 3)
                        {//第三诊断
                            iniParam.DiagnosisIcd10Three = CommonHelp.DiagnosisStr(nextDiagnosisList.Where(d => !diagnosisIcd10Two.Contains(d)).Take(3).ToList());
                        }
                    }
                    var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                    var infoData = new GetInpatientInfoParam()
                    {
                        User = userBase,
                        BusinessId = param.BusinessId,
                        IsSave = false,
                    };
                    var inpatientData = _webServiceBasicService.GetInpatientInfo(infoData);
                    if (!string.IsNullOrWhiteSpace(inpatientData.BusinessId))
                    {  //医保登录
                        _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                        iniParam.IdentityMark = param.IdentityMark;
                        iniParam.AfferentSign = param.AfferentSign;
                        iniParam.MedicalCategory = param.MedicalCategory;
                        iniParam.FetusNumber = param.FetusNumber;
                        iniParam.HouseholdNature = param.HouseholdNature;
                        iniParam.AdmissionDate = Convert.ToDateTime(inpatientData.AdmissionDate).ToString("yyyyMMdd");
                        iniParam.AdmissionMainDiagnosis = inpatientData.AdmissionMainDiagnosis.Substring(0, inpatientData.AdmissionMainDiagnosis.Length > 100 ? 100 : inpatientData.AdmissionMainDiagnosis.Length);
                        iniParam.InpatientDepartmentCode = inpatientData.InDepartmentName;
                        iniParam.BedNumber = inpatientData.AdmissionBed;
                        iniParam.HospitalizationNo = inpatientData.HospitalizationNo;
                        iniParam.Operators = inpatientData.AdmissionOperator;
                        iniParam.InsuranceType = param.InsuranceType;
                        iniParam.BusinessId = param.BusinessId;
                        //入院登记
                        _residentMedicalInsurance.HospitalizationRegister(iniParam, userBase);
                    }
                    else
                    {
                        infoData.IsSave = true;
                        //保存病人信息
                        _webServiceBasicService.GetInpatientInfo(infoData);
                    }

                }
                else
                {
                    throw new Exception("诊断不能为空!!!");
                }
            });

        }
        /// <summary>
        /// 居民入院登记查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData QueryMedicalInsurance([FromUri]QueryMedicalInsuranceUiParam param)
        {
            return new ApiJsonResultData(ModelState, new QueryMedicalInsuranceDetailInfoDto()).RunWithTry(y =>
            {
                var data = _webServiceBasicService.QueryMedicalInsuranceDetail(param);
                y.Data = data;

            });

        }
        /// <summary>
        /// 居民入院登记修改
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData HospitalizationModify([FromBody]HospitalizationModifyUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {  //his登陆
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                //医保登陆
                _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                if (param.DiagnosisList != null && param.DiagnosisList.Any())
                {   //主诊断
                    //医保修改
                    var modifyParam = new HospitalizationModifyParam()
                    {
                        AdmissionDate = Convert.ToDateTime(param.AdmissionDate).ToString("yyyyMMdd"),
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
                    var mainDiagnosis = param.DiagnosisList.FirstOrDefault(c => c.IsMainDiagnosis);
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
                    _residentMedicalInsurance.HospitalizationModify(modifyParam, userBase);
                }
                else
                {
                    throw new Exception("诊断不能为空!!!");
                }
            });

        }
        /// <summary>
        /// 住院清单查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData QueryHospitalizationFee([FromUri]QueryHospitalizationFeeUiParam param)
        {
            return new ApiJsonResultData(ModelState, new QueryHospitalizationFeeDto()).RunWithTry(y =>
           {
               var queryData = _hisSqlRepository.QueryHospitalizationFee(param);
               var data = new
               {
                   data = queryData.Values.FirstOrDefault(),
                   count = queryData.Keys.FirstOrDefault()
               };
               y.Data = data;
           });

        }
        /// <summary>
        /// 处方上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData PrescriptionUpload([FromBody]PrescriptionUploadUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
           {
               var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
               //医保登录
               _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
               //处方上传
               var data = _residentMedicalInsurance.PrescriptionUpload(param, userBase);
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

        }
        /// <summary>
        /// 处方自动上传
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData PrescriptionUploadAutomatic([FromUri]PrescriptionUploadAutomaticUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                //医保登录
                _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                _residentService.PrescriptionUploadAutomatic(new PrescriptionUploadAutomaticParam()
                { IsTodayUpload = param.IsTodayUpload }, userBase);

            });

        }
        /// <summary>
        /// 删除处方数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData DeletePrescriptionUpload([FromUri]BaseUiBusinessIdDataParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                //获取医保病人信息
                var residentDataParam = new QueryMedicalInsuranceResidentInfoParam()
                {
                    BusinessId = param.BusinessId,
                    OrganizationCode = userBase.OrganizationCode,
                };
                //获取病人明细
                var queryData = _hisSqlRepository.InpatientInfoDetailQuery
                             (new InpatientInfoDetailQueryParam() { BusinessId = param.BusinessId });
                var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(residentDataParam);
                if (queryData.Any())
                {
                    //获取已上传数据、
                    var uploadDataId = queryData.Where(c => c.UploadMark == 1).Select(d => d.Id).ToList();
                    var batchNumberList = queryData.Where(c => c.UploadMark == 1).GroupBy(d => d.BatchNumber).Select(b => b.Key).ToList();
                    if (batchNumberList.Any())
                    {
                        var deleteParam = new DeletePrescriptionUploadParam()
                        {
                            BatchNumber = string.Join(",", batchNumberList.ToArray()),
                            MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo
                        };
                        //医保登录
                        _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                        _residentMedicalInsurance.DeletePrescriptionUpload(deleteParam, uploadDataId, userBase);
                    }
                }
            });

        }
        /// <summary>
        /// 医保处方明细查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData QueryPrescriptionDetail([FromUri]QueryPrescriptionDetailUiParam param)
        {
            return new ApiJsonResultData(ModelState, new QueryPrescriptionDetailListDto()).RunWithTry(y =>
          {
              //获取操作人员信息
              var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
              var queryResidentParam = new QueryMedicalInsuranceResidentInfoParam()
              {
                  BusinessId = param.BusinessId,
                  OrganizationCode = userBase.OrganizationCode
              };
              //医保登录
              _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
              //获取医保病人信息
              var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
              var queryData = _residentMedicalInsurance.QueryPrescriptionDetail(new QueryPrescriptionDetailParam()
              { MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo });
              //分页
              var data = new
              {
                  data = queryData.Skip(param.Limit * (param.Page - 1)).Take(param.Limit),
                  count = queryData.Count()
              };
              y.Data = data;
          });

        }
        /// <summary>
        /// 医保住院费用预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData HospitalizationPreSettlement([FromUri]HospitalizationPresettlementUiParam param)
        {
            return new ApiJsonResultData(ModelState, new HospitalizationPresettlementDto()).RunWithTry(y =>
          {   //获取操作人员信息
             var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
              var queryResidentParam = new QueryMedicalInsuranceResidentInfoParam()
              {
                  BusinessId = param.BusinessId,
                  OrganizationCode = userBase.OrganizationCode
              };
             //获取医保病人信息
             var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
              if (residentData.MedicalInsuranceState == MedicalInsuranceState.医保结算) throw new Exception("当前病人已医保结算,不能预结算");
             //医保登录
             _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
              var presettlementParam = new HospitalizationPresettlementParam()
              {
                  MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo,
                  LeaveHospitalDate = DateTime.Now.ToString("yyyyMMdd"),
              };
              var infoParam = new HospitalizationPreSettlementInfoParam()
              {
                  User = userBase,
                  Id = residentData.Id,
                  BusinessId = param.BusinessId,
              };
              var data = _residentMedicalInsurance.HospitalizationPreSettlement(presettlementParam, infoParam);

              y.Data = data;
          });

        }
        /// <summary>
        /// 医保出院费用结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData LeaveHospitalSettlement([FromUri]LeaveHospitalSettlementUiParam param)
        {
            return new ApiJsonResultData(ModelState, new HospitalizationPresettlementDto()).RunWithTry(y =>
           {   //获取操作人员信息

               var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
               var queryResidentParam = new QueryMedicalInsuranceResidentInfoParam()
               {
                   BusinessId = param.BusinessId,
                   OrganizationCode = userBase.OrganizationCode
               };
               //获取医保病人信息
               var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
               if (residentData.MedicalInsuranceState == MedicalInsuranceState.医保结算) throw new Exception("当前病人已办理医保结算,不能办理预结算!!!");
               if (residentData.MedicalInsuranceState == MedicalInsuranceState.医保入院) throw new Exception("当前病人未办理预结算,不能办理结算!!!");
               var inpatientInfoParam = new QueryInpatientInfoParam() { BusinessId = param.BusinessId };
               //获取住院病人
               var inpatientInfoData = _hisSqlRepository.QueryInpatientInfo(inpatientInfoParam);
               if (inpatientInfoData == null) throw new Exception("该病人未在中心库中,请检查是否办理医保入院!!!");

               //医保登录
               _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
               var presettlementParam = new LeaveHospitalSettlementParam()
               {
                   MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo,
                   LeaveHospitalDate = (!string.IsNullOrWhiteSpace(inpatientInfoData.LeaveHospitalDate)) ? Convert.ToDateTime(inpatientInfoData.LeaveHospitalDate).ToString("yyyyMMdd") : DateTime.Now.ToString("yyyyMMdd"),
                   LeaveHospitalMainDiagnosisIcd10 = inpatientInfoData.LeaveHospitalMainDiagnosisIcd10,
                   //LeaveHospitalDiagnosisIcd10Two = inpatientInfoData.LeaveHospitalSecondaryDiagnosisIcd10,
                   LeaveHospitalMainDiagnosis = inpatientInfoData.LeaveHospitalMainDiagnosis,
                   //LeaveHospitalDate = DateTime.Now.ToString("yyyyMMdd"),
                   //LeaveHospitalMainDiagnosisIcd10 = InpatientInfoData.AdmissionMainDiagnosisIcd10,
                   ////LeaveHospitalDiagnosisIcd10Two = InpatientInfoData.LeaveHospitalSecondaryDiagnosisIcd10,
                   //LeaveHospitalMainDiagnosis = InpatientInfoData.AdmissionSecondaryDiagnosis,
                   UserId = CommonHelp.GuidToStr(userBase.UserId),
                   LeaveHospitalInpatientState = param.LeaveHospitalInpatientState
               };
               var infoParam = new LeaveHospitalSettlementInfoParam()
               {
                   User = userBase,
                   Id = residentData.Id,
                   InsuranceNo = residentData.InsuranceNo,
                   BusinessId = inpatientInfoData.BusinessId,
               };
               var data = _residentMedicalInsurance.LeaveHospitalSettlement(presettlementParam, infoParam);
               y.Data = data;


           });

        }
        /// <summary>
        /// 查询医保出院费用结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData QueryLeaveHospitalSettlement([FromUri]QueryHospitalizationPresettlementUiParam param)
        {
            return new ApiJsonResultData(ModelState, new HospitalizationPresettlementDto()).RunWithTry(y =>
            {   //获取操作人员信息

               var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                var queryResidentParam = new QueryMedicalInsuranceResidentInfoParam()
                {
                    BusinessId = param.BusinessId,
                    OrganizationCode = userBase.OrganizationCode
                };
               //医保登录
               _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
               //获取医保病人信息
               var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
                if (residentData != null)
                {
                    var data = _residentMedicalInsurance.QueryLeaveHospitalSettlement(new QueryLeaveHospitalSettlementParam() { MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo });
                    y.Data = data;

                }

            });

        }
        /// <summary>
        /// 取消医保出院费用结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData LeaveHospitalSettlementCancel([FromUri]LeaveHospitalSettlementCancelUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
           {   //获取操作人员信息

               var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
               var queryResidentParam = new QueryMedicalInsuranceResidentInfoParam()
               {
                   BusinessId = param.BusinessId,
                   OrganizationCode = userBase.OrganizationCode
               };
               //医保登录
               _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
               //获取医保病人信息
               var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
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
                   _residentMedicalInsurance.LeaveHospitalSettlementCancel(settlementCancelParam, cancelParam);


               }

           });

        }
        /// <summary>
        /// 医保连接
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData HospitalizationRegister([FromUri]QueryHospitalOperatorParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
           {

               var data = _systemManage.QueryHospitalOperator(param);
               if (string.IsNullOrWhiteSpace(data.MedicalInsuranceAccount))
               {
                   throw new Exception("当前用户未授权,医保账户信息,请重新授权!!!");
               }
               else
               {
                   _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });

               }

           });

        }
        #endregion
        #region 居民门诊医保
        /// <summary>
        /// 门诊费用结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData OutpatientDepartmentCostInput([FromUri]GetOutpatientUiParam param)
        { 
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {   //
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                //医保登录
                _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                var data = _outpatientDepartmentService.OutpatientDepartmentCostInput(new GetOutpatientPersonParam()
                {
                    User = userBase,
                    UiParam = param
                });
                y.Data = data;

            });

        }
        /// <summary>
        /// 取消门诊费用结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData CancelOutpatientDepartmentCost([FromUri]UiBaseDataParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                var outpatient = _hisSqlRepository.QueryOutpatient(new QueryOutpatientParam() { BusinessId = param.BusinessId });
                if (outpatient != null)
                {
                    if (!string.IsNullOrWhiteSpace(outpatient.SettlementTransactionId))
                    {
                        if (string.IsNullOrWhiteSpace(outpatient.SettlementCancelTransactionId))
                        {
                            throw new Exception("当前病人已办理结算");
                        }
                    }
                    //医保登录
                    _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                    _outpatientDepartmentRepository.CancelOutpatientDepartmentCost(new CancelOutpatientDepartmentCostParam()
                    {
                        User = userBase,
                        Id = outpatient.Id,
                        BusinessId = param.BusinessId,
                        Participation = new CancelOutpatientDepartmentCostParticipationParam()
                        {
                            DocumentNo = JsonConvert.DeserializeObject<OutpatientDepartmentCostInputDto>(outpatient.ReturnJson).DocumentNo,
                        }
                    });
                }

            });

        }
        /// <summary>
        ///查询门诊费用结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData QueryOutpatientDepartmentCost([FromUri]QueryOutpatientDepartmentCostUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                //医保登录
                _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                _outpatientDepartmentRepository.QueryOutpatientDepartmentCost(
                        new QueryOutpatientDepartmentCostParam()
                        { DocumentNo = param.DocumentNo });

            });

        }
        /// <summary>
        ///门诊月结汇总
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData MonthlyHospitalization([FromUri]MonthlyHospitalizationUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                //医保登录
                _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                _outpatientDepartmentRepository.MonthlyHospitalization(
                    new MonthlyHospitalizationParam()
                    {
                        User = userBase,
                        Participation = new MonthlyHospitalizationParticipationParam()
                        {
                            StartTime = param.StartTime,
                            EndTime = param.EndTime,
                            SummaryType = "22",
                            PeopleType =((int)param.PeopleType).ToString()
                        }
                    });

            });

        }
        /// <summary>
        ///取消门诊月结汇总
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData CancelMonthlyHospitalization([FromUri]CancelMonthlyHospitalizationUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                //医保登录
                _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                _outpatientDepartmentRepository.CancelMonthlyHospitalization(
                   new CancelMonthlyHospitalizationParam()
                   {
                       User = userBase,
                       Participation = new CancelMonthlyHospitalizationParticipationParam()
                       {
                           PeopleType = param.PeopleType,
                           SummaryType = param.SummaryType,
                           DocumentNo = param.DocumentNo
                       }
                   });

            });

        }
        #endregion
    }
}
