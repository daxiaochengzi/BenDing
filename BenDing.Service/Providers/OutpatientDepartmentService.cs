using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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
using BenDing.Domain.Xml;
using BenDing.Repository.Interfaces.Web;
using BenDing.Repository.Providers.Web;
using BenDing.Service.Interfaces;
using Newtonsoft.Json;
using NFine.Application.BenDingManage;
using NFine.Domain._03_Entity.BenDingManage;

namespace BenDing.Service.Providers
{

    public class OutpatientDepartmentService : IOutpatientDepartmentService
    {
        private readonly IOutpatientDepartmentRepository _outpatientDepartmentRepository;
        private readonly IWebServiceBasicService _serviceBasicService;
        private readonly IWebBasicRepository _webBasicRepository;
        private readonly IHisSqlRepository _hisSqlRepository;
        private readonly ISystemManageRepository _systemManageRepository;
        private readonly IResidentMedicalInsuranceRepository _residentMedicalInsuranceRepository;
        private readonly IResidentMedicalInsuranceService _residentMedicalInsuranceService;
        private readonly IMedicalInsuranceSqlRepository _medicalInsuranceSqlRepository;
        private readonly MonthlyHospitalizationBase _monthlyHospitalizationBase;


        public OutpatientDepartmentService(
           IOutpatientDepartmentRepository outpatientDepartmentRepository,
           IWebServiceBasicService webServiceBasicService,
           IWebBasicRepository webBasicRepository,
           IHisSqlRepository hisSqlRepository,
           ISystemManageRepository systemManageRepository,
           IResidentMedicalInsuranceRepository residentMedicalInsuranceRepository,
           IMedicalInsuranceSqlRepository medicalInsuranceSqlRepository,
           IResidentMedicalInsuranceService medicalInsuranceService
           )
        {
            _serviceBasicService = webServiceBasicService;
            _outpatientDepartmentRepository = outpatientDepartmentRepository;
            _webBasicRepository = webBasicRepository;
            _hisSqlRepository = hisSqlRepository;
            _systemManageRepository = systemManageRepository;
            _residentMedicalInsuranceRepository = residentMedicalInsuranceRepository;
            _medicalInsuranceSqlRepository = medicalInsuranceSqlRepository;
            _residentMedicalInsuranceService = medicalInsuranceService;
            _monthlyHospitalizationBase = new MonthlyHospitalizationBase();
        }

        /// <summary>
        /// 门诊费用录入
        /// </summary>
        public OutpatientDepartmentCostInputDto OutpatientDepartmentCostInput(GetOutpatientPersonParam param)
        {
            OutpatientDepartmentCostInputDto resultData = null;
            //获取门诊病人数据
            var outpatientPerson = _serviceBasicService.GetOutpatientPerson(param);
            if (outpatientPerson == null) throw new Exception("his中未获取到当前病人!!!");
            //if (string.IsNullOrWhiteSpace(outpatientPerson.IdCardNo)) throw new Exception("当前病人的身份证号码不能为空!!!");
            var inputParam = new OutpatientDepartmentCostInputParam()
            {
                AllAmount = outpatientPerson.MedicalTreatmentTotalCost,
                IdentityMark = param.IdentityMark,
                AfferentSign = param.AfferentSign,
                Operators = param.User.UserName
            };
            //医保数据写入
            resultData = _outpatientDepartmentRepository.OutpatientDepartmentCostInput(inputParam);
            if (resultData == null) throw new Exception("门诊费用医保执行失败!!!");

            param.IsSave = true;
            param.Id = Guid.NewGuid();
            //保存门诊病人
            _serviceBasicService.GetOutpatientPerson(param);
            //中间层数据写入
            var saveData = new MedicalInsuranceDto
            {
                AdmissionInfoJson = JsonConvert.SerializeObject(param),
                BusinessId = param.UiParam.BusinessId,
                Id = Guid.NewGuid(),
                IsModify = false,
                InsuranceType = 999,
                MedicalInsuranceState = MedicalInsuranceState.MedicalInsuranceHospitalized,
                MedicalInsuranceHospitalizationNo = resultData.DocumentNo,
                AfferentSign = param.AfferentSign,
                IdentityMark = param.IdentityMark
            };
            //存中间库
            _medicalInsuranceSqlRepository.SaveMedicalInsurance(param.User, saveData);

            //日志写入
            _systemManageRepository.AddHospitalLog(new AddHospitalLogParam()
            {
                User = param.User,
                JoinOrOldJson = JsonConvert.SerializeObject(inputParam),
                ReturnOrNewJson = JsonConvert.SerializeObject(resultData),
                RelationId = param.Id,
                Remark = "[R][OutpatientDepartment]门诊病人结算"
            });
            //获取病人的基础信息
            var userInfoData = _residentMedicalInsuranceRepository.GetUserInfo(new ResidentUserInfoParam()
            {
                IdentityMark = param.IdentityMark,
                AfferentSign = param.AfferentSign,
            });
            //回参构建
            var xmlData = new OutpatientDepartmentCostXml()
            {
                AccountBalance = userInfoData.InsuranceType == "342" ? userInfoData.ResidentInsuranceBalance : userInfoData.WorkersInsuranceBalance,
                MedicalInsuranceOutpatientNo = resultData.DocumentNo,
                CashPayment = resultData.SelfPayFeeAmount,
                SettlementNo = resultData.DocumentNo,
                AllAmount = outpatientPerson.MedicalTreatmentTotalCost,
                PatientName = outpatientPerson.PatientName,
                AccountAmountPay = 0,
                MedicalInsuranceType = userInfoData.InsuranceType == "342" ? "10" : userInfoData.InsuranceType,
            };

            var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
            var saveXml = new SaveXmlDataParam()
            {
                User = param.User,
                MedicalInsuranceBackNum = "zydj",
                MedicalInsuranceCode = "48",
                BusinessId = param.UiParam.BusinessId,
                BackParam = strXmlBackParam
            };
            //存基层
            _webBasicRepository.SaveXmlData(saveXml);
            var updateParam = new UpdateMedicalInsuranceResidentSettlementParam()
            {
                UserId = param.User.UserId,
                ReimbursementExpensesAmount = resultData.ReimbursementExpensesAmount,
                SelfPayFeeAmount = resultData.SelfPayFeeAmount,
                OtherInfo = JsonConvert.SerializeObject(resultData),
                Id = saveData.Id,
                SettlementNo = resultData.DocumentNo,
                MedicalInsuranceAllAmount = outpatientPerson.MedicalTreatmentTotalCost,
                SettlementTransactionId = param.User.TransKey,
                MedicalInsuranceState = MedicalInsuranceState.HisSettlement
            };
            //更新中间层
            _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParam);

            return resultData;
        }
        //门诊取消结算
        public void CancelOutpatientDepartmentCost(CancelOutpatientDepartmentCostUiParam param)
        {
            var userBase = _serviceBasicService.GetUserBaseInfo(param.UserId);
            userBase.TransKey = param.TransKey;
            //获取医保病人信息
            var queryResidentParam = new QueryMedicalInsuranceResidentInfoParam()
            {
                BusinessId = param.BusinessId,
                OrganizationCode = userBase.OrganizationCode
            };
            var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
            if (residentData == null) throw new Exception("当前病人未结算,不能取消结算!!!");
            if (residentData.MedicalInsuranceState != MedicalInsuranceState.HisSettlement) throw new Exception("当前病人未结算,不能取消结算!!!");
            //计划生育
            if (residentData.IsBirthHospital == 1)
            {
                _outpatientDepartmentRepository.OutpatientPlanBirthSettlementCancel(new OutpatientPlanBirthSettlementCancelParam()
                {
                     SettlementNo= residentData.SettlementNo,
                     CancelRemarks = param.CancelSettlementRemarks

                });
            }
            else  //普通门诊
            {
                _outpatientDepartmentRepository.CancelOutpatientDepartmentCost(new CancelOutpatientDepartmentCostParam()
                {
                    DocumentNo = residentData.SettlementNo
                });
            }

           

            //添加日志
            var logParam = new AddHospitalLogParam()
            {
                JoinOrOldJson = JsonConvert.SerializeObject(param),
                User = userBase,
                Remark = "门诊取消结算",
                RelationId = residentData.Id,
            };
            _systemManageRepository.AddHospitalLog(logParam);

            //回参构建
            var xmlData = new OutpatientDepartmentCostCancelXml()
            {
                SettlementNo = residentData.SettlementNo
            };
            var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
            var saveXml = new SaveXmlDataParam()
            {
                User = userBase,
                MedicalInsuranceBackNum = "Qxjs",
                MedicalInsuranceCode = "42MZ",
                BusinessId = param.BusinessId,
                BackParam = strXmlBackParam
            };
            //存基层
            _webBasicRepository.SaveXmlData(saveXml);

            var updateParamData = new UpdateMedicalInsuranceResidentSettlementParam()
            {
                UserId = param.UserId,
                Id = residentData.Id,
                CancelTransactionId = param.TransKey,
                MedicalInsuranceState = MedicalInsuranceState.MedicalInsuranceCancelSettlement,
                IsHisUpdateState = true,
                CancelSettlementRemarks = param.CancelSettlementRemarks
            };
            //更新中间层
            _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParamData);

        }
        /// <summary>
        /// 门诊结算查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public QueryOutpatientDepartmentCostjsonDto QueryOutpatientDepartmentCost(UiBaseDataParam param)
        {

            var resultData = new QueryOutpatientDepartmentCostjsonDto();
            var userBase = _serviceBasicService.GetUserBaseInfo(param.UserId);

            //获取医保病人信息
            var queryResidentParam = new QueryMedicalInsuranceResidentInfoParam()
            {
                BusinessId = param.BusinessId,
                OrganizationCode = userBase.OrganizationCode
            };
            var outpatient = _hisSqlRepository.QueryOutpatient(new QueryOutpatientParam() { BusinessId = param.BusinessId });
            if (outpatient == null) throw new Exception("当前病人查找失败!!!");
            var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
            if (residentData == null) throw new Exception("当前病人未结算,无结算数据!!!");
            if (residentData.MedicalInsuranceState != MedicalInsuranceState.HisSettlement) throw new Exception("当前病人无结算数据!!!");
          
            resultData.ReimbursementExpensesAmount = residentData.ReimbursementExpensesAmount;
            resultData.SelfPayFeeAmount = residentData.SelfPayFeeAmount;
            resultData.AllAmount = residentData.MedicalInsuranceAllAmount;
            return resultData;
        }
        /// <summary>
        /// 门诊月结
        /// </summary>
        /// <param name="param"></param>
        public void MonthlyHospitalization(MonthlyHospitalizationUiParam param)
        {
            var userBase = _serviceBasicService.GetUserBaseInfo(param.UserId);
            //医保登录
            _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
            var data = _outpatientDepartmentRepository.MonthlyHospitalization(
               new MonthlyHospitalizationParam()
               {
                   User = userBase,
                   Participation = new MonthlyHospitalizationParticipationParam()
                   {
                       StartTime = Convert.ToDateTime(param.StartTime).ToString("yyyyMMdd"),
                       EndTime = Convert.ToDateTime(param.EndTime).ToString("yyyyMMdd"),
                       SummaryType = "22",
                       PeopleType = ((int)param.PeopleType).ToString()
                   }
               });
            var insertParam = new MonthlyHospitalizationEntity()
            {
                Amount = data.ReimbursementAllAmount,
                Id = Guid.NewGuid(),
                DocumentNo = data.DocumentNo,
                PeopleNum = data.ReimbursementPeopleNum,
                PeopleType = ((int)param.PeopleType).ToString(),
                SummaryType = "22",
                StartTime = Convert.ToDateTime(param.StartTime + " 00:00:00.000"),
                EndTime = Convert.ToDateTime(param.EndTime + " 00:00:00.000"),
                IsRevoke = false
            };
            _monthlyHospitalizationBase.Insert(insertParam, userBase);
            //添加日志
            var logParam = new AddHospitalLogParam()
            {
                JoinOrOldJson = JsonConvert.SerializeObject(param),
                User = userBase,
                Remark = "门诊月结汇总",
                RelationId = insertParam.Id,
            };
            _systemManageRepository.AddHospitalLog(logParam);

        }
        /// <summary>
        /// 门诊月结取消
        /// </summary>
        /// <param name="param"></param>
        public void CancelMonthlyHospitalization(CancelMonthlyHospitalizationUiParam param)
        {
            var userBase = _serviceBasicService.GetUserBaseInfo(param.UserId);
            //医保登录
            _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
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
            //var monthlyHospitalization = _monthlyHospitalizationBase.GetForm(param.Id);

        }
        /// <summary>
        /// 门诊计划生育预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerHospitalizationPreSettlementDto OutpatientPlanBirthPreSettlement(OutpatientPlanBirthPreSettlementUiParam param)
        {
            WorkerHospitalizationPreSettlementDto data = null;
            var userBase = _serviceBasicService.GetUserBaseInfo(param.UserId);
            userBase.TransKey = param.TransKey;
           
            var outpatientParam = new GetOutpatientPersonParam()
            {
                User = userBase,
                UiParam = param,
            };
            var outpatientPerson = _serviceBasicService.GetOutpatientPerson(outpatientParam);
            if (outpatientPerson == null) throw new Exception("his中未获取到当前病人!!!");
            var outpatientDetailPerson = _serviceBasicService.GetOutpatientDetailPerson(new OutpatientDetailParam()
            {
                User = userBase,
                BusinessId = param.BusinessId,
            });
            var iniParam = GetOutpatientPlanBirthPreSettlementParam
             (param);
            iniParam.AfferentSign = param.AfferentSign;
            iniParam.IdentityMark = param.IdentityMark;
            //医保执行
            data = _outpatientDepartmentRepository.OutpatientPlanBirthPreSettlement(iniParam);
            //保存门诊病人
            outpatientParam.IsSave = true;
            outpatientParam.Id = Guid.NewGuid();
            _serviceBasicService.GetOutpatientPerson(outpatientParam);
            var saveData = new MedicalInsuranceDto
            {
                AdmissionInfoJson = JsonConvert.SerializeObject(param),
                BusinessId = param.BusinessId,
                Id = Guid.NewGuid(),
                IsModify = false,
                InsuranceType = 999,
                MedicalInsuranceState = MedicalInsuranceState.MedicalInsurancePreSettlement,
                AfferentSign = param.AfferentSign,
                IdentityMark = param.IdentityMark,
                IsBirthHospital = 1
            };
            //存中间库
            _medicalInsuranceSqlRepository.SaveMedicalInsurance(userBase, saveData);
            //日志写入
            _systemManageRepository.AddHospitalLog(new AddHospitalLogParam()
            {
                User = userBase,
                JoinOrOldJson = JsonConvert.SerializeObject(iniParam),
                ReturnOrNewJson = JsonConvert.SerializeObject(data),
                RelationId = outpatientParam.Id,
                Remark = "[R][OutpatientDepartment]门诊计划生育预结算"
            });
            //明细存入
            _serviceBasicService.GetOutpatientDetailPerson(new OutpatientDetailParam()
            {
                IsSave = true,
                BusinessId = param.BusinessId,
                User = userBase
            });
            return data;
        }
        /// <summary>
        /// 门诊生育结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerHospitalizationPreSettlementDto OutpatientPlanBirthSettlement(
            OutpatientPlanBirthSettlementUiParam param)
        {
            WorkerHospitalizationPreSettlementDto resultData = null;
            var userBase = _serviceBasicService.GetUserBaseInfo(param.UserId);
            userBase.TransKey = param.TransKey;
            //门诊病人信息存储
            var id = Guid.NewGuid();
            var outpatientParam = new GetOutpatientPersonParam()
            {
                User = userBase,
                UiParam = param,
                Id = id,
            };
            var outpatientPerson = _serviceBasicService.GetOutpatientPerson(outpatientParam);
            if (outpatientPerson == null) throw new Exception("his中未获取到当前病人!!!");
            var outpatientDetailPerson = _serviceBasicService.GetOutpatientDetailPerson(new OutpatientDetailParam()
            {
                User = userBase,
                BusinessId = param.BusinessId,
            });
            var queryResidentParam = new QueryMedicalInsuranceResidentInfoParam()
            {
                BusinessId = param.BusinessId,
            };
            //获取医保病人信息
            var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
            if (residentData.MedicalInsuranceState != MedicalInsuranceState.MedicalInsurancePreSettlement) throw new Exception("当前病人未办理预结算,不能办理结算!!!");
            if (residentData.MedicalInsuranceState == MedicalInsuranceState.HisSettlement) throw new Exception("当前病人已办理医保结算,不能办理再次结算!!!");
            //获取数据明细
            var iniParam = GetOutpatientPlanBirthSettlementParam
                (param );
            iniParam.AfferentSign = param.AfferentSign;
            iniParam.IdentityMark = param.IdentityMark;
            iniParam.OutpatientNo = CommonHelp.GuidToStr(param.BusinessId);
           
            // 医保执行
             resultData = _outpatientDepartmentRepository.OutpatientPlanBirthSettlement(iniParam);
            _serviceBasicService.GetOutpatientPerson(outpatientParam);
            var updateData = new UpdateMedicalInsuranceResidentSettlementParam()
            {
                UserId = userBase.UserId,
                ReimbursementExpensesAmount = CommonHelp.ValueToDouble(resultData.ReimbursementExpenses),
                SelfPayFeeAmount = resultData.CashPayment,
                OtherInfo = JsonConvert.SerializeObject(resultData),
                Id = residentData.Id,
                SettlementNo = resultData.DocumentNo,
                MedicalInsuranceAllAmount = resultData.TotalAmount,
                SettlementTransactionId = userBase.UserId,
                MedicalInsuranceState = MedicalInsuranceState.MedicalInsuranceSettlement
            };
            //存入中间层
            _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateData);
            //日志写入
            _systemManageRepository.AddHospitalLog(new AddHospitalLogParam()
            {
                User = userBase,
                JoinOrOldJson = JsonConvert.SerializeObject(iniParam),
                ReturnOrNewJson = JsonConvert.SerializeObject(resultData),
                RelationId = outpatientParam.Id,
                Remark = "[R][OutpatientDepartment]门诊生育结算"
            });
            //获取病人的基础信息
            var userInfoData = _residentMedicalInsuranceRepository.GetUserInfo(new ResidentUserInfoParam()
            {
                IdentityMark = param.IdentityMark,
                AfferentSign = param.AfferentSign,
            });
            // 回参构建
            var xmlData = new OutpatientDepartmentCostXml()
            {
                AccountBalance = userInfoData.InsuranceType == "342" ? userInfoData.ResidentInsuranceBalance : userInfoData.WorkersInsuranceBalance,
                MedicalInsuranceOutpatientNo = resultData.DocumentNo,
                CashPayment = resultData.CashPayment,
                SettlementNo = resultData.DocumentNo,
                AllAmount = outpatientPerson.MedicalTreatmentTotalCost,
                PatientName = outpatientPerson.PatientName,
                AccountAmountPay = resultData.AccountPayment,
                MedicalInsuranceType = userInfoData.InsuranceType == "342" ? "10" : userInfoData.InsuranceType,
            };

            var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
            var saveXml = new SaveXmlDataParam()
            {
                User = userBase,
                MedicalInsuranceBackNum = "zydj",
                MedicalInsuranceCode = "48",
                BusinessId = param.BusinessId,
                BackParam = strXmlBackParam
            };
            ////存基层
            _webBasicRepository.SaveXmlData(saveXml);
            var updateParamData = new UpdateMedicalInsuranceResidentSettlementParam()
            {
                UserId = param.UserId,
                Id = residentData.Id,
                MedicalInsuranceState = MedicalInsuranceState.HisSettlement,
                IsHisUpdateState = true
            };
            //  更新中间层
            _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParamData);

            return resultData;
        }
        ///// <summary>
        ///// 门诊计划生育结算
        ///// </summary>
        ///// <param name="param"></param>
        ///// <returns></returns>
        //public void OutpatientPlanBirthSettlement(OutpatientPlanBirthSettlementUiParam param)
        //{
        //    var userBase = _serviceBasicService.GetUserBaseInfo(param.UserId);
        //    userBase.TransKey = param.TransKey;
        //    //门诊病人信息存储
        //    var id = Guid.NewGuid();
        //    var outpatientParam = new GetOutpatientPersonParam()
        //    {
        //        User = userBase,
        //        UiParam = param,
        //        IsSave = true,
        //        Id = id,

        //    };
        //    var outpatientPerson = _serviceBasicService.GetOutpatientPerson(outpatientParam);
        //    if (outpatientPerson == null) throw new Exception("his中未获取到当前病人!!!");
        //    var outpatientDetailPerson = _serviceBasicService.GetOutpatientDetailPerson(new OutpatientDetailParam()
        //    {
        //        User = userBase,
        //        BusinessId = param.BusinessId,
        //    });
        //    var iniParam = GetOutpatientPlanBirthSettlementParam(param);
        //    var dataIni = JsonConvert.DeserializeObject<WorkerBirthPreSettlementJsonDto>(param.ResultData);
        //    var resultData = AutoMapper.Mapper.Map<WorkerHospitalizationPreSettlementDto>(dataIni); 
        //    var saveData = new MedicalInsuranceDto
        //    {
        //        AdmissionInfoJson = JsonConvert.SerializeObject(param),
        //        BusinessId = param.BusinessId,
        //        Id = Guid.NewGuid(),
        //        IsBirthHospital = 1,
        //        IsModify = false,
        //        InsuranceType = 999,
        //        MedicalInsuranceState = MedicalInsuranceState.HisHospitalized,
        //        MedicalInsuranceHospitalizationNo = outpatientPerson.OutpatientNumber,
        //        AfferentSign = param.AfferentSign,
        //        IdentityMark = param.IdentityMark

        //    };
        //    //存中间库
        //    _medicalInsuranceSqlRepository.SaveMedicalInsurance(userBase, saveData);
        //    //日志写入
        //    _systemManageRepository.AddHospitalLog(new AddHospitalLogParam()
        //    {
        //        User = userBase,
        //        JoinOrOldJson = JsonConvert.SerializeObject(iniParam),
        //        ReturnOrNewJson = param.ResultData,
        //        RelationId = outpatientParam.Id,
        //        Remark = "[R][OutpatientDepartment]门诊计划生育结算"
        //    });

        //    //获取病人的基础信息
        //    var userInfoData = _residentMedicalInsuranceRepository.GetUserInfo(new ResidentUserInfoParam()
        //    {
        //        IdentityMark = param.IdentityMark,
        //        AfferentSign = param.AfferentSign,
        //    });
        //   // 回参构建
        //    var xmlData = new OutpatientDepartmentCostXml()
        //    {
        //        AccountBalance = userInfoData.InsuranceType == "342" ? userInfoData.ResidentInsuranceBalance : userInfoData.WorkersInsuranceBalance,
        //        MedicalInsuranceOutpatientNo = resultData.DocumentNo,
        //        CashPayment = resultData.CashPayment,
        //        SettlementNo = resultData.DocumentNo,
        //        AllAmount = outpatientPerson.MedicalTreatmentTotalCost,
        //        PatientName = outpatientPerson.PatientName,
        //        AccountAmountPay = resultData.AccountPayment,
        //        MedicalInsuranceType = userInfoData.InsuranceType == "342" ? "10" : userInfoData.InsuranceType,
        //    };

        //    var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
        //    var saveXml = new SaveXmlDataParam()
        //    {
        //        User = userBase,
        //        MedicalInsuranceBackNum = "zydj",
        //        MedicalInsuranceCode = "48",
        //        BusinessId = param.BusinessId,
        //        BackParam = strXmlBackParam
        //    };
        //    ////存基层
        //    _webBasicRepository.SaveXmlData(saveXml);
        //    var updateParam = new UpdateMedicalInsuranceResidentSettlementParam()
        //    {
        //        UserId = userBase.UserId,
        //        SelfPayFeeAmount = resultData.CashPayment,
        //        OtherInfo = JsonConvert.SerializeObject(resultData),
        //        Id = saveData.Id,
        //        SettlementNo = resultData.DocumentNo,
        //        MedicalInsuranceAllAmount = outpatientPerson.MedicalTreatmentTotalCost,
        //        SettlementTransactionId = userBase.TransKey,
        //        MedicalInsuranceState = MedicalInsuranceState.HisSettlement
        //    };
        //    //更新中间层
        //    _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParam);
        //    //明细存入
        //    _serviceBasicService.GetOutpatientDetailPerson(new OutpatientDetailParam()
        //    {   IsSave = true,
        //        BusinessId = param.BusinessId,
        //        User = userBase
        //    });
        //}
        /// <summary>
        /// 门诊计划生育结算查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerHospitalizationPreSettlementDto OutpatientPlanBirthSettlementQuery(
            UiBaseDataParam param)
        {//OutpatientPlanBirthSettlementQueryParam

            var resultData = new WorkerHospitalizationPreSettlementDto();
            //获取医保病人信息
            var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(new QueryMedicalInsuranceResidentInfoParam()
            {
                BusinessId = param.BusinessId
            });
            if (residentData==null) throw new Exception("获取当前病人医保信息失败!!!");
            if (residentData.MedicalInsuranceState!= MedicalInsuranceState.HisSettlement) throw  new Exception("当前病人未办理结算!!!");
            //医保登录
            _residentMedicalInsuranceService.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });
            _outpatientDepartmentRepository.OutpatientPlanBirthSettlementQuery(
                new OutpatientPlanBirthSettlementQueryParam()
                {

                });

            return resultData;
        }
        /// <summary>
        /// 获取门诊预结算参数
        /// </summary>
        /// <returns></returns>
        private OutpatientPlanBirthPreSettlementParam GetOutpatientPlanBirthPreSettlementParam(
            OutpatientPlanBirthPreSettlementUiParam param
        )
        {
            var userBase = _serviceBasicService.GetUserBaseInfo(param.UserId);
            userBase.TransKey = param.TransKey;
            var outpatientParam = new GetOutpatientPersonParam()
            {
                User = userBase,
                UiParam = param,
            };
            var outpatientPerson = _serviceBasicService.GetOutpatientPerson(outpatientParam);
            if (outpatientPerson == null) throw new Exception("his中未获取到当前病人!!!");
            var outpatientDetailPerson = _serviceBasicService.GetOutpatientDetailPerson(new OutpatientDetailParam()
            {
                User = userBase,
                BusinessId = param.BusinessId,
            });
            //获取主诊断
            var diagnosisData = outpatientPerson.DiagnosisList.FirstOrDefault(c => c.IsMainDiagnosis == "是");
            var inpatientDiagnosisDataDto = diagnosisData;
            string mainDiagnosis = null;
            if (inpatientDiagnosisDataDto != null)
            {
                if (!string.IsNullOrWhiteSpace(inpatientDiagnosisDataDto.ProjectCode))
                {
                    mainDiagnosis = inpatientDiagnosisDataDto.ProjectCode;
                }
                else
                {
                    throw new Exception("未对码诊断:" + "[" + inpatientDiagnosisDataDto.DiseaseName + "]" + "[" + inpatientDiagnosisDataDto.DiseaseCoding + "]");
                   
                }
            }

            var resultData = new OutpatientPlanBirthPreSettlementParam()
            {
                OutpatientNo = CommonHelp.GuidToStr(param.BusinessId),//outpatientPerson.OutpatientNumber,
                DiagnosisDate = Convert.ToDateTime(outpatientPerson.VisitDate).ToString("yyyyMMddHHmmss"),
                ProjectNum = outpatientDetailPerson.Count(),
                TotalAmount = outpatientPerson.MedicalTreatmentTotalCost,
                AfferentSign = param.AfferentSign,
                IdentityMark = param.IdentityMark,
                AdmissionMainDiagnosisIcd10 = mainDiagnosis,

            };
            var rowDataList = new List<PlanBirthSettlementRow>();
            //升序
            var dataSort = outpatientDetailPerson.OrderBy(c => c.BillTime).ToArray();
            int num = 0;
            foreach (var item in dataSort)
            {
                if (string.IsNullOrWhiteSpace(item.MedicalInsuranceProjectCode)) throw new Exception("[" + item + "]名称:" + item.DirectoryName + "未对码!!!");
                if (!string.IsNullOrWhiteSpace(item.MedicalInsuranceProjectCode))
                {
                    var row = new PlanBirthSettlementRow()
                    {
                        ColNum = num,
                        ProjectCode = item.MedicalInsuranceProjectCode,
                        ProjectName = item.DirectoryName,
                        UnitPrice = item.UnitPrice,
                        Quantity = item.Quantity,
                        TotalAmount = item.Amount,
                        Formulation = item.Formulation,
                        ManufacturerName = item.DrugProducingArea,
                        Dosage = item.Dosage,
                        Specification = item.Specification,
                        Usage = item.Usage
                    };

                    rowDataList.Add(row);
                    num++;
                }


            }

            resultData.RowDataList = rowDataList;


            return resultData;

        }
        /// <summary>
        /// 获取门诊计划生育结算参数
        /// </summary>
        /// <returns></returns>
        public OutpatientPlanBirthSettlementParam GetOutpatientPlanBirthSettlementParam(
            OutpatientPlanBirthSettlementUiParam param
        )
        {
            var userBase = _serviceBasicService.GetUserBaseInfo(param.UserId);
            userBase.TransKey = param.TransKey;
            var outpatientParam = new GetOutpatientPersonParam()
            {
                User = userBase,
                UiParam = param,
            };
            var outpatientPerson = _serviceBasicService.GetOutpatientPerson(outpatientParam);
            if (outpatientPerson == null) throw new Exception("his中未获取到当前病人!!!");
            var outpatientDetailPerson = _serviceBasicService.GetOutpatientDetailPerson(new OutpatientDetailParam()
            {
                User = userBase,
                BusinessId = param.BusinessId,
            });
            //获取主诊断
            var diagnosisData = outpatientPerson.DiagnosisList.FirstOrDefault(c => c.IsMainDiagnosis == "是");
            var inpatientDiagnosisDataDto = diagnosisData;
            string mainDiagnosis = null;
            if (inpatientDiagnosisDataDto != null)
            {
                if (!string.IsNullOrWhiteSpace(inpatientDiagnosisDataDto.ProjectCode))
                {
                    mainDiagnosis = inpatientDiagnosisDataDto.ProjectCode;
                   
                }
                else
                {
                    throw new Exception("未对码诊断:" + "[" + inpatientDiagnosisDataDto.DiseaseName + "]" + "[" + inpatientDiagnosisDataDto.DiseaseCoding + "]");
                }
            }
            var resultData = new OutpatientPlanBirthSettlementParam()
            {
                OutpatientNo = outpatientPerson.OutpatientNumber,
                DiagnosisDate =  Convert.ToDateTime(outpatientPerson.VisitDate).ToString("yyyyMMddHHmmss") ,
                ProjectNum = outpatientDetailPerson.Count(),
                TotalAmount = outpatientPerson.MedicalTreatmentTotalCost,
                AfferentSign = param.AfferentSign,
                AccountPayment=string.IsNullOrWhiteSpace(param.AccountPayment)==true?0: Convert.ToDecimal(param.AccountPayment) ,
                IdentityMark = param.IdentityMark,
                AdmissionMainDiagnosisIcd10 = mainDiagnosis

            };
            var rowDataList = new List<PlanBirthSettlementRow>();
            //升序
            var dataSort = outpatientDetailPerson.OrderBy(c => c.BillTime).ToArray();
            int num = 0;
            foreach (var item in dataSort)
            {   if (string.IsNullOrWhiteSpace(item.MedicalInsuranceProjectCode)) throw new Exception("[" + item + "]名称:" + item.DirectoryName + "未对码!!!");
                if (!string.IsNullOrWhiteSpace(item.MedicalInsuranceProjectCode))
                {
                    var row = new PlanBirthSettlementRow()
                    {
                        ColNum = num,
                        ProjectCode = item.MedicalInsuranceProjectCode,
                        ProjectName = item.DirectoryName,
                        UnitPrice = item.UnitPrice,
                        Quantity = item.Quantity,
                        TotalAmount = item.Amount,
                        Formulation = item.Formulation,
                        ManufacturerName = item.DrugProducingArea,
                        Dosage = item.Dosage,
                        Specification = item.Specification,
                        Usage = item.Usage
                    };

                    rowDataList.Add(row);
                    num++;
                }
             
             
            }

            resultData.RowDataList = rowDataList;
          
          
            return resultData;

        }

       
    }
}
