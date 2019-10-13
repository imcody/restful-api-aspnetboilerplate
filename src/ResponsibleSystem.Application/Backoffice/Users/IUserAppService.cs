using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ResponsibleSystem.Backoffice.Users.Dto;
using ResponsibleSystem.Shared.Dto;

namespace ResponsibleSystem.Backoffice.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);

        Task ChangePassword(ChangePasswordInput input);

        Task PasswordRecovery(PasswordRecoveryInput input);

        Task ResetPassword(ResetPasswordInput input);
    }
}
