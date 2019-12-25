﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using BenDing.Domain.Models.Dto.JsonEntiy;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Enums;
using BenDing.Domain.Models.HisXml;
using BenDing.Domain.Models.Params.Resident;
using BenDing.Domain.Models.Params.SystemManage;
using BenDing.Domain.Models.Params.UI;
using BenDing.Domain.Models.Params.Web;
using BenDing.Domain.Xml;
using BenDing.Repository.Interfaces.Web;
using Newtonsoft.Json;

namespace BenDing.Repository.Providers.Web
{
    public class ResidentMedicalInsuranceRepository : IResidentMedicalInsuranceRepository
    {

        private readonly IHisSqlRepository _hisSqlRepository;
        private readonly IMedicalInsuranceSqlRepository _medicalInsuranceSqlRepository;
        private readonly ISystemManageRepository _systemManageRepository;
        private readonly IWebBasicRepository _webServiceBasic;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hisSqlRepository"></param>
        /// <param name="webBasicRepository"></param>
        /// <param name="medicalInsuranceSqlRepository"></param>
        /// <param name="systemManageRepository"></param>
        public ResidentMedicalInsuranceRepository(
            IHisSqlRepository hisSqlRepository,
            IWebBasicRepository webBasicRepository,
            IMedicalInsuranceSqlRepository medicalInsuranceSqlRepository,
            ISystemManageRepository systemManageRepository
        )
        {
            _hisSqlRepository = hisSqlRepository;
            _webServiceBasic = webBasicRepository;
            _medicalInsuranceSqlRepository = medicalInsuranceSqlRepository;
            _systemManageRepository = systemManageRepository;
        }

        public void Login(QueryHospitalOperatorParam param)
        {

            var userInfo = _systemManageRepository.QueryHospitalOperator(param);

            var result =
                MedicalInsuranceDll.ConnectAppServer_cxjb(userInfo.MedicalInsuranceAccount,
                    userInfo.MedicalInsurancePwd);
            if (result != 1)
            {
                XmlHelp.DeSerializerModel(new IniXmlDto(), true);
            }


        }

        /// <summary>
        /// 获取个人基础资料
        /// </summary>
        /// <param name="param"></param>
        public ResidentUserInfoDto GetUserInfo(ResidentUserInfoParam param)
        {

            var resulData = new ResidentUserInfoDto();

            var xmlStr = XmlHelp.SaveXml(param);
            if (xmlStr)
            {
                MedicalInsuranceDll.CallService_cxjb("CXJB001");
                var  data = XmlHelp.DeSerializerModel(new ResidentUserInfoJsonDto(), true);
                resulData= AutoMapper.Mapper.Map<ResidentUserInfoDto>(data);

            }


            return resulData;


        }

        /// <summary>
        /// 入院登记
        /// </summary>
        /// <returns></returns>
        public void HospitalizationRegister(ResidentHospitalizationRegisterParam param, UserInfoDto user)
        {

            var paramIni = param;
         
            var xmlStr = XmlHelp.SaveXml(paramIni);
            if (xmlStr)
            {
                int result = MedicalInsuranceDll.CallService_cxjb("CXJB002");
                if (result == 1)
                {
                    var data = XmlHelp.DeSerializerModel(new ResidentHospitalizationRegisterDto(), true);
                    var saveData = new MedicalInsuranceDto
                    {
                        AdmissionInfoJson = JsonConvert.SerializeObject(param),
                        BusinessId = param.BusinessId,
                        Id = Guid.NewGuid(),
                        IsModify = false,
                        InsuranceType =342,
                        MedicalInsuranceState = MedicalInsuranceState.MedicalInsuranceHospitalized
                    };
                    //保存中间库
                    _medicalInsuranceSqlRepository.SaveMedicalInsurance(user, saveData);
                   
                    //更新医保信息
                    var strXmlIntoParam = XmlSerializeHelper.XmlParticipationParam();
                    //回参构建
                    var xmlData = new HospitalizationRegisterXml()
                    {
                        MedicalInsuranceType = "10",
                        MedicalInsuranceHospitalizationNo = data.MedicalInsuranceInpatientNo,
                    };
                    var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
                    var saveXmlData = new SaveXmlData();
                    saveXmlData.OrganizationCode = user.OrganizationCode;
                    saveXmlData.AuthCode = user.AuthCode;
                    saveXmlData.BusinessId = param.BusinessId;
                    saveXmlData.TransactionId = saveData.Id.ToString("N");
                    saveXmlData.MedicalInsuranceBackNum = "CXJB002";
                    saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
                    saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
                    saveXmlData.MedicalInsuranceCode = "21";
                    saveXmlData.UserId = user.UserId;
                    //存基层
                    _webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData));
                    //更新中间库
                    saveData.MedicalInsuranceState = MedicalInsuranceState.HisHospitalized;
                    _medicalInsuranceSqlRepository.SaveMedicalInsurance(user, saveData);
                }
                else
                {
                    XmlHelp.DeSerializerModel(new IniDto(), true);
                }
            }
        }

        /// <summary>
        /// 修改入院登记
        /// </summary>
        /// <param name="param"></param>
        /// <param name="user"></param>
        public void HospitalizationModify(HospitalizationModifyParam param, UserInfoDto user)
        {
            var xmlStr = XmlHelp.SaveXml(param);
            if (xmlStr)
            {
                int result = MedicalInsuranceDll.CallService_cxjb("CXJB003");
                if (result == 1)
                {
                    var resultData = XmlHelp.DeSerializerModel(new IniDto(), true);
                    var strXmlIntoParam = XmlSerializeHelper.XmlParticipationParam();
                    //回参构建
                    var xmlData = new HospitalizationRegisterXml()
                    {
                        
                        MedicalInsuranceHospitalizationNo = param.MedicalInsuranceHospitalizationNo,
                    };
                    var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
                   
                    var saveXmlData = new SaveXmlData();
                    saveXmlData.OrganizationCode = user.OrganizationCode;
                    saveXmlData.AuthCode = user.AuthCode;
                    saveXmlData.BusinessId = param.BusinessId;
                    saveXmlData.TransactionId = param.TransactionId.ToString("N");
                    saveXmlData.MedicalInsuranceBackNum = "CXJB003";
                    saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
                    saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
                    saveXmlData.MedicalInsuranceCode = "23";
                    saveXmlData.UserId = user.UserId;
                    _webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData));
                    var paramStr = "";
                    var queryData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(
                        new QueryMedicalInsuranceResidentInfoParam
                        {
                            BusinessId = param.BusinessId,
                            OrganizationCode = user.OrganizationCode
                        });
                    if (!string.IsNullOrWhiteSpace(queryData.AdmissionInfoJson))
                    {
                        var data =
                            JsonConvert.DeserializeObject<QueryMedicalInsuranceDetailDto>(queryData
                                .AdmissionInfoJson);
                        if (!string.IsNullOrWhiteSpace(param.AdmissionDate))
                            data.AdmissionDate = param.AdmissionDate;
                        if (!string.IsNullOrWhiteSpace(param.AdmissionMainDiagnosis))
                            data.AdmissionMainDiagnosis = param.AdmissionMainDiagnosis;
                        if (!string.IsNullOrWhiteSpace(param.AdmissionMainDiagnosisIcd10))
                            data.AdmissionMainDiagnosisIcd10 = param.AdmissionMainDiagnosisIcd10;
                        if (!string.IsNullOrWhiteSpace(param.DiagnosisIcd10Three))
                            data.DiagnosisIcd10Three = param.DiagnosisIcd10Three;
                        if (!string.IsNullOrWhiteSpace(param.DiagnosisIcd10Two))
                            data.DiagnosisIcd10Two = param.DiagnosisIcd10Two;
                        if (!string.IsNullOrWhiteSpace(param.FetusNumber)) data.FetusNumber = param.FetusNumber;
                        if (!string.IsNullOrWhiteSpace(param.HouseholdNature))
                            data.HouseholdNature = param.HouseholdNature;
                        if (!string.IsNullOrWhiteSpace(param.InpatientDepartmentCode))
                            data.InpatientDepartmentCode = param.InpatientDepartmentCode;
                        if (!string.IsNullOrWhiteSpace(param.BedNumber)) data.BedNumber = param.BedNumber;
                        data.Id = queryData.Id;
                        paramStr = JsonConvert.SerializeObject(data);
                    }

                    var saveData = new MedicalInsuranceDto
                    {
                        AdmissionInfoJson = paramStr,
                        BusinessId = queryData.BusinessId,
                        Id = queryData.Id,
                        IsModify = true,
                        MedicalInsuranceHospitalizationNo = queryData.MedicalInsuranceHospitalizationNo
                    };
                    _medicalInsuranceSqlRepository.SaveMedicalInsurance(user, saveData);
                    //日志
                    var logParam = new AddHospitalLogParam();
                    logParam.User = user;
                    logParam.RelationId = queryData.Id;
                    logParam.JoinOrOldJson = queryData.AdmissionInfoJson;
                    logParam.ReturnOrNewJson = paramStr;
                    logParam.Remark = "医保入院登记修改";
                    _systemManageRepository.AddHospitalLog(logParam);
                }

            }

        }

        /// <summary>
        /// 项目下载
        /// </summary>
        public ResidentProjectDownloadDto ProjectDownload(ResidentProjectDownloadParam param)
        {

            var data = new ResidentProjectDownloadDto();
            var xmlStr = XmlHelp.SaveXml(param);


            if (xmlStr)
            {
                int result = MedicalInsuranceDll.CallService_cxjb("CXJB019");
                if (result == 1)
                {
                    string strXml = XmlHelp.DeSerializerModelStr("ROWDATA");
                    data = XmlHelp.DeSerializer<ResidentProjectDownloadDto>(strXml);

                }
            }


            return data;


        }

        /// <summary>
        /// 项目下载总条数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Int64 ProjectDownloadCount(ResidentProjectDownloadParam param)
        {

            Int64 resultData = 0;
            var xmlStr = XmlHelp.SaveXml(param);
            if (xmlStr)
            {

                int result = MedicalInsuranceDll.CallService_cxjb("CXJB019");
                if (result == 1)
                {
                    var data = XmlHelp.DeSerializerModel(new ProjectDownloadCountDto(), true);
                    resultData = data.PO_CNT;
                }
            }

            return resultData;

        }

        /// <summary>
        /// 住院预结算
        /// </summary>
        /// <param name="param"></param>
        /// <param name="infoParam"></param>
        /// <returns></returns>
        public HospitalizationPresettlementDto HospitalizationPreSettlement(HospitalizationPresettlementParam param,
            HospitalizationPreSettlementInfoParam infoParam)
        {


            var data = new HospitalizationPresettlementDto();
            var xmlStr = XmlHelp.SaveXml(param);
            if (xmlStr)
            {
                int result = MedicalInsuranceDll.CallService_cxjb("CXJB009");
                if (result == 1)
                {
                   var dataIni = XmlHelp.DeSerializerModel(new HospitalizationPresettlementJsonDto(), true);
                    if (dataIni != null) //var c = AutoMapper.Mapper.Map<b>(a);
                    {
                        data=AutoMapper.Mapper.Map<HospitalizationPresettlementDto>(dataIni);
                    }

                    //报销金额 =统筹支付+补充险支付+生育补助+民政救助+民政重大疾病救助+精准扶贫+民政优抚+其它支付
                    decimal reimbursementExpenses =
                        data.BasicOverallPay + data.SupplementPayAmount + data.BirthAallowance +
                        data.CivilAssistancePayAmount + data.CivilAssistanceSeriousIllnessPayAmount +
                        data.AccurateAssistancePayAmount + data.CivilServicessistancePayAmount +
                        data.OtherPaymentAmount;
                    var updateParam = new UpdateMedicalInsuranceResidentSettlementParam()
                    {

                        ReimbursementExpensesAmount = CommonHelp.ValueToDouble(reimbursementExpenses),
                        SelfPayFeeAmount = data.CashPayment,
                        OtherInfo = JsonConvert.SerializeObject(data),
                        Id = infoParam.Id,
                        UserId = infoParam.User.UserId,
                        SettlementNo = data.DocumentNo,
                        MedicalInsuranceAllAmount = data.TotalAmount,
                        PreSettlementTransactionId = Guid.NewGuid().ToString("N"),
                        MedicalInsuranceState= MedicalInsuranceState.MedicalInsurancePreSettlement
                    };
                    //存入中间库
                    _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParam);
                    var logParam = new AddHospitalLogParam()
                    {
                        JoinOrOldJson = JsonConvert.SerializeObject(param),
                        ReturnOrNewJson = JsonConvert.SerializeObject(data),
                        User = infoParam.User,
                   
                        Remark = "住院病人预结算"
                    };
                    _systemManageRepository.AddHospitalLog(logParam);
                   
                   
                }
                else
                {
                    throw new Exception("住院预结算执行失败!!!");
                }
            }

            return data;



        }

        /// <summary>
        /// 医保出院费用结算
        /// </summary>
        /// <param name="param"></param>
        /// <param name="infoParam"></param>
        /// <returns></returns>
        public HospitalizationPresettlementDto LeaveHospitalSettlement(LeaveHospitalSettlementParam param,
            LeaveHospitalSettlementInfoParam infoParam)
        {


            var data = new HospitalizationPresettlementDto();
            var xmlStr = XmlHelp.SaveXml(param);
            if (xmlStr)
            {
                int result = MedicalInsuranceDll.CallService_cxjb("CXJB010");
                if (result == 1)
                {
                    
                    var dataIni = XmlHelp.DeSerializerModel(new HospitalizationPresettlementJsonDto(), true);
                    if (dataIni != null) //var c = AutoMapper.Mapper.Map<b>(a);
                    {
                        data = AutoMapper.Mapper.Map<HospitalizationPresettlementDto>(dataIni);
                    }
                    //报销金额 =统筹支付+补充险支付+生育补助+民政救助+民政重大疾病救助+精准扶贫+民政优抚+其它支付
                    decimal reimbursementExpenses =
                        data.BasicOverallPay + data.SupplementPayAmount + data.BirthAallowance +
                        data.CivilAssistancePayAmount + data.CivilAssistanceSeriousIllnessPayAmount +
                        data.AccurateAssistancePayAmount + data.CivilServicessistancePayAmount +
                        data.OtherPaymentAmount;
                    var updateParam = new UpdateMedicalInsuranceResidentSettlementParam()
                    {
                        UserId = infoParam.User.UserId,
                        ReimbursementExpensesAmount = CommonHelp.ValueToDouble(reimbursementExpenses),
                        SelfPayFeeAmount = data.CashPayment,
                        OtherInfo = JsonConvert.SerializeObject(data),
                        Id = infoParam.Id,
                        SettlementNo = data.DocumentNo,
                        MedicalInsuranceAllAmount = data.TotalAmount,
                        SettlementTransactionId = Guid.NewGuid().ToString("N"),
                        MedicalInsuranceState = MedicalInsuranceState.MedicalInsuranceSettlement
                    };
                    //存入中间层
                    _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParam);

                    //更新医保信息
                    var strXmlIntoParam = XmlSerializeHelper.XmlParticipationParam();
                    var userInfoData = GetUserInfo(new ResidentUserInfoParam()
                    {
                        InformationNumber = infoParam.IdCardNo,
                        IdentityMark = "1"
                    });
                    //回参构建
                    var xmlData = new HospitalSettlementXml()
                    {

                        MedicalInsuranceHospitalizationNo = param.MedicalInsuranceHospitalizationNo,
                        CashPayment = data.CashPayment,
                        SettlementNo = data.DocumentNo,
                        PaidAmount= data.PaidAmount,
                        AllAmount = data.TotalAmount,
                        PatientName = userInfoData.PatientName,
                        AccountBalance = userInfoData.ResidentInsuranceBalance,
                        AccountAmountPay = 0

                    };
                    var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
                    var saveXmlData = new SaveXmlData();
                    saveXmlData.OrganizationCode = infoParam.User.OrganizationCode;
                    saveXmlData.AuthCode = infoParam.User.AuthCode;
                    saveXmlData.BusinessId = infoParam.BusinessId;
                    saveXmlData.TransactionId = updateParam.SettlementTransactionId;
                    saveXmlData.MedicalInsuranceBackNum = "CXJB009";
                    saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
                    saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
                    saveXmlData.MedicalInsuranceCode = "41";
                    saveXmlData.UserId = infoParam.User.UserId;
                    //存基层
                    _webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData));
                    //更新中间层
                    updateParam.IsHisUpdateState = true;
                    updateParam.MedicalInsuranceState = MedicalInsuranceState.HisSettlement;
                    //添加日志
                    var logParam = new AddHospitalLogParam()
                    {
                        JoinOrOldJson = JsonConvert.SerializeObject(param),
                        ReturnOrNewJson = JsonConvert.SerializeObject(data),
                        User = infoParam.User,
                      
                        Remark = "住院病人出院结算",
                        RelationId = infoParam.Id,
                    };

                    _systemManageRepository.AddHospitalLog(logParam);
                }
                else
                {
                    throw new Exception("住院病人出院结算执行失败!!!");
                }
            }

            return data;



        }

        /// <summary>
        ///查询医保出院结算信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public HospitalizationPresettlementDto QueryLeaveHospitalSettlement(QueryLeaveHospitalSettlementParam param)
        {


            var data = new HospitalizationPresettlementDto();
            var xmlStr = XmlHelp.SaveXml(param);
            if (xmlStr)
            {
                int result = MedicalInsuranceDll.CallService_cxjb("CXJB012");
                if (result == 1)
                {
                    var dataIni = XmlHelp.DeSerializerModel(new HospitalizationPresettlementJsonDto(), true);
                    if (dataIni != null) //var c = AutoMapper.Mapper.Map<b>(a);
                    {
                        data = AutoMapper.Mapper.Map<HospitalizationPresettlementDto>(dataIni);
                    }
                    //data = XmlHelp.DeSerializerModel(new HospitalizationPresettlementDto(), true);
                }
                else
                {
                    throw new Exception("医保出院结算信息查询执行失败!!!");
                }
            }

            return data;



        }

        /// <summary>
        /// 取消医保出院结算
        /// </summary>
        /// <param name="param"></param>
        /// <param name="infoParam"></param>
        /// <returns></returns>
        public void LeaveHospitalSettlementCancel(LeaveHospitalSettlementCancelParam param,
            LeaveHospitalSettlementCancelInfoParam infoParam)
        {
            var cancelLimit = param.CancelLimit;
            string cancelData;
            if (param.CancelLimit == "1")
            {
                cancelData = Cancel(param);

            }
            else
            {
                param.CancelLimit = "1";
                cancelData = Cancel(param);

                param.CancelLimit = "2";
                if (cancelData == "1")
                {
                    cancelData = Cancel(param);
                }
            }

            if (cancelData == "1")
            {
                //取消交易id
                var cancelTransactionId = Guid.NewGuid().ToString("N");
                var updateParam = new UpdateMedicalInsuranceResidentSettlementParam()
                {
                    UserId = infoParam.User.UserId,
                    Id = infoParam.Id,
                    CancelTransactionId = cancelTransactionId,
                    MedicalInsuranceState = MedicalInsuranceState.MedicalInsurancePreSettlement
                };
                //存入中间层
                _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParam);
                //添加日志
                var logParam = new AddHospitalLogParam()
                {
                    JoinOrOldJson = JsonConvert.SerializeObject(param),
                    User = infoParam.User,
                    Remark = "职工住院结算取消",
                    RelationId = infoParam.Id,
                };
               
                if (cancelLimit == "2") //取消结算,并删除资料<==>删除资料与取消入院
                {
                    //his取消入院登记
                    var strXmlIntoParam = XmlSerializeHelper.XmlSerialize(param);
                    //回参
                    var xmlData = new HospitalizationRegisterXml()
                    {

                        MedicalInsuranceHospitalizationNo = param.MedicalInsuranceHospitalizationNo,
                    };
                    var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
                  
                    var saveXmlData = new SaveXmlData();
                    saveXmlData.OrganizationCode = infoParam.User.OrganizationCode;
                    saveXmlData.AuthCode = infoParam.User.AuthCode;
                    saveXmlData.BusinessId = infoParam.BusinessId;
                    saveXmlData.TransactionId = cancelTransactionId;
                    saveXmlData.MedicalInsuranceBackNum = "CXJB004";
                    saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
                    saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
                    saveXmlData.MedicalInsuranceCode = "22";
                    saveXmlData.UserId = infoParam.User.UserId;

                    _webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData));

                    var updateParamData = new UpdateMedicalInsuranceResidentSettlementParam()
                    {
                        UserId = infoParam.User.UserId,
                        Id = infoParam.Id,
                        CancelTransactionId = cancelTransactionId,
                        MedicalInsuranceState = MedicalInsuranceState.MedicalInsuranceCancelHospitalized,
                        IsHisUpdateState=true
                    };
                    //更新中间层
                    _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParamData);
                }
            }

            string Cancel(LeaveHospitalSettlementCancelParam paramc)
            {
                string resultData = null;
                var xmlStr = XmlHelp.SaveXml(paramc);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDll.CallService_cxjb("CXJB011");
                    if (result == 1)
                    {
                        var data = XmlHelp.DeSerializerModel(new IniDto(), true);
                        resultData = data.ReturnState;
                    }

                }

                return resultData;
            }




        }

        /// <summary>
        /// 医保处方上传
        /// </summary>
        /// <param name="param"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public RetrunPrescriptionUploadDto PrescriptionUpload(PrescriptionUploadUiParam param, UserInfoDto user)
        {

            //处方上传解决方案
            //1.判断是id上传还是单个用户上传
            //3.获取医院等级判断金额是否符合要求
            //4.数据上传
            //4.1 id上传
            //4.1.2 获取医院等级判断金额是否符合要求
            //4.1.3 数据上传
            //4.1.3.1 数据上传失败,数据回写到日志
            //4.1.3.2 数据上传成功,保存批次号，数据回写至基层
            //4.2   单个病人整体上传
            //4.2.2 获取医院等级判断金额是否符合要求
            //4.2.3 数据上传
            //4.2.3.1 数据上传失败,数据回写到日志
            var resultData = new RetrunPrescriptionUploadDto();
            List<QueryInpatientInfoDetailDto> queryData;
            var queryParam = new InpatientInfoDetailQueryParam();
            //1.判断是id上传还是单个用户上传
            if (param.DataIdList != null && param.DataIdList.Any())
            {
                queryParam.IdList = param.DataIdList;
                queryParam.UploadMark = 0;
            }
            else
            {
                queryParam.BusinessId = param.BusinessId;
                queryParam.UploadMark = 0;
            }

            //获取病人明细
            queryData = _hisSqlRepository.InpatientInfoDetailQuery(queryParam);
            if (queryData.Any())
            {
                var queryBusinessId = (!string.IsNullOrWhiteSpace(queryParam.BusinessId))
                    ? param.BusinessId
                    : queryData.Select(c => c.BusinessId).FirstOrDefault();
                var medicalInsuranceParam = new QueryMedicalInsuranceResidentInfoParam()
                { BusinessId = queryBusinessId };
                //获取病人医保信息
                var medicalInsurance = 
                    _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(medicalInsuranceParam);
                if (medicalInsurance == null)
                {
                    if (!string.IsNullOrWhiteSpace(queryParam.BusinessId))
                    {
                        resultData.Msg += "病人未办理医保入院";
                    }
                    else
                    {
                        throw new Exception("病人未办理医保入院");
                    }
                }
                else
                {

                    var queryPairCodeParam = new QueryMedicalInsurancePairCodeParam()
                    {
                        DirectoryCodeList = queryData.Select(d => d.DirectoryCode).Distinct().ToList(),
                        OrganizationCode = user.OrganizationCode,
                    };
                    //获取医保对码数据
                    var queryPairCode =
                        _medicalInsuranceSqlRepository.QueryMedicalInsurancePairCode(queryPairCodeParam);
                    //处方上传数据金额验证
                    var validData = PrescriptionDataUnitPriceValidation(queryData, queryPairCode, user);
                    var validDataList = validData.Values.FirstOrDefault();
                    //错误提示信息
                    var validDataMsg = validData.Keys.FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(validDataMsg))
                    {
                        resultData.Msg += validDataMsg;
                    }

                    //获取处方上传入参
                    var paramIni = GetPrescriptionUploadParam(validDataList, queryPairCode, user,
                        medicalInsurance.InsuranceType);
                    //医保住院号
                    paramIni.MedicalInsuranceHospitalizationNo = medicalInsurance.MedicalInsuranceHospitalizationNo;
                    int num = paramIni.RowDataList.Count;
                    resultData.Count = num;
                    int a = 0;
                    int limit = 40; //限制条数
                    var count = Convert.ToInt32(num / limit) + ((num % limit) > 0 ? 1 : 0);
                    var idList = new List<Guid>();
                    while (a < count)
                    {
                        //排除已上传数据

                        var rowDataListAll = paramIni.RowDataList.Where(d => !idList.Contains(d.Id))
                            .OrderBy(c => c.PrescriptionSort).ToList();
                        var sendList = rowDataListAll.Take(limit).Select(s => s.Id).ToList();
                        //新的数据上传参数
                        var uploadDataParam = paramIni;
                        uploadDataParam.RowDataList = rowDataListAll.Where(c => sendList.Contains(c.Id)).ToList();
                        //数据上传
                        var uploadData = PrescriptionUploadData(uploadDataParam, param.BusinessId, user);
                        if (uploadData.ReturnState != "1")
                        {
                            resultData.Msg += uploadData.ReturnState;
                        }
                        else
                        {
                            //更新数据上传状态
                            idList.AddRange(sendList);
                            //获取总行数
                            resultData.Num += sendList.Count();
                        }

                        a++;
                    }

                }
            }


            return resultData;

        }

        /// <summary>
        /// 删除医保处方数据
        /// </summary>
        /// <param name="param"></param>
        /// <param name="ids"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public void DeletePrescriptionUpload(DeletePrescriptionUploadParam param, List<Guid> ids, UserInfoDto user)
        {

            var xmlStr = XmlHelp.SaveXml(param);
            if (xmlStr)
            {
                int result = 1; //MedicalInsuranceDll.CallService_cxjb("CXJB005");
                if (result == 1)
                {
                    XmlHelp.DeSerializerModel(new IniDto(), true);
                    //添加批次
                    var updateFeeParam =
                        ids.Select(c => new UpdateHospitalizationFeeParam { Id = c })
                            .ToList(); //new  UpdateHospitalizationFeeParam
                    _medicalInsuranceSqlRepository.UpdateHospitalizationFee(updateFeeParam, true, user);
                    //添加日志
                    var logParam = new AddHospitalLogParam()
                    {
                        JoinOrOldJson = JsonConvert.SerializeObject(param),
                        ReturnOrNewJson = JsonConvert.SerializeObject(ids),
                        User = user,
                        Remark = "[R][HospitalizationFee]删除处方数据",
                    };
                    _systemManageRepository.AddHospitalLog(logParam);
                }
            }


        }

        /// <summary>
        ///	医保处方明细查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<QueryPrescriptionDetailListDto> QueryPrescriptionDetail(QueryPrescriptionDetailParam param)
        {


            var resultdata = new List<QueryPrescriptionDetailListDto>();

            var xmlStr = XmlHelp.SaveXml(param);
            if (xmlStr)
            {
                int result = MedicalInsuranceDll.CallService_cxjb("CXJB006");
                if (result == 1)
                { 

                    string strXml = XmlHelp.DeSerializerModelStr("CFMX");
                    var data = XmlHelp.DeSerializer<QueryPrescriptionDetailDto>(strXml);
                    if (data.RowDataList == null && (data.RowDataList ?? throw new InvalidOperationException()).Any())
                    {
                        resultdata = data.RowDataList.Select(c => new QueryPrescriptionDetailListDto()
                        {
                            ProjectName = c.ProjectName,
                            ProjectCode = c.ProjectCode,
                            Amount = c.Amount,
                            ColNum = c.ColNum,
                            Dosage = c.Dosage,
                            FixedEncoding = c.FixedEncoding,
                            Formulation = c.Formulation,
                            HospitalNumber = c.HospitalNumber,
                            HospitalizationNo = c.HospitalizationNo,
                            PrescriptionSort = c.PrescriptionSort,
                            Quantity = c.Quantity,
                            Unit = c.Unit,
                            UnitPrice = c.UnitPrice,
                            Remark = c.Remark,
                            Usage = c.Usage,
                            UseDays = c.UseDays,
                            Specification = c.Specification,
                            PrescriptionNum = c.PrescriptionNum,
                            UseFrequency = c.UseFrequency,
                            ResidentSelfPayProportion = c.ResidentSelfPayProportion,
                            ProjectLevel = c.ProjectLevel != null
                                ? ((ProjectLevel)Convert.ToInt32(c.ProjectLevel)).ToString()
                                : c.ProjectLevel,
                            ProjectCodeType = c.ProjectCodeType != null
                                ? ((ProjectCodeType)Convert.ToInt32(c.ProjectCodeType)).ToString()
                                : c.ProjectCodeType,
                            DirectoryDate = c.DirectoryDate != null
                                ? Convert.ToDateTime(c.DirectoryDate)
                                : (DateTime?)null,
                            DirectorySettlementDate = c.DirectorySettlementDate != null
                                ? Convert.ToDateTime(c.DirectorySettlementDate)
                                : (DateTime?)null,

                        }).ToList();
                    }
                }
                else
                {
                    throw new Exception("医保处方明细查询执行失败!!!");
                }
            }

            return resultdata;



        }

        /// <summary>
        /// 处方数据上传
        /// </summary>
        /// <param name="param"></param>
        /// <param name="businessId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private PrescriptionUploadDto PrescriptionUploadData(PrescriptionUploadParam param, string businessId,
            UserInfoDto user)
        {
            var data = new PrescriptionUploadDto();
            var rowXml = param.RowDataList.Select(c =>new HospitalizationFeeUploadRowXml(){SerialNumber = c.DetailId }).ToList();
            var xmlStr = XmlHelp.SaveXml(param);
            if (xmlStr)
            {
                int result = MedicalInsuranceDll.CallService_cxjb("CXJB004");
                if (result == 1)
                {
                    //如果业务id存在则不直接抛出异常
                    if (!string.IsNullOrWhiteSpace(businessId))
                    {
                        data = XmlHelp.DeSerializerModel(new PrescriptionUploadDto(), false);
                    }
                    else
                    {
                        data = XmlHelp.DeSerializerModel(new PrescriptionUploadDto(), true);
                    }

                    if (data.ReturnState == "1")
                    {
                        //交易码
                        var transactionId = Guid.NewGuid().ToString("N");
                        //添加批次
                        var updateFeeParam = param.RowDataList.Select(d => new UpdateHospitalizationFeeParam
                        {
                            Id = d.Id,
                            BatchNumber = data.ProjectBatch,
                            TransactionId = transactionId,
                            UploadAmount = d.Amount
                        }).ToList();
                        _medicalInsuranceSqlRepository.UpdateHospitalizationFee(updateFeeParam, false, user);
                        //保存至基层
                        var strXmlIntoParam = XmlSerializeHelper.XmlParticipationParam();
                        //回参
                        var xmlData = new HospitalizationFeeUploadXml()
                        {

                            MedicalInsuranceHospitalizationNo = param.MedicalInsuranceHospitalizationNo,
                            RowDataList = rowXml,
                        };
                        var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
                      
                        var saveXmlData = new SaveXmlData();
                        saveXmlData.OrganizationCode = user.OrganizationCode;
                        saveXmlData.AuthCode = user.AuthCode;
                        saveXmlData.BusinessId = businessId;
                        saveXmlData.TransactionId = transactionId;
                        saveXmlData.MedicalInsuranceBackNum = "CXJB004";
                        saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam );
                        saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
                        saveXmlData.MedicalInsuranceCode = "31";
                        saveXmlData.UserId = user.UserId;
                        _webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData));
                        var batchConfirmParam = new BatchConfirmParam()
                        {
                            ConfirmType = 1,
                            MedicalInsuranceHospitalizationNo = param.MedicalInsuranceHospitalizationNo,
                            BatchNumber = data.ProjectBatch,
                            Operators = CommonHelp.GuidToStr(user.UserId)
                        };
                        var batchConfirmData = BatchConfirm(batchConfirmParam);
                        //如果批次号确认失败,更新病人处方上传标示为 0(未上传)
                        if (batchConfirmData == false)
                        {
                            _medicalInsuranceSqlRepository.UpdateHospitalizationFee(updateFeeParam, true, user);
                        }

                    }

                }
                else
                {
                    throw new Exception("[" + user.UserId + "]" + "处方上传执行出错!!!");
                }


            }

            return data;

        }

        /// <summary>
        /// 获取处方上传入参
        /// </summary>
        /// <returns></returns>
        private PrescriptionUploadParam GetPrescriptionUploadParam(List<QueryInpatientInfoDetailDto> param,
            List<QueryMedicalInsurancePairCodeDto> pairCodeList, UserInfoDto user, string insuranceType)
        {

            var resultData = new PrescriptionUploadParam();

            resultData.Operators = CommonHelp.GuidToStr(user.UserId);
            var rowDataList = new List<PrescriptionUploadRowParam>();
            foreach (var item in param)
            {
                var pairCodeData = pairCodeList.FirstOrDefault(c => c.DirectoryCode == item.DirectoryCode);

                if (pairCodeData != null)
                {
                    //自付金额
                    decimal residentSelfPayProportion = 0;
                    if (insuranceType == "342") //居民   
                    {
                        residentSelfPayProportion = CommonHelp.ValueToDouble(
                            (item.Amount + item.AdjustmentDifferenceValue) *
                            pairCodeData.ResidentSelfPayProportion);
                    }

                    if (insuranceType == "310") //职工
                    {
                        residentSelfPayProportion = CommonHelp.ValueToDouble(
                            (item.Amount + item.AdjustmentDifferenceValue) * pairCodeData.WorkersSelfPayProportion);
                    }

                    var rowData = new PrescriptionUploadRowParam()
                    {
                        ColNum = 0,
                        PrescriptionNum = item.DocumentNo,
                        PrescriptionSort = item.DataSort,
                        ProjectCode = pairCodeData.ProjectCode,
                        FixedEncoding = pairCodeData.FixedEncoding,
                        DirectoryDate = CommonHelp.FormatDateTime(item.BillTime),
                        DirectorySettlementDate = CommonHelp.FormatDateTime(item.BillTime),
                        ProjectCodeType = pairCodeData.ProjectCodeType,
                        ProjectName = pairCodeData.ProjectName,
                        ProjectLevel = pairCodeData.ProjectLevel,
                        UnitPrice = item.UnitPrice,
                        Quantity = item.Quantity,
                        Amount = item.Amount,
                        ResidentSelfPayProportion = residentSelfPayProportion, //自付金额计算
                        Formulation = pairCodeData.Formulation,
                        Dosage = (!string.IsNullOrWhiteSpace(item.Dosage))
                            ? CommonHelp.ValueToDouble(Convert.ToDecimal(item.Dosage))
                            : 0,
                        UseFrequency = "0",
                        Usage = item.Usage,
                        Specification = item.Specification,
                        Unit = item.Unit,
                        UseDays = 0,
                        Remark = "",
                        DoctorJobNumber = item.OperateDoctorId,
                        Id = item.Id,
                        LimitApprovalDate = "",
                        LimitApprovalUser = "",
                        LimitApprovalMark = "",
                        LimitApprovalRemark = ""

                    };
                    //是否现在使用药品
                    if (pairCodeData.RestrictionSign == "1")
                    {
                        rowData.LimitApprovalDate = CommonHelp.FormatDateTime(item.BillTime);
                        rowData.LimitApprovalUser = rowData.DoctorJobNumber;
                        rowData.LimitApprovalMark = "1";
                      
                    }

                    rowDataList.Add(rowData);
                }


            }

            resultData.RowDataList = rowDataList;
            return resultData;

        }

        ///  <summary>
        /// 处方上传数据金额验证
        ///  </summary>
        ///  <param name="param"></param>
        /// <param name="pairCode"></param>
        /// <param name="user"></param>
        ///  <returns></returns>
        private Dictionary<string, List<QueryInpatientInfoDetailDto>> PrescriptionDataUnitPriceValidation(
            List<QueryInpatientInfoDetailDto> param,
            List<QueryMedicalInsurancePairCodeDto> pairCode, UserInfoDto user)
        {

            var resultData = new Dictionary<string, List<QueryInpatientInfoDetailDto>>();
            var dataList = new List<QueryInpatientInfoDetailDto>();
            //获取医院等级
            var gradeData = _systemManageRepository.QueryHospitalOrganizationGrade(user.OrganizationCode);
            var grade = gradeData.OrganizationGrade;
            string msg = "";
            foreach (var item in param)
            {
                var queryData = pairCode.FirstOrDefault(c => c.DirectoryCode == item.DirectoryCode);
                if (queryData != null)
                {
                    decimal queryAmount = 0;
                    if (grade == OrganizationGrade.二级乙等以下) queryAmount = queryData.ZeroBlock;
                    if (grade == OrganizationGrade.二级乙等) queryAmount = queryData.OneBlock;
                    if (grade == OrganizationGrade.二级甲等) queryAmount = queryData.TwoBlock;
                    if (grade == OrganizationGrade.三级乙等) queryAmount = queryData.ThreeBlock;
                    if (grade == OrganizationGrade.三级甲等)
                    {
                        queryAmount = queryData.FourBlock;
                    }

                    //限价大于零判断
                    if (queryAmount > 0)
                    {
                        if (item.Amount < queryAmount)
                        {
                            dataList.Add(item);
                        }
                        else
                        {
                            msg += item.DirectoryCode + ",";
                        }
                    }
                    else
                    {
                        dataList.Add(item);
                    }
                }
            }

            msg = msg != "" ? msg + "金额超出限制等级" : "";
            resultData.Add(msg, dataList);
            return resultData;

        }

        /// <summary>
        /// 批次确认
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private bool BatchConfirm(BatchConfirmParam param)
        {

            var resultData = true;
            PrescriptionUploadDto data;
            var xmlStr = XmlHelp.SaveXml(param);
            if (xmlStr)
            {
                int result = MedicalInsuranceDll.CallService_cxjb("CXJB007");
                if (result == 1)
                {
                    data = XmlHelp.DeSerializerModel(new PrescriptionUploadDto(), false);
                    if (data.ReturnState != "1")
                    {
                        resultData = false;
                    }
                }
            }

            return resultData;

        }
    }

}

