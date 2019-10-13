using Abp.MultiTenancy;
using Abp.Zero.Configuration;
using ResponsibleSystem.Authorization.Permissions;
using ResponsibleSystem.Extensions;

namespace ResponsibleSystem.Authorization.Roles
{
    public static class AppRoleConfig
    {
        public static void Configure(IRoleManagementConfig roleManagementConfig)
        {
            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    AppUserRole.Admin.GetDescriptionFromValue(),
                    MultiTenancySides.Host
                )
            );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    AppUserRole.Farmer.GetDescriptionFromValue(),
                    MultiTenancySides.Host
                )
            );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    AppUserRole.Salt.GetDescriptionFromValue(),
                    MultiTenancySides.Host
                )
            );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    AppUserRole.ShoemakerAemme.GetDescriptionFromValue(),
                    MultiTenancySides.Host
                )
            );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    AppUserRole.ShoemakerFramat.GetDescriptionFromValue(),
                    MultiTenancySides.Host
                )
            );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    AppUserRole.ShoemakerItaly.GetDescriptionFromValue(),
                    MultiTenancySides.Host
                )
            );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    AppUserRole.Slaughterhouse.GetDescriptionFromValue(),
                    MultiTenancySides.Host
                )
            );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    AppUserRole.Storage.GetDescriptionFromValue(),
                    MultiTenancySides.Host
                )
            );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    AppUserRole.TanneryIt.GetDescriptionFromValue(),
                    MultiTenancySides.Host
                )
            );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    AppUserRole.TannerySwe.GetDescriptionFromValue(),
                    MultiTenancySides.Host
                )
            );

        }
    }
}
