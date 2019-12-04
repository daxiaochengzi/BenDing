using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.JsonEntiy;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Enums;
using BenDing.Domain.Models.Params.UI;
using BenDing.Domain.Models.Params.Web;
using BenDing.Repository.Interfaces.Web;
using BenDing.Service.Interfaces;
using Newtonsoft.Json;

namespace BenDing.Service.Providers
{
    public class WebServiceBasicService : IWebServiceBasicService
    {
        private readonly IHisSqlRepository _hisSqlRepository;
        private readonly IMedicalInsuranceSqlRepository _medicalInsuranceSqlRepository;
        private readonly IWebBasicRepository _webServiceBasic;
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
            var data = _webServiceBasic.HIS_InterfaceList(tradeCode, inputParameter, "");
            resultList = GetResultData(ini, data);
            if (resultList.Any())
            {
               var resultValueJson=  resultList.FirstOrDefault();
                resultData= AutoMapper.Mapper.Map<UserInfoDto>(resultValueJson);
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
            var data = _webServiceBasic.HIS_InterfaceList("30", JsonConvert.SerializeObject(info),
                userInfo.UserId);
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
            var data = _webServiceBasic.HIS_InterfaceList("06", JsonConvert.SerializeObject(oCatalogInfo),
                user.UserId);
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
                var catalogDtoData = _webServiceBasic.HIS_InterfaceList("05",
                    JsonConvert.SerializeObject(oCatalogInfo), user.UserId);
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
        public string GetICD10(UserInfoDto user, CatalogParam param)
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
                Newtonsoft.Json.JsonConvert.SerializeObject(oICD10Info), user.UserId);
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
                     _webServiceBasic.HIS_InterfaceList("07", JsonConvert.SerializeObject(oICD10Info),
                        user.UserId);
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
        public BaseOutpatientInfoDto GetOutpatientPerson(UserInfoDto user, GetOutpatientUiParam param, bool isSave)
        {
            var resultData = new BaseOutpatientInfoDto();
            List<OutpatientInfoJsonDto>  result;
            var outPatient = new OutpatientParam()
            {
                AuthCode = user.AuthCode,
                OrganizationCode = user.OrganizationCode,
                IdCardNo = param.IdCardNo,
                StartTime = Convert.ToDateTime(param.StartTime).ToString("yyyy-MM-dd HH:mm:ss"),
                EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),

            };
            var init = new OutpatientInfoJsonDto();
            var data = _webServiceBasic.HIS_InterfaceList("12", JsonConvert.SerializeObject(param),
                user.UserId);
            result = GetResultData(init, data);
            if (result.Any())
            {
               var  resultDataIni= result.FirstOrDefault(c => c.BusinessId == param.BusinessId);
                if (resultDataIni != null)
                {
                    resultData = AutoMapper.Mapper.Map<BaseOutpatientInfoDto>(resultDataIni);
                }
                if (isSave) _hisSqlRepository.SaveOutpatient(user, resultData);

            }

            return resultData;
        }

        /// <summary>
        /// 获取门诊病人费用明细
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<BaseOutpatientDetailDto> GetOutpatientDetailPerson(UserInfoDto user,OutpatientDetailParam param)
        {
            List<BaseOutpatientDetailJsonDto> result;
           var resultData= new List<BaseOutpatientDetailDto>();
            var init = new BaseOutpatientDetailJsonDto();
            var data = _webServiceBasic.HIS_InterfaceList("16", JsonConvert.SerializeObject(param),
                user.UserId);

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
            var resultDataIni = new InpatientInfoJsonDto();
            List<InpatientInfoJsonDto> result;
            var init = new InpatientInfoJsonDto();
            var data = _webServiceBasic.HIS_InterfaceList("10", JsonConvert.SerializeObject(param.InfoParam, Formatting.Indented), param.User.UserId);
            result = GetResultData(init, data);
            if (result.Any())
            {
                resultDataIni = result.FirstOrDefault(c => c.BusinessId == param.BusinessId);
                if (resultDataIni != null)
                {
                    resultData = AutoMapper.Mapper.Map<InpatientInfoDto>(resultDataIni);
                    if (param.IsSave == true)
                    {
                        _hisSqlRepository.SaveInpatientInfo(param.User, resultData);
                    }

                }



            }

            return resultData;
        }



        /// <summary>
        /// 获取住院明细
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<InpatientInfoDetailDto> GetInpatientInfoDetail(UserInfoDto user,
            InpatientInfoDetailParam param, string businessId)
        {
            var resultData = new List<InpatientInfoDetailDto>();
            var result = new List<InpatientInfoDetailJsonDto>();
            var init = new InpatientInfoDetailJsonDto();
            var data = _webServiceBasic.HIS_InterfaceList("14", JsonConvert.SerializeObject(param),
                user.UserId);
            result = GetResultData(init, data);

            if (result.Any())
            {
                var resultIni = AutoMapper.Mapper.Map<List<InpatientInfoDetailDto>>(result);
                _hisSqlRepository.SaveInpatientInfoDetail(user, resultIni);
                //  var msg = "获取住院号【" + resultFirst.住院号 + "】，业务ID【" + param.业务ID + "】的时间段内的住院费用成功，共" + result.Count +
                //          "条记录";
            }

            return resultData;
        }

        /// <summary>
        /// 医保信息保存
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
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
            var data = _webServiceBasic.HIS_InterfaceList("36", JsonConvert.SerializeObject(param),
                user.UserId);
            result = GetResultData(init, data);
            var resultFirst = result.FirstOrDefault();
            if (resultFirst != null)
            {
                //var msg = "获取住院号【" + resultFirst.住院号 + "】，业务ID【" + param.业务ID + "】的时间段内的住院费用成功，共" + result.Count +
                //          "条记录";
            }
        }

        /// <summary>
        /// 医保信息删除
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public void DeleteMedicalInsurance(UserInfoDto user, DeleteMedicalInsuranceParam param)
        {
            if (param.CheckLocal)
            {

                var num = 0; // _hisSqlRepository.QueryMedicalInsurance(param.业务ID);
                if (num == 0)
                {
                    var msg = "数据库中未找到相应的住院业务ID的医保信息！";
                }
            }

            var resultData = _webServiceBasic.HIS_InterfaceList("37",
                "{'验证码':'" + param.验证码 + "','业务ID':'" + param.业务ID + "'}", user.UserId);
            if (resultData.Result == "1")
            {
                var count = _hisSqlRepository.DeleteMedicalInsurance(user, param.业务ID);
            }
        }

        /// <summary>
        /// 保存HIS系统中科室、医师、病区、床位的基本信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<InformationDto> SaveInformation(UserInfoDto user, InformationParam param)
        {
          var  resultDataIni=  new List<InformationDto>();
            var resultData =
                 _webServiceBasic.HIS_InterfaceList("03", JsonConvert.SerializeObject(param), user.UserId);
            var result = new List<InformationJsonDto>();
            var init = new InformationJsonDto();
            if (resultData.Result == "1")
            {
                result = GetResultData(init, resultData);
                if (result.Any())
                {
                    resultDataIni  = AutoMapper.Mapper.Map<List<InformationDto>>(result);
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
            var data = _webServiceBasic.HIS_InterfaceList("39", JsonConvert.SerializeObject(param),
                param.UserId);
        }

        /// <summary>
        /// 获取医保构建参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public void SaveXmlData(SaveXmlData param)
        {

            var data = _webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(param),
                param.UserId);
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

        public int ThreeCataloguePairCodeUpload(UserInfoDto user)
        {
            int resultData = 0;
            var data = _medicalInsuranceSqlRepository.ThreeCataloguePairCodeUpload(user.OrganizationCode);
            if (data.Any())
            {
                var uploadDataRow = data.Select(c => new ThreeCataloguePairCodeUploadRowDto()
                {
                    //ProjectId = c.Id.ToString("N"),
                    HisDirectoryCode = c.DirectoryCode,
                    Manufacturer = "",
                    ProjectName = c.ProjectName,
                    ProjectCode = c.ProjectCode,
                    ProjectCodeType = c.DirectoryType,
                    ProjectCodeTypeDetail = c.ProjectCodeType,
                    Remark = c.Remark,
                    ProjectLevel = ((ProjectLevel)Convert.ToInt32(c.ProjectLevel)).ToString(),
                    RestrictionSign = GetStrData(c.ProjectCodeType, c.RestrictionSign)

                }).ToList();
                var uploadData = new ThreeCataloguePairCodeUploadDto()
                {
                    AuthCode = user.AuthCode,
                    CanCelState = "0",
                    UserName = user.UserName,
                    OrganizationCode = user.OrganizationCode,
                    PairCodeRow = uploadDataRow,
                    VersionNumber = ""
                };
                _webServiceBasic.HIS_InterfaceList("35", JsonConvert.SerializeObject(uploadData),
                   user.UserId);
                resultData = _medicalInsuranceSqlRepository.UpdateThreeCataloguePairCodeUpload(user.OrganizationCode);
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
            if (!string.IsNullOrWhiteSpace(queryData.AdmissionInfoJson))
            {
                var data = JsonConvert.DeserializeObject<QueryMedicalInsuranceDetailDto>(queryData.AdmissionInfoJson);
                resultData.Id = queryData.Id;
                resultData.MedicalInsuranceHospitalizationNo = queryData.MedicalInsuranceHospitalizationNo;
                resultData.AdmissionDate = DateTime.ParseExact(data.AdmissionDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd");
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
                    resultData.InpatientDepartmentName = infoList.Where(c => c.FixedEncoding == data.InpatientDepartmentCode).Select(d => d.DirectoryName).FirstOrDefault();
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
                    var icd10Data = _hisSqlRepository.QueryICD10(new QueryICD10UiParam() { DiseaseCoding = data.DiagnosisIcd10Two });
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
                    var icd10Data = _hisSqlRepository.QueryICD10(new QueryICD10UiParam() { DiseaseCoding = data.DiagnosisIcd10Three });
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

