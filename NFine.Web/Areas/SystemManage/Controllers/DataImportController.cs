using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFine.Application.SystemManage;
using NFine.Code;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class DataImportController :  ControllerBase
    {
        private UserApp userApp = new UserApp();
        // GET: SystemManage/DataImport
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ImportExcelIcd()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportExcel()
        {
            var loginInfo = OperatorProvider.Provider.GetCurrent();
            var user = userApp.GetForm(loginInfo.UserId);
            string name = Request.Form["sheetName"];
            HttpPostedFileBase File = Request.Files["file"];
            string content = "";
            if (File.ContentLength > 0)
            {
                var Isxls = System.IO.Path.GetExtension(File.FileName).ToString().ToLower();
                if (Isxls != ".xls" && Isxls != ".xlsx")
                {
                    Content("请上传Excel文件");
                }
                var FileName = File.FileName;//获取文件夹名称
                var path = Server.MapPath("~/FileExcel/" + FileName);
                File.SaveAs(path);

              var dataExcel= ExcelHelper.ExcelToDataTable(path, name,true);

                if (dataExcel.Columns.Count>0)
                {
                    content = "<script>alert('导入的数据不能为空'),window.location.href='/Position/Index'</script>";
                }

                content = "<script>alert('上传成功!!!')</script>";
                //将文件保存到服务器
                //PositionBLL bll = new PositionBLL();
                //var list = bll.FileUpLoad(path);
                //if (list.Count > 0)
                //{
                //    int num = bll.LoadFile(list);
                //    if (num > 0)
                //    {
                //        content = "<script>alert('数据导入成功'),window.location.href='/Position/Index'</script>";
                //    }
                //}
                //else
                //{
                //    content = "<script>alert('导入的数据不能为空'),window.location.href='/Position/Index'</script>";
                //}
            }
            //else
            //{
            //    content = "<script>alert('请选择上传的文件'),window.location.href='/Position/Index'</script>";
            //}
            return Content(content);
        }
    }
}