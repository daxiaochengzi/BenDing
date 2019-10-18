using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NFine.Web.Controllers
{ 
    public class InsuranceController : AsyncController
    {
        //
        // GET: /Insurance/

        public ActionResult Card()
        {
            return View();
        }
     
    }
}
