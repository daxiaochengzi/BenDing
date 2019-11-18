using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BenDing.Domain.Models.Dto.Base;

namespace NFine.Web.Controllers
{
    public class TestController : ApiController
    {
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiJsonResultData PageList(PaginationDto pagination)
        {
            return new ApiJsonResultData().RunWithTry(y =>
            {
                //var tokenHeader = HttpContext.Request.Headers["Authorization"];
                //tokenHeader = tokenHeader.ToString().Substring("Bearer ".Length).Trim();
                //TokenModel tm = JwtHelperb.SerializeJWT(tokenHeader);
                //y.Data = tm;
                throw  new  Exception("123123");
                var ddd = new PaginationDto()
                {
                    Limit = 2,
                    Page = 10
                };
                y.Data = ddd;

            });
        }

    }
}
