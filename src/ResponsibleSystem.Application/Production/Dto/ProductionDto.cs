using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;

namespace ResponsibleSystem.Production.Dto
{
    public class ProductionDto: IEntityDto<long>
    {
        public long Id { get; set; }

        public DateTime? DepartureTime { get; set; }
        public DateTime? Step2StartDate { get; set; }
        public DateTime? Step2EndDate { get; set; }
        public string Status { get; set; }

        public long? UpperLeatherId { get; set; }
        public long? LiningLeatherId { get; set; }
        public long? BackCounterLeatherId { get; set; }
        public long? WeltLeatherId { get; set; }
        public long? SoleLeatherId { get; set; }
        public long? HeelLeatherId { get; set; }
        public long? InSockLeatherId { get; set; }
        public long? FillingLeatherId { get; set; }
        public long? ReinforcementLeatherId { get; set; }
        public long? RemovableInSockLeatherId { get; set; }
    }
}
