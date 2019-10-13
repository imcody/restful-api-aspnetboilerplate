using Abp;
using Abp.Authorization;
using System;
using System.Threading.Tasks;
using ResponsibleSystem.Authorization.Roles;
using ResponsibleSystem.Authorization.Users;

namespace ResponsibleSystem.Authorization.Permissions
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        private readonly UserManager _userManager;

        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
            _userManager = userManager;
        }

        public override Task<bool> IsGrantedAsync(string permissionName)
        {
            return IsGranted(AbpSession.UserId, permissionName);
        }

        public override Task<bool> IsGrantedAsync(long userId, string permissionName)
        {
            return IsGranted(userId, permissionName);
        }

        public override Task<bool> IsGrantedAsync(UserIdentifier user, string permissionName)
        {
            return IsGranted(user.UserId, permissionName);
        }

        protected async Task<bool> IsGranted(long? userId, string permissionName)
        {
            if (!userId.HasValue)
                return false;

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new Exception("There is no current user!");

            return StaticRolePermissions.RolePermissions[user.UserRole].Contains(permissionName);
        }
    }
}
