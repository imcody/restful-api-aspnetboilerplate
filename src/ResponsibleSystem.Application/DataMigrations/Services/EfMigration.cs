using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResponsibleSystem.EntityFrameworkCore;
using ResponsibleSystem.Extensions;

namespace ResponsibleSystem.Shared.DataMigrations.Services
{
    public abstract class EfMigration : IDataMigration
    {
        protected readonly ResponsibleSystemDbContext Context;

        public EfMigration(ResponsibleSystemDbContext context)
        {
            Context = context;
        }

        public virtual string MigrationName => GetType().Name;
        protected virtual bool Enabled => true;

        public virtual async Task<bool?> Run()
        {
            int affectedRows = 0;
            if (Enabled && !(await AlreadyApplied()))
            {
                try
                {
                    affectedRows = await RunMigration();
                    await MarkAsCompleted(affectedRows);
                    return true;
                }
                catch (Exception e)
                {
                    await MarkAsFailed(e);
                    return false;
                }
            }
            return null;
        }

        protected virtual Task<bool> AlreadyApplied()
        {
            return Context.AppMigrations.AnyAsync(l => l.MigrationName == MigrationName && l.Success);
        }

        protected abstract Task<int> RunMigration();

        protected virtual async Task MarkAsCompleted(int affectedRows)
        {
            await Context.AppMigrations.AddAsync(new ResponsibleSystem.Migrations.AppMigration()
            {
                MigrationName = MigrationName,
                AffectedRows = affectedRows,
                ExecutionTime = DateTime.UtcNow,
                Success = true
            });
            Context.SaveChanges();
        }

        protected virtual async Task MarkAsFailed(Exception e)
        {
            await Context.AppMigrations.AddAsync(new ResponsibleSystem.Migrations.AppMigration()
            {
                MigrationName = MigrationName,
                AffectedRows = 0,
                ExecutionTime = DateTime.UtcNow,
                Success = false,
                ErrorMessage = e.GetFullErrorMessage(),
                StackTrace = e.StackTrace
            });
            Context.SaveChanges();
        }
    }
}
