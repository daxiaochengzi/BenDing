using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.OutpatientDepartment;
using BenDing.Domain.Models.HisXml;
using BenDing.Domain.Models.Params.OutpatientDepartment;
using BenDing.Domain.Models.Params.Resident;
using BenDing.Domain.Models.Params.SystemManage;
using BenDing.Domain.Models.Params.Web;
using BenDing.Domain.Xml;
using BenDing.Repository.Interfaces.Web;
using BenDing.Repository.Providers.Web;
using BenDing.Service.Interfaces;
using Newtonsoft.Json;

namespace BenDing.Service.Providers
{
    
   public class OutpatientDepartmentService: IOutpatientDepartmentService
   {
       private readonly IOutpatientDepartmentRepository _outpatientDepartmentRepository;
       private readonly IWebServiceBasicService _webServiceBasic;
       private readonly IWebBasicRepository _webBasicRepository;
       private readonly IHisSqlRepository _hisSqlRepository;
       private readonly ISystemManageRepository _systemManageRepository;
       private readonly IResidentMedicalInsuranceRepository _residentMedicalInsuranceRepository;


        public OutpatientDepartmentService(
           IOutpatientDepartmentRepository outpatientDepartmentRepository,
           IWebServiceBasicService webServiceBasic,
           IWebBasicRepository webBasicRepository,
           IHisSqlRepository hisSqlRepository,
           ISystemManageRepository systemManageRepository,
           IResidentMedicalInsuranceRepository residentMedicalInsuranceRepository
           )
       {
           _webServiceBasic = webServiceBasic;
           _outpatientDepartmentRepository = outpatientDepartmentRepository;
           _webBasicRepository = webBasicRepository;
           _hisSqlRepository = hisSqlRepository;
           _systemManageRepository = systemManageRepository;
           _residentMedicalInsuranceRepository = residentMedicalInsuranceRepository;
       }

       /// <summary>
        /// 门诊费用录入
        /// </summary>
        public OutpatientDepartmentCostInputDto OutpatientDepartmentCostInput(GetOutpatientPersonParam param)
        {
            var resultData = new OutpatientDepartmentCostInputDto();

            //获取门诊病人数据
            var outpatientPerson = _webServiceBasic.GetOutpatientPerson(param);
            if (outpatientPerson != null)
            {
                var inputParam = new OutpatientDepartmentCostInputParam()
                {
                    AllAmount = outpatientPerson.MedicalTreatmentTotalCost,
                    IdentityMark = "1",
                    InformationNumber = outpatientPerson.IdCardNo,
                    Operators = param.User.UserName
                };
                //医保数据写入
                resultData = _outpatientDepartmentRepository.OutpatientDepartmentCostInput(inputParam);
                if (resultData != null)
                {
                    var transactionId = param.User.TransKey;
                    param.ReturnJson = JsonConvert.SerializeObject(resultData);
                    param.Id = Guid.NewGuid();
                    param.IsSave = true;
                    //中间层数据写入
                    _webServiceBasic.GetOutpatientPerson(param);
                    //日志写入
                    _systemManageRepository.AddHospitalLog(new AddHospitalLogParam()
                    {
                        User = param.User,
                        JoinOrOldJson = JsonConvert.SerializeObject(inputParam),
                        ReturnOrNewJson = JsonConvert.SerializeObject(resultData),
                        RelationId = param.Id,
                        Remark = "[R][OutpatientDepartment]门诊病人结算"
                    });
                    //回参构建
                    var xmlData = new OutpatientDepartmentCostXml()
                    {

                        MedicalInsuranceOutpatientNo = resultData.DocumentNo,
                        CashPayment = resultData.SelfPayFeeAmount,
                        SettlementNo = resultData.DocumentNo,
                        AllAmount = outpatientPerson.MedicalTreatmentTotalCost,
                        PatientName = outpatientPerson.PatientName,
                        AccountAmountPay = resultData.ReimbursementExpensesAmount

                    };
                    //基层数据写入
                    var strXmlIntoParam = XmlSerializeHelper.XmlParticipationParam();
                    //获取病人的基础信息
                    var userInfoData= _residentMedicalInsuranceRepository.GetUserInfo(new ResidentUserInfoParam()
                    {
                        IdentityMark = "1",
                        InformationNumber = outpatientPerson.IdCardNo,
                    });
                    xmlData.AccountBalance = userInfoData.ResidentInsuranceBalance;

                    var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
                    var saveXmlData = new SaveXmlData();
                    saveXmlData.OrganizationCode = param.User.OrganizationCode;
                    saveXmlData.AuthCode = param.User.AuthCode;
                    saveXmlData.BusinessId = param.UiParam.BusinessId;
                    saveXmlData.TransactionId = transactionId;
                    saveXmlData.MedicalInsuranceBackNum = "TPYP301";
                    saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam );
                    saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
                    saveXmlData.MedicalInsuranceCode = "48";
                    saveXmlData.UserId = param.User.UserId;
                    //存基层
                     _webBasicRepository.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData));
                    //更新中间层确认基层写入成功
                    _hisSqlRepository.UpdateOutpatient(param.User, new UpdateOutpatientParam()
                    {
                        Id = param.Id,
                        SettlementTransactionId = transactionId
                    });

                }
            }


            return resultData;
        }
    }
}
