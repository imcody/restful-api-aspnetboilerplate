using System.Collections.Generic;
using System.Linq;
using Abp.Authorization;
using ResponsibleSystem.Authorization.Roles;

namespace ResponsibleSystem.Authorization.Permissions
{
    public class AppPermissionManager : IAppPermissionManager
    {
        public IList<Permission> GetPermissions(AppUserRole role)
        {
            return GetPermissions(StaticRolePermissions.RolePermissions[role]);
        }

        private readonly IPermissionManager _permissionManager;

        public AppPermissionManager(IPermissionManager permissionManager)
        {
            _permissionManager = permissionManager;
        }

        protected IList<Permission> GetPermissions(List<string> permissionNames)
        {
            return _permissionManager
                .GetAllPermissions()
                .Where(p => permissionNames.Contains(p.Name))
                .ToList();
        }
    }

    public interface IAppPermissionManager
    {
        IList<Permission> GetPermissions(AppUserRole role);
    }
}
