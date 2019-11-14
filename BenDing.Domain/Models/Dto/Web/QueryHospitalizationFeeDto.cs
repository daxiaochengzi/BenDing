using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Web
{
   public class QueryHospitalizationFeeDto
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 上传标记（1上传,0未上传）
        /// </summary>
        public int UploadMark { get; set; }
        /// <summary>
        /// 医保编码
        /// </summary>
        public  string ProjectCode { get; set; }
        /// <summary>
        /// 医保类别
        /// </summary>
        public string ProjectCodeType { get; set; }
        /// <summary>
        /// 项目等级
        /// </summary>
        public string ProjectLevel { get; set; }
        /// <summary>
        /// 自付比例
        /// </summary>
        public string SelfPayProportion { get; set; }
        /// <summary>
        /// 限制价格
        /// </summary>
        public decimal BlockPrice { get; set; }
        /// <summary>
        /// 限制使用说明
        /// </summary>
        public string BlockRemark { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public  string Specification { get; set; }
        
        /// <summary>
        /// His编码
        /// </summary>
        public  string DirectoryCode { get; set; }
        /// <summary>
        /// His名称
        /// </summary>
        public string  DirectoryName { get; set; }
        /// <summary>
        /// his类别
        /// </summary>
        public string DirectoryCategoryCode { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 费用日期
        /// </summary>
        public  string BillTime { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public  string UploadUserName { get; set; }
        /// <summary>
        /// 处方号
        /// </summary>
        public  string RecipeCode { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        public  string BillDepartment { get; set; }
        /// <summary>
        /// 执行医生
        /// </summary>
        public string OperateDoctorName { get; set; }
        /// <summary>
        /// 调整值
        /// </summary>
        public decimal AdjustmentNumber { get; set; }
        public  string OrganizationCode { get; set; }

    }
}
