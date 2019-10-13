using System;
using Abp.Domain.Entities;

namespace ResponsibleSystem.Entities
{
    public class Farm: Entity<long>, ISoftDelete
    {
        public string Name { get; set; }
        public string OrganizationNumber { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public bool IsDeleted { get; set; }
    }
}
