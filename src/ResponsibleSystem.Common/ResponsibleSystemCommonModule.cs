using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.MicroKernel.Registration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using ResponsibleSystem.Configuration;
using ResponsibleSystem.Common.Config;
using ResponsibleSystem.Common.Data;
using ResponsibleSystem.Common.Azure.Storage.Blob;
using ResponsibleSystem.Common.Azure.Storage.MessageQueues;
using ResponsibleSystem.Common.Azure.Storage.Tables;
using ResponsibleSystem.Common.Network.WebSockets;

namespace ResponsibleSystem.Common
{
    public class ResponsibleSystemCommonModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public ResponsibleSystemCommonModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment()); //env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ResponsibleSystemCommonModule).GetAssembly());

            IocManager.Register<IAzureBlobService, AzureBlobService>();
            IocManager.Register<IMessageQueueService, MessageQueueService>();
            IocManager.Register<ITableStorageService, TableStorageService>();

            IocManager.Register<ISqlDataService, SqlDataService>();
            IocManager.Register<IWebSocketService, WebSocketService>();

            // config

            IocManager.IocContainer.Register(
                Component
                 .For<IConfigurationRoot>()
                 .Instance(_appConfiguration));

            IocManager.IocContainer.Register(
                Component
                 .For(typeof(IConfigFactory<>))
                 .ImplementedBy(typeof(ConfigurationRootFactory<>))
                );
        }
    }
}
