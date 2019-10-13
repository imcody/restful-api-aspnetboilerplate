using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ResponsibleSystem.Configuration;
using ResponsibleSystem.Common;

namespace ResponsibleSystem.Web.Host.Startup
{
    [DependsOn(
       typeof(ResponsibleSystemWebCoreModule),
       typeof(ResponsibleSystemCommonModule))]
    public class ResponsibleSystemWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public ResponsibleSystemWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ResponsibleSystemWebHostModule).GetAssembly());
        }
    }
}
