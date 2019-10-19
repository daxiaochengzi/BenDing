using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using BenDingWeb.Models.Dto;
using BenDingWeb.Models.Params;
using BenDingWeb.Service.Interfaces;
using MedicalInsurance.Domain.Models.Enums;
using Newtonsoft.Json;
using NFine.Web.Model;

namespace NFine.Web.Controllers
{
   /// <summary>
   /// 
   /// </summary>
    public class BenDingController : AsyncController
    {
        private readonly IGrammarNewService _GrammarNewService;
        private IWebServiceBasicService _webServiceBasicService;
        private IDataBaseSqlServerService _dataBaseSqlServerService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iGrammarNewService"></param>
        /// <param name="iWebServiceBasicService"></param>
        /// <param name="iDataBaseSqlServerService"></param>
        public BenDingController(IGrammarNewService iGrammarNewService, 
            IWebServiceBasicService iWebServiceBasicService,
            IDataBaseSqlServerService iDataBaseSqlServerService)
        {
            _GrammarNewService = iGrammarNewService;
            _webServiceBasicService = iWebServiceBasicService;
            _dataBaseSqlServerService = iDataBaseSqlServerService;
        }
        // [HttpGet]
        // public string Data( string dd)
        //{

        //    return _webServiceBasicService.TestIf().ToString();

        //    //return Ok();

        //}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
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
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        public ActionResult Test()
        {
            return View();
        }
        /// <summary>
        /// 获取三大目录
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> GetCatalog( UiCatalogParam param)
        {
            var resultData = await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
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
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> DeleteCatalog( UiCatalogParam param)
        {
            return Json(await new ApiJsonResultData().RunWithTryAsync(async y =>
            {
                var userBase = await GetUserBaseInfo();
                var data = await _webServiceBasicService.DeleteCatalog(userBase, Convert.ToInt16(param.CatalogType));
                y.Data = data;


            }));
        }
        /// <summary>
        /// 获取ICD10
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> GetIcd10()
        {
            return Json(await new ApiJsonResultData().RunWithTryAsync(async y =>
            {
                var userBase = await GetUserBaseInfo();
                var data = await _webServiceBasicService.GetICD10(userBase, new CatalogParam()
                {
                    机构编码 = userBase.机构编码,
                    验证码 = userBase.验证码,
                    条数 = 500,
                    CatalogType = CatalogTypeEnum.中药
                });
                y.Data = data;


            }));
        }
        /// <summary>
        /// 更新机构
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> GetOrg(OrgParam param)
        {
            return Json(await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {
                var userBase = await GetUserBaseInfo();
                var data = await _webServiceBasicService.GetOrg(userBase, param.Name);
                y.Data = "更新机构" + data + "条";
            }), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取HIS系统中科室、医师、病区、床位的基本信息
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> GetInformation()
        {
            return Json(await new ApiJsonResultData().RunWithTryAsync(async y =>
            {
                var userBase = await GetUserBaseInfo();
                if (userBase != null)
                {
                    var inputInpatientInfo = new InformationParam()
                    {
                        验证码 = userBase.验证码,
                        机构编码 = userBase.机构编码,
                        目录类型 = "1"
                    };
                    //string inputInpatientInfoJson = JsonConvert.SerializeObject(inputInpatientInfo, Formatting.Indented);

                    var inputInpatientInfoData = await _webServiceBasicService.GetInformation(userBase, inputInpatientInfo);
                    if (inputInpatientInfoData.Any())
                    {
                        y.Data = inputInpatientInfoData;
                    }
                }

                //var data = await webService.ExecuteSp(param.Params);

            }), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 住院病人信息查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> QueryMedicalInsuranceResidentInfo([FromBody] QueryMedicalInsuranceResidentInfoParam param)
        {
            return Json( await new ApiJsonResultData(ModelState).RunWithTryAsync(async y =>
            {
                var data = await _dataBaseSqlServerService.QueryMedicalInsuranceResidentInfo(param);
                y.Data = data;

            }));
        }
        /// <summary>
        /// 获取住院病人
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> GetInpatientInfo()
        {
            return Json( await new ApiJsonResultData().RunWithTryAsync(async y =>
            {
                var verificationCode = await GetUserBaseInfo();
                if (verificationCode != null)
                {
                    var inputInpatientInfo = new InpatientInfoParam()
                    {
                        验证码 = verificationCode.验证码,
                        机构编码 = verificationCode.机构编码,
                        身份证号码 = "512501195802085180",
                        开始时间 = "2018-04-27 11:09:00",
                        结束时间 = "2020-04-27 11:09:00",
                        状态 = "0"
                    };
                    string inputInpatientInfoJson =
                        JsonConvert.SerializeObject(inputInpatientInfo, Formatting.Indented);

                    var inputInpatientInfoData = await _webServiceBasicService
                        .GetInpatientInfo(verificationCode, inputInpatientInfoJson);
                    if (inputInpatientInfoData.Any())
                    {
                        y.Data = inputInpatientInfoData;
                    }
                }



            }), JsonRequestBehavior.AllowGet);
        }
        [System.Web.Mvc.NonAction]
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
