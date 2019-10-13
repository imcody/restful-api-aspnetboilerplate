using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ResponsibleSystem.Authorization.Roles;
using ResponsibleSystem.Entities.Enums;

namespace ResponsibleSystem.Entities
{
    public class Inventory: Entity<long>
    {
        public ShoemakerType ShoemakerType { get; set; }

        public DateTime ArrivalDate { get; set; }

        [ForeignKey(nameof(Leather))]
        public long LeatherId { get; set; }
        public Leather Leather { get; set; }
    }
}
