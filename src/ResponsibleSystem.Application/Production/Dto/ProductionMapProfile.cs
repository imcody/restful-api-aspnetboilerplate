using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ResponsibleSystem.Extensions;

namespace ResponsibleSystem.Production.Dto
{
    public class ProductionMapProfile: Profile
    {
        public ProductionMapProfile()
        {
            CreateMap<CreateProductionDto, Entities.Production>();

            CreateMap<ProductionDto, Entities.Production>()
                .ReverseMap()
                .ForMember(x=> x.Status, opt=> opt.MapFrom(x=> x.Status.GetDescriptionFromValue()));
        }
    }
}
