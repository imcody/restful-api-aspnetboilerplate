using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace ResponsibleSystem.Inventory.Dto
{
    public class InventoryMapProfile: Profile
    {
        public InventoryMapProfile()
        {
            CreateMap<RegisterInventoryDto, Entities.Inventory>();
            CreateMap<InventoryDto, Entities.Inventory>().ReverseMap();
        }
    }
}
