using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Configuration;
using System.Linq;
using ResponsibleSystem.Common.Config;
using ResponsibleSystem.Common.Data;
using ResponsibleSystem.Common.Domain;
using ResponsibleSystem.Common.Logs;

namespace ResponsibleSystem.Common.Infrastructure
{
    public class DependencyResolverFactory : IDependencyResolverFactory
    {
        private readonly Type _originType;
        public Action<ContainerBuilder> RegisterCustomDependencies { get; set; } = null;
        private readonly string _configBasePath;
        private bool UseDotNetFramework => string.IsNullOrWhiteSpace(_configBasePath);

        /// <summary>
        /// Constructore for .NET Framework apps
        /// </summary>
        /// <param name="origin"></param>
        public DependencyResolverFactory(Type origin)
        {
            _originType = origin;
        }

        /// <summary>
        /// Constructor for .NET Core / .NET Standard apps
        /// </summary>
        /// <param name="origin"></param>
        public DependencyResolverFactory(Type origin, string configBasePath)
        {
            _originType = origin;
            _configBasePath = configBasePath;
        }

        public IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            ConfigureContainer(builder);
            return builder.Build();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var conn = ConfigurationManager.ConnectionStrings["Default"];
            if (conn != null)
                builder.RegisterModule(new DbContextModule(conn.ConnectionString));

            builder.RegisterModule<LogInjectionModule>();

            if (UseDotNetFramework)
                RegisterDotNetFrameworkDependencies(builder);
            else
                RegisterDotNetCoreDependencies(builder);

            var assembly = this.GetType().Assembly;
            builder
              .RegisterAssemblyTypes(assembly)
              .Where(t => t.Name.EndsWith("Service"))
              .AsImplementedInterfaces()
              .AsSelf();

            // all Jobs and services for origin assembly

            builder
              .RegisterAssemblyTypes(_originType.Assembly)
              .Where(t => t.Name.EndsWith("Job") || t.Name.EndsWith("Service") || t.Name.EndsWith("Handler"))
              .AsImplementedInterfaces()
              .AsSelf();

            // builder.RegisterType<LogManager>().SingleInstance().As<ILogManager>();
            builder.RegisterType<Logger>().As<ILogger>();
            builder.RegisterType<SystemClock>().As<IClock>().SingleInstance();

            RegisterCustomDependencies?.Invoke(builder);
        }

        protected virtual void RegisterDotNetFrameworkDependencies(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(AppConfigJobConfigFactory<>))
                .As(typeof(IConfigFactory<>))
                .InstancePerDependency();
        }

        protected virtual void RegisterDotNetCoreDependencies(ContainerBuilder builder)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(_configBasePath)
               .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables()
               .Build();

            // check if we need that
            if (!string.IsNullOrWhiteSpace(config["AzureWebJobsStorage"]))
                builder.Register(c => CloudStorageAccount.Parse(config["AzureWebJobsStorage"]));

            builder.Register(c => config).As<IConfigurationRoot>();
            builder.RegisterGeneric(typeof(ConfigurationRootFactory<>))
                .As(typeof(IConfigFactory<>))
                .InstancePerDependency();
        }
    }
}
