using System.Threading.Tasks;
using System.Web.Mvc;
using BenDingWeb.Models.Dto;
using BenDingWeb.Models.Params;
using BenDingWeb.Service.Interfaces;
using BenDingWeb.WebServiceBasic;
using Newtonsoft.Json;
using NFine.Web.Model;

namespace NFine.Web.Controllers
{
   
    public class BenDingController : AsyncController
    {
        private IGrammarNewService _GrammarNewService;
        private IWebServiceBasicService _webServiceBasicService;

        public BenDingController(IGrammarNewService iGrammarNewService, 
          
            IWebServiceBasicService iWebServiceBasicService)
        {
            _GrammarNewService = iGrammarNewService;
          
            _webServiceBasicService = iWebServiceBasicService;
        }
        // [HttpGet]
        // public string Data( string dd)
        //{

        //    return _webServiceBasicService.TestIf().ToString();

        //    //return Ok();

        //}

        [HttpGet]
        public async Task<ActionResult> GetUserBaseInfos()
        {
            
             var userBase = await GetUserBaseInfo();
            return Json(userBase, JsonRequestBehavior.AllowGet);


        }
        [HttpGet]
        public ApiJsonResultData GetUserBaseInfoss(string dd)
        {
            return new ApiJsonResultData().RunWithTry(y =>
            {

                y.Data = dd;

            });
        }


        [NonAction]
        private async Task<UserInfoDto> GetUserBaseInfo()
        {
            var inputParam = new UserInfoParam()
            {
                用户名 = "liqian",
                密码 = "123",
                厂商编号 = "510303001",
            };
            string inputParamJson = JsonConvert.SerializeObject(inputParam, Formatting.Indented);
            var verificationCode = await _webServiceBasicService.GetVerificationCode("01", inputParamJson);
            return verificationCode;
        }
    }
}
