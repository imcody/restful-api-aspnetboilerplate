using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ResponsibleSystem.Entities;

namespace ResponsibleSystem.Backoffice.Farms.Dto
{
    class FarmMapProfile: Profile
    {
        public FarmMapProfile()
        {
            CreateMap<CreateFarmDto, Farm>();

            CreateMap<FarmDto, Farm>()
                .ReverseMap();
        }
    }
}
