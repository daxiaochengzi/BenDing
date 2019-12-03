using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BenDing.Domain.Models.Dto;
using BenDing.Domain.Models.Dto.Web;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_userService"></param>
        public TestController(IUserService _userService,
            IWebServiceBasicService _webServiceBasicService,
            IWebBasicRepository iWebBasicRepository)
        {
            userService = _userService;
            webServiceBasicService = _webServiceBasicService;
            webServiceBasic = iWebBasicRepository;
        }

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
                webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData), userBase.UserId);

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
                webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData), userBase.UserId);
                
               

            });
        }
        
    }
}
