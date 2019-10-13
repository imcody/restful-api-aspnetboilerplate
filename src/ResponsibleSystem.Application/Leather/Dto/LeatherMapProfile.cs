using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ResponsibleSystem.Domain.Enums;
using ResponsibleSystem.Extensions;

namespace ResponsibleSystem.Leather.Dto
{
    class LeatherMapProfile: Profile
    {
        public LeatherMapProfile()
        {
            CreateMap<CreateLeatherDto, Entities.Leather>()
                .ForMember(x => x.Status, opt => opt.MapFrom(x=> LeatherProductionChainStatus.Farmer));

            CreateMap<LeatherDto, Entities.Leather>()
                .ForMember(x => x.Status, opt => opt.MapFrom(x => EnumExtensions.GetValueFromDescription<LeatherProductionChainStatus>(x.Status)))
                .ReverseMap()
                .ForMember(x => x.Farm, opt => opt.MapFrom(x => x.Farm.Name))
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.Status.GetDescriptionFromValue()));
        }
    }
}
