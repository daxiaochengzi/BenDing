using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BenDing.Domain.Models.Dto.OutpatientDepartment;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Base;
using BenDing.Domain.Models.Params.OutpatientDepartment;
using BenDing.Domain.Models.Params.UI;
using BenDing.Domain.Models.Params.Web;
using BenDing.Domain.Xml;
using BenDing.Repository.Interfaces.Web;
using BenDing.Service.Interfaces;
using Newtonsoft.Json;

namespace NFine.Web.Controllers
{    /// <summary>
/// 本鼎插件模式新接口
/// </summary>
    public class BenDingNewController : ApiController
    {
        private readonly IWebBasicRepository _webServiceBasicRepository;
        private readonly IHisSqlRepository _hisSqlRepository;
        private readonly IWebServiceBasicService _webServiceBasicService;
        private readonly IMedicalInsuranceSqlRepository _medicalInsuranceSqlRepository;
        private readonly ISystemManageRepository _systemManageRepository;
        private readonly IResidentMedicalInsuranceRepository _residentMedicalInsuranceRepository;
        private readonly IResidentMedicalInsuranceService _residentMedicalInsuranceService;
        private readonly IOutpatientDepartmentService _outpatientDepartmentService;
        private readonly IOutpatientDepartmentNewService _outpatientDepartmentNewService;
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
        /// <param name="outpatientDepartmentNewService"></param>
        public BenDingNewController(IResidentMedicalInsuranceRepository insuranceRepository,
            IWebServiceBasicService webServiceBasicService,
            IMedicalInsuranceSqlRepository medicalInsuranceSqlRepository,
            IHisSqlRepository hisSqlRepository,
            ISystemManageRepository manageRepository,
            IResidentMedicalInsuranceService residentMedicalInsuranceService,
            IWebBasicRepository webServiceBasicRepository,
            IOutpatientDepartmentService outpatientDepartmentService,
            IOutpatientDepartmentRepository outpatientDepartmentRepository,
            IWorkerMedicalInsuranceService workerMedicalInsuranceService,
            IOutpatientDepartmentNewService outpatientDepartmentNewService
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
            _outpatientDepartmentNewService = outpatientDepartmentNewService;
        }

        /// <summary>
        /// 获取普通门诊结算入参
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData GetOutpatientDepartmentCostInputParam([FromBody] OutpatientPlanBirthSettlementUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                userBase.TransKey = param.TransKey;
                var data = _outpatientDepartmentNewService.GetOutpatientDepartmentCostInputParam(new GetOutpatientPersonParam()
                {
                    User = userBase,
                    UiParam = param,
                    IdentityMark = param.IdentityMark,
                    AfferentSign = param.AfferentSign,

                });
                y.Data = XmlSerializeHelper.XmlSerialize(data);

            });
        }
        /// <summary>
        /// 获取门诊计划生育预结算参数
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData GetOutpatientPlanBirthPreSettlementParam([FromBody] OutpatientPlanBirthPreSettlementUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
             
                var data = _outpatientDepartmentNewService.GetOutpatientPlanBirthPreSettlementParam(param);
                y.Data = XmlSerializeHelper.XmlSerialize(data);

            });
        }
        /// <summary>
        /// 获取门诊计划生育结算参数
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData GetOutpatientPlanBirthSettlementParam([FromBody]OutpatientPlanBirthSettlementUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                var data = _outpatientDepartmentNewService.GetOutpatientPlanBirthSettlementParam(param);
                y.Data = XmlSerializeHelper.XmlSerialize(data);

            });
        }
        /// <summary>
        /// 获取门诊月结参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData GetMonthlyHospitalizationParam([FromBody] MonthlyHospitalizationUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                var data = _outpatientDepartmentNewService.GetMonthlyHospitalizationParam(param);
                y.Data = XmlSerializeHelper.XmlSerialize(data);

            });
        }
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
                //门诊计划生育
                if (param.IsBirthHospital == 1)
                {
                    var settlementData = _outpatientDepartmentNewService.OutpatientPlanBirthSettlement(param);
                    y.Data = new OutpatientCostReturnDataDto()
                    {
                       
                        SelfPayFeeAmount = settlementData.CashPayment,
                        PayMsg = CommonHelp.GetPayMsg(JsonConvert.SerializeObject(settlementData))
                    };

                }
                else
                {   //普通门诊
                    var data = _outpatientDepartmentNewService.OutpatientDepartmentCostInput(new GetOutpatientPersonParam()
                    {
                        User = userBase,
                        UiParam = param,
                        IdentityMark = param.IdentityMark,
                        AfferentSign = param.AfferentSign,
                        InsuranceType = param.InsuranceType,
                        AccountBalance = param.AccountBalance,
                        SettlementXml = param.SettlementXml,


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
               
                var settlementData = _outpatientDepartmentNewService.OutpatientPlanBirthPreSettlement(param);
                y.Data = new OutpatientCostReturnDataDto()
                {
                    SelfPayFeeAmount = settlementData.CashPayment,
                    PayMsg = CommonHelp.GetPayMsg(JsonConvert.SerializeObject(settlementData))
                };

            });

        }
        /// <summary>
        /// 获取取消门诊费用结算参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData GetCancelOutpatientDepartmentCostParam([FromUri]CancelOutpatientDepartmentCostUiParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {
                _outpatientDepartmentNewService.GetCancelOutpatientDepartmentCostParam(param);
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

                _outpatientDepartmentNewService.CancelOutpatientDepartmentCost(param);
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

                resultData.IsBirthHospital = residentData.IsBirthHospital;
                y.Data = resultData;

            });

        }
        //
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
                _outpatientDepartmentNewService.MonthlyHospitalization(param);
            });

        }
       
        ///// <summary>
        /////取消门诊月结汇总
        ///// </summary>
        ///// <param name="param"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public ApiJsonResultData CancelMonthlyHospitalization([FromUri]CancelMonthlyHospitalizationUiParam param)
        //{
        //    return new ApiJsonResultData(ModelState).RunWithTry(y =>
        //    {
        //        _outpatientDepartmentNewService.CancelMonthlyHospitalization(param);

        //    });

        //}
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
        #region 公共信息
        /// <summary>
        /// 获取医院信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData GetHospitalInfo([FromUri]UiBaseDataParam param)
        {
            return new ApiJsonResultData(ModelState, new QueryMedicalInsuranceDetailInfoDto()).RunWithTry(y =>
            {
                var userBase = _webServiceBasicService.GetUserBaseInfo(param.UserId);
                //获取医院等级
                var gradeData = _systemManageRepository.QueryHospitalOrganizationGrade(userBase.OrganizationCode);
                y.Data = gradeData;
            });

        }
        #endregion
    }
}
