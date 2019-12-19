﻿using BenDing.Domain.Models.Dto.Workers;
using BenDing.Domain.Models.Params.Workers;
using BenDing.Repository.Interfaces.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Web;
using BenDing.Domain.Xml;
using Newtonsoft.Json;
using BenDing.Domain.Models.Params.SystemManage;

namespace BenDing.Repository.Providers.Web
{
    public class WorkerMedicalInsuranceRepository : IWorkerMedicalInsuranceRepository
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
        public WorkerMedicalInsuranceRepository(
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
        /// <summary>
        /// 入院登记
        /// </summary>
        /// <returns></returns>
        public WorkerHospitalizationRegisterDto WorkerHospitalizationRegister(WorKerHospitalizationRegisterParam param)
        {
            var resultData = new WorkerHospitalizationRegisterDto();
            //社保住院号
            var medicalInsuranceHospitalizationNo = new StringBuilder(1024);
            //审批编号
            var approvalNumber = new StringBuilder(1024);
            //年住院次数
            var yearHospitalizationNumber = new StringBuilder(1024);
            //统筹已付金额
            var overallPlanningAlreadyAmount = new StringBuilder(1024);
            //统筹可付金额
            var overallPlanningCanAmount = new StringBuilder(1024);
            //返回状态
            var resultState = new StringBuilder(1024);
            //消息
            var msg = new StringBuilder(1024);

            WorkerMedicalInsurance.HospitalizationRegister
                (param.AfferentSign,
                param.IdentityMark,
                param.AdministrativeArea,
                param.OrganizationCode,
                param.MedicalCategory,
                param.AdmissionDate,
                param.AdmissionMainDiagnosisIcd10,
                param.DiagnosisIcd10Two,
                param.DiagnosisIcd10Three,
                param.AdmissionMainDiagnosis,
                param.InpatientArea,
                param.BedNumber,
                param.HospitalizationNo,
                param.Operators,
                medicalInsuranceHospitalizationNo,
                approvalNumber,
                yearHospitalizationNumber,
                overallPlanningAlreadyAmount,
                overallPlanningCanAmount,
                resultState,
                msg
                );
            if (resultState.ToString() != "1")
            {
                throw new Exception(msg.ToString());
            }
            else
            {
                resultData = new WorkerHospitalizationRegisterDto()
                {
                    MedicalInsuranceHospitalizationNo = medicalInsuranceHospitalizationNo.ToString(),
                    ApprovalNumber = approvalNumber.ToString(),
                    YearHospitalizationNumber = yearHospitalizationNumber.ToString(),
                    OverallPlanningAlreadyAmount = overallPlanningAlreadyAmount.ToString(),
                    OverallPlanningCanAmount = overallPlanningCanAmount.ToString(),
                };
            }


            return resultData;
        }
        /// <summary>
        /// 入院登记修改
        /// </summary>
        /// <param name="param"></param>
        public void ModifyWorkerHospitalization(ModifyWorkerHospitalizationParam param)
        {//返回状态
            var resultState = new StringBuilder(1024);
            //消息
            var msg = new StringBuilder(1024);

            WorkerMedicalInsurance.ModifyHospitalization
                (param.OrganizationCode,
                 param.MedicalInsuranceHospitalizationNo,
                 param.AdministrativeArea,
                 param.AdmissionDate,
                 param.AdmissionMainDiagnosisIcd10,
                 param.DiagnosisIcd10Two,
                 param.DiagnosisIcd10Three,
                 param.AdmissionMainDiagnosis,
                 param.InpatientArea,
                 param.BedNumber,
                 param.HospitalizationNo,
                 resultState,
                 msg
                );
            if (resultState.ToString() != "1")
            {
                throw new Exception(msg.ToString());
            }

        }

        /// <summary>
        /// 职工划卡
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerStrokeCardDto WorkerStrokeCard(WorkerStrokeCardParam param)
        {
            var resultData = new WorkerStrokeCardDto();
            //流水号
            var documentNo = new StringBuilder(1024);
            //自费金额
            var selfPayFeeAmount = new StringBuilder(1024);
            //报销金额
            var overallPlanningCanAmount = new StringBuilder(1024);
            //返回状态
            var resultState = new StringBuilder(1024);
            //消息
            var msg = new StringBuilder(1024);
            // param.OrganizationCode==cpq2677
            var result = WorkerMedicalInsurance.WorkerHospitalSettlement
                      (param.Port,
                       param.Pwd,
                       param.AllAmount,
                       param.CardType,
                       param.OrganizationCode,
                       param.Operators,
                       documentNo,
                       overallPlanningCanAmount,
                       selfPayFeeAmount,
                       resultState,
                      msg
                      );
            if (resultState.ToString() != "1")
            {
                throw new Exception(msg.ToString());
            }

            resultData = new WorkerStrokeCardDto()
            {
                SelfPayFeeAmount = Convert.ToDecimal(selfPayFeeAmount.ToString()),
                DocumentNo = documentNo.ToString(),
                ReimbursementExpensesAmount = Convert.ToDecimal(overallPlanningCanAmount.ToString())
            };

            //var updateParam = new UpdateMedicalInsuranceResidentSettlementParam()
            //{

            //    ReimbursementExpensesAmount = resultData.ReimbursementExpensesAmount,
            //    SelfPayFeeAmount = resultData.SelfPayFeeAmount,
            //    OtherInfo = JsonConvert.SerializeObject(resultData),
            //    Id = param.Id,
            //    UserId = param.User.UserId,
            //    SettlementNo = resultData.DocumentNo,
            //    PreSettlementTransactionId = Guid.NewGuid().ToString("N"),
            //};
            ////更新医保病人信息
            //_medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParam);
            //var logParam = new AddHospitalLogParam()
            //{
            //    JoinOrOldJson = JsonConvert.SerializeObject(param),
            //    ReturnOrNewJson = JsonConvert.SerializeObject(resultData),
            //    User = param.User,
            //    Remark = "职工划卡"
            //};
            //_systemManageRepository.AddHospitalLog(logParam);
            ////更新医保信息
            //var strXmlIntoParam = XmlSerializeHelper.XmlParticipationParam();
            //var strXmlBackParam = XmlSerializeHelper.XmlBackParam();
            //var saveXmlData = new SaveXmlData();
            //saveXmlData.OrganizationCode = param.User.OrganizationCode;
            //saveXmlData.AuthCode = param.User.AuthCode;
            //saveXmlData.BusinessId = param.BusinessId;
            //saveXmlData.TransactionId = updateParam.PreSettlementTransactionId;
            //saveXmlData.MedicalInsuranceBackNum = "CXJB009";
            //saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
            //saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
            //saveXmlData.MedicalInsuranceCode = "43";
            //saveXmlData.UserId = param.User.UserId;
            //存基层
            //_webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData));
            return resultData;
        }
    }
}
