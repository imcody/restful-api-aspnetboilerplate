using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using ResponsibleSystem.Authorization.Users;
using ResponsibleSystem.Configuration;
using ResponsibleSystem.MultiTenancy;
using ResponsibleSystem.Shared.Sessions.Services;
using Abp.Dependency;

namespace ResponsibleSystem
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class ResponsibleSystemAppServiceBase : ApplicationService
    {
        protected ISessionService SessionService;
        public TenantManager TenantManager { get; set; }
        public UserManager UserManager { get; set; }

        protected ResponsibleSystemAppServiceBase()
        {
            SessionService = IocManager.Instance.Resolve<ISessionService>();
            LocalizationSourceName = AppConfig.LocalizationSourceName;
        }

        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
