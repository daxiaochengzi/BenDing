/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/

using System;
using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BenDing.Service.Interfaces;
using BenDing.Domain.Models.Params.Web;
using Newtonsoft.Json;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class UserController : ControllerBase
    {
        private UserApp userApp = new UserApp();
        private UserLogOnApp userLogOnApp = new UserLogOnApp();
        private OrganizeApp organizeApp = new OrganizeApp();
        private readonly IWebServiceBasicService _basicService;
        public UserController()
        {
            _basicService = Bootstrapper.UnityIOC.Resolve<IWebServiceBasicService>();
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = userApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = userApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(UserEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {   //组织机构编号
            string organizationCode = null;
            if (userEntity.F_IsHisAccount == null) userEntity.F_IsHisAccount = false;
            //是否机构账户
            if (userEntity.F_IsHisAccount == true)
            {
                var inputParam = new UserInfoParam()
                {
                    UserName = userEntity.F_Account,
                    Pwd = userLogOnEntity.F_UserPassword,
                    ManufacturerNumber = userEntity.F_ManufacturerNumber,
                };
                    string inputParamJson = JsonConvert.SerializeObject(inputParam, Formatting.Indented);
                    var verificationCode = _basicService.GetVerificationCode("01", inputParamJson);
                    //获取userid
                    if (verificationCode == null) throw  new  Exception("基层用户登陆失败!!!");
                        userEntity.F_HisUserId = verificationCode.UserId;
                        organizationCode = verificationCode.OrganizationCode;
            }
            userApp.SubmitForm(userEntity, userLogOnEntity, keyValue);
            //是否机构账户
            if (userEntity.F_IsHisAccount == true)
            { //更新机构编号
                organizeApp.UpDateEnCode(userEntity.F_DepartmentId, organizationCode);
            }
            return Success("操作成功。");
           
    }
        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            userApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
        [HttpGet]
        public ActionResult RevisePassword()
        {
            return View();
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitRevisePassword(string userPassword, string keyValue)
        {
           var userLog= userLogOnApp.GetForm(keyValue);
            userLogOnApp.RevisePassword(userPassword, keyValue);
            //更新账户密码
            userApp.UpDateHisUserPwd(userLog.F_UserId, userPassword);
            return Success("重置密码成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DisabledAccount(string keyValue)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.F_Id = keyValue;
            userEntity.F_EnabledMark = false;
            userApp.UpdateForm(userEntity);
            return Success("账户禁用成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult EnabledAccount(string keyValue)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.F_Id = keyValue;
            userEntity.F_EnabledMark = true;
            userApp.UpdateForm(userEntity);
            return Success("账户启用成功。");
        }

        [HttpGet]
        public ActionResult Info()
        {
            return View();
        }
    }
}
