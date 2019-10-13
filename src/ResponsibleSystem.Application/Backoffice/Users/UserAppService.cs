using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.IdentityFramework;
using Abp.Localization;
using Abp.Runtime.Session;
using ResponsibleSystem.Authorization.Permissions;
using ResponsibleSystem.Authorization.Roles;
using ResponsibleSystem.Authorization.Users;
using ResponsibleSystem.Extensions;
using ResponsibleSystem.Backoffice.Users.Dto;
using System;
using Abp.UI;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using ResponsibleSystem.Configuration;
using ResponsibleSystem.Entities;
using ResponsibleSystem.Shared.Services;
using ResponsibleSystem.Shared.Dto;

namespace ResponsibleSystem.Backoffice.Users
{
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedResultRequestDto, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<Farm, long> _farmRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ICryptoService _cryptoService;
        private readonly EmailSendingSettings _emailSendingSettings;
        private readonly AppConfigBoundValues _appConfig;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IRepository<Farm, long> farmRepository,
            IPasswordHasher<User> passwordHasher,
            IOptions<EmailSendingSettings> emailSendingSettings,
            IOptions<AppConfigBoundValues> appConfig,
            ICryptoService cryptoService)
            : base(repository)
        {
            _userRepository = repository;
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _farmRepository = farmRepository;
            _passwordHasher = passwordHasher;
            _cryptoService = cryptoService;
            _emailSendingSettings = emailSendingSettings.Value;
            _appConfig = appConfig.Value;
        }

        public override Task<PagedResultDto<UserDto>> GetAll(PagedResultRequestDto input)
        {
            var result = new PagedResultDto<UserDto>();
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var tenantId = AbpSession.TenantId;
                if (tenantId == null)
                {
                    result.Items = _userManager.Users.MapTo<List<UserDto>>();
                }
                else
                {
                    result.Items = _userManager.Users.Where(u => u.TenantId == tenantId).MapTo<List<UserDto>>();
                }
            }

            return Task.Run(() => result);
        }

        public Task<List<FarmInfoDto>> GetFarms()
        {
            return _farmRepository.GetAll()
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name)
                .Select(x => new FarmInfoDto { Id = x.Id, Name = x.Name })
                .ToListAsync();
        }

        public override async Task<UserDto> Create(CreateUserDto input)
        {
            using (GetCurrentUnitOfWork())
            {
                CheckCreatePermission();

                var user = ObjectMapper.Map<User>(input);
                if (user.UserRole != AppUserRole.Farmer)
                {
                    input.FarmId = null;
                }
                else if (user.UserRole == AppUserRole.Farmer && !input.FarmId.HasValue)
                {
                    throw new UserFriendlyException("Farm is required");
                }

                user.TenantId = input.TenantId ?? AbpSession.TenantId;
                user.Password = _passwordHasher.HashPassword(user, input.Password);
                user.IsEmailConfirmed = true;

                CheckErrors(await _userManager.CreateAsync(user));

                CurrentUnitOfWork.SaveChanges();

                await _userManager.AddToRoleAsync(user, input.UserRole);

                return MapToEntityDto(user);
            }
        }

        public override async Task<UserDto> Update(UserDto input)
        {
            using (GetCurrentUnitOfWork())
            {
                CheckUpdatePermission();

                var user = await _userManager.Users.Include(x => x.Roles)
                                                   .FirstOrDefaultAsync(x => x.Id == input.Id);

                if (user == null)
                {
                    throw new UserFriendlyException("Invalid user");
                }

                var role = user.UserRole;

                MapToEntity(input, user);

                if (user.UserRole != AppUserRole.Farmer)
                {
                    input.FarmId = null;
                }
                else if (user.UserRole == AppUserRole.Farmer && !input.FarmId.HasValue)
                {
                    throw new UserFriendlyException("Farm is required");
                }

                CheckErrors(await _userManager.UpdateAsync(user));
                try
                {

                    if (input.UserRole != role.GetDescriptionFromValue())
                    {
                        await _userManager.RemoveFromRoleAsync(user, role.GetDescriptionFromValue());
                        await _userManager.AddToRoleAsync(user, input.UserRole);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }


                return await Get(input);
            }
        }

        public override async Task Delete(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var roles = AbpSession.TenantId == null ?
                    await _roleRepository.GetAllListAsync() :
                    await _roleRepository.GetAllListAsync(r => r.Name != AppUserRole.Admin.GetDescriptionFromValue());
                return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
            }
        }

        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles);
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            using (GetCurrentUnitOfWork())
            {
                return await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedResultRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        protected IDisposable GetCurrentUnitOfWork()
        {
            return AbpSession.TenantId == null ?
                CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant) :
                CurrentUnitOfWork.SetTenantId(AbpSession.TenantId);
        }

        public async Task ChangePassword(ChangePasswordInput input)
        {
            CheckUpdatePermission();

            var currentUser = await _userManager.GetUserByIdAsync(AbpSession.UserId.Value);
            var user = await _userManager.GetUserByIdAsync(input.UserId);

            if (user == null)
                throw new UserFriendlyException($"User doesn't exists");

            if (user.IsDeleted)
                throw new UserFriendlyException($"User {user.Name} {user.Surname} is deleted");

            if (input.OldPasswordRequired && _passwordHasher.VerifyHashedPassword(user, user.Password, input.OldPassword) == PasswordVerificationResult.Failed)
                throw new UserFriendlyException($"Please provide a valid old password");

            var rolesAllowedToChangeOthers = new[]
            {
                AppUserRole.Admin
            };

            if (input.UserId != currentUser.Id && !rolesAllowedToChangeOthers.Contains(currentUser.UserRole))
            {
                throw new UserFriendlyException($"Your account doesn't have enough permissions to complete the request");
            }

            user.Password = _passwordHasher.HashPassword(user, input.NewPassword);

            CheckErrors(await _userManager.UpdateAsync(user));
        }

        [AbpAllowAnonymous]
        public async Task PasswordRecovery(PasswordRecoveryInput input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var user = await _userRepository.FirstOrDefaultAsync(u => u.EmailAddress.Equals(input.Email, StringComparison.InvariantCultureIgnoreCase));

                if (user == null)
                    throw new UserFriendlyException($"User with provided email doesn't exists");

                var client = new SendGridClient(_emailSendingSettings.SendGridApiKey);

                var resetToken = new TokenHelper(_cryptoService).SignToken(user.GetUserToken());

                var msg = new SendGridMessage()
                {
                    From = new EmailAddress(_emailSendingSettings.MailFromAddress, _emailSendingSettings.MailFromDisplayName),
                    Subject = "ResponsibleSystem Password recovery",
                    PlainTextContent = $"In order to change your password please use following URL in your browser {_appConfig.ClientRootAddress}/#/authentication/password-reset/{resetToken}",
                    HtmlContent = $"In order to change your password please <a href='{_appConfig.ClientRootAddress}/#/authentication/password-reset/{resetToken}'>click here</a>."
                };

                msg.AddTo(new EmailAddress(user.EmailAddress, user.EmailAddress));
                var response = await client.SendEmailAsync(msg);
            }
        }

        [AbpAllowAnonymous]
        public async Task ResetPassword(ResetPasswordInput input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var token = new TokenHelper(_cryptoService).ExtractTokenIfValid(input.Token);
                if (token == null)
                {
                    throw new UserFriendlyException("Invalid token");
                }

                var userId = User.GetUserIdFromToken(token);
                var user = userId != null ?
                    await _userManager.GetUserByIdAsync(userId.Value) :
                    null;

                if (user == null)
                    throw new UserFriendlyException($"User doesn't exists");

                if (user.IsDeleted)
                    throw new UserFriendlyException($"User {user.Name} {user.Surname} is deleted");

                user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
                await _userRepository.UpdateAsync(user);
            }
        }
    }
}
