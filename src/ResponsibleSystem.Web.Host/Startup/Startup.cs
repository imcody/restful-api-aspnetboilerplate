using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Abp.AspNetCore;
using Microsoft.AspNetCore.Http;
using ResponsibleSystem.Configuration;
using ResponsibleSystem.Exceptions;
using ResponsibleSystem.Identity;
using NLog.Web;
using NLog.Config;
using NLog.Targets;
using ResponsibleSystem.Web.Host.Api;
using ResponsibleSystem.Web.Host.Docs;
#if FEATURE_SIGNALR
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;
using Abp.Owin;
using ResponsibleSystem.Owin;
#elif FEATURE_SIGNALR_ASPNETCORE
using Abp.AspNetCore.SignalR.Hubs;
#endif

namespace ResponsibleSystem.Web.Host.Startup
{
    public class Startup
    {
        private const string DefaultCorsPolicyName = "localhost";

        private readonly IConfigurationRoot _appConfiguration;

        public Startup(IHostingEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
            LoggingConfiguration config = env.ConfigureNLog("nlog.config");
            var target = config.FindTargetByName<DatabaseTarget>("databaseTarget");
            target.ConnectionString = _appConfiguration.GetConnectionString("Default");
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // MVC
            services.AddMvc(
                options =>
                {
                    options.Filters.Add(new CorsAuthorizationFilterFactory(DefaultCorsPolicyName));
                    options.Conventions.Add(new AppPublicApisControlerModelConvention());
                });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            IdentityRegistrar.Register(services);
            AuthConfigurer.Configure(services, _appConfiguration);

#if FEATURE_SIGNALR_ASPNETCORE
            services.AddSignalR();
#endif

            // Configure CORS for angular2 UI

            services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
                    builder
                        .WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });


            // Swagger - Enable this line and the related lines in Configure method to enable swagger UI
            services.AddSwaggerGen(SwaggerHelper.ConfigureSwaggerGen);

            // Configure settings
            services.Configure<AppConfigBoundValues>(_appConfiguration.GetSection("App"));
            services.Configure<ErrorLogsSettings>(_appConfiguration.GetSection("App:Logs"));
            services.Configure<EmailSendingSettings>(_appConfiguration.GetSection("App:EmailSending"));
            services.Configure<CryptographySettings>(_appConfiguration.GetSection("App:Cryptography"));

            // Configure Abp and Dependency Injection
            return services.AddAbp<ResponsibleSystemWebHostModule>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAbp(options => { options.UseAbpRequestLocalization = false; }); // Initializes ABP framework.

            app.UseCors(DefaultCorsPolicyName); // Enable CORS!

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAbpRequestLocalization();

#if FEATURE_SIGNALR
            // Integrate with OWIN
            app.UseAppBuilder(ConfigureOwinServices);
#elif FEATURE_SIGNALR_ASPNETCORE
            app.UseSignalR(routes =>
            {
                routes.MapHub<AbpCommonHub>("/signalr");
            });
#endif

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "defaultWithArea",
                    template: "{area}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();
            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(SwaggerHelper.ConfigureSwaggerUI(_appConfiguration["App:ServerRootAddress"]));
        }

#if FEATURE_SIGNALR
        private static void ConfigureOwinServices(IAppBuilder app)
        {
            app.Properties["host.AppName"] = "ResponsibleSystem";

            app.UseAbp();
            
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    EnableJSONP = true
                };
                map.RunSignalR(hubConfiguration);
            });
        }
#endif
    }
}
