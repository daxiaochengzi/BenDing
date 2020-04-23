using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BenDing.Domain.Models.Dto.DifferentPlaces;
using BenDing.Domain.Models.Dto.JsonEntity;
using BenDing.Domain.Models.Dto.JsonEntity.DifferentPlaces;
using BenDing.Domain.Models.Dto.OutpatientDepartment;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Dto.Workers;
using BenDing.Domain.Models.Params.Resident;
using BenDing.Domain.Models.Params.UI;
using BenDing.Domain.Models.Params.Web;
using NFine.Domain._03_Entity.BenDingManage;

namespace NFine.Web.App_Start
{/// <summary>
/// 
/// </summary>
    public class AutoMapperConfig
    {/// <summary>
    /// 实体映射
    /// </summary>
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
               cfg.CreateMap<HospitalizationPresettlementJsonDto, HospitalizationPresettlementDto>();
               cfg.CreateMap<OutpatientInfoJsonDto, BaseOutpatientInfoDto>();
               cfg.CreateMap<InpatientInfoDto, InpatientEntity>();
               cfg.CreateMap<InpatientDetailJsonDto, InpatientInfoDetailDto>();
               cfg.CreateMap<QueryOutpatientDepartmentCostDto, QueryOutpatientDepartmentCostjsonDto>();
               //cfg.CreateMap<BaseOutpatientDetailJsonDto, BaseOutpatientDetailDto>();
               cfg.CreateMap<OutpatientPersonBaseJsonDto, BaseOutpatientInfoDto>();
               cfg.CreateMap<PatientLeaveHospitalInfoDto, LeaveHospitalSettlementInfoParam>();
               cfg.CreateMap<PatientLeaveHospitalInfoDto, SaveInpatientSettlementParam>();
               cfg.CreateMap<InpatientInfoDto, HisHospitalizationPreSettlementDto>();
               cfg.CreateMap<BaseOutpatientInfoDto, QueryOutpatientDepartmentCostDataDto>();
               cfg.CreateMap<QueryInpatientInfoDto, HisHospitalizationSettlementCancelDto>();
              
               cfg.CreateMap<WorkerBirthPreSettlementJsonDto, WorkerHospitalizationPreSettlementDto>();
                //ui
                cfg.CreateMap<ResidentHospitalizationRegisterUiParam, WorKerHospitalizationRegisterUiParam>();
                cfg.CreateMap<BirthHospitalizationRegisterUiParam, ResidentHospitalizationRegisterUiParam>();
                
                //门诊
                cfg.CreateMap<OutpatientDetailJsonDto, BaseOutpatientDetailDto>();
                cfg.CreateMap<OutpatientDepartmentCostInputJsonDto, OutpatientDepartmentCostInputDto>();
                cfg.CreateMap<WorkerBirthPreSettlementJsonDto, WorkerBirthSettlementDto>();
              
                //异地
                cfg.CreateMap<DifferentPlacesUserInfoJsonDto, DifferentPlacesUserInfoDto>();
               cfg.CreateMap<DifferentPlacesHospitalizationRegisterJsonDto, DifferentPlacesHospitalizationRegisterDto>();
                
            });
        }
    }
}