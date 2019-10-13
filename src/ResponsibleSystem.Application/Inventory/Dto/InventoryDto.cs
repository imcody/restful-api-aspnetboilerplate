using System;
using Abp.Application.Services.Dto;

namespace ResponsibleSystem.Inventory.Dto
{
    public class InventoryDto: EntityDto<long>
    {
        public DateTime ArrivalDate { get; set; }
        public long LeatherId { get; set; }
    }
}
