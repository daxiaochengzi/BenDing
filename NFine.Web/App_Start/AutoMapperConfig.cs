using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BenDing.Domain.Models.Dto.JsonEntity;
using BenDing.Domain.Models.Dto.OutpatientDepartment;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Resident;
using NFine.Domain._03_Entity.BenDingManage;

namespace NFine.Web.App_Start
{
    public class AutoMapperConfig
    {
        public static void Config()
        {
            Mapper.Initialize(cfg =>
            {
                // cfg.CreateMap<a, b>();
                //var c = AutoMapper.Mapper.Map<b>(a);
               cfg.CreateMap<InformationJsonDto, InformationDto>();
               cfg.CreateMap<UserInfoJsonDto, UserInfoDto>();
               cfg.CreateMap<InpatientInfoJsonDataDto, InpatientInfoDto>();
               cfg.CreateMap<ResidentUserInfoJsonDto, ResidentUserInfoDto>();
               cfg.CreateMap<QueryInpatientInfoJsonDto, QueryInpatientInfoDto>();
               //cfg.CreateMap<InpatientInfoDetailJsonDto, InpatientInfoDetailDto>();
               cfg.CreateMap<HospitalizationPresettlementJsonDto, HospitalizationPresettlementDto>();
               cfg.CreateMap<OutpatientInfoJsonDto, BaseOutpatientInfoDto>();
               cfg.CreateMap<InpatientInfoDto, InpatientEntity>();
               cfg.CreateMap<InpatientDetailJsonDto, InpatientInfoDetailDto>();
               cfg.CreateMap<QueryOutpatientDepartmentCostDto, QueryOutpatientDepartmentCostjsonDto>();
                cfg.CreateMap<BaseOutpatientDetailJsonDto, BaseOutpatientDetailDto>();
               




            });
        }
    }
}