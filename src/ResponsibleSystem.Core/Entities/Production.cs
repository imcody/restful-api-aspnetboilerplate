using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Abp.Domain.Entities;
using ResponsibleSystem.Entities.Enums;

namespace ResponsibleSystem.Entities
{
    public class Production : Entity<long>
    {
        public ProductionChainStatus Status { get; set; }

        public DateTime? DepartureTime { get; set; }
        public DateTime? Step2StartDate { get; set; }
        public DateTime? Step2EndDate { get; set; }

        [ForeignKey(nameof(UpperLeather))]
        public long? UpperLeatherId { get; set; }
        public virtual Inventory UpperLeather { get; set; }

        [ForeignKey(nameof(LiningLeather))]
        public long? LiningLeatherId { get; set; }
        public virtual Inventory LiningLeather { get; set; }

        [ForeignKey(nameof(BackCounterLeather))]
        public long? BackCounterLeatherId { get; set; }
        public virtual Inventory BackCounterLeather { get; set; }

        [ForeignKey(nameof(WeltLeather))]
        public long? WeltLeatherId { get; set; }
        public virtual Inventory WeltLeather { get; set; }

        [ForeignKey(nameof(SoleLeather))]
        public long? SoleLeatherId { get; set; }
        public virtual Inventory SoleLeather { get; set; }

        [ForeignKey(nameof(HeelLeather))]
        public long? HeelLeatherId { get; set; }
        public virtual Inventory HeelLeather { get; set; }

        [ForeignKey(nameof(InSockLeather))]
        public long? InSockLeatherId { get; set; }
        public virtual Inventory InSockLeather { get; set; }

        [ForeignKey(nameof(FillingLeather))]
        public long? FillingLeatherId { get; set; }
        public virtual Inventory FillingLeather { get; set; }

        [ForeignKey(nameof(ReinforcementLeather))]
        public long? ReinforcementLeatherId { get; set; }
        public virtual Inventory ReinforcementLeather { get; set; }

        [ForeignKey(nameof(RemovableInSockLeather))]
        public long? RemovableInSockLeatherId { get; set; }
        public virtual Inventory RemovableInSockLeather { get; set; }

        [NotMapped]
        public bool IsCompleted
        {
            get
            {
                long?[] parts = new long?[]
                {
                    this.BackCounterLeatherId,
                    this.FillingLeatherId,
                    this.HeelLeatherId,
                    this.InSockLeatherId,
                    this.LiningLeatherId,
                    this.ReinforcementLeatherId,
                    this.RemovableInSockLeatherId,
                    this.SoleLeatherId,
                    this.UpperLeatherId,
                    this.WeltLeatherId
                };

                return parts.All(x => x.HasValue);
            }
        }

        [NotMapped]
        public bool IsAnythingFilled
        {
            get
            {
                long?[] parts = new long?[]
                {
                    this.BackCounterLeatherId,
                    this.FillingLeatherId,
                    this.HeelLeatherId,
                    this.InSockLeatherId,
                    this.LiningLeatherId,
                    this.ReinforcementLeatherId,
                    this.RemovableInSockLeatherId,
                    this.SoleLeatherId,
                    this.UpperLeatherId,
                    this.WeltLeatherId
                };

                return parts.Any(x => x.HasValue);
            }
        }
    }
}
