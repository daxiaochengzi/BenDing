﻿using BenDing.Domain.Models.Params.Workers;
using BenDing.Repository.Interfaces.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Repository.Providers.Web
{
   public class WorkerMedicalInsuranceRepository: IWorkerMedicalInsuranceRepository
    {
        /// <summary>
        /// 入院登记
        /// </summary>
        /// <returns></returns>
        public void WorkerHospitalizationRegister(WorKerHospitalizationRegisterParam param)
        {
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
            var Msg = new StringBuilder(1024);
            WorkerMedicalInsurance.HospitalizationRegister
                (param.AfferentSign,
                param.IdentityMark,
                param.AdministrativeArea,
                "cpq2677",
                param.MedicalCategory,
                param.AdmissionMainDiagnosisIcd10,
                param.DiagnosisIcd10Two,
                param.DiagnosisIcd10Three,
                param.AdmissionDate,
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
                Msg
                );
            if (resultState.ToString() != "1")
            {
                throw new Exception(Msg.ToString());
            }
            //    //保存医保信息
            //    _medicalInsuranceSqlRepository.SaveMedicalInsurance(user, saveData);
            //    int result = MedicalInsuranceDll.CallService_cxjb("CXJB002");
            //    if (result == 1)
            //    {
            //        var data = XmlHelp.DeSerializerModel(new ResidentHospitalizationRegisterDto(), true);
            //        saveData.MedicalInsuranceHospitalizationNo = data.MedicalInsuranceInpatientNo;
            //        //更新医保信息
            //        var strXmlIntoParam = XmlSerializeHelper.XmlParticipationParam();
            //        var strXmlBackParam = XmlSerializeHelper.XmlBackParam();
            //        var saveXmlData = new SaveXmlData();
            //        saveXmlData.OrganizationCode = user.OrganizationCode;
            //        saveXmlData.AuthCode = user.AuthCode;
            //        saveXmlData.BusinessId = param.BusinessId;
            //        saveXmlData.TransactionId = saveData.Id.ToString("N");
            //        saveXmlData.MedicalInsuranceBackNum = "CXJB002";
            //        saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
            //        saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
            //        saveXmlData.MedicalInsuranceCode = "21";
            //        saveXmlData.UserId = user.UserId;
            //        //存基层
            //        _webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData));
            //        //存中间库
            //        _medicalInsuranceSqlRepository.SaveMedicalInsurance(user, saveData);
            //    }
            //    else
            //    {
            //        XmlHelp.DeSerializerModel(new IniDto(), true);
            //    }

        }
    }
}
