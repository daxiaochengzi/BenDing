using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Enums;
using BenDing.Domain.Models.Params.Base;
using BenDing.Domain.Models.Params.UI;
using BenDing.Service.Interfaces;
using NFine.Application.BenDingManage;
using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Params;

namespace NFine.Web.Areas.SystemManage.Controllers
{/// <summary>
/// 
/// </summary>
    public class DoorDiagnosisMonthlySettlementController : ControllerBase
    {
        private UserApp userApp = new UserApp();
        private MonthlyHospitalizationApp monthlyApp = new MonthlyHospitalizationApp();
        private readonly IOutpatientDepartmentService _outpatientService;
      
        /// <summary>
        /// 
        /// </summary>
        public DoorDiagnosisMonthlySettlementController()
        {
            _outpatientService = Bootstrapper.UnityIOC.Resolve<IOutpatientDepartmentService>();
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(DoorDiagnosisMonthlyParam pagination)
        {
            var param = new Pagination()
            {
                page = pagination.page,
                records = pagination.records,
                rows = pagination.rows,
                sidx = pagination.sidx,
                sord = pagination.sord
            };
            var data = new
            {
                rows = monthlyApp.GetList(param, pagination),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(SaveDoorDiagnosisMonthlyParam doorDiagnosis)
        { 
            if (string.IsNullOrWhiteSpace(doorDiagnosis.SettlementStartTime))
                throw  new  Exception("结算开始不能为空!!!");
            if (string.IsNullOrWhiteSpace(doorDiagnosis.SettlementEndTime))
                throw new Exception("结算结束不能为空!!!");
            var loginInfo = OperatorProvider.Provider.GetCurrent();
            var user = userApp.GetForm(loginInfo.UserId);
            _outpatientService.MonthlyHospitalization(new MonthlyHospitalizationUiParam()
            {
                UserId = user.F_HisUserId,
                PeopleType = (PeopleType)Convert.ToInt16(doorDiagnosis.PeopleType),
                EndTime = doorDiagnosis.SettlementEndTime,
                StartTime = doorDiagnosis.SettlementStartTime,
            });
           
            return Success("操作成功。");
        }
        //门诊月结取消
        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {

            var monthly= monthlyApp.GetForm(keyValue);
            var loginInfo = OperatorProvider.Provider.GetCurrent();
            var user = userApp.GetForm(loginInfo.UserId);
            _outpatientService.CancelMonthlyHospitalization(new CancelMonthlyHospitalizationUiParam()
            {
                UserId = user.F_HisUserId,
                DocumentNo = monthly.DocumentNo,
                PeopleType = monthly.PeopleType,
                SummaryType = monthly.SummaryType
            });
            monthly.IsRevoke = true;
            //更新月结状态
            monthlyApp.SubmitForm(monthly, keyValue, new UserInfoDto()
            {
                UserId = user.F_HisUserId,
            });
            return Success("操作成功。");
        }
    }
}