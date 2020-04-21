using BenDing.Domain.Models.Dto.Workers;
using BenDing.Domain.Models.Params.Workers;
using BenDing.Repository.Interfaces.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.JsonEntity;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Resident;
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
            var medicalInsuranceHospitalizationNo = new byte[1024];
            //审批编号
            var approvalNumber = new byte[1024];
            //年住院次数
            var yearHospitalizationNumber = new byte[1024];
            //统筹已付金额
            var overallPlanningAlreadyAmount = new byte[1024];
            //统筹可付金额
            var overallPlanningCanAmount = new byte[1024];
            //返回状态
            var resultState = new byte[1024];
            //消息
            var msg = new byte[1024];
            //  param.AdministrativeArea,
            WorkerMedicalInsurance.HospitalizationRegister
                (param.IdentityMark,
                param.AfferentSign,
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
            if (CommonHelp.StrToTransCoding(resultState) != "1") throw new Exception(CommonHelp.StrToTransCoding(msg));
            resultData = new WorkerHospitalizationRegisterDto()
            {
                MedicalInsuranceHospitalizationNo = CommonHelp.StrToTransCoding(medicalInsuranceHospitalizationNo),
                ApprovalNumber = CommonHelp.StrToTransCoding(approvalNumber),
                YearHospitalizationNumber = CommonHelp.StrToTransCoding(yearHospitalizationNumber),
                OverallPlanningAlreadyAmount = CommonHelp.StrToTransCoding(overallPlanningAlreadyAmount),
                OverallPlanningCanAmount = CommonHelp.StrToTransCoding(overallPlanningCanAmount),
            };

            return resultData;
        }

        /// <summary>
        /// 职工生育入院登记
        /// </summary>
        /// <returns></returns>
        public WorkerBirthHospitalizationRegisterDto WorkerBirthHospitalizationRegister(WorkerBirthHospitalizationRegisterParam param)
        {
            WorkerBirthHospitalizationRegisterDto data = null;
            var xmlStr = XmlHelp.SaveXml(param);
            if (!xmlStr) throw new Exception("职工生育入院登记保存参数出错");
            int result = MedicalInsuranceDll.CallService_cxjb("SYBX001");
            if (result != 1) throw new Exception("职工生育入院执行出错!!!");
            data = XmlHelp.DeSerializerModel(new WorkerBirthHospitalizationRegisterDto(), true);
            return data;
        }
        /// <summary>
        /// 职工生育预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerHospitalizationPreSettlementDto WorkerBirthPreSettlement(WorkerBirthPreSettlementParam param)
        {
            WorkerHospitalizationPreSettlementDto data = null;
            var xmlStr = XmlHelp.SaveXml(param);
            if (!xmlStr) throw new Exception("职工生育预结算保存参数出错");
            int result = MedicalInsuranceDll.CallService_cxjb("SYBX002");
            if (result != 1) throw new Exception("职工生育预结算执行出错!!!");
            var dataIni = XmlHelp.DeSerializerModel(new WorkerBirthPreSettlementJsonDto(), true);
            if (dataIni != null) data = AutoMapper.Mapper.Map<WorkerHospitalizationPreSettlementDto>(dataIni);
            return data;
        }
        /// <summary>
        /// 职工生育结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerHospitalizationPreSettlementDto WorkerBirthSettlement(WorkerBirthSettlementParam param)
        {
            WorkerHospitalizationPreSettlementDto data = null;
            var xmlStr = XmlHelp.SaveXml(param);
            if (!xmlStr) throw new Exception("职工生育结算保存参数出错");
            int result = MedicalInsuranceDll.CallService_cxjb("SYBX003");
            if (result != 1) throw new Exception("职工生育结算执行出错!!!");
            var dataIni = XmlHelp.DeSerializerModel(new WorkerBirthPreSettlementJsonDto(), true);
            if (dataIni!= null) data = AutoMapper.Mapper.Map<WorkerHospitalizationPreSettlementDto>(dataIni);
            return data;
        }

        /// <summary>
        /// 入院登记修改
        /// </summary>
        /// <param name="param"></param>
        public void ModifyWorkerHospitalization(ModifyWorkerHospitalizationParam param)
        {//返回状态
            var resultState = new byte[1024];
            //消息
            var msg = new byte[1024];

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
            if (CommonHelp.StrToTransCoding(resultState) != "1") throw new Exception(CommonHelp.StrToTransCoding(msg));



        }
        /// <summary>
        /// 职工住院预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerHospitalizationPreSettlementDto WorkerHospitalizationPreSettlement(WorkerHospitalizationPreSettlementParam param)
        {
            //社保住院号
            var documentNo = new byte[1024];
            //发生费用金额
            var totalAmount = new byte[1024];
            //基本统筹支付
            var basicOverallPay = new byte[1024];
            //补充医疗保险支付金额
            var supplementPayAmount = new byte[1024];
            //专项基金支付金额
            var specialFundPayAmount = new byte[1024];
            //公务员补贴
            var civilServantsSubsidies = new byte[1024];
            //公务员补助
            var civilServantsSubsidy = new byte[1024];
            //其它支付金额
            var otherPaymentAmount = new byte[1024];
            //账户支付
            var accountPayment = new byte[1024];
            //现金支付
            var cashPayment = new byte[1024];
            //起付金额
            var paidAmount = new byte[1024];
            // 备注
            var remark = new byte[1024];
            //返回状态
            var resultState = new byte[1024];
            //消息
            var msg = new byte[1024];
            WorkerMedicalInsurance.WorkerHospitalizationPreSettlement
                (param.OrganizationCode,
                 param.MedicalInsuranceHospitalizationNo,
                 param.AdministrativeArea,
                 param.IsHospitalizationFrequency,
                 param.Operators,
                 param.LeaveHospitalDate,
                 totalAmount,
                 basicOverallPay,
                 supplementPayAmount,
                 specialFundPayAmount,
                 civilServantsSubsidies,
                 civilServantsSubsidy,
                 otherPaymentAmount,
                 accountPayment,
                 cashPayment,
                 paidAmount,
                 documentNo,
                 remark,
                 resultState,
                 msg
                );
            if (CommonHelp.StrToTransCoding(resultState) != "1") throw new Exception(CommonHelp.StrToTransCoding(msg));

            var resultData = new WorkerHospitalizationPreSettlementDto()
            {
                DocumentNo = CommonHelp.StrToTransCoding(documentNo),
                TotalAmount = Convert.ToDecimal(CommonHelp.StrToTransCoding(totalAmount)),
                BasicOverallPay = Convert.ToDecimal(CommonHelp.StrToTransCoding(basicOverallPay)),
                SupplementPayAmount = Convert.ToDecimal(CommonHelp.StrToTransCoding(supplementPayAmount)),
                SpecialFundPayAmount = Convert.ToDecimal(CommonHelp.StrToTransCoding(specialFundPayAmount)),
                CivilServantsSubsidies = Convert.ToDecimal(CommonHelp.StrToTransCoding(civilServantsSubsidies)),
                CivilServantsSubsidy = Convert.ToDecimal(CommonHelp.StrToTransCoding(civilServantsSubsidy)),
                OtherPaymentAmount = Convert.ToDecimal(CommonHelp.StrToTransCoding(otherPaymentAmount)),
                AccountPayment = Convert.ToDecimal(CommonHelp.StrToTransCoding(accountPayment)),
                CashPayment = Convert.ToDecimal(CommonHelp.StrToTransCoding(cashPayment)),
                PaidAmount = Convert.ToDecimal(CommonHelp.StrToTransCoding(paidAmount)),
                Remark = CommonHelp.StrToTransCoding(cashPayment),
            };
            return resultData;
        }
        /// <summary>
        /// 职工住院结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerHospitalizationPreSettlementDto WorkerHospitalizationSettlement(WorkerHospitalizationSettlementParam param)
        {
           
            //社保住院号
            var documentNo = new byte[1024];
            //发生费用金额
            var totalAmount = new byte[1024];
            //基本统筹支付
            var basicOverallPay = new byte[1024];
            //补充医疗保险支付金额
            var supplementPayAmount = new byte[1024];
            //专项基金支付金额
            var specialFundPayAmount = new byte[1024];
            //公务员补贴
            var civilServantsSubsidies = new byte[1024];
            //公务员补助
            var civilServantsSubsidy = new byte[1024];
            //其它支付金额
            var otherPaymentAmount = new byte[1024];
            //账户支付
            var accountPayment = new byte[1024];
            //现金支付
            var cashPayment = new byte[1024];
            //起付金额
            var paidAmount = new byte[1024];
            // 备注
            var remark = new byte[1024];
            //返回状态
            var resultState = new byte[1024];
            //消息
            var msg = new byte[1024];
            WorkerMedicalInsurance.WorkerHospitalizationSettlement
                (param.OrganizationCode,
                 param.MedicalInsuranceHospitalizationNo,
                 param.AdministrativeArea,
                 param.IsHospitalizationFrequency,
                 param.Operators,
                 param.LeaveHospitalDate,
                 param.LeaveHospitalState,
                 param.AdmissionMainDiagnosisIcd10,
                 param.DiagnosisIcd10Two,
                 param.DiagnosisIcd10Three,
                 param.LeaveHospitalMainDiagnosis,
                 totalAmount,
                 basicOverallPay,
                 supplementPayAmount,
                 specialFundPayAmount,
                 civilServantsSubsidies,
                 civilServantsSubsidy,
                 otherPaymentAmount,
                 accountPayment,
                 cashPayment,
                 paidAmount,
                 documentNo,
                 remark,
                 resultState,
                 msg
                );
            if (CommonHelp.StrToTransCoding(resultState) != "1") throw new Exception(CommonHelp.StrToTransCoding(msg));
            var resultData = new WorkerHospitalizationPreSettlementDto()
            {
                DocumentNo = CommonHelp.StrToTransCoding(documentNo),
                TotalAmount = Convert.ToDecimal(CommonHelp.StrToTransCoding(totalAmount)),
                BasicOverallPay = Convert.ToDecimal(CommonHelp.StrToTransCoding(basicOverallPay)),
                SupplementPayAmount = Convert.ToDecimal(CommonHelp.StrToTransCoding(supplementPayAmount)),
                SpecialFundPayAmount = Convert.ToDecimal(CommonHelp.StrToTransCoding(specialFundPayAmount)),
                CivilServantsSubsidies = Convert.ToDecimal(CommonHelp.StrToTransCoding(civilServantsSubsidies)),
                CivilServantsSubsidy = Convert.ToDecimal(CommonHelp.StrToTransCoding(civilServantsSubsidy)),
                OtherPaymentAmount = Convert.ToDecimal(CommonHelp.StrToTransCoding(otherPaymentAmount)),
                AccountPayment = Convert.ToDecimal(CommonHelp.StrToTransCoding(accountPayment)),
                CashPayment = Convert.ToDecimal(CommonHelp.StrToTransCoding(cashPayment)),
                PaidAmount = Convert.ToDecimal(CommonHelp.StrToTransCoding(paidAmount)),
                Remark = CommonHelp.StrToTransCoding(cashPayment),
            };
            return resultData;
        }
        /// <summary>
        /// 职工取消结算
        /// </summary>
        /// <param name="param"></param>
        public string WorkerSettlementCancel(WorkerSettlementCancelParam param)
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
                cancelData = Cancel(param);
            }
            string Cancel(WorkerSettlementCancelParam paramc)
            {

                //返回状态
                var resultState = new byte[1024];
                //消息
                var msg = new byte[1024];
                var yearSign = new byte[1024];
                WorkerMedicalInsurance.WorkerSettlementCancel
                    (param.AdministrativeArea,
                     param.OrganizationCode,
                     param.MedicalInsuranceHospitalizationNo,
                     param.SettlementNo,
                     param.CancelLimit,
                     param.User.UserName,
                     yearSign,
                     resultState,
                     msg
                    );
                if (CommonHelp.StrToTransCoding(resultState) != "1")
                {
                    throw new Exception(CommonHelp.StrToTransCoding(msg));
                }
                return CommonHelp.StrToTransCoding(yearSign);
            }
            return cancelData;
        }
        //职工取消划卡
        public void CancelWorkerStrokeCard(CancelWorkersStrokeCardParam param)
        {
            var data = new HospitalizationPresettlementDto();
            var xmlStr = XmlHelp.SaveXml(param);
            if (!xmlStr) throw new Exception("职工取消划卡保存参数出错");
            int result = MedicalInsuranceDll.CallService_cxjb("CXJB027");
            if (result != 1) throw new Exception("职工取消划卡执行出错");
            var dataIni = XmlHelp.DeSerializerModel(new IniDto(), true);
        }

        /// <summary>
        /// 职工结算信息查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public QueryWorkerHospitalizationSettlementDto QueryWorkerHospitalizationSettlement(
            QueryWorkerHospitalizationSettlementParam param)
        {

            //基本统筹支付
            var basicOverallPay = new byte[1024];
            //补充医疗保险支付金额
            var supplementPayAmount = new byte[1024];
            //专项基金支付金额
            var specialFundPayAmount = new byte[1024];
            //公务员补贴
            var civilServantsSubsidies = new byte[1024];
            //公务员补助
            var civilServantsSubsidy = new byte[1024];
            //其它支付金额
            var otherPaymentAmount = new byte[1024];
            //账户支付
            var accountPayment = new byte[1024];
            //现金支付
            var cashPayment = new byte[1024];
            //起付金额
            var paidAmount = new byte[1024];
            //结算日期
            var settlementTime = new byte[1024];
            // 备注
            var remark = new byte[1024];
            //返回状态
            var resultState = new byte[1024];
            //消息
            var msg = new byte[1024];
            // param.OrganizationCode==cpq2677
            var result = WorkerMedicalInsurance.QueryWorkerHospitalSettlement
            (param.OrganizationCode,
                param.MedicalInsuranceHospitalizationNo,
                param.AdministrativeArea,
                basicOverallPay,
                supplementPayAmount,
                specialFundPayAmount,
                civilServantsSubsidies,
                civilServantsSubsidy,
                otherPaymentAmount,
                accountPayment,
                cashPayment,
                paidAmount,
                settlementTime,
                remark,
                resultState,
                msg
            );
            if (CommonHelp.StrToTransCoding(resultState) != "1") throw new Exception(CommonHelp.StrToTransCoding(msg));
            var resultData = new QueryWorkerHospitalizationSettlementDto()
            {
                BasicOverallPay = Convert.ToDecimal(CommonHelp.StrToTransCoding(basicOverallPay)),
                SupplementPayAmount = Convert.ToDecimal(CommonHelp.StrToTransCoding(supplementPayAmount)),
                SpecialFundPayAmount = Convert.ToDecimal(CommonHelp.StrToTransCoding(specialFundPayAmount)),
                CivilServantsSubsidies = Convert.ToDecimal(CommonHelp.StrToTransCoding(civilServantsSubsidies)),
                CivilServantsSubsidy = Convert.ToDecimal(CommonHelp.StrToTransCoding(civilServantsSubsidy)),
                OtherPaymentAmount = Convert.ToDecimal(CommonHelp.StrToTransCoding(otherPaymentAmount)),
                AccountPayment = Convert.ToDecimal(CommonHelp.StrToTransCoding(accountPayment)),
                CashPayment = Convert.ToDecimal(CommonHelp.StrToTransCoding(cashPayment)),
                PaidAmount = Convert.ToDecimal(CommonHelp.StrToTransCoding(paidAmount)),
                Remark = CommonHelp.StrToTransCoding(cashPayment),
                SettlementTime = CommonHelp.StrToTransCoding(settlementTime),

            };
            decimal reimbursementExpenses = resultData.BasicOverallPay + resultData.SupplementPayAmount + resultData.SpecialFundPayAmount
                                            + resultData.CivilServantsSubsidies + resultData.CivilServantsSubsidy + resultData.OtherPaymentAmount;
            resultData.ReimbursementExpenses = reimbursementExpenses;

            return resultData;
        }
    }
}
