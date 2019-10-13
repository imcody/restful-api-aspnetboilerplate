using System.Collections.Generic;
using System.Threading.Tasks;
using ResponsibleSystem.EntityFrameworkCore;
using System.Linq;
using System;
using ResponsibleSystem.Shared.DataMigrations.Migrations.v1_01;

namespace ResponsibleSystem.Shared.DataMigrations.Services
{
    public class DataMigrationService
    {
        private readonly IList<IDataMigration> _migrations;
        private readonly ResponsibleSystemDbContext _context;

        public DataMigrationService(ResponsibleSystemDbContext context)
        {
            _context = context;

            _migrations = new List<IDataMigration>()
            {
                new SampleMigration(context)
            };
        }

        public async Task Run()
        {
            var results = _migrations.Select(x => x.Run().Result)
                                     .ToArray();

            int count(bool? value)
            {
                return results.Count(x => x == value);
            }

            _context.Logs.Add(new Logs.Log
            {
                Level = "DataMigration",
                CreationTime = DateTime.UtcNow,
                Message = $"Succeeded: { count(true) }, Failed: { count(false) }, Skipped: { count(null) }",
            });
            await _context.SaveChangesAsync();
        }
    }
}
