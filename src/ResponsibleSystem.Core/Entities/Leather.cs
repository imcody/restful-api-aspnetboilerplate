using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using ResponsibleSystem.Authorization.Users;
using ResponsibleSystem.Domain.Enums;

namespace ResponsibleSystem.Entities
{
    public class Leather : Entity<long>
    {
        public string PPNo { get; set; }
        public string IdNo { get; set; }

        public int Age { get; set; }
        public string Gender { get; set; }
        public DateTime EstimatedSlaughterDate { get; set; }

        public double Weight { get; set; }

        public bool IsCrust { get; set; }

        public string Thickness { get; set; }
        public double TotalArea { get; set; }
        public decimal PricePerFt { get; set; }
        public bool IsWaxed { get; set; }
        public string Extra { get; set; }
        public string Color { get; set; }
        public bool EarsOn { get; set; }

        public LeatherProductionChainStatus Status { get; set; }

        [ForeignKey(nameof(Inventory))]
        public long? InventoryId { get; set; }
        public virtual Inventory Inventory { get; set; }

        [ForeignKey(nameof(SlaughterhouseUser))]
        public long? SlaughterhouseUserId { get; set; }
        public virtual User SlaughterhouseUser { get; set; }

        [ForeignKey(nameof(TanneryUser))]
        public long? TanneryUserId { get; set; }
        public virtual User TanneryUser { get; set; }

        [ForeignKey(nameof(Farm))]
        public long FarmId { get; set; }
        public virtual Farm Farm { get; set; }
    }
}
