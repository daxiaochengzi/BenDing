using System;
using System.Linq;
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
            var resultData= await new ApiJsonResultData().RunWithTryAsync(async y =>
            {
                var userBase = await GetUserBaseInfo();
                y.Data = userBase;
                
            });
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
        //[HttpGet]
        //public ApiJsonResultData GetUserBaseInfoss(string dd)
        //{
        //    return new ApiJsonResultData().RunWithTry(y =>
        //    {

        //        y.Data = dd;

        //    });
        //}
        public ActionResult Test()
        {
            return View();
        }
        /// <summary>
        /// 获取三大目录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetCatalog( UiCatalogParam param)
        {
            var resultData = await new ApiJsonResultData().RunWithTryAsync(async y =>
            {
                var userBase = await GetUserBaseInfo();
                if (userBase != null)
                {
                    var inputInpatientInfo = new CatalogParam()
                    {
                        验证码 = userBase.验证码,
                        CatalogType = param.CatalogType,
                        机构编码 = userBase.机构编码,
                        条数 = 500,
                    };


                    var inputInpatientInfoData = await _webServiceBasicService.GetCatalog(userBase, inputInpatientInfo);
                    if (inputInpatientInfoData.Any())
                    {
                        y.Data = inputInpatientInfoData;
                    }
                }

                //var data = await webService.ExecuteSp(param.Params);

            });
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 删除三大目录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> DeleteCatalog( UiCatalogParam param)
        {
            return Json(await new ApiJsonResultData().RunWithTryAsync(async y =>
            {
                var userBase = await GetUserBaseInfo();
                var data = await _webServiceBasicService.DeleteCatalog(userBase, Convert.ToInt16(param.CatalogType));
                y.Data = data;


            }));
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
