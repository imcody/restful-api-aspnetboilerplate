using System.Threading.Tasks;
using Abp.Auditing;
using Abp.AutoMapper;
using ResponsibleSystem.Shared.Sessions.Dto;

namespace ResponsibleSystem.Shared.Sessions
{
    public class SessionAppService : ResponsibleSystemAppServiceBase, ISessionAppService
    {
        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var session = await SessionService.CurrentLoginInformations();
            var output = new GetCurrentLoginInformationsOutput
            {
                Application = session.Application,
                Headers = session.Headers
            };

            if (AbpSession.TenantId.HasValue)
            {
                output.Tenant = ObjectMapper.Map<TenantLoginInfoDto>(await GetCurrentTenantAsync());
            }

            if (session.User != null)
            {
                output.User = session.User.MapTo<UserLoginInfoDto>();
            }

            return output;
        }
    }
}
