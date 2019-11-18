using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using BenDing.Domain.Models.Dto.Base;
using BenDing.Domain.Models.Params.UI;
using BenDing.Service.Interfaces;
using NFine.Code;

namespace NFine.Web.Controllers
{
    public class TestController : AsyncController
    {
       
        private IWebServiceBasicService _webServiceBasic;
        public TestController(IWebServiceBasicService webServiceBasic)
        {
            _webServiceBasic = webServiceBasic;
        }
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public void  PageList(PaginationDto pagination)
        {
            string msg = "888";
           var log=  LogFactory.GetLogger(msg.GetType().ToString());
            log.Error(msg);
            //return new ApiJsonResultData().RunWithTry(y =>
            //{
            //    //var tokenHeader = HttpContext.Request.Headers["Authorization"];
            //    //tokenHeader = tokenHeader.ToString().Substring("Bearer ".Length).Trim();
            //    //TokenModel tm = JwtHelperb.SerializeJWT(tokenHeader);
            //    //y.Data = tm;





            //});

            //var data =aw  _webServiceBasic.QueryMedicalInsuranceDetail(new QueryMedicalInsuranceUiParam()
            //{
            //    UserId = "E075AC49FCE443778F897CF839F3B924",
            //    BusinessId = "E8FB1C8604DB41CFBD0DF10E85608214"
            //}).Result;
            //return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}
