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
       

        /// <summary>
        /// 读卡
        /// </summary>
        /// <returns></returns>
        public ActionResult Card()
        {
            return View();
        }


        /**
         * http://47.75.154.115:19519/ybsp/his/insurance/admissionRegistration?
         * YbOrgCode=99999&
         * OrgID=51072600000000000000000513435964&
         * EmpID=E075AC49FCE443778F897CF839F3B924&
         * BID=FFE6ADE4D0B746C58B972C7824B8C9DF&
         * BsCode=21&
         * IsCw=0&
         * apiurl=&
         * TransKey=FFE6ADE4D0B746C58B972C7824B8C9DF
         */

        /// <summary>
        /// 入院登记
        /// </summary>
        /// <param name="bid"></param>
        /// <param name="empid"></param>
        /// <returns></returns>
        public ActionResult AdmissionRegistration(string bid,string empid)
        {
            ViewBag.bid = bid;
            ViewBag.empid = empid;
            return View();
        }

        /// <summary>
        /// 修改医保登记
        /// </summary>
        /// <param name="bid"></param>
        /// <param name="empid"></param>
        /// <returns></returns>
        public ActionResult AdmissionRegistrationUpdate(string bid, string empid)
        {
            ViewBag.bid = bid;
            ViewBag.empid = empid;
            return View();
        }

        /// <summary>
        /// 医保目录对码
        /// </summary>
        /// <returns></returns>
        public ActionResult MedicalDirectoryCode()
        {
            return View();
        }


    }
}
