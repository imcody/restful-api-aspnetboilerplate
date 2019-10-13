using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ResponsibleSystem.Common;
using ResponsibleSystem.Sandbox.HFEAForms.Repository;

namespace ResponsibleSystem.Application.Sandbox
{
    [DependsOn(
        typeof(ResponsibleSystemCoreModule), 
        typeof(AbpAutoMapperModule),
        typeof(ResponsibleSystemCommonModule),
        typeof(ResponsibleSystemCommonCosmosDbModule),
        typeof(ResponsibleSystemApplicationModule))]
    public class ResponsibleSystemApplicationSandboxModule : AbpModule
    {
        protected void InitializeEvents()
        {
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ResponsibleSystemApplicationSandboxModule).GetAssembly();
            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }

        public override void PostInitialize()
        {
            var thisAssembly = typeof(ResponsibleSystemApplicationSandboxModule).GetAssembly();
            IocManager.RegisterAssemblyByConvention(thisAssembly);
            IocManager.Register<IHfeaRepository, HfeaRepository>();
        }
    }
}
