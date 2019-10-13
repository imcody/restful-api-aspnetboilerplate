using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;
using ResponsibleSystem.Configuration;

namespace ResponsibleSystem.Controllers
{
    public abstract class ResponsibleSystemControllerBase: AbpController
    {
        protected ResponsibleSystemControllerBase()
        {
            LocalizationSourceName = AppConfig.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
