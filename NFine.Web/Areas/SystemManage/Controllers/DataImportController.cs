using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BenDing.Domain.Models.Enums;
using BenDing.Domain.Models.Params.Web;
using BenDing.Service.Interfaces;
using NFine.Application.SystemManage;
using NFine.Code;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class DataImportController :  ControllerBase
    {
        private UserApp userApp = new UserApp();
        private readonly IWebServiceBasicService _webServiceBasicService;

        public DataImportController()
        {
            _webServiceBasicService = Bootstrapper.UnityIOC.Resolve<IWebServiceBasicService>();
        }

        // GET: SystemManage/DataImport
        public ActionResult ImportExcelIcd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ImportExcel()
        {
            var loginInfo = OperatorProvider.Provider.GetCurrent();
            var user = userApp.GetForm(loginInfo.UserId);
            if (user.F_IsHisAccount == false) throw new Exception("非基层认证人员不能下载icd10");
            string name = Request.Form["sheetName"];
            HttpPostedFileBase file = Request.Files["file"];
            string content = "";
            if (string.IsNullOrWhiteSpace(name))
            {
                content = "<script>alert('表单元名称不能为空!!!')</script>";
            }
            else
            {
                if (file.ContentLength > 0)
                {
                    var isxls = System.IO.Path.GetExtension(file.FileName)?.ToString().ToLower();
                    if (isxls != ".xls" && isxls != ".xlsx")
                    {
                        Content("请上传Excel文件");
                    }
                    var fileName = file.FileName;//获取文件夹名称
                    var path = Server.MapPath("~/FileExcel/" + fileName);
                    file.SaveAs(path);
                    var dataExcel = ExcelHelper.ExcelToDataTable(path, name, true);
                    if (dataExcel.Columns.Count == 0)
                    {
                        content = "<script>alert('导入的数据不能为空')</script>";
                    }
                    else
                    {
                        var count = _webServiceBasicService.MedicalInsuranceDownloadIcd10(dataExcel, user.F_HisUserId);
                        content = "<script>alert('+" + count + "数据上传成功!!!')</script>";
                    }


                }
            }

            return Content(content);

        }

        [HttpPost]
       
        public ActionResult BaseDownload(string keyValue)
        {
            var loginInfo = OperatorProvider.Provider.GetCurrent();
            var user = userApp.GetForm(loginInfo.UserId);
            if (user.F_IsHisAccount==false) throw new Exception("非基层认证人员不能下载icd10");
            var userBase = _webServiceBasicService.GetUserBaseInfo(user.F_HisUserId);
            var data = _webServiceBasicService.GetIcd10(userBase, new CatalogParam()
            {
                OrganizationCode = userBase.OrganizationCode,
                AuthCode = userBase.AuthCode,
                Nums = 1000,
                CatalogType = (CatalogTypeEnum)Convert.ToInt16(keyValue)
            });
            return Success("下载成功");
        }
       
    }
}