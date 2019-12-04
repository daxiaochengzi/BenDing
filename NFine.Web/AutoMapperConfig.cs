using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BenDing.Domain.Models.Dto.JsonEntity;
using BenDing.Domain.Models.Dto.Web;

namespace NFine.Web
{
    public static class AutoMapperConfig
    {
        public static void Config()
        {
            Mapper.Initialize(cfg =>
            {
                //cfg.CreateMap<a,b>();
                //  var c = AutoMapper.Mapper.Map<b>(a);
                cfg.CreateMap<BaseOutpatientInfoDto, OutpatientInfoJsonDto>();
                cfg.CreateMap<BaseOutpatientDetailDto, OutpatientDetailJsonDto>();
                cfg.CreateMap<UserInfoDto, UserInfoJsonDto>();
                cfg.CreateMap<InformationDto, InformationJsonDto>();
                


            });
        }
    }
}