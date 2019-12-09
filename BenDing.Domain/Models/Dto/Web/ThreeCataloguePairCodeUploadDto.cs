using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Web
{
   public class ThreeCataloguePairCodeUploadDto
    {
        [JsonProperty(PropertyName = "版本")]
        public string VersionNumber { get; set; }="";
        /// <summary>
        /// 验证码
        /// </summary>
        [JsonProperty(PropertyName = "验证码")]
        public string AuthCode { get; set; }
        /// <summary>
        /// 操作人员姓名
        /// </summary>
        [JsonProperty(PropertyName = "操作人员姓名")]
        public string UserName { get; set; }
        /// <summary>
        /// 撤销状态   
        /// </summary>
        [JsonProperty(PropertyName = "撤销状态")]
        public string CanCelState { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        [JsonProperty(PropertyName = "机构编码")]
        public string OrganizationCode { get; set; }
        /// <summary>
        /// 对码详细信息
        /// </summary>
        [JsonProperty(PropertyName = "对码详细信息")]
        public List<ThreeCataloguePairCodeUploadRowDto> PairCodeRow { get; set; } 


    }

    public class ThreeCataloguePairCodeUploadRowDto
    {/// <summary>
     /// 基卫目录编码
     /// </summary>
        [JsonProperty(PropertyName = "基卫目录编码")]
        public string HisDirectoryCode { get; set; }
        [JsonProperty(PropertyName = "社保目录类别")]
        public  string ProjectCodeType { get; set; }
        /// <summary>
        /// 社保目录细类
        /// </summary>
        [JsonProperty(PropertyName = "社保目录细类")]
        public string ProjectCodeTypeDetail { get; set; }

        [JsonProperty(PropertyName = "社保目录ID")]
        public string ProjectId { get; set; } = "";
        [JsonProperty(PropertyName = "社保目录编码")]
        public string ProjectCode { get; set; }
        [JsonProperty(PropertyName = "社保目录名称")]
        public string ProjectName { get; set; }
        [JsonProperty(PropertyName = "拼音")]
        public string MnemonicCode { get; set; }="";

        /// <summary>
        /// 剂型
        /// </summary>
        [JsonProperty(PropertyName = "剂型")]
        public string Formulation { get; set; } = "";

        /// <summary>
        /// 剂型编码
        /// </summary>
        [JsonProperty(PropertyName = "剂型编码")]
        public string FormulationCode { get; set; } = "";

        /// <summary>
        /// 规格
        /// </summary>
        [JsonProperty(PropertyName = "规格")]
        public string Specification { get; set; } = "";

        /// <summary>
        /// 规格编码
        /// </summary>
        [JsonProperty(PropertyName = "规格编码")]
        public string SpecificationCode { get; set; } = "";

        /// <summary>
        /// 单位
        /// </summary>
        [JsonProperty(PropertyName = "单位")]
        public string Unit { get; set; } = "";
        /// <summary>
        /// 进价
        /// </summary>
        [JsonProperty(PropertyName = "进价")]
        public  int PurchasePrice { get; set; }
        /// <summary>
        /// 售价
        /// </summary>
        [JsonProperty(PropertyName = "售价")]
        public int SellPrice { get; set; }
        /// <summary>
        /// 生产厂家
        /// </summary>
        [JsonProperty(PropertyName = "生产厂家")]
        public  string Manufacturer { get; set; }
        /// <summary>
        /// 收费级别
        /// </summary>
        [JsonProperty(PropertyName = "收费级别")]
        public string ProjectLevel { get; set; }
        /// <summary>
        /// 限制用药
        /// </summary>
        [JsonProperty(PropertyName = "限制用药")]
        public string RestrictionSign { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty(PropertyName = "备注")]
        public string Remark { get; set; }
    }

    public class QueryThreeCataloguePairCodeUploadDto
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 项目类型
        /// </summary>
        public string ProjectCodeType { get; set; }
        /// <summary>
        /// 项目等级
        /// </summary>
        public string ProjectLevel { get; set; }
        /// <summary>
        /// 限制标志
        /// </summary>

        public  string RestrictionSign { get; set; }
       /// <summary>
       /// 备注
       /// </summary>
        public  string Remark { get; set; }
        /// <summary>
        /// 目录类型
        /// </summary>
     
        public  string DirectoryCategoryCode { get; set; }
        /// <summary>
        /// 目录编码
        /// </summary>
        public string DirectoryCode { get; set; }
       
    }
}
