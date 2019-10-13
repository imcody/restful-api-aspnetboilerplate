using ResponsibleSystem.EntityFrameworkCore;
using ResponsibleSystem.Shared.DataMigrations.Services;

namespace ResponsibleSystem.Shared.DataMigrations.Migrations.v1_01
{
    public class SampleMigration : SqlMigration
    {
        public SampleMigration(ResponsibleSystemDbContext dbContext) : base(dbContext) { }

        protected override string GetMigrationSqlCommand() =>
            $@"
                UPDATE [AbpUsers]
                SET [EmailAddress] = 'master@saninudge.com'  
                WHERE Id = 1
            ";
    }
}
