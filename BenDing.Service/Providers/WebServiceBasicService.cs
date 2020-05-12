using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.JsonEntity;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Dto.Workers;
using BenDing.Domain.Models.Enums;
using BenDing.Domain.Models.HisXml;
using BenDing.Domain.Models.Params;
using BenDing.Domain.Models.Params.Base;
using BenDing.Domain.Models.Params.OutpatientDepartment;
using BenDing.Domain.Models.Params.Resident;
using BenDing.Domain.Models.Params.SystemManage;
using BenDing.Domain.Models.Params.UI;
using BenDing.Domain.Models.Params.Web;
using BenDing.Domain.Models.Params.Workers;
using BenDing.Domain.Xml;
using BenDing.Repository.Interfaces.Web;
using BenDing.Service.Interfaces;
using Newtonsoft.Json;
using NFine.Application.BenDingManage;
using NFine.Domain._03_Entity.BenDingManage;

namespace BenDing.Service.Providers
{
    public class WebServiceBasicService : IWebServiceBasicService
    {
        private readonly IHisSqlRepository _hisSqlRepository;
        private readonly IMedicalInsuranceSqlRepository _medicalInsuranceSqlRepository;
        private readonly IWebBasicRepository _webServiceBasic;
        private InpatientBase inpatientBaseService = new InpatientBase();
        private readonly ISystemManageRepository _systemManageRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iWebServiceBasic"></param>
        /// <param name="hisSqlRepository"></param>
        /// <param name="medicalInsuranceSqlRepository"></param>
        /// <param name="iSystemManageRepository"></param>
        public WebServiceBasicService(IWebBasicRepository iWebServiceBasic,
            IHisSqlRepository hisSqlRepository,
            IMedicalInsuranceSqlRepository medicalInsuranceSqlRepository,
            ISystemManageRepository iSystemManageRepository

        )
        {
            _webServiceBasic = iWebServiceBasic;
            _hisSqlRepository = hisSqlRepository;
            _medicalInsuranceSqlRepository = medicalInsuranceSqlRepository;
            _systemManageRepository = iSystemManageRepository;

        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="tradeCode"></param>
        /// <param name="inputParameter"></param>
        /// <returns></returns>
        public UserInfoDto GetVerificationCode(string tradeCode, string inputParameter)
        {

            var resultData = new UserInfoDto();
            var ini = new UserInfoJsonDto();
            List<UserInfoJsonDto> resultList;
            var iniData = new UserInfoDto();
            var data = _webServiceBasic.HIS_InterfaceList(tradeCode, inputParameter);
            resultList = GetResultData(ini, data);
            if (resultList.Any())
            {
                var resultValueJson = resultList.FirstOrDefault();
                resultData = AutoMapper.Mapper.Map<UserInfoDto>(resultValueJson);
            }

            return resultData;

        }

        /// <summary>
        /// 获取医疗机构
        /// </summary>
        /// <param name="verCode">验证码</param>
        /// <param name="name">医院名称</param>
        /// <returns></returns>
        public Int32 GetOrg(UserInfoDto userInfo, string name)
        {
            Int32 resultData = 0;
            List<OrgDto> result;
            var init = new OrgDto();
            var info = new { 验证码 = userInfo.AuthCode, 医院名称 = name };
            var data = _webServiceBasic.HIS_InterfaceList("30", JsonConvert.SerializeObject(info));
            result = GetResultData(init, data);
            if (result.Any())
            {
                _hisSqlRepository.ChangeOrg(userInfo, result);
                resultData = result.Count;
            }

            return resultData;
        }
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Int64 MedicalInsuranceDownloadIcd10(DataTable dt, string userId)
        {
            var data = _hisSqlRepository.MedicalInsuranceDownloadIcd10(dt, userId);
            return  data;
        }
        /// <summary>
        /// icd10对码
        /// </summary>
        /// <param name="param"></param>
        public void Icd10PairCode(Icd10PairCodeParam  param)
        {  //回参构建
            var icd10List = new List<Icd10PairCodeDateXml>();
            icd10List.AddRange(param.DataList.Select(c=> new Icd10PairCodeDateXml()
            {
                DiseaseId = c.DiseaseId,
                DiseaseName = c.ProjectName,
                DiseaseCoding = c.ProjectCode
            }));
               
            var xmlData = new Icd10PairCodeXml()
            {
                row   = icd10List
            };
            var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
            var saveXml = new SaveXmlDataParam()
            {
                User = param.User,
                MedicalInsuranceBackNum = "CXJB002",
                MedicalInsuranceCode = "91",
                BusinessId = param.BusinessId,
                BackParam = strXmlBackParam
            };
            //存基层
            _webServiceBasic.SaveXmlData(saveXml);
           
            _hisSqlRepository.Icd10PairCode(param);
        }

        /// <summary>
        /// 3.4 获取三大目录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetCatalog(UserInfoDto user, CatalogParam param)
        {

            var time = _hisSqlRepository.GetTime(Convert.ToInt16(param.CatalogType));
            _hisSqlRepository.DeleteCatalog(user, Convert.ToInt16(param.CatalogType));
            var timeNew = Convert.ToDateTime(time).ToString("yyyy-MM-dd HH:ss:mm") ??
                          DateTime.Now.AddYears(-40).ToString("yyyy-MM-dd HH:ss:mm");
            var oCatalogInfo = new CatalogInfoDto
            {
                目录类型 = Convert.ToInt16(param.CatalogType).ToString(),
                目录名称 = "",
                开始时间 = timeNew,
                结束时间 = DateTime.Now.ToString("yyyy-MM-dd HH:ss:mm"),
                验证码 = param.AuthCode,
                机构编码 = param.OrganizationCode,
            };
            var data = _webServiceBasic.HIS_InterfaceList("06", JsonConvert.SerializeObject(oCatalogInfo));
            List<ListCount> nums;
            var init = new ListCount();
            nums = GetResultData(init, data);
            var cnt = Convert.ToInt32(nums?.FirstOrDefault()?.行数);
            var resultCatalogDtoList = new List<CatalogDto>();
            var i = 0;
            while (i < cnt)
            {
                oCatalogInfo.开始行数 = i;
                oCatalogInfo.结束行数 = i + param.Nums;
                var catalogDtoData =
                    _webServiceBasic.HIS_InterfaceList("05", JsonConvert.SerializeObject(oCatalogInfo));
                List<CatalogDto> resultCatalogDto;
                var initCatalogDto = new CatalogDto();
                resultCatalogDto = GetResultData(initCatalogDto, catalogDtoData);
                if (resultCatalogDto.Any())
                {
                    resultCatalogDtoList.AddRange(resultCatalogDto);
                }

                if (resultCatalogDto.Count > 1) //排除单条更新
                {
                    _hisSqlRepository.AddCatalog(user, resultCatalogDto, param.CatalogType);
                }


                i = i + param.Nums;
            }

            Int64 allNum = resultCatalogDtoList.Count() == 1 ? 0 : resultCatalogDtoList.Count();
            return "下载【" + param.CatalogType + "】成功 共" + allNum.ToString() + "条记录";
        }

        /// <summary>
        /// 删除下载的三大目录
        /// </summary>
        /// <param name="catalog"></param>
        /// <returns></returns>
        public string DeleteCatalog(UserInfoDto user, int catalog)
        {
            var num = _hisSqlRepository.DeleteCatalog(user, catalog);
            return "删除【" + (CatalogTypeEnum)catalog + "】 成功 " + num + "条";
        }

        /// <summary>
        /// 获取ICD-10
        /// </summary>
        /// <param name="verCode"></param>
        /// <param name="code"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public string GetIcd10(UserInfoDto user, CatalogParam param)
        {
            var time = _hisSqlRepository.GetICD10Time();
            var timeNew = Convert.ToDateTime(time).ToString("yyyy-MM-dd HH:ss:mm") ??
                          DateTime.Now.AddYears(-40).ToString("yyyy-MM-dd HH:ss:mm");
            var rowParam = new ICD10InfoRowParam
            {
                开始时间 = timeNew,
                结束时间 = DateTime.Now.ToString("yyyy-MM-dd HH:ss:mm"),
                验证码 = param.AuthCode,
                疾病类别 = 0,
                病种名称 = ""
            };
            var oICD10Info = new ICD10InfoParam
            {
                开始时间 = timeNew,
                结束时间 = DateTime.Now.ToString("yyyy-MM-dd HH:ss:mm"),
                验证码 = param.AuthCode,
                疾病类别 = 0,
                病种名称 = ""
            };
            var data = _webServiceBasic.HIS_InterfaceList("08",
                Newtonsoft.Json.JsonConvert.SerializeObject(rowParam));
            List<ListCount> nums;
            var init = new ListCount();
            nums = GetResultData(init, data);
            var cnt = Convert.ToInt32(nums?.FirstOrDefault()?.行数);
            var resultCatalogDtoList = new List<ICD10InfoDto>();
            var i = 0;
            while (i < cnt)
            {
                oICD10Info.开始行数 = i;
                oICD10Info.结束行数 = i + param.Nums;
                var catalogDtoData =
                    _webServiceBasic.HIS_InterfaceList("07", JsonConvert.SerializeObject(oICD10Info));
                List<ICD10InfoDto> resultCatalogDto;
                var initCatalogDto = new ICD10InfoDto();
                resultCatalogDto = GetResultData(initCatalogDto, catalogDtoData);
                if (resultCatalogDto.Any())
                {
                    resultCatalogDtoList.AddRange(resultCatalogDto);
                    _hisSqlRepository.AddICD10(resultCatalogDto, user);
                    i = i + param.Nums;
                }
            }

            return "下载【ICD10】成功 共" + resultCatalogDtoList.Count() + "条记录";
        }

        /// <summary>
        /// 获取门诊病人
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseOutpatientInfoDto GetOutpatientPerson(GetOutpatientPersonParam param)
        {
            BaseOutpatientInfoDto resultData = null;
            var xmlData = new MedicalInsuranceXmlDto();
            xmlData.BusinessId = param.UiParam.BusinessId;
            xmlData.HealthInsuranceNo = "48";//42MZ
            xmlData.TransactionId = param.UiParam.TransKey;
            xmlData.AuthCode = param.User.AuthCode;
            xmlData.UserId = param.User.UserId;
            xmlData.OrganizationCode = param.User.OrganizationCode;
            var jsonParam = JsonConvert.SerializeObject(xmlData);
            var data = _webServiceBasic.HIS_Interface("39", jsonParam);
            OutpatientPersonJsonDto dataValue = JsonConvert.DeserializeObject<OutpatientPersonJsonDto>(data.Msg);
            var dataValueFirst = dataValue.OutpatientPersonBase;
            if (dataValueFirst != null)
            {
                resultData = AutoMapper.Mapper.Map<BaseOutpatientInfoDto>(dataValueFirst);
                resultData.Id = param.Id;
                resultData.BusinessId = param.UiParam.BusinessId;
                resultData.DiagnosticJson = JsonConvert.SerializeObject(dataValue.DiagnosisList);
                resultData.DiagnosisList = dataValue.DiagnosisList;
                if (param.IsSave)
                {
                    _hisSqlRepository.SaveOutpatient(param.User, resultData);
                    
                }

            }
            return resultData;

            


        }
        public HisHospitalizationSettlementCancelInfoJsonDto GetOutpatientSettlementCancel(SettlementCancelParam param)
        {
            var resultData = new PatientLeaveHospitalInfoDto();
            var xmlData = new MedicalInsuranceXmlDto();
            xmlData.BusinessId = param.BusinessId;
            xmlData.HealthInsuranceNo = "42MZ";
            xmlData.TransactionId = param.User.TransKey;
            xmlData.AuthCode = param.User.AuthCode;
            xmlData.UserId = param.User.UserId;
            xmlData.OrganizationCode = param.User.OrganizationCode;
            var jsonParam = JsonConvert.SerializeObject(xmlData);
            var data = _webServiceBasic.HIS_Interface("39", jsonParam);
            HisHospitalizationSettlementCancelJsonDto dataValue = JsonConvert.DeserializeObject<HisHospitalizationSettlementCancelJsonDto>(data.Msg);
            return dataValue.InfoData;
        }
        /// <summary>
        /// 获取门诊病人费用明细
        /// </summary>
     
        /// <param name="param"></param>
        /// <returns></returns>
        public List<BaseOutpatientDetailDto> GetOutpatientDetailPerson(OutpatientDetailParam param)
        {
           var resultData =new List<BaseOutpatientDetailDto>(); 
            var xmlData = new MedicalInsuranceXmlDto();
            xmlData.BusinessId = param.BusinessId;
            xmlData.HealthInsuranceNo = "48";//42MZ
            xmlData.TransactionId = param.User.TransKey;
            xmlData.AuthCode = param.User.AuthCode;
            xmlData.UserId = param.User.UserId;
            xmlData.OrganizationCode = param.User.OrganizationCode;
            var jsonParam = JsonConvert.SerializeObject(xmlData);
            var data = _webServiceBasic.HIS_Interface("39", jsonParam);
            OutpatientPersonJsonDto dataValue = JsonConvert.DeserializeObject<OutpatientPersonJsonDto>(data.Msg);

            var detailInfo = dataValue.DetailInfo;
            foreach (var item in detailInfo)
            {
                var detail = AutoMapper.Mapper.Map<BaseOutpatientDetailDto>(item);
                detail.OrganizationCode = param.User.OrganizationCode;
                detail.OrganizationName = param.User.OrganizationName;
                detail.OutpatientNo = dataValue.OutpatientPersonBase.OutpatientNumber;
                resultData.Add(detail);

            }
            if (param.IsSave)
            {
                  _hisSqlRepository.SaveOutpatientDetail(param.User, resultData);
            }
            return resultData;
        }

        /// <summary>
        /// 获取住院病人信息
        /// </summary>
        /// <param name="infoParam"></param>
        /// <returns></returns>
        public InpatientInfoDto GetInpatientInfo(GetInpatientInfoParam param)
        {
            var resultData = new InpatientInfoDto();
            var xmlData = new MedicalInsuranceXmlDto();
            xmlData.BusinessId = param.BusinessId;
            xmlData.HealthInsuranceNo = "21";
            xmlData.TransactionId = param.User.TransKey;
            xmlData.AuthCode = param.User.AuthCode;
            xmlData.UserId = param.User.UserId;
            xmlData.OrganizationCode = param.User.OrganizationCode;
            var data = _webServiceBasic.HIS_Interface("39", JsonConvert.SerializeObject(xmlData));
            InpatientInfoJsonDto dataValue = JsonConvert.DeserializeObject<InpatientInfoJsonDto>(data.Msg);
            if (dataValue != null && dataValue.InpatientInfoJsonData != null)
            {
                var diagnosisList = dataValue.DiagnosisJson.Select(c => new InpatientDiagnosisDto()
                {
                    DiseaseCoding = c.DiseaseCoding,
                    DiseaseName = c.DiseaseName,
                    IsMainDiagnosis = c.IsMainDiagnosis == "是" ? true : false,
                    ProjectCode=c.ProjectCode,
               
                    
                }).ToList();
                resultData = new InpatientInfoDto()
                {
                    DiagnosisList = diagnosisList,
                    HospitalizationId = dataValue.InpatientInfoJsonData.HospitalizationId,
                    BusinessId = param.BusinessId,
                    DiagnosisJson = JsonConvert.SerializeObject(dataValue.DiagnosisJson),
                    AdmissionBed = dataValue.InpatientInfoJsonData.AdmissionBed,
                    AdmissionDate = dataValue.InpatientInfoJsonData.AdmissionDate,
                    AdmissionDiagnosticDoctor = dataValue.InpatientInfoJsonData.AdmissionDiagnosticDoctor,
                    AdmissionDiagnosticDoctorId = dataValue.InpatientInfoJsonData.AdmissionDiagnosticDoctorId,
                    AdmissionMainDiagnosis = dataValue.InpatientInfoJsonData.AdmissionMainDiagnosis,
                    AdmissionMainDiagnosisIcd10 = dataValue.InpatientInfoJsonData.AdmissionMainDiagnosisIcd10,
                    AdmissionOperateTime = dataValue.InpatientInfoJsonData.AdmissionOperateTime,
                    AdmissionOperator = dataValue.InpatientInfoJsonData.AdmissionOperator,
                    AdmissionWard = dataValue.InpatientInfoJsonData.AdmissionWard,
                    FamilyAddress = dataValue.InpatientInfoJsonData.FamilyAddress,
                    IdCardNo = dataValue.InpatientInfoJsonData.IdCardNo,
                    PatientName = dataValue.InpatientInfoJsonData.PatientName,
                    PatientSex = dataValue.InpatientInfoJsonData.PatientSex,
                    ContactName = dataValue.InpatientInfoJsonData.ContactName,
                    ContactPhone = dataValue.InpatientInfoJsonData.ContactPhone,
                    Remark = dataValue.InpatientInfoJsonData.Remark,
                    HospitalName = param.User.OrganizationName,
                    HospitalizationNo = dataValue.InpatientInfoJsonData.HospitalizationNo,
                    TransactionId = param.User.TransKey,
                    InDepartmentName = dataValue.InpatientInfoJsonData.InDepartmentName,
                    InDepartmentId = dataValue.InpatientInfoJsonData.InDepartmentId,
                    DocumentNo = dataValue.InpatientInfoJsonData.DocumentNo,
                };
                if (param.IsSave == true)
                {
                    //删除以前记录
                    var deleteData = _hisSqlRepository.DeleteDatabase(new DeleteDatabaseParam()
                    {
                        User = param.User,
                        Field = "BusinessId",
                        Value = param.BusinessId,
                        TableName = "Inpatient"
                    });
                    //添加病人信息
                    var inpatientEntity = new InpatientEntity();
                    var insertEntity = AutoMapper.Mapper.Map<InpatientEntity>(resultData);
                    insertEntity.Id = Guid.NewGuid();
                   
                    inpatientBaseService.Insert(insertEntity, param.User);

                }

            }

            return resultData;
        }
        /// <summary>
        /// 获取his住院结算
        /// </summary>
        /// <param name="param"></param>
        public PatientLeaveHospitalInfoDto GetHisHospitalizationSettlement(GetInpatientInfoParam param)
        {
            var resultData = new PatientLeaveHospitalInfoDto();
            var xmlData = new MedicalInsuranceXmlDto();
            xmlData.BusinessId = param.BusinessId;
            xmlData.HealthInsuranceNo = "41";
            xmlData.TransactionId = param.User.TransKey;
            xmlData.AuthCode = param.User.AuthCode;
            xmlData.UserId = param.User.UserId;
            xmlData.OrganizationCode = param.User.OrganizationCode;
            var jsonParam = JsonConvert.SerializeObject(xmlData);
            var data = _webServiceBasic.HIS_Interface("39", jsonParam);
            HisHospitalizationSettlementJsonDto dataValue = JsonConvert.DeserializeObject<HisHospitalizationSettlementJsonDto>(data.Msg);
            var dataValueFirst = dataValue.LeaveHospitalInfoData.FirstOrDefault();
            //实体转换
            if (dataValueFirst != null)
            {
                resultData.LeaveHospitalBedNumber = dataValueFirst.LeaveHospitalBedNumber;
                resultData.LeaveHospitalDate = dataValueFirst.LeaveHospitalDate;
                resultData.LeaveHospitalDepartmentId = dataValueFirst.LeaveHospitalDepartmentId;
                resultData.LeaveHospitalDepartmentName = dataValueFirst.LeaveHospitalDepartmentName;
                resultData.LeaveHospitalDiagnosticDoctor = dataValueFirst.LeaveHospitalDiagnosticDoctor;
                resultData.LeaveHospitalOperator = dataValueFirst.LeaveHospitalOperator;
             
            }
            var diagnosisList = dataValue.DiagnosisJson.Select(c => new InpatientDiagnosisDto()
            {
                DiseaseCoding = c.DiseaseCoding,
                DiseaseName = c.DiagnosisName,
                ProjectCode = c.DiagnosisMedicalInsuranceCode,
                IsMainDiagnosis = c.IsMainDiagnosis == "是" ? true : false,

            }).ToList();

            resultData.DiagnosisList = diagnosisList;
            return resultData;
        }
        /// <summary>
        /// 获取his住院预结算
        /// </summary>
        /// <param name="param"></param>
        public HisHospitalizationPreSettlementJsonDto GetHisHospitalizationPreSettlement(GetInpatientInfoParam param)
        {

            var xmlData = new MedicalInsuranceXmlDto();
            xmlData.BusinessId = param.BusinessId;
            xmlData.HealthInsuranceNo = "43";
            xmlData.TransactionId = param.User.TransKey;
            xmlData.AuthCode = param.User.AuthCode;
            xmlData.UserId = param.User.UserId;
            xmlData.OrganizationCode = param.User.OrganizationCode;
            var jsonParam = JsonConvert.SerializeObject(xmlData);
            var data = _webServiceBasic.HIS_Interface("39", jsonParam);
            HisHospitalizationPreSettlementJsonDto dataValue = JsonConvert.DeserializeObject<HisHospitalizationPreSettlementJsonDto>(data.Msg);
            return dataValue;
        }
        /// <summary>
        /// 获取基础取消结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public HisHospitalizationSettlementCancelInfoJsonDto GetHisHospitalizationSettlementCancel(SettlementCancelParam param)
        {
            var resultData = new PatientLeaveHospitalInfoDto();
            var xmlData = new MedicalInsuranceXmlDto();
            xmlData.BusinessId = param.BusinessId;
            xmlData.HealthInsuranceNo = "42";
            xmlData.TransactionId = param.User.TransKey;
            xmlData.AuthCode = param.User.AuthCode;
            xmlData.UserId = param.User.UserId;
            xmlData.OrganizationCode = param.User.OrganizationCode;
            var jsonParam = JsonConvert.SerializeObject(xmlData);
            var data = _webServiceBasic.HIS_Interface("39", jsonParam);
            HisHospitalizationSettlementCancelJsonDto dataValue = JsonConvert.DeserializeObject<HisHospitalizationSettlementCancelJsonDto>(data.Msg);
            return dataValue.InfoData;
        }

        /// <summary>
        /// 获取住院明细
        /// </summary>
        /// <param name="user"></param>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public List<InpatientInfoDetailDto> GetInpatientInfoDetail(UserInfoDto user, string businessId)
        {
            var resultData = new List<InpatientInfoDetailDto>();

            var queryParam = new DatabaseParam()
            {
                Field = "BusinessId",
                Value = businessId,
                TableName = "Inpatient"
            };
            var inpatientData = _hisSqlRepository.QueryDatabase(new InpatientEntity(), queryParam);
            if (inpatientData == null) throw new Exception("该病人未在中心库中,请检查是否办理医保入院!!!");
            //获取当前病人
            var inpatient = inpatientData.FirstOrDefault();
            var transactionId = Guid.NewGuid().ToString("N");
            var xmlData = new MedicalInsuranceXmlDto();
            xmlData.BusinessId = businessId;
            xmlData.HealthInsuranceNo = "31";
            xmlData.TransactionId = transactionId;
            xmlData.AuthCode = user.AuthCode;
            xmlData.UserId = user.UserId;
            xmlData.OrganizationCode = user.OrganizationCode;
            var data = _webServiceBasic.HIS_Interface("39", JsonConvert.SerializeObject(xmlData));
            InpatientDetailListJsonDto dataValue = JsonConvert.DeserializeObject<InpatientDetailListJsonDto>(data.Msg);
            if (dataValue != null)
            {
                resultData = AutoMapper.Mapper.Map<List<InpatientInfoDetailDto>>(dataValue.DetailList);
                var saveParam = new SaveInpatientInfoDetailParam()
                {
                    DataList = resultData,
                    HospitalizationId = inpatient.HospitalizationId,
                    User = user
                };
                _hisSqlRepository.SaveInpatientInfoDetail(saveParam);
                //    //  var msg = "获取住院号【" + resultFirst.住院号 + "】，业务ID【" + param.业务ID + "】的时间段内的住院费用成功，共" + result.Count +
                //    //          "条记录";
                //}


            }

            return resultData;
        }
        /// <summary>
        /// 医保信息保存
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        public void MedicalInsuranceSave(UserInfoDto user, MedicalInsuranceParam param)
        {
            // var num =  _hisSqlRepository.QueryMedicalInsurance(param.业务ID);
            //if (num == 0)
            //{
            //    throw new Exception("数据库中未找到相应的住院业务ID的医保信息！");
            //}
            //else
            //{
            //    var oResult =  _webServiceBasic.HIS_InterfaceList("37",
            //        "{'验证码':'" + param.验证码 + "','业务ID':'" + param.业务ID + "'}", user.UserId);
            //    if (oResult.Result == "1")
            //    {
            //        throw new Exception("此业务ID已经报销过，在试图调用接口删除中心住院医保信息时异常。中心返回删除失败消息：" +
            //                            oResult.Msg.FirstOrDefault()?.ToString() + "！");
            //    }

            //    var count =  _hisSqlRepository.DeleteMedicalInsurance(user, param.业务ID);
            //}

            List<MedicalInsuranceDto> result;
            var init = new MedicalInsuranceDto();
            var data = _webServiceBasic.HIS_InterfaceList("36", JsonConvert.SerializeObject(param));
            result = GetResultData(init, data);
            var resultFirst = result.FirstOrDefault();
            if (resultFirst != null)
            {
                //var msg = "获取住院号【" + resultFirst.住院号 + "】，业务ID【" + param.业务ID + "】的时间段内的住院费用成功，共" + result.Count +
                //          "条记录";
            }
        }


        /// <summary>
        /// 保存HIS系统中科室、医师、病区、床位的基本信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<InformationDto> SaveInformation(UserInfoDto user, InformationParam param)
        {
            var resultDataIni = new List<InformationDto>();
            var resultData =
                _webServiceBasic.HIS_InterfaceList("03", JsonConvert.SerializeObject(param));
            var result = new List<InformationJsonDto>();
            var init = new InformationJsonDto();
            if (resultData.Result == "1")
            {
                result = GetResultData(init, resultData);
                if (result.Any())
                {
                    resultDataIni = AutoMapper.Mapper.Map<List<InformationDto>>(result);
                    //保存基础信息
                    _hisSqlRepository.SaveInformationInfo(user, resultDataIni, param);
                }


            }

            return resultDataIni;
        }

        /// <summary>
        /// 获取医保构建参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public void GetXmlData(MedicalInsuranceXmlDto param)
        {
            var data = _webServiceBasic.HIS_InterfaceList("39", JsonConvert.SerializeObject(param));
        }

        /// <summary>
        /// 医保信息回写至基层系统
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>



        public UserInfoDto GetUserBaseInfo(string param)
        {
            var data = _systemManageRepository.QueryHospitalOperator(new QueryHospitalOperatorParam()
            { UserId = param });
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
                var verificationCode = GetVerificationCode("01", inputParamJson);

                return verificationCode;
            }
        }

        /// <summary>
        /// 返回结果解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="resultList"></param>
        /// <returns></returns>
        private List<T> GetResultData<T>(T t, BasicResultDto resultList)
        {
            var result = new List<T>();
            if (resultList != null && resultList.Msg.Any())
            {
                foreach (var item in resultList.Msg)
                {
                    string strData = item.ToString();
                    var itemData = JsonConvert.DeserializeObject<T>(strData);
                    if (itemData != null)
                    {
                        result.Add(itemData);
                    }


                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public int ThreeCataloguePairCodeUpload(UpdateThreeCataloguePairCodeUploadParam param)
        {
            int resultData = 0;
            var data = _medicalInsuranceSqlRepository.ThreeCataloguePairCodeUpload(param);
            int a = 0;
            int limit = 100; //限制条数
            int num = data.Count;
            var count = Convert.ToInt32(num / limit) + ((num % limit) > 0 ? 1 : 0);
            var idList = new List<string>();
            while (a < count)
            {
                //排除已上传数据
                var rowDataListAll = data.Where(d => !idList.Contains(d.DirectoryCode))
                    .ToList();
                var sendList = rowDataListAll.Take(limit).ToList();

                if (data.Any())
                {
                    var uploadDataRow = sendList.Select(c => new ThreeCataloguePairCodeUploadRowDto()
                    {
                        //ProjectId = c.Id.ToString("N"),
                        HisDirectoryCode = c.DirectoryCode,
                        Manufacturer = "",
                        ProjectName = c.ProjectName,
                        ProjectCode = c.ProjectCode,
                        ProjectCodeType = c.DirectoryCategoryCode,
                        ProjectCodeTypeDetail = ((ProjectCodeType)Convert.ToInt32(c.ProjectCodeType)).ToString(),
                        Remark = c.Remark,
                        ProjectLevel = ((ProjectLevel)Convert.ToInt32(c.ProjectLevel)).ToString(),
                        RestrictionSign = GetStrData(c.ProjectCodeType, c.RestrictionSign)

                    }).ToList();
                    var uploadData = new ThreeCataloguePairCodeUploadDto()
                    {
                        AuthCode = param.User.AuthCode,
                        CanCelState = "0",
                        UserName = param.User.UserName,
                        OrganizationCode = param.User.OrganizationCode,
                        PairCodeRow = uploadDataRow,
                        VersionNumber = ""
                    };
                    _webServiceBasic.HIS_Interface("35", JsonConvert.SerializeObject(uploadData));


                    //resultData = _medicalInsuranceSqlRepository.UpdateThreeCataloguePairCodeUpload(param);
                }
                //更新数据上传状态
                idList.AddRange(sendList.Select(d => d.DirectoryCode).ToList());
                a++;

            }

          

            //限制用药
            string GetStrData(string projectCodeType, string restrictionSign)
            {
                string str = "0";
                if (projectCodeType != "92")
                {
                    str = restrictionSign == "0" ? "" : "1";
                }

                return str;
            }

            return resultData;
        }

        /// <summary>
        /// 住院医保查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public QueryMedicalInsuranceDetailInfoDto QueryMedicalInsuranceDetail(
            QueryMedicalInsuranceUiParam param)
        {
            var resultData = new QueryMedicalInsuranceDetailInfoDto();
            var userBase = GetUserBaseInfo(param.UserId);
            var queryData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(
                new QueryMedicalInsuranceResidentInfoParam()
                {
                    OrganizationCode = userBase.OrganizationCode,
                    BusinessId = param.BusinessId
                });
            if (queryData == null) throw new Exception("当前病人未办理医保入院!!!");
            if (string.IsNullOrWhiteSpace(queryData.AdmissionInfoJson)) throw new Exception("当前病人没有入参参数记录!!!");
            if (queryData.InsuranceType == "310")
            {
                resultData = QueryWorkerMedicalInsuranceDetail(queryData.AdmissionInfoJson);
            }
            if (queryData.InsuranceType == "342")
            {
                resultData = QueryResidentMedicalInsuranceDetail(queryData.AdmissionInfoJson);
            }
            resultData.Id = queryData.Id;
            resultData.MedicalInsuranceHospitalizationNo = queryData.MedicalInsuranceHospitalizationNo;
            resultData.InsuranceType = queryData.InsuranceType;
            userBase.TransKey = param.TransKey;

            var infoData = new GetInpatientInfoParam()
            {
                User = userBase,
                BusinessId = param.BusinessId,
            };
            //获取住院病人信息
            var inpatientData = GetInpatientInfo(infoData);
            resultData.DiagnosisList = inpatientData.DiagnosisList;

            return resultData;
        }
        /// <summary>
        /// 获取职工医保数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private QueryMedicalInsuranceDetailInfoDto QueryWorkerMedicalInsuranceDetail(string param)
        {
            var resultData = new QueryMedicalInsuranceDetailInfoDto();
            var data = JsonConvert.DeserializeObject<WorKerHospitalizationRegisterParam>(param);
            resultData.AdmissionDate = DateTime
                .ParseExact(data.AdmissionDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture)
                .ToString("yyyy-MM-dd");
            resultData.BedNumber = data.BedNumber;
            resultData.HospitalizationNo = data.HospitalizationNo;
            resultData.InpatientDepartmentCode = data.InpatientDepartmentCode;
            //var diagnosisList = GetDiagnosisList(
            //    data.AdmissionMainDiagnosisIcd10,
            //    data.DiagnosisIcd10Two,
            //    data.DiagnosisIcd10Three
            //    );
            //resultData.DiagnosisList = diagnosisList;
            return resultData;
        }
        /// <summary>
        /// 获取职工医保数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private QueryMedicalInsuranceDetailInfoDto QueryResidentMedicalInsuranceDetail(string param)
        {
            var resultData = new QueryMedicalInsuranceDetailInfoDto();
            var data = JsonConvert.DeserializeObject<ResidentHospitalizationRegisterParam>(param);
            resultData.AdmissionDate = DateTime
                .ParseExact(data.AdmissionDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture)
                .ToString("yyyy-MM-dd");
            resultData.BedNumber = data.BedNumber;
            resultData.FetusNumber = data.FetusNumber;
            resultData.HouseholdNature = data.HouseholdNature;
            resultData.HospitalizationNo = data.HospitalizationNo;
            resultData.InpatientDepartmentCode = data.InpatientDepartmentCode;
            //var diagnosisList = GetDiagnosisList(
            //    data.AdmissionMainDiagnosisIcd10,
            //    data.DiagnosisIcd10Two,
            //    data.DiagnosisIcd10Three
            //);
            //resultData.DiagnosisList = diagnosisList;
            return resultData;
        }
        /// <summary>
        /// 获取诊断列表
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <param name="three"></param>
        /// <returns></returns>
        private List<InpatientDiagnosisDto> GetDiagnosisList(string one, string two, string three)
        {
            var resultData = new List<InpatientDiagnosisDto>();
            var param=new List<string>();
            if (!string.IsNullOrWhiteSpace(one)) param.Add(one);
            if (!string.IsNullOrWhiteSpace(one)) param.Add(two);
            if (!string.IsNullOrWhiteSpace(one)) param.Add(three);
            //var queryIcd10Data = _hisSqlRepository.QueryICD10(new QueryICD10UiParam()
            //{ Page = 1, Limit = 10000000 });
            //var icd10Data = queryIcd10Data.Values.FirstOrDefault();

            var icd10Data = _hisSqlRepository.QueryICD10Detail(param);
            if (icd10Data == null) throw new Exception("icd10为空请更新icd10!!!");
            if (!string.IsNullOrWhiteSpace(one))
            {
                var strList = GetStrList(one);
                var oneData = icd10Data.Where(c => strList.Contains(c.DiseaseCoding)).ToList();
                if (strList.Count() != oneData.Count()) throw new Exception("icd10与基层数量不匹配请更新icd10!!!");
                resultData.AddRange(oneData.Select(c => new InpatientDiagnosisDto()
                {
                    IsMainDiagnosis = true,
                    DiseaseName = c.DiseaseName,
                    DiseaseCoding = c.DiseaseCoding,
                    ProjectCode=c.ProjectCode!=null? c.ProjectCode:c.DiseaseCoding
                }).ToList());
            }
            if (!string.IsNullOrWhiteSpace(two))
            {
                var strList = GetStrList(two);
                var data = icd10Data.Where(c => strList.Contains(c.DiseaseCoding)).ToList();
                if (strList.Count() != data.Count()) throw new Exception("icd10与基层数量不匹配请更新icd10!!!");
                resultData.AddRange(data.Select(c => new InpatientDiagnosisDto()
                {
                    IsMainDiagnosis = false,
                    DiseaseName = c.DiseaseName,
                    DiseaseCoding = c.DiseaseCoding,
                    ProjectCode = c.ProjectCode != null ? c.ProjectCode : c.DiseaseCoding
                }).ToList());
            }
            if (!string.IsNullOrWhiteSpace(three))
            {
                var strList = GetStrList(three);
                var data = icd10Data.Where(c => strList.Contains(c.DiseaseCoding)).ToList();
                if (strList.Count() != data.Count()) throw new Exception("icd10与基层数量不匹配请更新icd10!!!");
                resultData.AddRange(data.Select(c => new InpatientDiagnosisDto()
                {
                    IsMainDiagnosis = false,
                    DiseaseName = c.DiseaseName,
                    DiseaseCoding = c.DiseaseCoding,
                    ProjectCode = c.ProjectCode != null ? c.ProjectCode : c.DiseaseCoding
                }).ToList());
            }
            //获取诊断字符串集
            List<string> GetStrList(string diagnosis)
            {
                var returnData = new List<string>();
                string[] sArray = diagnosis.Split(',');
                foreach (var item in sArray)
                {
                    returnData.Add(item);
                }
                return returnData;
            }

            return resultData;

        }
    }

}




