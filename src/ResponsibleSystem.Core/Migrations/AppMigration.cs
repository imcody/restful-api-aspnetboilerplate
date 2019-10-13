using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ResponsibleSystem.Migrations
{
    [Table("__AppMigrationsHistory")]
    public class AppMigration : Entity
    {
        public string MigrationName { get; set; }
        public long AffectedRows { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public DateTime ExecutionTime { get; set; } = DateTime.Now;
    }
}
