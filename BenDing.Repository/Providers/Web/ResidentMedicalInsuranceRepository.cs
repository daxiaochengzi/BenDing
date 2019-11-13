using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Enums;
using BenDing.Domain.Models.Params.Resident;
using BenDing.Domain.Models.Params.UI;
using BenDing.Domain.Models.Params.Web;
using BenDing.Domain.Xml;
using BenDing.Repository.Interfaces.Web;
using Newtonsoft.Json;

namespace BenDing.Repository.Providers.Web
{
    public class ResidentMedicalInsuranceRepository : IResidentMedicalInsuranceRepository
    {
        private IDataBaseHelpRepository _baseHelpRepository;
        private IBaseSqlServerRepository _baseSqlServerRepository;
        private ISystemManageRepository _iSystemManageRepository;
        private IWebBasicRepository _webServiceBasic;

        public ResidentMedicalInsuranceRepository(
            IDataBaseHelpRepository iDataBaseHelpRepository,
            IWebBasicRepository webBasicRepository,
            IBaseSqlServerRepository baseSqlServerRepository,
            ISystemManageRepository iSystemManageRepository
            )
        {
            _baseHelpRepository = iDataBaseHelpRepository;
            _webServiceBasic = webBasicRepository;
            _baseSqlServerRepository = baseSqlServerRepository;
        }
        public async Task Login(QueryHospitalOperatorParam param)
        {
            await Task.Run(async () =>
            {
                var userInfo = await _iSystemManageRepository.QueryHospitalOperator(param);

                var result = MedicalInsuranceDll.ConnectAppServer_cxjb(userInfo.MedicalInsuranceAccount, userInfo.MedicalInsurancePwd);

                if (result == 1)
                {
                    var data = XmlHelp.DeSerializerModel(new IniDto());

                }

            });
        }
        /// <summary>
        /// 获取个人基础资料
        /// </summary>
        /// <param name="param"></param>
        public async Task<ResidentUserInfoDto> GetUserInfo(ResidentUserInfoParam param)
        {
            return await Task.Run(async () =>
            {
                var data = new ResidentUserInfoDto();

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDll.CallService_cxjb("CXJB001");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new ResidentUserInfoDto());

                    }
                    else
                    {
                        throw new Exception("居民个人基础资料执行失败!!!");
                    }

                }



                return data;
            });
        }
        /// <summary>
        /// 入院登记
        /// </summary>
        /// <returns></returns>
        public async Task HospitalizationRegister(ResidentHospitalizationRegisterParam param, UserInfoDto user)
        {
            await Task.Run(async () =>
            {
                var paramIni = param;
                paramIni.Operators = BitConverter.ToInt64(Guid.Parse(param.Operators).ToByteArray(), 0).ToString();
                paramIni.InpatientDepartmentCode = BitConverter.ToInt64(Guid.Parse(param.InpatientDepartmentCode).ToByteArray(), 0).ToString();
                paramIni.HospitalizationNo = BitConverter.ToInt64(Guid.Parse(param.BusinessId).ToByteArray(), 0).ToString();
                paramIni.AdmissionDate = Convert.ToDateTime(param.AdmissionDate).ToString("yyyyMMdd");
                var xmlStr = XmlHelp.SaveXml(paramIni);
                if (xmlStr)
                {
                    var saveData = new MedicalInsuranceDto
                    {
                        AdmissionInfoJson = JsonConvert.SerializeObject(param),
                        HisHospitalizationId = param.HospitalizationNo,
                        Id = Guid.NewGuid(),
                    };
                    //保存医保信息
                    await _baseHelpRepository.SaveMedicalInsurance(user, saveData);

                    int result = MedicalInsuranceDll.CallService_cxjb("CXJB002");
                    if (result == 1)
                    {
                        var data = XmlHelp.DeSerializerModel(new ResidentHospitalizationRegisterDto());
                        saveData.MedicalInsuranceHospitalizationNo = data.MedicalInsuranceInpatientNo;
                        saveData.MedicalInsuranceYearBalance = Convert.ToDecimal(data.MedicalInsuranceYearBalance);
                        //更新医保信息
                        var strXmlIntoParam = XmlSerializeHelper.XmlSerialize(paramIni);
                        var strXmlBackParam = XmlSerializeHelper.XmlBackParam();
                        var saveXmlData = new SaveXmlData();
                        saveXmlData.OrganizationCode = user.OrganizationCode;
                        saveXmlData.AuthCode = user.AuthCode;
                        saveXmlData.BusinessId = param.BusinessId;
                        saveXmlData.TransactionId = saveData.Id.ToString("N");
                        saveXmlData.MedicalInsuranceBackNum = "CXJB002";
                        saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
                        saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
                        saveXmlData.MedicalInsuranceCode = "21";
                        saveXmlData.UserId = user.UserId;
                        //存基层
                        await _webServiceBasic.HIS_InterfaceListAsync("38", JsonConvert.SerializeObject(saveXmlData), param.UserId);
                       //存中间库
                        await _baseHelpRepository.SaveMedicalInsurance(user, saveData);
                    }
                    else
                    {
                        throw new Exception("居民入院登记执行失败!!!");
                    }

                }


            });

        }
        /// <summary>
        /// 修改入院登记
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task HospitalizationModify(HospitalizationModifyParam param, UserInfoDto user)
        {
            await Task.Run(async () =>
           {
               var xmlStr = XmlHelp.SaveXml(param);
               if (xmlStr)
               {
                   int result = MedicalInsuranceDll.CallService_cxjb("CXJB003");
                   if (result == 1)
                   {
                       var strXmlBackParam = XmlSerializeHelper.XmlBackParam();
                       var strXmlIntoParam = XmlSerializeHelper.XmlSerialize(param);
                       var saveXmlData = new SaveXmlData();
                       saveXmlData.OrganizationCode = user.OrganizationCode;
                       saveXmlData.AuthCode = user.AuthCode;
                       saveXmlData.BusinessId = param.BusinessId;
                       saveXmlData.TransactionId = param.TransactionId.ToString("N");
                       saveXmlData.MedicalInsuranceBackNum = "CXJB002";
                       saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
                       saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
                       saveXmlData.MedicalInsuranceCode = "23";
                       saveXmlData.UserId = user.UserId;
                       await _webServiceBasic.HIS_InterfaceListAsync("38", JsonConvert.SerializeObject(saveXmlData), param.UserId);
                       var paramStr = "";
                       var queryData = await _baseHelpRepository.QueryMedicalInsurance(user, param.BusinessId);
                       if (!string.IsNullOrWhiteSpace(queryData.AdmissionInfoJson))
                       {
                           var data = JsonConvert.DeserializeObject<QueryMedicalInsuranceDetailDto>(queryData.AdmissionInfoJson);
                           if (!string.IsNullOrWhiteSpace(param.AdmissionDate)) data.AdmissionDate = param.AdmissionDate;
                           if (!string.IsNullOrWhiteSpace(param.AdmissionMainDiagnosis)) data.AdmissionMainDiagnosis = param.AdmissionMainDiagnosis;
                           if (!string.IsNullOrWhiteSpace(param.AdmissionMainDiagnosisIcd10)) data.AdmissionMainDiagnosisIcd10 = param.AdmissionMainDiagnosisIcd10;
                           if (!string.IsNullOrWhiteSpace(param.DiagnosisIcd10Three)) data.DiagnosisIcd10Three = param.DiagnosisIcd10Three;
                           if (!string.IsNullOrWhiteSpace(param.DiagnosisIcd10Two)) data.DiagnosisIcd10Two = param.DiagnosisIcd10Two;
                           if (!string.IsNullOrWhiteSpace(param.FetusNumber)) data.FetusNumber = param.FetusNumber;
                           if (!string.IsNullOrWhiteSpace(param.HouseholdNature)) data.HouseholdNature = param.HouseholdNature;
                           if (!string.IsNullOrWhiteSpace(param.InpatientDepartmentCode)) data.InpatientDepartmentCode = param.InpatientDepartmentCode;
                           if (!string.IsNullOrWhiteSpace(param.BedNumber)) data.BedNumber = param.BedNumber;
                           data.Id = queryData.Id;
                           paramStr = JsonConvert.SerializeObject(data);
                       }

                       var saveData = new MedicalInsuranceDto
                       {
                           AdmissionInfoJson = paramStr,
                           HisHospitalizationId = queryData.HisHospitalizationId,
                           Id = queryData.Id,
                           IsModify = true,
                           MedicalInsuranceHospitalizationNo = queryData.MedicalInsuranceHospitalizationNo
                       };

                       await _baseHelpRepository.SaveMedicalInsurance(user, saveData);
                   }

               }
           });



        }
        /// <summary>
        /// 项目下载
        /// </summary>
        public async Task<ResidentProjectDownloadDto> ProjectDownload(ResidentProjectDownloadParam param)
        {
            return await Task.Run(async () =>
            {
                var data = new ResidentProjectDownloadDto();

                string strXml = XmlHelp.DeSerializerModelStr("ROWDATA");

                data = XmlHelp.DeSerializer<ResidentProjectDownloadDto>(strXml);
                // data = XmlHelp.DeSerializerModel(new ResidentProjectDownloadDto());
                //var xmlStr = XmlHelp.SaveXml(param);
                //if (xmlStr)
                //{
                //    int result = MedicalInsuranceDll.CallService_cxjb("CXJB019");
                //    if (result == 1)
                //    {
                //        data = XmlHelp.DeSerializerModel(new ResidentProjectDownloadDto());

                //    }

                //}
                //else
                //{
                //    throw new Exception("居民项目下载执行失败!!!");
                //}

                return data;
            });

        }
        /// <summary>
        /// 处方上传
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<bool> PrescriptionUpload(PrescriptionUploadUiParam param, UserInfoDto user)
        {
            return await Task.Run(async () =>
            {
                //处方上传解决方案
                //1.判断是id上传还是单个用户上传
                //3.获取医院等级判断金额是否符合要求
                //4.数据上传
                //4.1 id上传
                //4.1.2 获取医院等级判断金额是否符合要求
                //4.1.3 数据上传
                //4.1.3.1 数据上传失败,数据回写到日志
                //4.1.3.2 数据上传成功,数据回写至基层
                //4.2   单个病人整体上传
                //4.2.2 获取医院等级判断金额是否符合要求
                //4.2.3 数据上传
                //4.2.3.1 数据上传失败,数据回写到日志
                var resultData = true;

                //1.判断是id上传还是单个用户上传
                if (param.DataIdList != null && param.DataIdList.Any())
                {
                    var queryParam = new InpatientInfoDetailQueryParam();
                    queryParam.IdList = param.DataIdList;
                    //获取病人明细
                    var queryData = await _baseSqlServerRepository.InpatientInfoDetailQuery(queryParam);
                    if (queryData.Any())
                    {
                        var queryPairCodeParam = new QueryMedicalInsurancePairCodeParam()
                        {
                            DirectoryCodeList = queryData.Select(d => d.CostItemCode).ToList(),
                            OrganizationCode = user.OrganizationCode,
                        };
                        //获取医保对码数据
                        var queryPairCode = await _baseSqlServerRepository.QueryMedicalInsurancePairCode(queryPairCodeParam);
                        //处方上传数据金额验证
                        var validData = await PrescriptionDataUnitPriceValidation(queryData,queryPairCode,user);

                        var validDataList = validData.Values.FirstOrDefault();
                        //错误提示信息
                        var validDataMsg = validData.Keys.FirstOrDefault();
                        //获取处方上传入参
                        await GetPrescriptionUploadParam(validDataList, queryPairCode, user, param.InsuranceType);
                    }
                }
             
                //var xmlStr = XmlHelp.SaveXml(inIParam);
                return resultData;
            });
        }
        /// <summary>
        /// 获取处方上传入参
        /// </summary>
        /// <returns></returns>
        private async Task<PrescriptionUploadParam> GetPrescriptionUploadParam(List<QueryInpatientInfoDetailDto> param,
            List<QueryMedicalInsurancePairCodeDto> pairCodeList, UserInfoDto user,string insuranceType)
        {
            return await Task.Run(async () =>
            {
                var resultData = new PrescriptionUploadParam();
                resultData.MedicalInsuranceHospitalizationNo = param.FirstOrDefault()?.HospitalizationNo.ToString();
                resultData.Operators = CommonHelp.GuidToStr(user.UserId);
                var rowDataList=new List<PrescriptionUploadRowParam>(); 
                foreach (var item in param)
                {
                    var pairCodeData = pairCodeList.FirstOrDefault(c => c.DirectoryCode == item.CostItemCode);
                  
                    if (pairCodeData != null)
                    {
                        string residentSelfPayProportion = "";
                        if (insuranceType == "342")//居民
                        {
                            residentSelfPayProportion =
                                (Convert.ToDecimal(item.Amount) * pairCodeData.ResidentSelfPayProportion).ToString();
                        }
                        if (insuranceType == "310")//职工
                        {
                            residentSelfPayProportion =
                                (Convert.ToDecimal(item.Amount) * pairCodeData.WorkersSelfPayProportion).ToString();
                        }

                        var rowData = new PrescriptionUploadRowParam()
                        {
                            ColNum = 0,
                            PrescriptionNum = item.RecipeCodeFixedEncoding,
                            PrescriptionSort = item.DataSort.ToString(),
                            ProjectCode = pairCodeData.ProjectCode,
                            FixedEncoding = pairCodeData.FixedEncoding,
                            DirectoryDate = CommonHelp.FormatDateTime(item.BillTime),
                            DirectorySettlementDate = CommonHelp.FormatDateTime(item.OperateTime),
                            ProjectCodeType = pairCodeData.ProjectCodeType,
                            ProjectName = pairCodeData.ProjectName,
                            ProjectLevel = pairCodeData.ProjectLevel,
                            UnitPrice = item.UnitPrice,
                            Quantity = item.Quantity,
                            Amount = item.Amount,
                            ResidentSelfPayProportion = residentSelfPayProportion,//自付金额计算
                            Formulation = pairCodeData.Formulation,
                            Dosage= item.Dosage,
                            UseFrequency="0",
                            Usage = item.Usage,
                            Specification = item.Specification,
                            Unit = item.Unit,
                            UseDays = "0",
                            Remark = "",
                            DoctorJobNumber = item.BillDoctorIdFixedEncoding,
                        };
                        //是否现在使用药品
                        if (pairCodeData.RestrictionSign == "1")
                        {
                            rowData.LimitApprovalDate = CommonHelp.FormatDateTime(item.OperateTime);
                            rowData.LimitApprovalUser = rowData.DoctorJobNumber;
                            rowData.LimitApprovalMark = "1";
                            rowData.LimitApprovalRemark = item.BillDoctorIdFixedEncoding;
                        }

                        rowDataList.Add(rowData);
                    }

                    
                }
               
                resultData.RowDataList = rowDataList;
                return resultData;
            });
        }

        ///  <summary>
        /// 处方上传数据金额验证
        ///  </summary>
        ///  <param name="param"></param>
        /// <param name="pairCode"></param>
        /// <param name="user"></param>
        ///  <returns></returns>
        private async Task<Dictionary<string, List<QueryInpatientInfoDetailDto>>> PrescriptionDataUnitPriceValidation(List<QueryInpatientInfoDetailDto> param, List<QueryMedicalInsurancePairCodeDto> pairCode, UserInfoDto user)
        {
            return await Task.Run(async () =>
            {
                var resultData = new Dictionary<string, List<QueryInpatientInfoDetailDto>>();
                var dataList = new List<QueryInpatientInfoDetailDto>();
                //获取医院等级
                var grade = await _iSystemManageRepository.QueryHospitalOrganizationGrade(user.OrganizationCode);
                string msg = null;
                foreach (var item in param)
                {
                    var queryData = pairCode.FirstOrDefault(c => c.DirectoryCode == item.CostItemCode);
                    decimal queryAmount = 0;
                    if (grade == OrganizationGrade.二级乙等以下) queryAmount = queryData.ZeroBlock;
                    if (grade == OrganizationGrade.二级乙等) queryAmount = queryData.OneBlock;
                    if (grade == OrganizationGrade.二级甲等) queryAmount = queryData.TwoBlock;
                    if (grade == OrganizationGrade.三级乙等) queryAmount = queryData.ThreeBlock;
                    if (grade == OrganizationGrade.三级甲等)
                    {
                        queryAmount = queryData.FourBlock;
                    }

                    if (Convert.ToDecimal(item.Amount) < queryAmount)
                    {
                        dataList.Add(item);
                    }
                    else
                    {
                        msg += item.CostItemName+",";
                    }

                }

                msg = msg != null ? msg + "金额超出限制等级" : null;
                resultData.Add(msg, dataList);
                return resultData;
            });
        }
    }
}

