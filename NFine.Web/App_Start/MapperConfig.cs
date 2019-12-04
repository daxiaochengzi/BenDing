using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BenDing.Domain.Models.Dto.JsonEntity;
using BenDing.Domain.Models.Dto.Web;

namespace NFine.Web.App_Start
{
    public static class MapperConfig
    {
        public static void Config()
        {
            Mapper.Initialize(cfg =>
            {
                //cfg.CreateMap<a, b>();
                //var c = AutoMapper.Mapper.Map<b>(a);
                cfg.CreateMap<UserInfoJsonDto, UserInfoDto>();
                cfg.CreateMap<InformationJsonDto, InformationDto>();
                


            });
        }
    }
}