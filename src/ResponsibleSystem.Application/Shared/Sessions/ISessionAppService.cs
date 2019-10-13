using System.Threading.Tasks;
using Abp.Application.Services;
using ResponsibleSystem.Shared.Sessions.Dto;

namespace ResponsibleSystem.Shared.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
