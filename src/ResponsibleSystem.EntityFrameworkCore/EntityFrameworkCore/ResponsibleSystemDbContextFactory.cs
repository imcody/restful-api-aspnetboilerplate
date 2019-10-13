using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ResponsibleSystem.Configuration;
using ResponsibleSystem.Web;

namespace ResponsibleSystem.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class ResponsibleSystemDbContextFactory : IDesignTimeDbContextFactory<ResponsibleSystemDbContext>
    {
        public ResponsibleSystemDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ResponsibleSystemDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            ResponsibleSystemDbContextConfigurer.Configure(builder, 
                configuration.GetConnectionString(AppConfig.ConnectionStringName));

            return new ResponsibleSystemDbContext(builder.Options);
        }


    }
}
