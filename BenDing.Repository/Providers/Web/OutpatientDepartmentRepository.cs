using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.JsonEntity;
using BenDing.Domain.Models.Dto.OutpatientDepartment;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.HisXml;
using BenDing.Domain.Models.Params.OutpatientDepartment;
using BenDing.Domain.Models.Params.SystemManage;
using BenDing.Domain.Models.Params.Web;
using BenDing.Domain.Xml;
using BenDing.Repository.Interfaces.Web;
using Newtonsoft.Json;
using NFine.Application.BenDingManage;
using NFine.Domain._03_Entity.BenDingManage;

namespace BenDing.Repository.Providers.Web
{
   public class OutpatientDepartmentRepository: IOutpatientDepartmentRepository
    {
        private readonly MonthlyHospitalizationBase _monthlyHospitalizationBase;
        private readonly IHisSqlRepository _hisSqlRepository;
        private readonly ISystemManageRepository _systemManageRepository;
        private readonly IWebBasicRepository _webBasicRepository;
        public OutpatientDepartmentRepository(IHisSqlRepository hisSqlRepository,
            ISystemManageRepository systemManageRepository,
            IWebBasicRepository webBasicRepository)
        {
            _monthlyHospitalizationBase = new MonthlyHospitalizationBase();
            _hisSqlRepository = hisSqlRepository;
            _systemManageRepository = systemManageRepository;
            _webBasicRepository = webBasicRepository;
        }

        /// <summary>
        /// 门诊费用录入
        /// </summary>
        public OutpatientDepartmentCostInputDto OutpatientDepartmentCostInput(OutpatientDepartmentCostInputParam param)
        {
            OutpatientDepartmentCostInputDto resultData = null;

            var xmlStr = XmlHelp.SaveXml(param);
            if (xmlStr)
            {
                var result = MedicalInsuranceDll.CallService_cxjb("TPYP301");
                if (result == 1)
                {
                    resultData = XmlHelp.DeSerializerModel(new OutpatientDepartmentCostInputDto(), true);
                }
                else {
                    throw new Exception("门诊费用医保执行失败!!!");
                }
            }
            return resultData;
        }

        /// <summary>
        /// 门诊费取消
        /// </summary>
        public void CancelOutpatientDepartmentCost(CancelOutpatientDepartmentCostParam param)
        {


            var xmlStr = XmlHelp.SaveXml(param.Participation);
            if (xmlStr)
            {
                var result = MedicalInsuranceDll.CallService_cxjb("TPYP302");
                if (result == 1)
                {   //医保执行
                    var data = XmlHelp.DeSerializerModel(new IniDto(), true);
                    if (data != null)
                    {
                        if (data.ReturnState =="1")
                        {
                           
                                var transactionId = param.User.TransKey;
                                //写入日志
                                _systemManageRepository.AddHospitalLog(new AddHospitalLogParam()
                                {
                                    RelationId = param.Id,
                                    JoinOrOldJson = JsonConvert.SerializeObject(param.Participation),
                                    ReturnOrNewJson = JsonConvert.SerializeObject(data),
                                    Remark = "[R][OutpatientDepartment]门诊病人结算取消",
                                    User= param.User,
                                });
                                //回参构建
                                var xmlData = new OutpatientDepartmentCostCancelXml()
                                {

                                    SettlementNo = param.Participation.DocumentNo,
                                };
                                var strXmlIntoParam = XmlSerializeHelper.XmlParticipationParam();
                                var strXmlBackParam = XmlSerializeHelper.XmlBackParam();
                                var saveXmlData = new SaveXmlData();
                                saveXmlData.OrganizationCode = param.User.OrganizationCode;
                                saveXmlData.AuthCode = param.User.AuthCode;
                                saveXmlData.BusinessId = param.BusinessId;
                                saveXmlData.TransactionId = transactionId;
                                saveXmlData.MedicalInsuranceBackNum = "TPYP302";
                                saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
                                saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
                                saveXmlData.MedicalInsuranceCode = "42MZ";
                                saveXmlData.UserId = param.User.UserId;
                                //存基层
                                _webBasicRepository.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData));
                                //更新中间层确认取消成功
                                _hisSqlRepository.UpdateOutpatient(param.User, new UpdateOutpatientParam()
                                {
                                    Id = param.Id,
                                    SettlementCancelTransactionId = transactionId
                                });
                            }
                           
                          
                        
                    }
                    

                }
            }
        }

        /// <summary>
        /// 查询门诊费
        /// </summary>
        public List<QueryOutpatientDepartmentCostjsonDto>  QueryOutpatientDepartmentCost(QueryOutpatientDepartmentCostParam param)
        {

            var resultData = new List<QueryOutpatientDepartmentCostjsonDto>();
            var xmlStr = XmlHelp.SaveXml(param);
            if (xmlStr)
            {
                var result = MedicalInsuranceDll.CallService_cxjb("TPYP303");
                if (result == 1)
                {
                    string strXml = XmlHelp.DeSerializerModelStr("PO_GHXX");
                    var data = XmlHelp.DeSerializer<QueryOutpatientDepartmentCostDto>(strXml);
                    if (data.Row != null && data.Row.Any())
                    {
                        resultData =  AutoMapper.Mapper.Map<List<QueryOutpatientDepartmentCostjsonDto>>(data.Row);
                    }
                }
            }

            return resultData;
        }
        /// <summary>
        /// 门诊月结汇总
        /// </summary>
        /// <param name="param"></param>
        public MonthlyHospitalizationDto MonthlyHospitalization(MonthlyHospitalizationParam param)
        {
           
            var xmlStr = XmlHelp.SaveXml(param.Participation);
            var data = new MonthlyHospitalizationDto();
            if (xmlStr)
            {
                var result = MedicalInsuranceDll.CallService_cxjb("TPYP214");
                if (result == 1)
                {
                    data = XmlHelp.DeSerializerModel(new MonthlyHospitalizationDto(), true);
                    var insertParam = new MonthlyHospitalizationEntity()
                    {   Amount = data.ReimbursementAllAmount,
                        Id = Guid.NewGuid(),
                        DocumentNo = data.DocumentNo,
                        PeopleNum = data.ReimbursementPeopleNum,
                        PeopleType = param.Participation.PeopleType,
                        SummaryType = param.Participation.SummaryType,
                        StartTime = param.Participation.StartTime,
                        EndTime = param.Participation.EndTime,
                    };
                     _monthlyHospitalizationBase.Insert(insertParam, param.User);
                   
                }
            }
            return data;
        }
        /// <summary>
        /// 取消门诊月结汇总
        /// </summary>
        /// <param name="param"></param>
        public void CancelMonthlyHospitalization(CancelMonthlyHospitalizationParam param)
        {

            var xmlStr = XmlHelp.SaveXml(param.Participation);
            if (xmlStr)
            {
                var result = MedicalInsuranceDll.CallService_cxjb("TPYP215");
                if (result == 1)
                {
                    var data = XmlHelp.DeSerializerModel(new IniDto(), true);
                    var monthlyHospitalization = _monthlyHospitalizationBase.GetForm(param.Id);
                    if (monthlyHospitalization != null)
                    {
                        monthlyHospitalization.IsRevoke = true;
                        //更新月结状态
                       _monthlyHospitalizationBase.Modify(monthlyHospitalization, param.User, param.Id);
                    }
                }
            }
        }
    }
}
