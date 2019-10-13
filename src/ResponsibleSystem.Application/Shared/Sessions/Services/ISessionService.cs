using ResponsibleSystem.Authorization.Users;
using System.Threading.Tasks;
using ResponsibleSystem.Shared.Dto;
using ResponsibleSystem.Shared.Sessions.Dto;
using ResponsibleSystem.Authorization.Roles;

namespace ResponsibleSystem.Shared.Sessions.Services
{
    public interface ISessionService
    {
        Task<CurrentLoginInformations> CurrentLoginInformations();

        Task<long> GetCurrentUserIdAsync();
        Task<User> GetCurrentUserAsync();
        Task<UserDto> GetPublisherAsync();
        Task<User> GetUserAsync(long userId);
        Task<UserDto> TryGetPublisherAsync();

        long? GetCurrentUserFarmId();
        AppUserRole? GetCurrentUserRole();
    }
}