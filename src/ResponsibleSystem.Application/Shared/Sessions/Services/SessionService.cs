using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using ResponsibleSystem.Authorization.Users;
using ResponsibleSystem.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Runtime.Security;
using ResponsibleSystem.Authorization.Roles;
using ResponsibleSystem.Shared.Sessions.Dto;
using ResponsibleSystem.Shared.Dto;
using ResponsibleSystem.SignalR;
using ResponsibleSystem.Extensions;

namespace ResponsibleSystem.Shared.Sessions.Services
{
    public class SessionService : ISessionService
    {
        private readonly IOptions<AppConfigBoundValues> _appSettings;
        private readonly IPrincipalAccessor _principalAccessor;
        private readonly IRepository<User, long> _userRepository;
        private readonly IAbpSession _abpSession;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(
            IAbpSession abpSession,
            IRepository<User, long> userRepository,
            IHttpContextAccessor httpContextAccessor,
            IOptions<AppConfigBoundValues> appSettings,
            IPrincipalAccessor principalAccessor)
        {
            _abpSession = abpSession;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appSettings;
            _principalAccessor = principalAccessor;
        }

        public async Task<CurrentLoginInformations> CurrentLoginInformations()
        {
            var output = new CurrentLoginInformations
            {
                Application = new ApplicationInfoDto
                {
                    Version = AppVersionHelper.Version.ToString(),
                    ReleaseDate = AppVersionHelper.ReleaseDate,
                    Features = new Dictionary<string, bool>
                    {
                        { "SignalR", SignalRFeature.IsAvailable }
                    }
                }
            };

            LoadAppHeaders(output);

            if (_abpSession.UserId.HasValue)
            {
                output.User = await GetCurrentUserAsync();
            }

            return output;
        }

        #region Users


        public virtual async Task<User> GetUserAsync(long userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
                throw new Exception("There is no current user!");

            return user;
        }

        public virtual Task<User> GetCurrentUserAsync()
        {
            return GetUserAsync(_abpSession.GetUserId());
        }

        public virtual Task<long> GetCurrentUserIdAsync()
        {
            return Task.FromResult(_abpSession.UserId ?? 0);
        }

        public virtual async Task<UserDto> GetPublisherAsync()
        {
            var user = await GetCurrentUserAsync();
            var publisher = user.MapTo<UserDto>();
            return publisher;
        }

        public virtual async Task<UserDto> TryGetPublisherAsync()
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == 0)
                return null;

            var user = await _userRepository.GetAsync(userId);
            var publisher = user?.MapTo<UserDto>();
            return publisher;
        }

        public long? GetCurrentUserFarmId()
        {
            var userIdClaim = _principalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == CustomClaimNames.FarmId);
            if (string.IsNullOrEmpty(userIdClaim?.Value))
            {
                return null;
            }

            if (!long.TryParse(userIdClaim.Value, out var userId))
            {
                return null;
            }

            return userId;
        }

        public AppUserRole? GetCurrentUserRole()
        {
            var roleString = _principalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == AbpClaimTypes.Role)?.Value;
            return EnumExtensions.GetValueFromDescription<AppUserRole>(roleString);
        }

        #endregion

        #region AppHeaders

        protected virtual void LoadAppHeaders(CurrentLoginInformations loginInformations)
        {
            var referer = new StringValues();
            var origin = new StringValues();
            var host = new StringValues();
            _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Referer", out referer);
            _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Origin", out origin);
            _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Host", out host);

            loginInformations.Headers = new AppHeaders()
            {
                Referer = referer,
                Origin = origin,
                Host = host,
                ClientRootAddress = _appSettings.Value.ClientRootAddress
            };
        }

        #endregion
    }
}
