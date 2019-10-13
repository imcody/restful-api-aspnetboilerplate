using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using ResponsibleSystem.Authorization.Roles;
using ResponsibleSystem.Authorization.Users;
using ResponsibleSystem.Authorization.Permissions;
using ResponsibleSystem.Configuration;
using ResponsibleSystem.Extensions;

namespace ResponsibleSystem.EntityFrameworkCore.Seed.Host
{
    public class HostRoleAndUserCreator
    {
        private readonly ResponsibleSystemDbContext _context;

        public HostRoleAndUserCreator(ResponsibleSystemDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateRoles();
            CreateMasterAdmin();
            CreateAdmin();
        }

        protected virtual void CreateRoles()
        {
            CreateRoleAndPermissions(AppUserRole.Farmer);
            CreateRoleAndPermissions(AppUserRole.Salt);
            CreateRoleAndPermissions(AppUserRole.ShoemakerAemme);
            CreateRoleAndPermissions(AppUserRole.ShoemakerFramat);
            CreateRoleAndPermissions(AppUserRole.ShoemakerItaly);
            CreateRoleAndPermissions(AppUserRole.Slaughterhouse);
            CreateRoleAndPermissions(AppUserRole.Storage);
            CreateRoleAndPermissions(AppUserRole.TannerySwe);
            CreateRoleAndPermissions(AppUserRole.TanneryIt);
            CreateRoleAndPermissions(AppUserRole.Admin);
        }

        protected virtual void CreateRoleAndPermissions(AppUserRole roleType)
        {

            var roleFromDatabase = _context.Roles
                .IgnoreQueryFilters()
                .FirstOrDefault(r => r.TenantId == null &&
                                     r.Name == roleType.GetDescriptionFromValue());
            if (roleFromDatabase == null)
            {
                roleFromDatabase = new Role(null,
                    roleType.GetDescriptionFromValue(),
                    roleType.GetDescriptionFromValue())
                {
                    IsStatic = true,
                    IsDefault = false
                };
                _context.Roles.Add(roleFromDatabase);
                _context.SaveChanges();
            }

            var activeRolePermissions = _context.RolePermissions
                .Where(p => p.RoleId == roleFromDatabase.Id && p.IsGranted).ToList();

            // Grant all permissions
            var permissionNames = StaticRolePermissions.RolePermissions.ContainsKey(roleType) ?
                StaticRolePermissions.RolePermissions[roleType] : 
                new List<string>();
            foreach (var permission in permissionNames)
            {
                if (activeRolePermissions.Any(p => p.Name == permission))
                    continue;

                _context.Permissions.Add(
                    new RolePermissionSetting
                    {
                        TenantId = null,
                        Name = permission,
                        IsGranted = true,
                        RoleId = roleFromDatabase.Id
                    });
            }

            // remove not applyable permissions

            foreach (var activePermission in activeRolePermissions)
            {
                if (!permissionNames.Contains(activePermission.Name))
                    activePermission.IsGranted = false;
            }

            _context.SaveChanges();
        }

        protected virtual void CreateAdmin()
        {
            var adminRoleForHost = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == null && r.Name == AppUserRole.Admin.GetDescriptionFromValue());
            var adminUserForHost = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.TenantId == null && u.EmailAddress == AppConfig.Defaults.AdminEmail);
            if (adminUserForHost == null)
            {
                var user = new User
                {
                    TenantId = null,
                    UserName = AppConfig.Defaults.AdminUserName,
                    Name = AppConfig.Defaults.AdminName,
                    Surname = AppConfig.Defaults.AdminSurname,
                    EmailAddress = AppConfig.Defaults.AdminEmail,
                    IsEmailConfirmed = true,
                    IsActive = true,
                    Password = AppConfig.User.DefaultPasswordEncrypted,
                    UserRole = AppUserRole.Admin
                };

                user.SetNormalizedNames();

                adminUserForHost = _context.Users.Add(user).Entity;
                _context.SaveChanges();

                // Assign Admin role to admin user
                _context.UserRoles.Add(new UserRole(null, adminUserForHost.Id, adminRoleForHost.Id));
                _context.SaveChanges();

                // User account of admin user
                _context.UserAccounts.Add(new UserAccount
                {
                    TenantId = null,
                    UserId = adminUserForHost.Id,
                    UserName = adminUserForHost.UserName,
                    EmailAddress = adminUserForHost.EmailAddress
                });

                _context.SaveChanges();
            }
        }

        protected void CreateMasterAdmin()
        {
            var masterRoleForHost = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == null && r.Name == AppUserRole.Admin.GetDescriptionFromValue());
            var masterUserForHost = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.TenantId == null && u.EmailAddress == AppConfig.Defaults.AdminEmail);
            if (masterUserForHost == null && masterRoleForHost != null)
            {
                var user = new User
                {
                    TenantId = null,
                    UserName = AppConfig.Defaults.MasterUserName,
                    Name = AppConfig.Defaults.MasterName,
                    Surname = AppConfig.Defaults.MasterSurname,
                    EmailAddress = AppConfig.Defaults.MasterEmail,
                    IsEmailConfirmed = true,
                    IsActive = true,
                    Password = AppConfig.User.DefaultPasswordEncrypted,
                    UserRole = AppUserRole.Admin
                };

                user.SetNormalizedNames();

                masterUserForHost = _context.Users.Add(user).Entity;
                _context.SaveChanges();

                // Assign Master role to master user
                _context.UserRoles.Add(new UserRole(null, masterUserForHost.Id, masterRoleForHost.Id));
                _context.SaveChanges();

                // User account of admin user
                _context.UserAccounts.Add(new UserAccount
                {
                    TenantId = null,
                    UserId = masterUserForHost.Id,
                    UserName = masterUserForHost.UserName,
                    EmailAddress = masterUserForHost.EmailAddress
                });
                _context.SaveChanges();
            }
        }
    }
}
