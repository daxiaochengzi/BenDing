using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Http;
using BenDing.Domain.Models.Dto.JsonEntity;
using BenDing.Domain.Models.Dto.OutpatientDepartment;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Dto.Workers;
using BenDing.Domain.Models.Enums;
using BenDing.Domain.Models.HisXml;
using BenDing.Domain.Models.Params.Base;
using BenDing.Domain.Models.Params.OutpatientDepartment;
using BenDing.Domain.Models.Params.Resident;
using BenDing.Domain.Models.Params.SystemManage;
using BenDing.Domain.Models.Params.UI;
using BenDing.Domain.Models.Params.Web;
using BenDing.Domain.Models.Params.Workers;
using BenDing.Domain.Xml;
using BenDing.Repository.Interfaces.Web;
using BenDing.Repository.Providers.Web;
using BenDing.Service.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NFine.Web.Controllers
{
    /// <summary>
    /// 本鼎接口
    /// </summary>
    public class BenDingController : ApiController
    {
        private readonly IWebBasicRepository _webServiceBasicRepository;
        private readonly IHisSqlRepository _hisSqlRepository;
        private readonly IWebServiceBasicService _webServiceBasicService;
        private readonly IMedicalInsuranceSqlRepository _medicalInsuranceSqlRepository;
        private readonly ISystemManageRepository _systemManageRepository;
        private readonly IResidentMedicalInsuranceRepository _residentMedicalInsuranceRepository;
        private readonly IResidentMedicalInsuranceService _residentMedicalInsuranceService;
        private readonly IOutpatientDepartmentService _outpatientDepartmentService;
        private readonly IOutpatientDepartmentRepository _outpatientDepartmentRepository;
        private readonly IWorkerMedicalInsuranceService _workerMedicalInsuranceService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="insuranceRepository"></param>
        /// <param name="webServiceBasicService"></param>
        /// <param name="medicalInsuranceSqlRepository"></param>
        /// <param name="hisSqlRepository"></param>
        /// <param name="manageRepository"></param>
        /// <param name="residentMedicalInsuranceService"></param>
        /// <param name="webServiceBasicRepository"></param>
        /// <param name="outpatientDepartmentService"></param>
        /// <param name="outpatientDepartmentRepository"></param>
        /// <param name="workerMedicalInsuranceService"></param>
        public BenDingController(IResidentMedicalInsuranceRepository insuranceRepository,
            IWebServiceBasicService webServiceBasicService,
            IMedicalInsuranceSqlRepository medicalInsuranceSqlRepository,
            IHisSqlRepository hisSqlRepository,
            ISystemManageRepository manageRepository,
            IResidentMedicalInsuranceService residentMedicalInsuranceService,
            IWebBasicRepository webServiceBasicRepository,
            IOutpatientDepartmentService outpatientDepartmentService,
            IOutpatientDepartmentRepository outpatientDepartmentRepository,
            IWorkerMedicalInsuranceService workerMedicalInsuranceService  
            )
        {
            _webServiceBasicService = webServiceBasicService;
            _medicalInsuranceSqlRepository = medicalInsuranceSqlRepository;
            _residentMedicalInsuranceRepository = insuranceRepository;
            _hisSqlRepository = hisSqlRepository;
            _systemManageRepository = manageRepository;
            _residentMedicalInsuranceService = residentMedicalInsuranceService;
            _webServiceBasicRepository = webServiceBasicRepository;
            _outpatientDepartmentService = outpatientDepartmentService;
            _outpatientDepartmentRepository = outpatientDepartmentRepository;
            _workerMedicalInsuranceService = workerMedicalInsuranceService;
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

               var data = _systemManageRepository.QueryHospitalOperator(param);
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
        [HttpPost]
        public ApiJsonResultData GetCatalog([FromBody]UiCatalogParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
             {
                 var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                 if (userBase != null)
                 {
                     var inputInpatientInfo = new CatalogParam()
                     {
                         AuthCode = userBase.AuthCode,
                         CatalogType =(CatalogTypeEnum)Convert.ToInt16(param.CatalogType),
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
        public ApiJsonResultData GetIcd10([FromUri]UiInIParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                var data = _webServiceBasicService.GetIcd10(userBase, new CatalogParam()
                {
                    OrganizationCode = userBase.OrganizationCode,
                    AuthCode = userBase.AuthCode,
                    Nums = 1000,
                   
                });
                y.Data = data;


            });
        }
        /// <summary>
        /// 查询基层ICD10
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData QueryIcd10([FromUri]QueryICD10UiParam param)
        {
            return new ApiJsonResultData(ModelState, new QueryICD10InfoDto()).RunWithTry(y =>
            {
                if (!string.IsNullOrWhiteSpace(param.Search))
                {
                    param.DiseaseName = param.Search;
                }

                var queryData = _hisSqlRepository.QueryICD10(param);

                var data = new
                {
                    data = queryData.Values.FirstOrDefault(),
                    count = queryData.Keys.FirstOrDefault()
                };
                y.Data = data;
            });
        }
        /// <summary>
        /// icd10对码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData Icd10PairCode([FromBody]Icd10PairCodeUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                userBase.TransKey = param.TransKey;
                _webServiceBasicService.Icd10PairCode(new Icd10PairCodeParam()
                {
                    DiseaseId = param.DiseaseId,
                    ProjectCode = param.ProjectCode,
                    ProjectName = param.ProjectName,
                    User = userBase,
                    BusinessId = param.BusinessId
                });


            });
        }
        
        /// <summary>
        /// 查询医保ICD10
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData QueryMedicalInsuranceIcd10([FromUri]QueryICD10UiParam param)
        {
            return new ApiJsonResultData(ModelState, new QueryICD10InfoDto()).RunWithTry(y =>
            {
                param.IsMedicalInsurance = 1;
                param.DiseaseName = param.ProjectName;
                param.DiseaseCoding= param.ProjectCode;
                var queryData = _hisSqlRepository.QueryICD10(param);
                var data = new
                {
                    data = queryData.Values.FirstOrDefault(),
                    count = queryData.Keys.FirstOrDefault()
                };
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
        public ApiJsonResultData QueryMedicalInsuranceResidentInfo([FromBody]QueryMedicalInsuranceResidentInfoParam param)
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
        public ApiJsonResultData GetInpatientInfo([FromBody]UiBaseDataParam param)
        {
            return new ApiJsonResultData(ModelState, new InpatientInfoDto()).RunWithTry(y =>
           {
               var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
               userBase.TransKey = param.TransKey;
               var infoData = new GetInpatientInfoParam()
               {
                   User = userBase,
                   BusinessId = param.BusinessId,
               };
               //获取病人信息
               var inpatientData = _webServiceBasicService.GetInpatientInfo(infoData);
               y.Data = inpatientData ?? throw new Exception("基层获取住院病人失败!!!");


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
                   y.Data = "成功更新数据:" + data.Count() + "条!!!";
               }

           });

        }
        /// <summary>
        /// 获取门诊病人
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData GetOutpatient([FromUri] UiBaseDataParam param)
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
              var baseUser = _webServiceBasicService.GetUserBaseInfo(param.UserId);
              baseUser.TransKey = param.TransKey;
              var outpatientDetailParam = new OutpatientDetailParam()
              {
                  User = baseUser,
                  BusinessId = param.BusinessId,

              };
              var data = _webServiceBasicService.GetOutpatientDetailPerson(outpatientDetailParam);
              y.Data = data;

              //var queryData = _hisSqlRepository.QueryOutpatient(new QueryOutpatientParam() { BusinessId = param.BusinessId });
              //if (queryData != null)
              //{

              //}
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
        ///获取his预结算数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData GetHisHospitalizationPreSettlement([FromUri]UiBaseDataParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                userBase.TransKey = param.TransKey;
                var infoData = new GetInpatientInfoParam()
                {
                    User = userBase,
                    BusinessId = param.BusinessId,
                };
                //获取医保病人
                var queryData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(
                    new QueryMedicalInsuranceResidentInfoParam()
                    {
                        OrganizationCode = userBase.OrganizationCode,
                        BusinessId = param.BusinessId
                    });
                //获取his预结算
                //var hisPreSettlementData = _webServiceBasicService.GetHisHospitalizationPreSettlement(infoData);
                //var preSettlementData = hisPreSettlementData.PreSettlementData.FirstOrDefault();
                //获取病人结算信息
                var settlementData = _webServiceBasicService.GetHisHospitalizationSettlement(infoData);
                if (settlementData.DiagnosisList.Any() == false) throw new Exception("当前病人没有出院诊断信息，不能办理医保预结算!!!");
                if (settlementData.LeaveHospitalDate == null) throw new Exception("当前病人没有办理出院，不能办理医保结算!!!");

                //获取病人信息
                var inpatientData = _webServiceBasicService.GetInpatientInfo(infoData);
                if (inpatientData == null) throw new Exception("基层获取住院病人失败!!!");
                var data = AutoMapper.Mapper.Map<HisHospitalizationPreSettlementDto>(inpatientData);
                data.LeaveHospitalDate = settlementData.LeaveHospitalDate;
                data.Operator = settlementData.LeaveHospitalOperator;
                data.IsBirthHospital = queryData.IsBirthHospital;
                data.Operator = settlementData.LeaveHospitalOperator;
                data.DiagnosisList = settlementData.DiagnosisList;
                data.InsuranceType = queryData.InsuranceType;
                data.LeaveHospitalDate = settlementData.LeaveHospitalDate;
               
                y.Data = data;
            });
        }
        /// <summary>
        ///获取his结算数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData GetHisHospitalizationSettlement([FromUri]UiBaseDataParam param)
        {
            return new ApiJsonResultData(ModelState, new PatientLeaveHospitalInfoDto()).RunWithTry(y =>
             {
                 //获取操作人员信息
                 var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                 userBase.TransKey = param.TransKey;
                 var infoData = new GetInpatientInfoParam()
                 {
                     User = userBase,
                     BusinessId = param.BusinessId,
                 };
                 //获取医保病人
                 var queryData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(
                     new QueryMedicalInsuranceResidentInfoParam()
                     {
                         OrganizationCode = userBase.OrganizationCode,
                         BusinessId = param.BusinessId
                     });
                 if (queryData == null) throw new Exception("当前病人未办理医保入院登记!!!");
                 if (queryData.MedicalInsuranceState == MedicalInsuranceState.HisSettlement) throw new Exception("当前病人已经办理结算!!!");
                 if (queryData.MedicalInsuranceState != MedicalInsuranceState.MedicalInsurancePreSettlement) throw new Exception("当前病人未办理预结算,不能进行结算!!!");
                 //获取病人信息
                 var inpatientData = _webServiceBasicService.GetInpatientInfo(infoData);
                 if (inpatientData == null) throw new Exception("基层获取住院病人失败!!!");
                 var data = AutoMapper.Mapper.Map<HisHospitalizationPreSettlementDto>(inpatientData);
                 //获取病人结算信息
                 var settlementData = _webServiceBasicService.GetHisHospitalizationSettlement(infoData);
                 if (settlementData.DiagnosisList.Any() == false) throw new Exception("当前病人没有出院诊断信息，不能办理医保结算!!!");
                 if (settlementData.LeaveHospitalDate == null) throw new Exception("当前病人没有办理出院，不能办理医保结算!!!");

                 data.Operator = settlementData.LeaveHospitalOperator;
                 data.DiagnosisList = settlementData.DiagnosisList;
                 data.InsuranceType = queryData.InsuranceType;
                 data.LeaveHospitalDate = settlementData.LeaveHospitalDate;
                 data.IsBirthHospital = queryData.IsBirthHospital;
                 data.MedicalInsuranceState = queryData.MedicalInsuranceState;
                 data.SettlementNo = queryData.SettlementNo;
               
                 y.Data = data;

             });
        }
        /// <summary>
        /// 获取取消结算参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData GetHisHospitalizationSettlementCancel([FromUri]UiBaseDataParam param)
        {
            return new ApiJsonResultData(ModelState, new PatientLeaveHospitalInfoDto()).RunWithTry(y =>
            { ////获取操作人员信息
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                userBase.TransKey = param.TransKey;
                var inpatientInfoParam = new QueryInpatientInfoParam() { BusinessId = param.BusinessId };
                //获取住院病人
                var inpatientInfoData = _hisSqlRepository.QueryInpatientInfo(inpatientInfoParam);
                if (inpatientInfoData == null) throw new Exception("获取当前病人失败!!!");
                var data = AutoMapper.Mapper.Map<HisHospitalizationSettlementCancelDto>(inpatientInfoData);

                if (!string.IsNullOrWhiteSpace(inpatientInfoData.LeaveHospitalDiagnosisJson))
                {
                    data.DiagnosisList = JsonConvert.DeserializeObject<List<InpatientDiagnosisDto>>(inpatientInfoData.LeaveHospitalDiagnosisJson);
                }
                var settlementCancelData = _webServiceBasicService.GetHisHospitalizationSettlementCancel(new SettlementCancelParam()
                {
                    BusinessId = param.BusinessId,
                    User = userBase
                });
                if (settlementCancelData == null) throw new Exception("获取住院结算取消基层信息失败!!!");
                data.CancelOperator = settlementCancelData.CancelOperator;
                data.SettlementNo = settlementCancelData.SettlementNo;
                data.DiagnosisNo = settlementCancelData.DiagnosisNo;

                y.Data = data;

            });
        }

        /// <summary>
        /// 基层入院登记取消
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData HospitalizationRegisterCancel([FromUri]HospitalizationRegisterCancelParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                userBase.TransKey = param.TransKey;
                var infoData = new GetInpatientInfoParam()
                {
                    User = userBase,
                    BusinessId = param.BusinessId,
                };
                //获取病人信息
                // var inpatientData = _webServiceBasicService.GetInpatientInfo(infoData);


                //回参构建
                var xmlData = new HospitalizationRegisterCancelXml()
                {
                    MedicalInsuranceHospitalizationNo = param.HospitalizationNo
                };
                var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
                var saveXml = new SaveXmlDataParam()
                {
                    User = userBase,
                    MedicalInsuranceBackNum = "CXJB004",
                    MedicalInsuranceCode = "22",
                    BusinessId = param.BusinessId,
                    BackParam = strXmlBackParam
                };
                //存基层
                _webServiceBasicRepository.SaveXmlData(saveXml);


            });
        }
        /// <summary>
        /// 医保入院登记取消
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData MedicalInsuranceHospitalizationRegisterCancel([FromUri]UiBaseDataParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            { //获取操作人员信息
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                var queryResidentParam = new QueryMedicalInsuranceResidentInfoParam()
                {
                    BusinessId = param.BusinessId,
                    OrganizationCode = userBase.OrganizationCode
                };
                userBase.TransKey = param.TransKey;
                //医保登录
                _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                //获取医保病人信息
                var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
                if (residentData == null) throw new Exception("当前病人未办理医保入院登记,不能取消入院登记!!!");
                if (residentData.MedicalInsuranceState == MedicalInsuranceState.HisSettlement)
                {
                    throw new Exception("当前病人已办理结算,请先取消结算后,再取消入院登记!!!");
                }
                else
                {
                    CancelOperation("2");
                }
                void CancelOperation(string cancelLimit)
                {
                    //居民
                    if (residentData.InsuranceType == "342")
                    {
                        var settlementCancelParam = new LeaveHospitalSettlementCancelParam()
                        {
                            MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo,
                            SettlementNo = residentData.SettlementNo,
                            Operators = CommonHelp.GuidToStr(userBase.UserId),
                            CancelLimit = cancelLimit,

                        };
                        var cancelParam = new LeaveHospitalSettlementCancelInfoParam()
                        {
                            BusinessId = param.BusinessId,
                            Id = residentData.Id,
                            User = userBase,
                        };
                        _residentMedicalInsuranceRepository.LeaveHospitalSettlementCancel(settlementCancelParam, cancelParam);
                    }
                    //职工
                    if (residentData.InsuranceType == "310")
                    {
                        if (residentData.IsBirthHospital == 0)
                        {
                           
                            //获取医院等级
                            var gradeData = _systemManageRepository.QueryHospitalOrganizationGrade(userBase.OrganizationCode);
                            var cancelParam = new WorkerSettlementCancelParam()
                            {
                                BusinessId = param.BusinessId,
                                Id = residentData.Id,
                                User = userBase,
                                SettlementNo = residentData.SettlementNo,
                                CancelLimit = cancelLimit,
                                MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo,
                                AdministrativeArea = gradeData.AdministrativeArea,
                                OrganizationCode = userBase.OrganizationCode,
                                WorkersStrokeCardNo = residentData.WorkersStrokeCardNo,
                                CancelSettlementRemarks = "",
                            };
                            _workerMedicalInsuranceService.WorkerSettlementCancel(cancelParam);
                        }
                        //职工生育住院取消采用居民取消结算
                        if (residentData.IsBirthHospital == 1)
                        {
                            var settlementCancelParam = new LeaveHospitalSettlementCancelParam()
                            {
                                MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo,
                                SettlementNo = residentData.SettlementNo,
                                Operators = CommonHelp.GuidToStr(userBase.UserId),
                                CancelLimit = cancelLimit,

                            };
                            var cancelParam = new LeaveHospitalSettlementCancelInfoParam()
                            {
                                BusinessId = param.BusinessId,
                                Id = residentData.Id,
                                User = userBase,
                            };
                            _residentMedicalInsuranceRepository.LeaveHospitalSettlementCancel(settlementCancelParam, cancelParam);
                        }


                    }
                }
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
        #region 住院医保
        
        /// <summary>
        /// 获取居民医保信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData GetUserInfo([FromBody]GetUserInfoUiParam param)
        {
            return new ApiJsonResultData(ModelState, new ResidentUserInfoDto()).RunWithTry(y =>
             {
                 //医保登陆
                 _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                 var userInfoParam = new ResidentUserInfoParam()
                 {
                     AfferentSign = param.AfferentSign,
                     IdentityMark = param.IdentityMark
                 };
                 y.Data = _residentMedicalInsuranceRepository.GetUserInfo(userInfoParam);
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
                _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                var data = _residentMedicalInsuranceService.ProjectDownload(param);
                y.Data = data;
            });

        }
        /// <summary>
        /// 医保入院登记
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData HospitalizationRegister([FromBody]ResidentHospitalizationRegisterUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                //初始化职工参数
                var workerParam = AutoMapper.Mapper.Map<WorKerHospitalizationRegisterUiParam>(param);
                if (param.DiagnosisList == null) throw new Exception("诊断不能为空!!!");
                //医保登录
                _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                //职工
                if (param.InsuranceType == "310") _workerMedicalInsuranceService.WorkerHospitalizationRegister(workerParam);
                //居民
                if (param.InsuranceType == "342") _residentMedicalInsuranceService.HospitalizationRegister(param);
            });

        }
        /// <summary>
        /// 医保入院登记查询
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
        /// 医保入院登记修改
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData HospitalizationModify([FromBody]HospitalizationModifyUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {

                if (param.DiagnosisList == null) throw new Exception("诊断不能为空!!!");
                //医保登陆
                _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                //获取医保病人信息
                var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(new QueryMedicalInsuranceResidentInfoParam()
                {
                    BusinessId = param.BusinessId
                });
                //职工
                if (residentData.InsuranceType == "310") _workerMedicalInsuranceService.ModifyWorkerHospitalization(param);
                //居民
                if (residentData.InsuranceType == "342") _residentMedicalInsuranceService.HospitalizationModify(param);
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
               if (param.IsLoad)
               {//获取病人明细
                   var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                   _webServiceBasicService.GetInpatientInfoDetail(userBase, param.BusinessId);
               }
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
        /// 医保处方上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData PrescriptionUpload([FromBody]PrescriptionUploadUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
           {
               var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
               //医保登录
               _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
               //处方上传
               var data = _residentMedicalInsuranceRepository.PrescriptionUpload(param, userBase);
               if (!string.IsNullOrWhiteSpace(data.Msg))
               {
                   throw new Exception(data.Msg);
               }

               if (data.Num > 0)
               {
                   y.Data = "处方上传成功" + data.Num + " 条,失败" + (data.Count - data.Num) + " 条";
               }
           });

        }
        /// <summary>
        ///医保处方自动上传
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData PrescriptionUploadAutomatic([FromUri]PrescriptionUploadAutomaticUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                //医保登录
                _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                _residentMedicalInsuranceService.PrescriptionUploadAutomatic(new PrescriptionUploadAutomaticParam()
                { IsTodayUpload = param.IsTodayUpload }, userBase);

            });

        }
        /// <summary>
        /// 医保删除处方数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData DeletePrescriptionUpload([FromBody]BaseUiBusinessIdDataParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            { //医保登录
                _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                _residentMedicalInsuranceService.DeletePrescriptionUpload(param);
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
              _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
              //获取医保病人信息
              var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
              var queryData = _residentMedicalInsuranceRepository.QueryPrescriptionDetail(new QueryPrescriptionDetailParam()
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
        public ApiJsonResultData HospitalizationPreSettlement([FromUri]HospitalizationPreSettlementUiParam param)
        {
            return new ApiJsonResultData(ModelState, new HospitalizationPresettlementDto()).RunWithTry(y =>
            {
                var resultData = new SettlementDto();
                //医保登录
                _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                //获取医保病人信息
                var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(new QueryMedicalInsuranceResidentInfoParam()
                {
                    BusinessId = param.BusinessId
                });
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                //更新病人处方明细
                _webServiceBasicService.GetInpatientInfoDetail(userBase, param.BusinessId);
                var queryParam = new InpatientInfoDetailQueryParam()
                {
                    BusinessId = param.BusinessId
                };
                //获取病人处方明细
                var queryData = _hisSqlRepository.InpatientInfoDetailQuery(queryParam);
                if (!queryData.Any()) throw new Exception("当前病人没有处方明细,不能进行预结算!!!");
                if (queryData.Count(c => c.UploadMark == 0) > 0) throw new Exception("当前病人还有处方明细未上传至医保,不能进行预结算!!!");
                if (residentData.MedicalInsuranceState == MedicalInsuranceState.HisSettlement) throw new Exception("当前病人已医保结算,不能预结算!!!");
                //职工
                if (residentData.InsuranceType == "310")
                {

                    if (residentData.IsBirthHospital == 0)
                    {
                        var workerSettlementData = _workerMedicalInsuranceService.WorkerHospitalizationPreSettlement(param);
                        resultData.PayMsg = CommonHelp.GetPayMsg(JsonConvert.SerializeObject(workerSettlementData));
                        resultData.CashPayment = workerSettlementData.CashPayment;
                        resultData.ReimbursementExpenses = workerSettlementData.ReimbursementExpenses;
                        resultData.TotalAmount = workerSettlementData.TotalAmount;
                    }//职工生育预结算
                    else if (residentData.IsBirthHospital == 1)
                    {
                        if (param.DiagnosisList == null) throw new Exception("诊断不能为空!!!");
                        var workerSettlementData = _workerMedicalInsuranceService.WorkerBirthPreSettlement(new WorkerBirthPreSettlementUiParam()
                        {
                            TransKey = param.TransKey,
                            BusinessId = param.BusinessId,
                            DiagnosisList = param.DiagnosisList,
                            UserId = param.UserId,
                            MedicalCategory = param.MedicalCategory,
                            FetusNumber = param.FetusNumber

                        });
                        resultData.PayMsg = CommonHelp.GetPayMsg(JsonConvert.SerializeObject(workerSettlementData));
                        resultData.CashPayment = workerSettlementData.CashPayment;
                        resultData.ReimbursementExpenses = workerSettlementData.ReimbursementExpenses;
                        resultData.TotalAmount = workerSettlementData.TotalAmount;
                    }


                }
                //居民
                if (residentData.InsuranceType == "342")
                {
                    var residentSettlementData = _residentMedicalInsuranceService.HospitalizationPreSettlement(param);
                    resultData.PayMsg = CommonHelp.GetPayMsg(JsonConvert.SerializeObject(residentSettlementData));
                    resultData.CashPayment = residentSettlementData.CashPayment;
                    resultData.ReimbursementExpenses = residentSettlementData.ReimbursementExpenses;
                    resultData.TotalAmount = residentSettlementData.TotalAmount;
                }

                y.Data = resultData;


            });

        }
        /// <summary>
        /// 医保出院费用结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData LeaveHospitalSettlement([FromBody]LeaveHospitalSettlementUiParam param)
        {
            return new ApiJsonResultData(ModelState, new HospitalizationPresettlementDto()).RunWithTry(y =>
            {
                var resultData = new SettlementDto();
                if (param.DiagnosisList == null) throw new Exception("诊断不能为空!!!");
                //医保登录
                _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                //获取医保病人信息
                var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(new QueryMedicalInsuranceResidentInfoParam()
                {
                    BusinessId = param.BusinessId
                });
                //职工
                if (residentData.InsuranceType == "310")
                {
                    if (residentData.IsBirthHospital == 0)
                    {
                        var workerSettlementData = _workerMedicalInsuranceService.WorkerHospitalizationSettlement(new WorkerHospitalizationSettlementUiParam()
                        {
                            DiagnosisList = param.DiagnosisList,
                            TransKey = param.TransKey,
                            BusinessId = param.BusinessId,
                            LeaveHospitalInpatientState = param.LeaveHospitalInpatientState,
                            UserId = param.UserId,
                        });
                        resultData.CashPayment = workerSettlementData.CashPayment;
                        resultData.PayMsg = CommonHelp.GetPayMsg(JsonConvert.SerializeObject(workerSettlementData));
                        resultData.ReimbursementExpenses = workerSettlementData.ReimbursementExpenses;
                        resultData.TotalAmount = workerSettlementData.TotalAmount;
                    }//职工生育结算
                    else if (residentData.IsBirthHospital == 1)
                    {
                        var workerSettlementData = _workerMedicalInsuranceService.WorkerBirthSettlement(
                          new WorkerBirthSettlementUiParam()
                          {
                              TransKey = param.TransKey,
                              UserId = param.UserId,
                              BusinessId = param.BusinessId,
                              AccountPayment =!string.IsNullOrWhiteSpace(param.AccountPayment)==true?Convert.ToDecimal(param.AccountPayment):0 ,
                              DiagnosisList = param.DiagnosisList,
                              MedicalCategory = param.MedicalCategory,
                              FetusNumber = param.FetusNumber,
                              LeaveHospitalInpatientState = param.LeaveHospitalInpatientState,

                          });
                        resultData.CashPayment = workerSettlementData.CashPayment;
                        resultData.PayMsg = CommonHelp.GetPayMsg(JsonConvert.SerializeObject(workerSettlementData));
                        resultData.ReimbursementExpenses = workerSettlementData.ReimbursementExpenses;
                        resultData.TotalAmount = workerSettlementData.TotalAmount;
                    }


                }
                //居民
                if (residentData.InsuranceType == "342")
                {
                    var residentSettlementData = _residentMedicalInsuranceService.LeaveHospitalSettlement(param);
                    resultData.PayMsg = CommonHelp.GetPayMsg(JsonConvert.SerializeObject(residentSettlementData));
                    resultData.CashPayment = residentSettlementData.CashPayment;
                    resultData.ReimbursementExpenses = residentSettlementData.ReimbursementExpenses;
                    resultData.TotalAmount = residentSettlementData.TotalAmount;
                }

                y.Data = resultData;

            });

        }
        /// <summary>
        /// 查询医保出院结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData QueryLeaveHospitalSettlement([FromUri]QueryHospitalizationPresettlementUiParam param)
        {
            return new ApiJsonResultData(ModelState, new HospitalizationPresettlementDto()).RunWithTry(y =>
            {   //获取操作人员信息
                var resultData = new SettlementDto();

                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                var queryResidentParam = new QueryMedicalInsuranceResidentInfoParam()
                {
                    BusinessId = param.BusinessId,
                    OrganizationCode = userBase.OrganizationCode
                };
                //医保登录
                _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                //获取医保病人信息
                var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
                if (residentData != null)
                {
                    //职工
                    if (residentData.InsuranceType == "310")
                    {
                        //获取医院等级
                        var gradeData = _systemManageRepository.QueryHospitalOrganizationGrade(userBase.OrganizationCode);
                        var workerSettlementData = _workerMedicalInsuranceService.QueryWorkerHospitalizationSettlement(
                            new QueryWorkerHospitalizationSettlementParam()
                            {
                                BusinessId = param.BusinessId,
                                MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo,
                                User = userBase,
                                AdministrativeArea = gradeData.AdministrativeArea,
                                OrganizationCode = gradeData.MedicalInsuranceAccount,
                            });
                        workerSettlementData.TotalAmount = residentData.MedicalInsuranceAllAmount;

                        resultData.PayMsg = CommonHelp.GetPayMsg(JsonConvert.SerializeObject(workerSettlementData));
                        resultData.ReimbursementExpenses = residentData.ReimbursementExpensesAmount;


                    }
                    //居民
                    if (residentData.InsuranceType == "342")
                    {
                        var residentSettlementData = _residentMedicalInsuranceRepository.QueryLeaveHospitalSettlement(new QueryLeaveHospitalSettlementParam() { MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo });
                        residentSettlementData.TotalAmount = residentData.MedicalInsuranceAllAmount;
                        resultData.PayMsg = CommonHelp.GetPayMsg(JsonConvert.SerializeObject(residentSettlementData));
                        resultData.ReimbursementExpenses = residentSettlementData.ReimbursementExpenses;
                    }
                    resultData.CashPayment = residentData.SelfPayFeeAmount;
                    resultData.TotalAmount = residentData.MedicalInsuranceAllAmount;
                }
                y.Data = resultData;
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
               userBase.TransKey = param.TransKey;
               //医保登录
               _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
               //获取医保病人信息
               var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
               if (residentData == null) throw new Exception("当前病人未办理医保入院登记!!!");

               //居民
               if (residentData.InsuranceType == "342")
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
                   _residentMedicalInsuranceRepository.LeaveHospitalSettlementCancel(settlementCancelParam, cancelParam);
               }
               //职工
               if (residentData.InsuranceType == "310")
               {
                   if (residentData.IsBirthHospital == 0)
                   {
                       if (residentData.MedicalInsuranceState != MedicalInsuranceState.HisSettlement) throw new Exception("当前病人未医保结算");
                       //获取医院等级
                       var gradeData = _systemManageRepository.QueryHospitalOrganizationGrade(userBase.OrganizationCode);
                       var cancelParam = new WorkerSettlementCancelParam()
                       {
                           BusinessId = param.BusinessId,
                           Id = residentData.Id,
                           User = userBase,
                           SettlementNo = residentData.SettlementNo,
                           CancelLimit = param.CancelLimit,
                           MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo,
                           AdministrativeArea = gradeData.AdministrativeArea,
                           OrganizationCode = userBase.OrganizationCode,
                           WorkersStrokeCardNo = residentData.WorkersStrokeCardNo,
                           CancelSettlementRemarks = param.CancelSettlementRemarks,
                       };
                       _workerMedicalInsuranceService.WorkerSettlementCancel(cancelParam);
                   }
                   //职工生育住院取消采用居民取消结算
                   if (residentData.IsBirthHospital == 1)
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
                       _residentMedicalInsuranceRepository.LeaveHospitalSettlementCancel(settlementCancelParam, cancelParam);
                   }


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
               var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
               //医保登录
               _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });


           });

        }
        #endregion
        #region 门诊医保
        /// <summary>
        /// 门诊费用结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData OutpatientDepartmentCostInput([FromBody]OutpatientPlanBirthSettlementUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                userBase.TransKey = param.TransKey;
                //医保登录
                _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });

                //门诊计划生育
                if (param.IsBirthHospital==1)
                {
                   var settlementData= _outpatientDepartmentService.OutpatientPlanBirthSettlement(param);
                    y.Data = new OutpatientCostReturnDataDto()
                    {
                        SelfPayFeeAmount = settlementData.CashPayment,
                        PayMsg = CommonHelp.GetPayMsg(JsonConvert.SerializeObject(settlementData))
                    };

                }
                else
                {   //普通门诊
                    var data = _outpatientDepartmentService.OutpatientDepartmentCostInput(new GetOutpatientPersonParam()
                    {
                        User = userBase,
                        UiParam = param,
                        IdentityMark = param.IdentityMark,
                        AfferentSign = param.AfferentSign,

                    });
                    if (data == null) throw new Exception("获取门诊结算反馈数据失败!!!");

                    y.Data = new OutpatientCostReturnDataDto()
                    {
                        ReimbursementExpensesAmount = data.ReimbursementExpensesAmount,
                        SelfPayFeeAmount = data.SelfPayFeeAmount,
                        PayMsg = CommonHelp.GetPayMsg(JsonConvert.SerializeObject(data))
                    };
                }

            });

        }
        /// <summary>
        /// 门诊计划生育预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData OutpatientPlanBirthPreSettlement([FromBody]OutpatientPlanBirthPreSettlementUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                userBase.TransKey = param.TransKey;
                //医保登录
                _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                var settlementData = _outpatientDepartmentService.OutpatientPlanBirthPreSettlement(param);
                y.Data = new OutpatientCostReturnDataDto()
                {
                    SelfPayFeeAmount = settlementData.CashPayment,
                    PayMsg = CommonHelp.GetPayMsg(JsonConvert.SerializeObject(settlementData))
                };

            });

        }
        /// <summary>
        /// 取消门诊费用结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData CancelOutpatientDepartmentCost([FromUri]CancelOutpatientDepartmentCostUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                //医保登录
                _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                _outpatientDepartmentService.CancelOutpatientDepartmentCost(param);
            });

        }
        /// <summary>
        ///查询门诊费用结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData QueryOutpatientDepartmentCost([FromUri]UiBaseDataParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                var resultData = new QueryOutpatientDepartmentCostDataDto();
                //医保登录
                _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                var baseUser = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                baseUser.TransKey = param.TransKey;
                var paramIni = new SettlementCancelParam
                {
                    User = baseUser,
                    BusinessId = param.BusinessId
                };
                var cancelSettlementData = _webServiceBasicService.GetOutpatientSettlementCancel(paramIni);
                var queryData = _hisSqlRepository.QueryOutpatient(new QueryOutpatientParam() { BusinessId = param.BusinessId });
                if (queryData == null) throw new Exception("获取门诊结算病人失败!!!");
                //获取医保病人信息
                var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(new QueryMedicalInsuranceResidentInfoParam()
                {
                    BusinessId = param.BusinessId
                });
                //获取门诊病人信息
                resultData.DepartmentName = queryData.DepartmentName;
                resultData.DiagnosticDoctor = queryData.DiagnosticDoctor;
                resultData.IdCardNo = queryData.IdCardNo;
                resultData.Operator = cancelSettlementData.CancelOperator;
                resultData.PatientName = queryData.PatientName;
                resultData.OutpatientNumber = queryData.OutpatientNumber;
                resultData.VisitDate = queryData.VisitDate;
                //医保门诊结算查询
                var queryOutpatientData = _outpatientDepartmentService.QueryOutpatientDepartmentCost(param);
                resultData.ReimbursementExpensesAmount = queryOutpatientData.ReimbursementExpensesAmount;
                resultData.SelfPayFeeAmount = queryOutpatientData.SelfPayFeeAmount;
                resultData.MedicalTreatmentTotalCost = queryOutpatientData.AllAmount;
                resultData.SettlementNo = cancelSettlementData.SettlementNo;
                if (!string.IsNullOrWhiteSpace(residentData.OtherInfo))
                {
                    resultData.PayMsg = CommonHelp.GetPayMsg(residentData.OtherInfo);
                }
                y.Data = resultData;

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
                _outpatientDepartmentService.MonthlyHospitalization(param);
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
                _outpatientDepartmentService.CancelMonthlyHospitalization(param);

            });

        }
        /// <summary>
        /// 获取门诊生育结算参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData GetOutpatientPlanBirthSettlementParam([FromBody]OutpatientPlanBirthSettlementUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                var data = _outpatientDepartmentService.GetOutpatientPlanBirthSettlementParam(param);


                y.Data = data;
            });

        }
        /// <summary>
        /// 门诊生育结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData OutpatientPlanBirthSettlement([FromBody]OutpatientPlanBirthSettlementUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                //var resultData = JsonConvert.DeserializeObject<WorkerHospitalizationPreSettlementDto>(param.ResultData);
                //_outpatientDepartmentService.OutpatientPlanBirthSettlement(param);

            });

        }


        #endregion
        #region 职工医保
        /// <summary>
        /// 职工入院登记
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData WorKerHospitalizationRegister([FromBody]WorKerHospitalizationRegisterUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                if (param.DiagnosisList != null && param.DiagnosisList.Any())
                {
                    _workerMedicalInsuranceService.WorkerHospitalizationRegister(param);
                }
                else
                {
                    throw new Exception("诊断不能为空!!!");
                }




            });

        }
        /// <summary>
        /// 职工入院登记查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData QueryWorKerMedicalInsurance([FromUri]QueryMedicalInsuranceUiParam param)
        {
            return new ApiJsonResultData(ModelState, new QueryMedicalInsuranceDetailInfoDto()).RunWithTry(y =>
            {
                var data = _webServiceBasicService.QueryMedicalInsuranceDetail(param);
                y.Data = data;

            });
        }
        /// <summary>
        /// 职工入院登记修改
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData ModifyWorkerHospitalization([FromBody]HospitalizationModifyUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                //医保登陆
                _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                if (param.DiagnosisList != null && param.DiagnosisList.Any())
                    _workerMedicalInsuranceService.ModifyWorkerHospitalization(param);
                throw new Exception("诊断不能为空!!!");
            });

        }
        /// <summary>
        /// 职工住院预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData WorkerHospitalizationPreSettlement([FromUri]UiBaseDataParam param)
        {
            return new ApiJsonResultData(ModelState, new HospitalizationPresettlementDto()).RunWithTry(y =>
            {
                //医保登录
                _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                var data = _workerMedicalInsuranceService.WorkerHospitalizationPreSettlement(param);
                y.Data = data;
            });

        }
        /// <summary>
        /// 职工住院结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData WorkerHospitalizationSettlement([FromBody]WorkerHospitalizationSettlementUiParam param)
        {
            return new ApiJsonResultData(ModelState, new HospitalizationPresettlementDto()).RunWithTry(y =>
            {     //医保登录
                _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                var data = _workerMedicalInsuranceService.WorkerHospitalizationSettlement(param);
                y.Data = data;
            });

        }
        /// <summary>
        /// 职工取消结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData WorkerSettlementCancel([FromUri] LeaveHospitalSettlementCancelUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {   //获取操作人员信息

                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                var queryResidentParam = new QueryMedicalInsuranceResidentInfoParam()
                {
                    BusinessId = param.BusinessId,
                    OrganizationCode = userBase.OrganizationCode
                };
                userBase.TransKey = param.TransKey;
                //医保登录
                _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                //获取医保病人信息
                var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
                if (residentData != null)
                {
                    if (residentData.MedicalInsuranceState != MedicalInsuranceState.HisSettlement) throw new Exception("当前病人未医保结算");
                    //获取医院等级
                    var gradeData = _systemManageRepository.QueryHospitalOrganizationGrade(userBase.OrganizationCode);
                    var cancelParam = new WorkerSettlementCancelParam()
                    {
                        BusinessId = param.BusinessId,
                        Id = residentData.Id,
                        User = userBase,
                        SettlementNo = residentData.SettlementNo,
                        CancelLimit = param.CancelLimit,
                        MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo,
                        AdministrativeArea = gradeData.AdministrativeArea,
                        OrganizationCode = userBase.OrganizationCode,
                    };
                    _workerMedicalInsuranceService.WorkerSettlementCancel(cancelParam);


                }

            });
        }

        /// <summary>
        /// 职工生育入院登记
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData WorkerBirthHospitalizationRegister([FromBody]WorkerBirthHospitalizationRegisterUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {//医保登录
                _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                if (param.DiagnosisList != null && param.DiagnosisList.Any())
                {
                    _workerMedicalInsuranceService.WorkerBirthHospitalizationRegister(param);
                }
                else
                {
                    throw new Exception("诊断不能为空!!!");
                }

            });

        }
        /// <summary>
        /// 职工划卡
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData WorkerStrokeCard([FromBody]WorkerStrokeCardUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                userBase.TransKey = param.TransKey;
                //userBase.TransKey = "72A3764C186F488FBBBB46BB864EF252";
                //医保登录
                _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
                //获取医保病人信息
                var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(new QueryMedicalInsuranceResidentInfoParam() { BusinessId = param.BusinessId });
                if (residentData.MedicalInsuranceState != MedicalInsuranceState.MedicalInsuranceSettlement) throw new Exception("当前病人未医保结算不能划卡!!!");
                //获取住院病人
                var inpatientInfoData = _hisSqlRepository.QueryInpatientInfo(new QueryInpatientInfoParam() { BusinessId = param.BusinessId });
                var cardParam = new WorkerStrokeCardParam()
                {
                    AllAmount = param.AllAmount,
                    AccountPayAmount = param.AccountPayAmount,
                    BusinessId = param.BusinessId,
                    CardType = param.CardType,
                    DocumentNo = param.DocumentNo,
                    MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo,
                    Id = residentData.Id,
                    IdCardNo = inpatientInfoData.IdCardNo,
                    User = userBase,
                    SelfPayFeeAmount = param.SelfPayFeeAmount
                };
                _workerMedicalInsuranceService.WorkerStrokeCard(cardParam);

            });

        }
        #endregion

        /// <summary>
        /// 下载文件
        /// </summary>
        [HttpGet]
        public HttpResponseMessage DownloadFile()
        {
            string fileName = "ActiveSetup.msi";
            string filePath = HttpContext.Current.Server.MapPath("~/")+ "bin/ActiveSetup.msi";
            FileStream stream = new FileStream(filePath, FileMode.Open);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = HttpUtility.UrlEncode(fileName)
            };
            response.Headers.Add("Access-Control-Expose-Headers", "FileName");
            response.Headers.Add("FileName", HttpUtility.UrlEncode(fileName));
            return response;

        }
    }
}
