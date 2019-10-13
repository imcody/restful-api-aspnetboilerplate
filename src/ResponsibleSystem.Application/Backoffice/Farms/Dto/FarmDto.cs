using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;

namespace ResponsibleSystem.Backoffice.Farms.Dto
{
    public class FarmDto: EntityDto<long>, ISoftDelete
    {
        public string Name { get; set; }
        public string OrganizationNumber { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public bool IsDeleted { get; set; }
    }
}
