using System.Runtime.Serialization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using Castle.DynamicProxy.Contributors;
using ResponsibleSystem.Authorization.Roles;

namespace ResponsibleSystem.Authorization.Users
{
    public class UserClaimsPrincipalFactory : AbpUserClaimsPrincipalFactory<User, Role>
    {
        public UserClaimsPrincipalFactory(
            UserManager userManager,
            RoleManager roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(
                  userManager,
                  roleManager,
                  optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            if (user?.FarmId.HasValue == true)
            {
                identity?.AddClaim(new Claim(CustomClaimNames.FarmId, user.FarmId.ToString()));
            }
            return identity;
        }
    }
}
