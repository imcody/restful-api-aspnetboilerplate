using System;
using System.Transactions;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Uow;
using Abp.MultiTenancy;
using Abp.Reflection.Extensions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ResponsibleSystem.EntityFrameworkCore;
using ResponsibleSystem.Shared.Services;
using ResponsibleSystem.Shared.Sessions.Services;
using System.Net.Http;
using ResponsibleSystem.Common.Config;
using ResponsibleSystem.Shared.DataMigrations.Services;

namespace ResponsibleSystem
{
    public class InitializationManager
    {
        public static void InitializeAutoMapper(IMapperConfigurationExpression config)
        {
            
        }

        public static void InitalizeIocRegistrations(IIocManager ioc)
        {
            var thisAssembly = typeof(ResponsibleSystemApplicationModule).GetAssembly();
            ioc.RegisterAssemblyByConvention(thisAssembly);
            ioc.Register<ISessionService, SessionService>();
            ioc.Register<HttpClient, HttpClient>(DependencyLifeStyle.Transient);
            ioc.Register<ICryptoService, CryptoService>(); // TODO: sprawdzić czy nie jest zbędne
        }

        public static async void InitializeApp(IIocManager iocManger)
        {
            var logger = iocManger.Resolve<ILogger<InitializationManager>>();
            // EF data Migrations.
            using (var uowManager = iocManger.ResolveAsDisposable<IUnitOfWorkManager>())
            {
                using (var uow = uowManager.Object.Begin(TransactionScopeOption.Suppress))
                {
                    var dbContext = uowManager.Object.Current.GetDbContext<ResponsibleSystemDbContext>(MultiTenancySides.Host);
                    await new DataMigrationService(dbContext).Run();
                    logger.LogDebug("EfDataMigrationService - completed");

                    uow.Complete();
                }
            }
        }
    }
}
