using BenDingActive.Model;
using BenDingActive.Model.Dto;
using BenDingActive.Model.Params;
using BenDingActive.Model.Params.Service;
using BenDingActive.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDingActive.Model.Params.Basiclevel;
using BenDingActive.Model.Params.Basiclevel.Dto;
using Newtonsoft.Json;
using BenDingActive.Model.Params.Basiclevel.Param;
using BenDingActive.Model.Params.Web;
using BenDingActive.Service;


namespace BenDingActive.Service
{
    public class ResidentMedicalInsuranceTest
    {
       // private ResidentMedicalInsuranceServices _resident = new ResidentMedicalInsuranceServices();
        //public ApiJsonResultData GetUserInfo()
        //{
        //    var resultData = new ApiJsonResultData();
        //    //获取社保卡信息
        //    var hisBase = new HisBaseParam() 
        //    {
        //        YbOrgCode = "003",
        //        OrgID = Guid.NewGuid().ToString(),
        //        BID = Guid.NewGuid().ToString(),
        //        EmpID = "A2A4866484DC40F0A10FB7482603AABD",
        //        BsCode = "003",
        //        TransKey = Guid.NewGuid().ToString(),
        //    };
        //    //读取社保卡
        //    var Param = new UserInfoParam
        //    {
        //        PI_CRBZ = "1",
        //        PI_SFBZ = "513701199002124815"
        //    };
        //    try
        //    {
        //        var data = Resident.GetUserInfo(Param);
        //        if (data.PO_FHZ == "1")
        //        {
        //            //查看当前病人是否有医保信息
        //            var residentInfoParam = new ResidentInfoBasiclevelParam()
        //            {
        //                BusinessId = hisBase.BID,
        //                IdCard = data.PO_SFZH,
        //                OrgCode = hisBase.OrgID
        //            };
        //            var result = HttpHelp.HttpPost(Logs.ToJson(residentInfoParam), "QueryMedicalInsuranceResidentInfo",
        //                new MedicalInsuranceResidentInfoBasiclevelDto ());
        //            if (result == null)
        //            {//保存医保病人信息
        //                var saveResidentInfoParam = new SaveMedicalInsuranceResidentInfoBasiclevelParam()
        //                {  YbOrgCode= hisBase.YbOrgCode,
        //                    BID = hisBase.YbOrgCode,
        //                    BsCode = hisBase.BsCode,
        //                    TransKey= hisBase.TransKey,
        //                    EmpID= hisBase.EmpID,
        //                    OrgID= hisBase.OrgID,
        //                    BusinessId = hisBase.BID,
        //                    ContentJson = Logs.ToJson(residentInfoParam),
        //                    ResultDatajson = Logs.ToJson(data),
        //                    DataAllId = Guid.NewGuid().ToString("N"),
        //                    DataId = hisBase.BID,
        //                    IdCard= data.PO_SFZH,
        //                    DataType = hisBase.BsCode
        //                };
        //                var resultSaveData = HttpHelp.HttpPost(Logs.ToJson(residentInfoParam), "SaveMedicalInsuranceResidentInfo",
        //                    new ApiJsonResultData());
        //            }
        //            //CXJB001 存入基层
        //            var saveXmlParam = new SaveXmlDataServiceParam()
        //            {
        //                YbOrgCode = hisBase.YbOrgCode,
        //                OrgID = hisBase.OrgID,
        //                BID = hisBase.BID,
        //                EmpID = hisBase.EmpID,
        //                BsCode = hisBase.BsCode,
        //                TransKey = hisBase.TransKey,
        //                Participation = Logs.ToJson(Param),
        //                ResultData = Logs.ToJson(data),
        //                BusinessNumber = "CXJB001",

        //            };
        //            var resultXml = HttpHelp.HttpPost(Logs.ToJson(saveXmlParam), 
        //                "SaveXmlData",new ApiJsonResultData());
        //        }
        //        //日志
        //        Logs.LogWrite(new LogParam()
        //        {
        //            OperatorCode = hisBase.EmpID.ToString(),
        //            Params = Logs.ToJson(Param),
        //            ResultData = Logs.ToJson(data) //Logs.ToJson(data)
        //        });
        //    }
        //    catch (Exception e)
        //    {
        //        resultData.Code = false;
        //        resultData.Message = e.Message;
        //        Logs.LogWrite(new LogParam()
        //        {
        //            Msg = e.Message,
        //            OperatorCode = hisBase.EmpID.ToString(),
        //        });  
        //    }
        //    return resultData;
        //}
        //public ApiJsonResultData HospitalizationRegister(QueryInpatientInfoBasicParam param)
        //{  // 1.查询中间库当前病人是否入院
        //   // 1.1.1如果入院直接返回查询结果
        //   // 1.1.2如果住院病人无数据则对当前病人进行基础入院信息下载(GetInpatientInfo),再进行查询并返回结果
        //   // 1.2 判断医保信息回写至基层系统CXJB002数据是否存在入院数据
        //   // 1.2.1 如果已有入院登记记录(则返回已有入院记录)
        //   // 1.2.2 居民医保进行入院办理(HospitalizationRegister)  
        //   // 1.2.3 更新医保人员信息   
        //   // 1.2.4 医保信息回写至基层系统CXJB002
        //   //-------------------------------

        //    var resultData = new ApiJsonResultData();

        //    try
        //    {
        //        //1.    1.1.1    1.1.2
        //        var result = HttpHelp.HttpPost(Logs.ToJson(param), "QueryInpatientInfo",
        //            new QueryInpatientInfoBasicLevelDto());

        //        if (result != null && !string.IsNullOrWhiteSpace(result.Id)) //判断是否存在
        //        {
        //            var residentInfoParam = new ResidentInfoBasiclevelParam()
        //            {
        //                BusinessId = baseParam.BID,
        //                IdCard = result.身份证号,
        //                OrgCode = baseParam.OrgID
        //            };
        //            // 1.2
        //            var saveDataAllQueryData = HttpHelp.HttpPost(Logs.ToJson(residentInfoParam),
        //                "QueryMedicalInsuranceResidentInfo",
        //                new MedicalInsuranceResidentInfoBasiclevelDto());
        //            if (saveDataAllQueryData != null &&
        //                !string.IsNullOrWhiteSpace(saveDataAllQueryData.DataAllId)) //判断是否存在
        //            {   //获取社保卡信息
        //                if (saveDataAllQueryData.DataType == "003")
        //                {//医保办理入院

        //                    //1.2.2居民医保进行入院办理(HospitalizationRegister)   
        //                    var dateTime = HttpHelp.HttpPost("",
        //                        "GetServiceTime",
        //                        new ApiJsonResultData());
        //                    var RegisterParam = new HospitalizationRegisterParam()
        //                    {
        //                        PI_SFBZ = param.PI_SFBZ,
        //                        PI_CRBZ = param.PI_CRBZ,
        //                        PI_YLLB = param.PI_YLLB,
        //                        PI_TES = param.PI_TES,
        //                        PI_HKXZ = param.PI_HKXZ,
        //                        PI_RYRQ = Convert.ToDateTime(result.入院日期).ToString("yyyyMMdd"),
        //                        PI_ICD10 = result.入院主诊断ICD10,
        //                        PI_ICD10_2 = result.入院次诊断ICD10,
        //                        PI_RYZD = result.入院主诊断,
        //                        PI_ZYBQ = result.入院科室,
        //                        PI_CWH = result.入院床位,
        //                        PI_YYZYH = Convert.ToDateTime(dateTime.Data).ToString("yyyyMMddHHmmss") + baseParam.OrgID.ToString().Substring(0, 6),
        //                        PI_JBR = baseParam.EmpIDCode,
        //                    };
        //                    var RegisterData = Resident.HospitalizationRegister(RegisterParam);
        //                    if (RegisterData.PO_FHZ == "1")
        //                    {

        //                        // 1.2.3 更新医保人员信息   

        //                        var saveResidentInfoParam = new SaveMedicalInsuranceResidentInfoBasiclevelParam()
        //                        {
        //                            YbOrgCode = baseParam.YbOrgCode,
        //                            BID = baseParam.YbOrgCode,
        //                            BsCode = baseParam.BsCode,
        //                            TransKey = baseParam.TransKey,
        //                            EmpID = baseParam.EmpID,
        //                            OrgID = baseParam.OrgID,
        //                            BusinessId = baseParam.BID,
        //                            ContentJson = Logs.ToJson(RegisterParam),
        //                            ResultDatajson = Logs.ToJson(RegisterData),
        //                            DataAllId = saveDataAllQueryData.DataAllId,
        //                            DataId = baseParam.BID,
        //                            DataType = baseParam.BsCode
        //                        };
        //                        var resultSaveData = HttpHelp.HttpPost(Logs.ToJson(residentInfoParam), "SaveMedicalInsuranceResidentInfo",
        //                            new ApiJsonResultData());

        //                        //1.2.4医保信息回写至基层系统CXJB002

        //                        var DataAllBasiclevelParam = new SaveXmlDataServiceParam()
        //                        {
        //                            YbOrgCode = baseParam.YbOrgCode,
        //                            OrgID = baseParam.OrgID,
        //                            BID = baseParam.BID,
        //                            EmpID = baseParam.EmpID,
        //                            BsCode = baseParam.BsCode,
        //                            TransKey = baseParam.TransKey,
        //                            Participation = Logs.ToJson(RegisterParam),
        //                            ResultData = Logs.ToJson(RegisterData),
        //                            BusinessNumber = "CXJB002",
        //                        };
        //                        //CXJB001 存入基层
        //                        var SaveDataAll = HttpHelp.HttpPost(Logs.ToJson(DataAllBasiclevelParam),
        //                            "SaveMedicalInsuranceDataAll",
        //                            new ApiJsonResultData());
        //                        if (SaveDataAll.Code == false)
        //                        {
        //                            throw new Exception(SaveDataAll.Message);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        throw new Exception(RegisterData.PO_MSG);
        //                    }
        //                }
        //                else
        //                {
        //                    throw new Exception("BusinessId:" + saveDataAllQueryData.BusinessId + "DataType" +
        //                                        saveDataAllQueryData.DataType + "病人状态不正确");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            throw new Exception("IdCard:" + param.IdCard + "该病人未在基层系统中住院，请检查该病人是否离院!!!");

        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        resultData.Code = false;
        //        resultData.Message = e.Message;
        //        Logs.LogWrite(new LogParam()
        //        {
        //            //Msg = data.Po_MSG,
        //            Msg = e.Message,
        //            OperatorCode = baseParam.EmpID.ToString(),

        //        });
        //    }

        //    return resultData;
        //}
        //public ApiJsonResultData HospitalizationModify(HospitalizationModifyWebParam param, HisBaseParam baseParam)
        //{ var resultData = new ApiJsonResultData();

        //    try
        //    {
        //        var result = HttpHelp.HttpPost(Logs.ToJson(param), "QueryInpatientInfo",
        //            new QueryInpatientInfoBasicLevelDto());

        //        var RegisterParam = new HospitalizationModifyParam()
        //        {

        //            PI_TES = param.PI_TES,
        //            PI_HKXZ = param.PI_HKXZ,
        //            PI_RYRQ = Convert.ToDateTime(result.入院日期).ToString("yyyyMMdd"),
        //            PI_ICD10 = result.入院主诊断ICD10,
        //            PI_ICD10_2 = result.入院次诊断ICD10,
        //            PI_RYZD = result.入院主诊断,
        //            PI_ZYBQ = result.入院科室,
        //            PI_CWH = result.入院床位,
        //            PI_ZHY = param.PI_ZYH,
        //            PI_YYZYH = param.PI_YYZYH,

        //        };
        //        //入院登记修改
        //        var RegisterData = Resident.HospitalizationModify(RegisterParam);
        //        if (RegisterData.PO_FHZ == "1")
        //        {
        //            //1.2.3 医保信息回写至基层系统CXJB002
        //            var hisMedicalInsuranceId = Guid.NewGuid().ToString("N");
        //            var DataAllBasiclevelParam = new SaveXmlDataServiceParam()
        //            {
        //                YbOrgCode = baseParam.YbOrgCode,
        //                OrgID = baseParam.OrgID,
        //                BID = baseParam.BID,
        //                EmpID = baseParam.EmpID,
        //                BsCode = baseParam.BsCode,
        //                TransKey = baseParam.TransKey,
        //                Participation = Logs.ToJson(RegisterParam),
        //                ResultData = Logs.ToJson(RegisterData),
        //                BusinessNumber = "CXJB003",
        //            };
        //            //CXJB001 存入基层
        //            var SaveDataAll = HttpHelp.HttpPost(Logs.ToJson(DataAllBasiclevelParam),
        //                "SaveMedicalInsuranceDataAll",
        //                new ApiJsonResultData());
        //            if (SaveDataAll.Code == false)
        //            {
        //                throw new Exception(SaveDataAll.Message);
        //            }
        //        }
        //        else
        //        {

        //            Logs.LogWrite(new LogParam()
        //            {
        //                Msg = RegisterData.PO_MSG,
        //                OperatorCode = baseParam.EmpID.ToString(),
        //                Params = Logs.ToJson(RegisterParam),
        //                ResultData = Logs.ToJson(RegisterData)

        //            });
        //            throw new Exception(RegisterData.PO_MSG);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        resultData.Code = false;
        //        resultData.Message = e.Message;

        //    }

        //    return resultData;


        //}

        ///// <summary>
        ///// 处方上传
        ///// </summary>
        ///// <returns></returns>
        //public ApiJsonResultData PrescriptionUpload(PrescriptionUploadWebParam param, HisBaseParam baseParam)
        //{   //选择性上次
        //    var resultData = new ApiJsonResultData();

        //    if (param.BusinessIdDetailList != null && param.BusinessIdDetailList.Any())
        //    {
        //        var queryParam = new InpatientInfoDetailQueryBasiclevelParam();
        //        queryParam.IdList = param.BusinessIdDetailList;

        //        var resultSaveData = HttpHelp.HttpPost(Logs.ToJson(queryParam), "InpatientInfoDetailQuery",
        //            new List<OutpatientDetailQueryBasiclevelDto>());
        //        if (resultSaveData.Any())
        //        {
        //            var uploadData = resultSaveData.OrderBy(c => c.Id).ToList();

        //            var uploadParam = new PrescriptionUploadParam();
        //            uploadParam.PI_ZHY = param.PI_ZHY;
        //            uploadParam.PI_JBR = baseParam.EmpIDCode;
        //            uploadParam.CFMX = uploadData.Select(c => new PrescriptionUploadCFMX()
        //            {  CO="0",
        //               AKA072="0",
        //               AKC229="0"
        //            }).ToList();

        //        }

        //    }

        //    return resultData;
        //}

        //private List<string> CheckCodeData(string Id, List<OutpatientDetailQueryBasiclevelDto>dataList)
        //{

        //}
        public UserInfoDto GetUserInfo()
        {
            //登录账户:cpg2677
            //居民保险
            //YbOrgCode = 99999
            //OrgID = 51072600000000000000000513435964
            //EmpID = E075AC49FCE443778F897CF839F3B924
            //BID = FFE6ADE4D0B746C58B972C7824B8C9DF &
            //      BsCode = 21
            //TransKey = FFE6ADE4D0B746C58B972C7824B8C9DF
            //伊珍敏
            //512501195802085180
            string baseParam = JsonConvert.SerializeObject(new HisBaseParam()
            {
                YbOrgCode = "99999",
                EmpID = "E075AC49FCE443778F897CF839F3B924",
                OrgID = "51072600000000000000000513435964",
                BID = "FFE6ADE4D0B746C58B972C7824B8C9DF",
                BsCode = "21",
                TransKey = "FFE6ADE4D0B746C58B972C7824B8C9DF"
            });
            //var paramEntity = new UserInfoParam();
            //paramEntity.PI_CRBZ = "1";
            //paramEntity.PI_SFBZ = "512527196604306139";

            var paramEntity = new ActiveUserInfoParam();
            paramEntity.PI_CRBZ = "2";
            paramEntity.PI_SFBZ = "1000039551";

            //var dataResult = _resident.GetUserInfo(JsonConvert.SerializeObject(paramEntity), JsonConvert.DeserializeObject<HisBaseParam>(baseParam));
            return new UserInfoDto();
        }
    }
}
