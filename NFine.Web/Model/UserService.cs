using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFine.Web.Model
{
    public class UserService: IUserService
    {
        public UserInfo GetUserInfo()
        {
            return new UserInfo()
            {
                UserName = "橙子",
                UserPwd = "222",
                CreateTime =Convert.ToDateTime("2019-09-06 11:29:19.000")

            };
        }
    }
}