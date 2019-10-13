using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ResponsibleSystem.EntityFrameworkCore
{
    public static class ResponsibleSystemDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ResponsibleSystemDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ResponsibleSystemDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
