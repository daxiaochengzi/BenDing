using System.Web.Mvc;
using BenDing.Domain.Models.Params.UI;

namespace NFine.Web.Controllers
{
    /// <summary>
    /// 基层HIS医保嵌入页面
    /// </summary>
    public class InsuranceController : Controller
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
        public ActionResult AdmissionRegistration(string bid, string empid)
        {
            ViewBag.bid = bid;
            ViewBag.empid = empid;
            ViewBag.title = "医保入院登记";
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
            ViewBag.title = "修改医保登记";
            return View();
        }


        /// <summary>
        /// 医保生育住院登记
        /// </summary>
        /// <param name="bid"></param>
        /// <param name="empid"></param>
        /// <returns></returns>
        public ActionResult AdmissionBirthRegistration(string bid, string empid)
        {
            ViewBag.bid = bid;
            ViewBag.empid = empid;
            ViewBag.title = "医保生育住院登记";
            return View();
        }

        /// <summary>
        /// 医保目录对码
        /// </summary>
        /// <returns></returns>
        public ActionResult MedicalDirectoryCode(string empid)
        {
            ViewBag.empid = empid;
            ViewBag.title = "医保目录对码";
            return View();
        }

        /// <summary>
        /// 住院清单上传,2.2.4.	处方项目传输
        /// </summary>
        /// <param name="bid">业务ID</param>
        /// <param name="empid">用户ID</param>
        /// <returns></returns>
        public ActionResult HospitalizationExpensesManagement(string bid, string empid)
        {
            ViewBag.empid = empid;
            ViewBag.bid = bid;
            ViewBag.title = "住院清单上传";
            return View();
        }

        /// <summary>
        /// 住院清单撤销
        /// </summary>
        /// <param name="bid">用户ID</param>
        /// <param name="empid">业务ID</param>
        /// <returns></returns>
        public ActionResult HospitalizationExpensesManagementReceive(string bid, string empid)
        {
            ViewBag.empid = empid;
            ViewBag.bid = bid;
            ViewBag.title = "住院清单撤销";
            return View();
        }

        ///// <summary>
        ///// 目录对码页面
        ///// </summary>
        ///// <param name="param"></param>
        ///// <returns></returns>
        //public ActionResult MedicalDirectoryPairCode(MedicalDirectoryCodePairUiParam param)
        //{
        //    //参数可查询医保中心目录
        //    ViewBag.DirectoryName = param.ProjectName;
        //    ViewBag.DirectoryCode = param.ProjectCode;
        //    ViewBag.DirectoryCategoryCode = param.ProjectCodeType;
        //    ViewBag.empid = param.UserId;
        //    return View();
        //}


        /// <summary>
        /// 住院预结算
        /// </summary>
        /// <param name="bid">用户ID</param>
        /// <param name="empid">业务ID</param>
        /// <returns></returns>
        public ActionResult HospitalPreSettle(string bid, string empid)
        {
            ViewBag.empid = empid;
            ViewBag.bid = bid;
            ViewBag.title = "住院预结算";
            return View();
        }


        /// <summary>
        /// 住院结算
        /// </summary>
        /// <param name="bid">用户ID</param>
        /// <param name="empid">业务ID</param>
        /// <returns></returns>
        public ActionResult LeaveHospitalSettlement(string bid, string empid)
        {
            ViewBag.empid = empid;
            ViewBag.bid = bid;
            ViewBag.title = "住院结算";
            return View();
        }

        /// <summary>
        /// 住院取消结算
        /// </summary>
        /// <param name="bid">用户ID</param>
        /// <param name="empid">业务ID</param>
        /// <returns></returns>
        public ActionResult LeaveHospitalSettlementCancel(string bid, string empid)
        {
            ViewBag.empid = empid;
            ViewBag.bid = bid;
            ViewBag.title = "住院取消结算";
            return View();
        }


    }
}
