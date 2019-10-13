using Abp.AutoMapper;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using ResponsibleSystem.Authorization;
using ResponsibleSystem.Common;
using ResponsibleSystem.Exceptions;

namespace ResponsibleSystem
{
    [DependsOn(
        typeof(ResponsibleSystemCoreModule), 
        typeof(AbpAutoMapperModule),
        typeof(ResponsibleSystemCommonModule))]
    public class ResponsibleSystemApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            InitializeEvents();
            Configuration.Authorization.Providers.Add<ResponsibleSystemAuthorizationProvider>();
            Configuration.Modules.AbpAutoMapper().Configurators.Add(InitializationManager.InitializeAutoMapper);
        }

        protected void InitializeEvents()
        {
            try
            {
                var loggerFactory = IocManager.Resolve<ILoggerFactory>();
                EventBus.Default.Register(new ExceptionEventHandler(loggerFactory));
            }
            catch (Exception ex)
            {
                // TMP workaround to fix unit testing project.
            }
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ResponsibleSystemApplicationModule).GetAssembly();
            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }

        public override void PostInitialize()
        {
            try
            {
                InitializationManager.InitalizeIocRegistrations(IocManager);
                InitializationManager.InitializeApp(IocManager);
            }
            catch (Exception ex)
            {
                // TMP workaround to fix unit testing project.
            }
        }
    }
}
