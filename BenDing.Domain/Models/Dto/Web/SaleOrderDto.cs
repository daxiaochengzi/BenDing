using System;
using System.Collections.Generic;
using System.Text;

namespace BenDing.Domain.Models.Dto.Web
{
   public class SaleOrderDto
    {
        public int Id { get; set; }

        /// <summary>
    /// 订单号
    /// </summary>
        public int OrderNo { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductNo { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustName { get; set; }
        public  string CustAddress { get; set; }
        public string CustPhone { get; set; }
        public string CustCompany { get; set; }
        public string CreateDateTime { get; set; }
        public string UpdateDateTime { get; set; }
    }
}
