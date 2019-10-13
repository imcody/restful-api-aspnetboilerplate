using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ResponsibleSystem.EntityFrameworkCore;

namespace ResponsibleSystem.Shared.DataMigrations.Services
{
    public abstract class SqlMigration : EfMigration
    {
        public SqlMigration(ResponsibleSystemDbContext context) : base(context) { }

        protected abstract string GetMigrationSqlCommand();

        protected override async Task<int> RunMigration()
        {
            return await Context.Database.ExecuteSqlCommandAsync(GetMigrationSqlCommand());
        }
    }
}
