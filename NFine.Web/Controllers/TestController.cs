﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using BenDing.Domain.Models.Dto;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.HisXml;
using BenDing.Domain.Models.Params.Base;
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
        /// get测试
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData PageList([FromUri] UserInfo pagination)
        {
            return new ApiJsonResultData(ModelState, new UiInIParam()).RunWithTry(y =>
            {
                y.DataDescribe = CommonHelp.GetPropertyAliasDict(new UserInfoDto());
                y.Data = userService.GetUserInfo();

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
                y.DataDescribe = CommonHelp.GetPropertyAliasDict(new UserInfoDto());
                y.Data = userService.GetUserInfo();

            });

        }
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
        [HttpGet]
        public ApiJsonResultData MedicalInsuranceXmlUpload([FromUri] MedicalInsuranceXmlUiParam param)
        {
            return new ApiJsonResultData(ModelState, new UiInIParam()).RunWithTry(y =>
            {
                var userBase = webServiceBasicService.GetUserBaseInfo(param.UserId);
                var rowXml = new List<HospitalizationFeeUploadRowXml>();
                rowXml.Add(new HospitalizationFeeUploadRowXml(){SerialNumber = "ABFB478F5E6B4B7A8500D20C83BBBC15" });
                rowXml.Add(new HospitalizationFeeUploadRowXml() { SerialNumber = "AA5D0D462F2743F993F7DF3CCE9B1D5B" });
                rowXml.Add(new HospitalizationFeeUploadRowXml() { SerialNumber = "060746259DE44F41B1291FB7278F246B" });
                rowXml.Add(new HospitalizationFeeUploadRowXml() { SerialNumber = "FAFE1927497844728543AE77E284AD6C" });
                rowXml.Add(new HospitalizationFeeUploadRowXml() { SerialNumber = "E02B5F44D0F04415BF8C844762F848C8" });
                rowXml.Add(new HospitalizationFeeUploadRowXml() { SerialNumber = "54BC930971BA4073A9D3F8D7FA8F9EB5" });
                rowXml.Add(new HospitalizationFeeUploadRowXml() { SerialNumber = "5F79C4B103A64130812C7A0D5F7C0E50" });
                var xmlData = new HospitalizationFeeUploadXml()
                {

                    MedicalInsuranceHospitalizationNo = "44116476",
                    RowDataList = rowXml,
                };
                var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
                //
                var transactionId = Guid.Parse("8EB3E8F1-26C9-4A23-8441-4DEB61131026").ToString("N");
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
            var data = new HospitalizationFeeUploadXml();


            
            data.MedicalInsuranceHospitalizationNo = "123";
          
            var rowDataList = new List<HospitalizationFeeUploadRowXml>();
            data.RowDataList = rowDataList;
            rowDataList.Add(new HospitalizationFeeUploadRowXml()
            {
                SerialNumber = "777"
            });
            rowDataList.Add(new HospitalizationFeeUploadRowXml()
            {
                SerialNumber = "77888"
            });
            string dd= XmlSerializeHelper.HisXmlSerialize(data);

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

                var dd = new ResidentUserInfoParam {IdentityMark = "1", InformationNumber = "111"};
                var userBase = webServiceBasicService.GetUserBaseInfo(param.UserId);
                var strXmlIntoParam = XmlSerializeHelper.XmlSerialize(dd);
                var strXmlBackParam = XmlSerializeHelper.XmlSerialize(dd);
                var saveXmlData = new SaveXmlData();
                saveXmlData.OrganizationCode = userBase.OrganizationCode;
                saveXmlData.AuthCode = userBase.AuthCode;
                saveXmlData.BusinessId = param.BusinessId;
                saveXmlData.TransactionId = param.BusinessId;
                saveXmlData.MedicalInsuranceBackNum = "CXJB009";
                saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
                saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
                saveXmlData.MedicalInsuranceCode = "43";
                saveXmlData.UserId = param.UserId;
                webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData));

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

                var dd = new ResidentUserInfoParam { IdentityMark = "1", InformationNumber = "111" };
                var userBase = webServiceBasicService.GetUserBaseInfo(param.UserId);
                var strXmlIntoParam = XmlSerializeHelper.XmlSerialize(dd);
                var strXmlBackParam = XmlSerializeHelper.XmlSerialize(dd);
                var saveXmlData = new SaveXmlData();
                saveXmlData.OrganizationCode = userBase.OrganizationCode;
                saveXmlData.AuthCode = userBase.AuthCode;
                saveXmlData.BusinessId = param.BusinessId;
                saveXmlData.TransactionId = param.BusinessId;
                saveXmlData.MedicalInsuranceBackNum = "CXJB009";
                saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
                saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
                saveXmlData.MedicalInsuranceCode = "41";
                saveXmlData.UserId = param.UserId;
                webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData));
                
               

            });
        }
        
    }
}
