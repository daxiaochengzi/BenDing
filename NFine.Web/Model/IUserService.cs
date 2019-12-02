using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFine.Web.Model
{
    public interface IUserService
    {
        UserInfo GetUserInfo();
    }
}