using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ResponsibleSystem.Helpers;

namespace ResponsibleSystem.Logs
{
    [Table("Logs")]
    public class Log : Entity<long>, IHasCreationTime
    {
        public const int MaxLevelLength = 20;

        public DateTime CreationTime { get; set; }

        [StringLength(MaxLevelLength)]
        public string Level { get; set; }

        [StringLength(EntityConsts.MaxVeryLongFieldLength)]
        public string Message { get; set; }

        public string AdditionalDetails { get; set; }

        [StringLength(EntityConsts.MaxVeryLongFieldLength)]
        public string ExceptionType { get; set; }

        public string StackTrace { get; set; }
    }
}
