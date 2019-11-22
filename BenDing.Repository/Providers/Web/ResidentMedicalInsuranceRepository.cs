using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Enums;
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
            _iSystemManageRepository = iSystemManageRepository;
        }
        public async Task Login(QueryHospitalOperatorParam param)
        {
            await Task.Run(async () =>
            {
                var userInfo = await _iSystemManageRepository.QueryHospitalOperator(param);

                var result = MedicalInsuranceDll.ConnectAppServer_cxjb(userInfo.MedicalInsuranceAccount, userInfo.MedicalInsurancePwd);
                if (result != 1)
                {
                    var data = XmlHelp.DeSerializerModel(new IniXmlDto(), true);
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
                    data = XmlHelp.DeSerializerModel(new ResidentUserInfoDto(), true);

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
                        IsModify = false,
                        InsuranceType = (!string.IsNullOrWhiteSpace(param.InsuranceType)) == true ? Convert.ToInt32(param.InsuranceType) : 0


                    };
                    //保存医保信息
                    await _baseHelpRepository.SaveMedicalInsurance(user, saveData);

                    int result = MedicalInsuranceDll.CallService_cxjb("CXJB002");
                    if (result == 1)
                    {
                        var data = XmlHelp.DeSerializerModel(new ResidentHospitalizationRegisterDto(), true);
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
                        XmlHelp.DeSerializerModel(new IniDto(), true);
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
                       //日志
                       var logParam = new AddHospitalLogParam();
                       logParam.UserId = user.UserId;
                       logParam.OrganizationCode = user.OrganizationCode;
                       logParam.RelationId = queryData.Id;
                       logParam.JoinOrOldJson = queryData.AdmissionInfoJson;
                       logParam.ReturnOrNewJson = paramStr;
                       logParam.Remark = "医保入院登记修改";
                       _iSystemManageRepository.AddHospitalLog(logParam);
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
            });

        }
        /// <summary>
        /// 项目下载总条数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<Int64> ProjectDownloadCount(ResidentProjectDownloadParam param)
        {
            return await Task.Run(async () =>
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
            });
        }
        /// <summary>
        /// 处方上传
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<RetrunPrescriptionUploadDto> PrescriptionUpload(PrescriptionUploadUiParam param, UserInfoDto user)
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
                //4.1.3.2 数据上传成功,保存批次号，数据回写至基层
                //4.2   单个病人整体上传
                //4.2.2 获取医院等级判断金额是否符合要求
                //4.2.3 数据上传
                //4.2.3.1 数据上传失败,数据回写到日志
                var resultData = new RetrunPrescriptionUploadDto();
                var queryData = new List<QueryInpatientInfoDetailDto>();
                var queryParam = new InpatientInfoDetailQueryParam();
                //1.判断是id上传还是单个用户上传
                if (param.DataIdList != null && param.DataIdList.Any())
                {
                    queryParam.IdList = param.DataIdList;
                }
                else
                {
                    queryParam.BusinessId = param.BusinessId;
                }
                //获取病人明细
                queryData = await _baseSqlServerRepository.InpatientInfoDetailQuery(queryParam);
                if (queryData.Any())
                {
                    var queryBusinessId = (!string.IsNullOrWhiteSpace(queryParam.BusinessId)) == true ? param.BusinessId : 
                                        queryData.Select(c => c.BusinessId).FirstOrDefault();
                    var medicalInsuranceParam = new QueryMedicalInsuranceResidentInfoParam(){BusinessId = queryBusinessId};
                    //获取病人医保信息
                    var medicalInsurance = await _baseSqlServerRepository.QueryMedicalInsuranceResidentInfo(medicalInsuranceParam);
                    if (medicalInsurance == null && (!string.IsNullOrWhiteSpace(medicalInsurance.HisHospitalizationId)) == false)
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
                        resultData.Count = queryData.Count;
                        var queryPairCodeParam = new QueryMedicalInsurancePairCodeParam()
                        {
                            DirectoryCodeList = queryData.Select(d => d.CostItemCode).Distinct().ToList(),
                            OrganizationCode = user.OrganizationCode,
                        };
                        //获取医保对码数据
                        var queryPairCode =
                            await _baseSqlServerRepository.QueryMedicalInsurancePairCode(queryPairCodeParam);
                        //处方上传数据金额验证
                        var validData = await PrescriptionDataUnitPriceValidation(queryData, queryPairCode, user);
                        var validDataList = validData.Values.FirstOrDefault();
                        //错误提示信息
                        var validDataMsg = validData.Keys.FirstOrDefault();
                        if (!string.IsNullOrWhiteSpace(validDataMsg))
                        {
                            resultData.Msg += validDataMsg;
                        }
                        //获取处方上传入参
                        var paramIni = await GetPrescriptionUploadParam(validDataList, queryPairCode, user, medicalInsurance.InsuranceType);
                        //医保住院号
                        paramIni.MedicalInsuranceHospitalizationNo = medicalInsurance.MedicalInsuranceHospitalizationNo;
                        int num = paramIni.RowDataList.Count;
                        int a = 0;
                        int limit = 2;//限制条数
                        var count = Convert.ToInt32(num / limit) + ((num % limit) > 0 ? 1 : 0);
                        var idList = new List<Guid>();
                        while (a < count)
                        {//排除已上传数据

                            var rowDataListAll = paramIni.RowDataList.Where(d => !idList.Contains(d.Id)).OrderBy(c => c.PrescriptionSort).ToList();
                            var sendList = rowDataListAll.Take(limit).Select(s=>s.Id).ToList();
                            //新的数据上传参数
                            var uploadDataParam = paramIni;
                            uploadDataParam.RowDataList = rowDataListAll.Where(c=> sendList.Contains(c.Id)).ToList();
                            var uploadData = await PrescriptionUploadData(uploadDataParam, param.BusinessId, user);
                            if (uploadData.PO_FHZ != "1")
                            {
                                resultData.Msg += uploadData.PO_MSG;
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
            });
        }

        /// <summary>
        /// 处方数据上传
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<PrescriptionUploadDto> PrescriptionUploadData(PrescriptionUploadParam param, string businessId, UserInfoDto user)
        {
            return await Task.Run(async () =>
            {

                var data = new PrescriptionUploadDto();

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDll.CallService_cxjb("CXJB004");
                    if (result == 1)
                    {//如果业务id存在则不直接抛出异常
                        if (!string.IsNullOrWhiteSpace(businessId))
                        {
                            data = XmlHelp.DeSerializerModel(new PrescriptionUploadDto(), false);
                        }
                        else
                        {
                            data = XmlHelp.DeSerializerModel(new PrescriptionUploadDto(), true);
                        }

                        if (data.PO_FHZ == "1")
                        {   //交易码
                            var transactionId = Guid.NewGuid().ToString("N");
                            //添加批次
                            var updateFeeParam = param.RowDataList.Select(d => new UpdateHospitalizationFeeParam
                            {
                                Id = d.Id,
                                BatchNumber = data.ProjectBatch,
                                TransactionId = transactionId,
                                UploadAmount = d.Amount
                            }).ToList();
                            await _baseSqlServerRepository.UpdateHospitalizationFee(updateFeeParam, user);
                            //保存至基层
                            var strXmlIntoParam = XmlSerializeHelper.XmlSerialize(xmlStr);
                            var strXmlBackParam = XmlSerializeHelper.XmlBackParam();
                            var saveXmlData = new SaveXmlData();
                            saveXmlData.OrganizationCode = user.OrganizationCode;
                            saveXmlData.AuthCode = user.AuthCode;
                            saveXmlData.BusinessId = businessId;
                            saveXmlData.TransactionId = transactionId;
                            saveXmlData.MedicalInsuranceBackNum = "CXJB004";
                            saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
                            saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
                            saveXmlData.MedicalInsuranceCode = "31";
                            saveXmlData.UserId = user.UserId;
                            //await _webServiceBasic.HIS_InterfaceListAsync("38", JsonConvert.SerializeObject(saveXmlData), user.UserId);
                        }

                    }
                    else
                    {
                        throw new Exception("[" + user.UserId + "]" + "处方上传执行出错!!!");
                    }


                }

                return data;
            });
        }


        /// <summary>
        /// 获取处方上传入参
        /// </summary>
        /// <returns></returns>
        private async Task<PrescriptionUploadParam> GetPrescriptionUploadParam(List<QueryInpatientInfoDetailDto> param,
            List<QueryMedicalInsurancePairCodeDto> pairCodeList, UserInfoDto user, string insuranceType)
        {
            return await Task.Run(async () =>
            {
                var resultData = new PrescriptionUploadParam();

                resultData.Operators = CommonHelp.GuidToStr(user.UserId);
                var rowDataList = new List<PrescriptionUploadRowParam>();
                foreach (var item in param)
                {
                    var pairCodeData = pairCodeList.FirstOrDefault(c => c.DirectoryCode == item.CostItemCode);

                    if (pairCodeData != null)
                    {//自付金额
                        decimal residentSelfPayProportion = 0;
                        if (insuranceType == "342")//居民   
                        {
                            residentSelfPayProportion = CommonHelp.ValueToDouble((item.Amount + item.AdjustmentDifferenceValue) * pairCodeData.ResidentSelfPayProportion);
                        }
                        if (insuranceType == "310")//职工
                        {
                            residentSelfPayProportion = CommonHelp.ValueToDouble((item.Amount + item.AdjustmentDifferenceValue) * pairCodeData.WorkersSelfPayProportion);
                        }

                        var rowData = new PrescriptionUploadRowParam()
                        {
                            ColNum = 0,
                            PrescriptionNum = item.RecipeCodeFixedEncoding,
                            PrescriptionSort = item.DataSort,
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
                            Dosage = item.Dosage,
                            UseFrequency = "0",
                            Usage = item.Usage,
                            Specification = item.Specification,
                            Unit = item.Unit,
                            UseDays = "0",
                            Remark = "",
                            DoctorJobNumber = item.BillDoctorIdFixedEncoding,
                            Id = item.Id
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
                string msg = "";
                foreach (var item in param)
                {
                    var queryData = pairCode.FirstOrDefault(c => c.DirectoryCode == item.CostItemCode);
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
                                msg += item.CostItemName + ",";
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
            });
        }
    }
}

