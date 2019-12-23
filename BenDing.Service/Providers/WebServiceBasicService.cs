using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.JsonEntiy;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Dto.Workers;
using BenDing.Domain.Models.Enums;
using BenDing.Domain.Models.Params;
using BenDing.Domain.Models.Params.Base;
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
            var info = new {验证码 = userInfo.AuthCode, 医院名称 = name};
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
            return "删除【" + (CatalogTypeEnum) catalog + "】 成功 " + num + "条";
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
            var oICD10Info = new ICD10InfoParam
            {
                开始时间 = timeNew,
                结束时间 = DateTime.Now.ToString("yyyy-MM-dd HH:ss:mm"),
                验证码 = param.AuthCode,
                病种名称 = ""
            };
            var data = _webServiceBasic.HIS_InterfaceList("08",
                Newtonsoft.Json.JsonConvert.SerializeObject(oICD10Info));
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
        /// <param name="user"></param>
        /// <param name="param"></param>
        /// <param name="isSave"></param>
        /// <returns></returns>
        public BaseOutpatientInfoDto GetOutpatientPerson(GetOutpatientPersonParam param)
        {
            var resultData = new BaseOutpatientInfoDto();
            List<OutpatientInfoJsonDto> result;
            var outPatient = new OutpatientParam()
            {
                AuthCode = param.User.AuthCode,
                OrganizationCode = param.User.OrganizationCode,
                IdCardNo = param.UiParam.IdCardNo,
                StartTime = Convert.ToDateTime(param.UiParam.StartTime).ToString("yyyy-MM-dd HH:mm:ss"),
                EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };
            var init = new OutpatientInfoJsonDto();
            var data = _webServiceBasic.HIS_InterfaceList("12", JsonConvert.SerializeObject(outPatient));

            result = GetResultData(init, data);
            if (result.Any())
            {
                var resultDataIni = result.FirstOrDefault(c => c.BusinessId == param.UiParam.BusinessId);
                if (resultDataIni != null)
                {
                    resultData = AutoMapper.Mapper.Map<BaseOutpatientInfoDto>(resultDataIni);
                }

                //if (resultData.ReceptionStatus < 2) throw  new  Exception("门诊病人需要已接诊才能结算!!!");

                if (param.IsSave)
                {
                    resultData.ReturnJson = param.ReturnJson;
                    resultData.Id = param.Id;

                    _hisSqlRepository.SaveOutpatient(param.User, resultData);
                    //门诊病人明细下载
                    GetOutpatientDetailPerson(param.User, new OutpatientDetailParam()
                    {
                        AuthCode = param.User.AuthCode,
                        OutpatientNo = resultData.OutpatientNumber,
                        BusinessId = resultData.BusinessId
                    });
                }


            }

            return resultData;
        }

        /// <summary>
        /// 获取门诊病人费用明细
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<BaseOutpatientDetailDto> GetOutpatientDetailPerson(UserInfoDto user, OutpatientDetailParam param)
        {
            List<BaseOutpatientDetailJsonDto> result;
            var resultData = new List<BaseOutpatientDetailDto>();
            var init = new BaseOutpatientDetailJsonDto();
            var data = _webServiceBasic.HIS_InterfaceList("16", JsonConvert.SerializeObject(param));

            result = GetResultData(init, data);

            if (result.Any())
            {
                resultData = AutoMapper.Mapper.Map<List<BaseOutpatientDetailDto>>(result);
                _hisSqlRepository.SaveOutpatientDetail(user, resultData);
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
            var transactionId = Guid.NewGuid().ToString("N");
            var xmlData = new MedicalInsuranceXmlDto();
            xmlData.BusinessId = param.BusinessId;
            xmlData.HealthInsuranceNo = "21";
            xmlData.TransactionId = transactionId;
            xmlData.AuthCode = param.User.AuthCode;
            xmlData.UserId = param.User.UserId;
            xmlData.OrganizationCode = param.User.OrganizationCode;
            var data = _webServiceBasic.HIS_Interface("39", JsonConvert.SerializeObject(xmlData));
            InpatientInfoJsonDto dataValue = JsonConvert.DeserializeObject<InpatientInfoJsonDto>(data.Msg);
            if (dataValue != null && dataValue.InpatientInfoJsonData != null)
            {
                var diagnosisList = dataValue.DiagnosisJson.Select(c => new InpatientDiagnosisDto()
                {
                    DiagnosisCode = c.DiagnosisCode,
                    DiagnosisName = c.DiagnosisName,
                    IsMainDiagnosis = c.IsMainDiagnosis == "是" ? true : false,
                    DiagnosisMedicalInsuranceCode = c.DiagnosisMedicalInsuranceCode
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
                    TransactionId = transactionId,
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
        public void SaveXmlData(SaveXmlData param)
        {


            //var data = _webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(param));
            //if (data.Result == "1")
            //{
            //    var saveParam = new MedicalInsuranceDataAllParam()
            //    {
            //        DataAllId = Guid.NewGuid().ToString("N"),
            //        BusinessId = param.业务ID,
            //        CreateUserId = param.操作人员ID,
            //        DataId = param.发起交易的动作ID,
            //        DataType = param.医保交易码,
            //        OrgCode = param.机构ID,
            //        ParticipationJson = param.入参,
            //        ResultDataJson = param.出参,
            //        HisMedicalInsuranceId = param.发起交易的动作ID,
            //        Remark = param.Remark,
            //        IdCard = param.IDCard,

            //    };
            //     _hisSqlRepository.SaveMedicalInsuranceDataAll(saveParam);
            //}


        }


        public UserInfoDto GetUserBaseInfo(string param)
        {
            var data = _systemManageRepository.QueryHospitalOperator(new QueryHospitalOperatorParam()
                {UserId = param});
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
            if (data.Any())
            {
                var uploadDataRow = data.Select(c => new ThreeCataloguePairCodeUploadRowDto()
                {
                    //ProjectId = c.Id.ToString("N"),
                    HisDirectoryCode = c.DirectoryCode,
                    Manufacturer = "",
                    ProjectName = c.ProjectName,
                    ProjectCode = c.ProjectCode,
                    ProjectCodeType = c.DirectoryCategoryCode,
                    ProjectCodeTypeDetail = c.ProjectCodeType,
                    Remark = c.Remark,
                    ProjectLevel = ((ProjectLevel) Convert.ToInt32(c.ProjectLevel)).ToString(),
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


                resultData = _medicalInsuranceSqlRepository.UpdateThreeCataloguePairCodeUpload(param);
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
            if (queryData==null)throw  new  Exception("当前病人未办理医保入院!!!");
            if (!string.IsNullOrWhiteSpace(queryData.AdmissionInfoJson))
            {
                var data = JsonConvert.DeserializeObject<QueryMedicalInsuranceDetailDto>(queryData.AdmissionInfoJson);
                resultData.Id = queryData.Id;
                resultData.MedicalInsuranceHospitalizationNo = queryData.MedicalInsuranceHospitalizationNo;
                resultData.AdmissionDate = DateTime
                    .ParseExact(data.AdmissionDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture)
                    .ToString("yyyy-MM-dd");
                resultData.BedNumber = data.BedNumber;
                resultData.FetusNumber = data.FetusNumber;
                resultData.HospitalizationNo = data.HospitalizationNo;
                if (data.IdentityMark == "1")
                {
                    resultData.IdCardNo = data.AfferentSign;
                }
                else
                {
                    resultData.PersonNumber = data.AfferentSign;
                }

                var queryParam = new InformationParam()
                {
                    DirectoryType = "0"
                };
                //获取科室
                var infoList = _hisSqlRepository.QueryInformationInfo(queryParam);
                if (infoList.Any())
                {
                    resultData.InpatientDepartmentCode = data.InpatientDepartmentCode;
                    resultData.InpatientDepartmentName = infoList
                        .Where(c => c.FixedEncoding == data.InpatientDepartmentCode).Select(d => d.DirectoryName)
                        .FirstOrDefault();
                }

                //诊疗
                var diagnosisList = new List<InpatientDiagnosisDto>();
                diagnosisList.Add(new InpatientDiagnosisDto
                {
                    IsMainDiagnosis = true,
                    DiagnosisName = data.AdmissionMainDiagnosis,
                    DiagnosisCode = data.AdmissionMainDiagnosisIcd10
                });
                if (!string.IsNullOrWhiteSpace(data.DiagnosisIcd10Two))
                {
                    var icd10Data = _hisSqlRepository.QueryICD10(new QueryICD10UiParam()
                        {DiseaseCoding = data.DiagnosisIcd10Two});
                    if (icd10Data.Any())
                    {
                        var Icd10Two = icd10Data.FirstOrDefault();
                        diagnosisList.Add(new InpatientDiagnosisDto
                        {
                            IsMainDiagnosis = false,
                            DiagnosisName = Icd10Two.DiseaseName,
                            DiagnosisCode = data.DiagnosisIcd10Two
                        });
                    }
                }

                if (!string.IsNullOrWhiteSpace(data.DiagnosisIcd10Three))
                {
                    var icd10Data = _hisSqlRepository.QueryICD10(new QueryICD10UiParam()
                        {DiseaseCoding = data.DiagnosisIcd10Three});
                    if (icd10Data.Any())
                    {
                        var Icd10Three = icd10Data.FirstOrDefault();
                        diagnosisList.Add(new InpatientDiagnosisDto
                        {
                            IsMainDiagnosis = false,
                            DiagnosisName = Icd10Three.DiseaseName,
                            DiagnosisCode = data.DiagnosisIcd10Three
                        });
                    }
                }

                resultData.DiagnosisList = diagnosisList;


            }

            return resultData;
        }


        public QueryWorkerMedicalInsuranceDto QueryWorKerMedicalInsuranceDetail(
            QueryMedicalInsuranceUiParam param)
        {
            var resultData = new QueryWorkerMedicalInsuranceDto();
            var userBase = GetUserBaseInfo(param.UserId);
            var queryData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(
                new QueryMedicalInsuranceResidentInfoParam()
                {
                    OrganizationCode = userBase.OrganizationCode,
                    BusinessId = param.BusinessId
                });
            if (!string.IsNullOrWhiteSpace(queryData.AdmissionInfoJson))
            {
                var data = JsonConvert.DeserializeObject<WorKerHospitalizationRegisterParam>(queryData.AdmissionInfoJson);
                resultData.Id = queryData.Id;
                resultData.MedicalInsuranceHospitalizationNo = queryData.MedicalInsuranceHospitalizationNo;
                resultData.AdmissionDate = DateTime
                    .ParseExact(data.AdmissionDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture)
                    .ToString("yyyy-MM-dd");
                resultData.BedNumber = data.BedNumber;
                resultData.HospitalizationNo = data.HospitalizationNo;
                //1为医保卡号2为公民身份号码 3为个人编号
                if (data.IdentityMark == "1") resultData.WorkerCardNo = data.AfferentSign;
                if (data.IdentityMark == "2") resultData.IdCardNo = data.AfferentSign;
                if (data.IdentityMark == "3") resultData.PersonNumber = data.AfferentSign;
                //诊疗
                var diagnosisList = new List<InpatientDiagnosisDto>();
                diagnosisList.Add(new InpatientDiagnosisDto
                {
                    IsMainDiagnosis = true,
                    DiagnosisName = data.AdmissionMainDiagnosis,
                    DiagnosisCode = data.AdmissionMainDiagnosisIcd10
                });
                if (!string.IsNullOrWhiteSpace(data.DiagnosisIcd10Two))
                {
                    var icd10Data = _hisSqlRepository.QueryICD10(new QueryICD10UiParam()
                        {DiseaseCoding = data.DiagnosisIcd10Two});
                    if (icd10Data.Any())
                    {
                        var Icd10Two = icd10Data.FirstOrDefault();
                        diagnosisList.Add(new InpatientDiagnosisDto
                        {
                            IsMainDiagnosis = false,
                            DiagnosisName = Icd10Two.DiseaseName,
                            DiagnosisCode = data.DiagnosisIcd10Two
                        });
                    }
                }
                if (!string.IsNullOrWhiteSpace(data.DiagnosisIcd10Three))
                {
                    var icd10Data = _hisSqlRepository.QueryICD10(new QueryICD10UiParam()
                        {DiseaseCoding = data.DiagnosisIcd10Three});
                    if (icd10Data.Any())
                    {
                        var Icd10Three = icd10Data.FirstOrDefault();
                        diagnosisList.Add(new InpatientDiagnosisDto
                        {
                            IsMainDiagnosis = false,
                            DiagnosisName = Icd10Three.DiseaseName,
                            DiagnosisCode = data.DiagnosisIcd10Three
                        });
                    }
                }
                resultData.DiagnosisList = diagnosisList;


            }

            return resultData;
        }
    }
}




