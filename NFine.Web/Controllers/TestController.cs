using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BenDing.Domain.Models.Dto.Web;
using NFine.Web.Model;


namespace NFine.Web.Controllers
{
    public class TestController : ApiController
    {

        /// <summary>
        /// 
        /// </summary>
        private readonly IUserService userService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_userService"></param>
        public TestController(IUserService _userService)
        {
            userService = _userService;
        }

        [HttpGet]
        public ApiJsonResultData PageList([FromUri]UserInfo pagination)
        {
            return new ApiJsonResultData(ModelState, new UserInfoDto()).RunWithTry(y =>
            {
                
                y.Data= userService.GetUserInfo();
                
            });
               
        }
    }
}
