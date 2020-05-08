using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using BenDing.Domain.Models.Dto;
using BenDing.Domain.Models.Dto.JsonEntity;
using BenDing.Domain.Models.Dto.OutpatientDepartment;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.HisXml;
using BenDing.Domain.Models.Params.Base;
using BenDing.Domain.Models.Params.DifferentPlaces;
using BenDing.Domain.Models.Params.Resident;
using BenDing.Domain.Models.Params.UI;
using BenDing.Domain.Models.Params.Web;
using BenDing.Domain.Xml;
using BenDing.Repository.Interfaces.Web;
using BenDing.Service.Interfaces;
using Newtonsoft.Json;
using NFine.Web.Model;


namespace NFine.Web.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>
    public class TestController : ApiController
    {
        private readonly IUserService userService;
        private readonly IWebServiceBasicService webServiceBasicService;
        private readonly IWebBasicRepository webServiceBasic;
        private readonly IHisSqlRepository hisSqlRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_userService"></param>
        public TestController(IUserService _userService,
            IWebServiceBasicService _webServiceBasicService,
            IWebBasicRepository _WebBasicRepository,
            IHisSqlRepository _hisSqlRepository)
        {
            userService = _userService;
            webServiceBasicService = _webServiceBasicService;
            webServiceBasic = _WebBasicRepository;
            hisSqlRepository = _hisSqlRepository;
        }
        /// <summary>
        /// 测试专用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData TestData()
        {
            return new ApiJsonResultData().RunWithTry(y =>
            {
                var resultStr = XmlHelp.DeSerializerXmlInfo("123");
                var iniData = XmlHelp.DeSerializer<OutpatientDepartmentCostInputJsonDto>(resultStr);
                //HospitalizationPresettlementDto data = null;
                //var dataIni = XmlHelp.DeSerializerModel(new HospitalizationPresettlementJsonDto(), true);
                //data = AutoMapper.Mapper.Map<HospitalizationPresettlementDto>(dataIni);
                ////报销金额 =统筹支付+补充险支付+生育补助+民政救助+民政重大疾病救助+精准扶贫+民政优抚+其它支付
                //decimal reimbursementExpenses =
                //    data.BasicOverallPay + data.SupplementPayAmount + data.BirthAAllowance +
                //    data.CivilAssistancePayAmount + data.CivilAssistanceSeriousIllnessPayAmount +
                //    data.AccurateAssistancePayAmount + data.CivilServicessistancePayAmount +
                //    data.OtherPaymentAmount;
                //data.ReimbursementExpenses = reimbursementExpenses;

                //var datacc= CommonHelp.GetPayMsg(JsonConvert.SerializeObject(data));
                //y.Data = datacc;
            });

        }
        [HttpGet]
        public ApiJsonResultData PageList([FromUri] UserInfo pagination)
        {
            return new ApiJsonResultData(ModelState, new UiInIParam()).RunWithTry(y =>
            {
                var paramList = new List<DifferentPlacesOtherDiagnosis>();
                var param = new DifferentPlacesHospitalizationRegisterParam()
                {
                    AdmissionDate = "123123",
                    DiagnosisList = paramList,

                };
                paramList.Add(new DifferentPlacesOtherDiagnosis()
                {
                    DiagnosisCode = "123",
                    DiagnosisName = "sss"
                });
                paramList.Add(new DifferentPlacesOtherDiagnosis()
                {
                    DiagnosisCode = "13323",
                    DiagnosisName = "ss333s"
                });
                var xmlStr = XmlHelp.SaveXml(param);
                //y.DataDescribe = CommonHelp.GetPropertyAliasDict(new UserInfoDto());
                //y.Data = userService.GetUserInfo();

            });

        }
        /// <summary>
        /// post测试
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData PageListPost([FromBody] UserInfo pagination)
        {
            return new ApiJsonResultData(ModelState, new UiInIParam()).RunWithTry(y =>
            {
               
                //y.DataDescribe = CommonHelp.GetPropertyAliasDict(new UserInfoDto());
                //y.Data = userService.GetUserInfo();

            });

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiJsonResultData GetUserInfo([FromBody] UiInIParam param)
        {
            return new ApiJsonResultData(ModelState, new UiInIParam()).RunWithTry(y =>
            {
                var userBase = webServiceBasicService.GetUserBaseInfo(param.UserId);
               
                y.Data = userBase;

            });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData MedicalInsuranceXml([FromUri] MedicalInsuranceXmlUiParam param)
        {
            return new ApiJsonResultData(ModelState, new UiInIParam()).RunWithTry(y =>
            {
                var userBase = webServiceBasicService.GetUserBaseInfo(param.UserId);
                //更新医保信息
                var strXmlIntoParam = XmlSerializeHelper.XmlParticipationParam();
                //回参构建
                var xmlData = new HospitalizationRegisterXml()  
                {
                    MedicalInsuranceType = "10",
                    MedicalInsuranceHospitalizationNo = "44116476",
                };

                var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
                var saveXmlData = new SaveXmlData();
                saveXmlData.OrganizationCode = userBase.OrganizationCode;
                saveXmlData.AuthCode = userBase.AuthCode;
                saveXmlData.BusinessId = param.BusinessId;
                saveXmlData.TransactionId = Guid.Parse("E67C69F5-5FA8-438A-94EC-85E092CA56E9").ToString("N");
                saveXmlData.MedicalInsuranceBackNum = "CXJB002";
                saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
                saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
                saveXmlData.MedicalInsuranceCode = "21";
                saveXmlData.UserId = userBase.UserId;
                //存基层
                webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData));
            });

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData MedicalInsuranceXmlCancel([FromUri] MedicalInsuranceXmlUiParam param)
        {
            return new ApiJsonResultData(ModelState, new UiInIParam()).RunWithTry(y =>
            {
                var userBase = webServiceBasicService.GetUserBaseInfo(param.UserId);
                //更新医保信息
                var strXmlIntoParam = XmlSerializeHelper.XmlParticipationParam();
                //回参构建
                var xmlData = new HospitalizationRegisterCancelXml()
                {
                   
                    MedicalInsuranceHospitalizationNo = "44116476",

                };

                var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
                var saveXmlData = new SaveXmlData();
                saveXmlData.OrganizationCode = userBase.OrganizationCode;
                saveXmlData.AuthCode = userBase.AuthCode;
                saveXmlData.BusinessId = param.BusinessId;
                saveXmlData.TransactionId = Guid.Parse("EA144C5D-1146-4229-87FB-7D9EEA0B3F78").ToString("N");
                saveXmlData.MedicalInsuranceBackNum = "CXJB003";
                saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
                saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
                saveXmlData.MedicalInsuranceCode = "22";
                saveXmlData.UserId = userBase.UserId;
                //存基层
                webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData));
            });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData MedicalInsuranceXmlCancelSettlement([FromUri] MedicalInsuranceXmlUiParam param)
        {
            return new ApiJsonResultData(ModelState, new UiInIParam()).RunWithTry(y =>
            {
                var userBase = webServiceBasicService.GetUserBaseInfo(param.UserId);
                //更新医保信息
                var strXmlIntoParam = XmlSerializeHelper.XmlParticipationParam();
                //回参构建
                var xmlData = new HospitalSettlementCancelXml()
                {


                    SettlementNo = param.SettlementNo
                };

                var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
                var saveXmlData = new SaveXmlData();
                saveXmlData.OrganizationCode = userBase.OrganizationCode;
                saveXmlData.AuthCode = userBase.AuthCode;
                saveXmlData.BusinessId = param.BusinessId;
                saveXmlData.TransactionId =param.TransKey;
                saveXmlData.MedicalInsuranceBackNum = "CXJB003";
                saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
                saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
                saveXmlData.MedicalInsuranceCode = "42";
                saveXmlData.UserId = userBase.UserId;
                //存基层
                webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData));
            });

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData MedicalInsuranceXmlUpload([FromUri] MedicalInsuranceXmlUiParam param)
        {
            return new ApiJsonResultData(ModelState, new UiInIParam()).RunWithTry(y =>
            {
                var userBase = webServiceBasicService.GetUserBaseInfo(param.UserId);

                var hospitalizationFeeList = hisSqlRepository.InpatientInfoDetailQuery(
                    new InpatientInfoDetailQueryParam() {BusinessId = param.BusinessId});

             
                var rowXml = hospitalizationFeeList.Where(d=>d.UploadMark==0).Select(c => new HospitalizationFeeUploadRowXml()
                {
                    SerialNumber = c.DetailId
                }).ToList();


                var xmlData = new HospitalizationFeeUploadXml()
                {

                    MedicalInsuranceHospitalizationNo = "44116476",
                    RowDataList = rowXml,
                };
                var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
                //
                var transactionId = Guid.Parse("79D71ACA-EDBB-419C-A382-2271922E708D").ToString("N");
                var saveXmlData = new SaveXmlData();
                saveXmlData.OrganizationCode = userBase.OrganizationCode;
                saveXmlData.AuthCode = userBase.AuthCode;
                saveXmlData.BusinessId = param.BusinessId;
                saveXmlData.TransactionId = transactionId;
                saveXmlData.MedicalInsuranceBackNum = "CXJB004";
                saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
                saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
                saveXmlData.MedicalInsuranceCode = "31";
                saveXmlData.UserId = userBase.UserId;
                webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData));
            });

        }
        /// <summary>
        /// 测试xml生成
        /// </summary>
        [HttpGet]
        public void TestXml()
        {
            var data = XmlHelp.DeSerializerModel(new BenDing.Domain.Models.Dto.OutpatientDepartment.QueryOutpatientDepartmentCostDto(), true);
            if (data == null) throw new Exception("门诊费用查询出错");
            var cc = AutoMapper.Mapper.Map<QueryOutpatientDepartmentCostjsonDto>(data);
            //var ddd =new List<InpatientDiagnosisDto>();
            //ddd.Add(new InpatientDiagnosisDto()
            //{
            //     IsMainDiagnosis = true,
            //    DiagnosisCode = "T82.003",
            //    DiagnosisName = "主动脉机械瓣周漏"
            //});
            //ddd.Add(new InpatientDiagnosisDto()
            //{
            //    IsMainDiagnosis = true,
            //    DiagnosisCode = "T82.201",
            //    DiagnosisName = "冠状动脉搭桥术机械性并发症"
            //});
            //ddd.Add(new InpatientDiagnosisDto()
            //{
            //    IsMainDiagnosis = true,
            //    DiagnosisCode = "T82.812",
            //    DiagnosisName = "主动脉机械瓣周漏"
            //});
            //ddd.Add(new InpatientDiagnosisDto()
            //{
            //    IsMainDiagnosis = false,
            //    DiagnosisCode = "T83.304",
            //    DiagnosisName = "子宫内节育器脱落"
            //});
            //ddd.Add(new InpatientDiagnosisDto()
            //{
            //    IsMainDiagnosis = false,
            //    DiagnosisCode = "T84.502",
            //    DiagnosisName = "膝关节假体植入感染"
            //});
            //var ddds=CommonHelp.GetDiagnosis(ddd);




            //var data = new HospitalizationFeeUploadXml();



            //data.MedicalInsuranceHospitalizationNo = "123";

            //var rowDataList = new List<HospitalizationFeeUploadRowXml>();
            //data.RowDataList = rowDataList;
            //rowDataList.Add(new HospitalizationFeeUploadRowXml()
            //{
            //    SerialNumber = "777"
            //});
            //rowDataList.Add(new HospitalizationFeeUploadRowXml()
            //{
            //    SerialNumber = "77888"
            //});
            //string dd= XmlSerializeHelper.HisXmlSerialize(data);

        }

        /// <summary>
        /// 基层预结算测试
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData HisPreSettlement([FromUri] BaseUiBusinessIdDataParam param)
        {
            return new ApiJsonResultData(ModelState, new UiInIParam()).RunWithTry(y =>
            {

                //var dd = new ResidentUserInfoParam {IdentityMark = "1", InformationNumber = "111"};
                //var userBase = webServiceBasicService.GetUserBaseInfo(param.UserId);
                //var strXmlIntoParam = XmlSerializeHelper.XmlSerialize(dd);
                //var strXmlBackParam = XmlSerializeHelper.XmlSerialize(dd);
                //var saveXmlData = new SaveXmlData();
                //saveXmlData.OrganizationCode = userBase.OrganizationCode;
                //saveXmlData.AuthCode = userBase.AuthCode;
                //saveXmlData.BusinessId = param.BusinessId;
                //saveXmlData.TransactionId = param.BusinessId;
                //saveXmlData.MedicalInsuranceBackNum = "CXJB009";
                //saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
                //saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
                //saveXmlData.MedicalInsuranceCode = "43";
                //saveXmlData.UserId = param.UserId;
                //webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData));

            });
        }

        /// <summary>
        /// 基层结算测试
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData Settlement([FromUri]BaseUiBusinessIdDataParam param)
        {
            return new ApiJsonResultData(ModelState).RunWithTry(y =>
            {

                //var dd = new ResidentUserInfoParam { IdentityMark = "1", InformationNumber = "111" };
                //var userBase = webServiceBasicService.GetUserBaseInfo(param.UserId);
                //var strXmlIntoParam = XmlSerializeHelper.XmlSerialize(dd);
                //var strXmlBackParam = XmlSerializeHelper.XmlSerialize(dd);
                //var saveXmlData = new SaveXmlData();
                //saveXmlData.OrganizationCode = userBase.OrganizationCode;
                //saveXmlData.AuthCode = userBase.AuthCode;
                //saveXmlData.BusinessId = param.BusinessId;
                //saveXmlData.TransactionId = param.BusinessId;
                //saveXmlData.MedicalInsuranceBackNum = "CXJB009";
                //saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
                //saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
                //saveXmlData.MedicalInsuranceCode = "41";
                //saveXmlData.UserId = param.UserId;
               // webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData));
                
               

            });
        }
        
    }
}
