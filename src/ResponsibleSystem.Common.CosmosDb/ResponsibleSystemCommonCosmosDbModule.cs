using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.MicroKernel.Registration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using ResponsibleSystem.Common.CosmosDb.Repositories;
using ResponsibleSystem.Common.CosmosDb.Services;
using ResponsibleSystem.Configuration;

namespace ResponsibleSystem.Common
{
    [DependsOn(
    typeof(ResponsibleSystemCommonModule))]
    public class ResponsibleSystemCommonCosmosDbModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public ResponsibleSystemCommonCosmosDbModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment()); //env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ResponsibleSystemCommonModule).GetAssembly());
            IocManager.Register<ICosmosDbClient, CosmosDbClient>();
            IocManager.IocContainer.Register(
                Component
                 .For(typeof(ICosmosDbRepository<>))
                 .ImplementedBy(typeof(CosmosDbRepository<>))
            );
        }
    }
}
