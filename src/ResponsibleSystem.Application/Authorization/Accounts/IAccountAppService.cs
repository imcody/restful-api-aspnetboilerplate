using System.Threading.Tasks;
using Abp.Application.Services;
using ResponsibleSystem.Authorization.Accounts.Dto;

namespace ResponsibleSystem.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
