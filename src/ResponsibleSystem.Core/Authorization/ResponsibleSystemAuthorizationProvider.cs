using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;
using ResponsibleSystem.Authorization.Permissions;
using ResponsibleSystem.Configuration;

namespace ResponsibleSystem.Authorization
{
    public class ResponsibleSystemAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Farms, L("Farms"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Leather, L("Leather"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Leather_Add, L("Leather_Add"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Leather_Edit, L("Leather_Edit"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Leather_Delete, L("Leather_Delete"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Leather_SelectFarm, L("Leather_SelectFarm"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Leather_Details, L("Leather_Details"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Slaughterhouse, L("Slaughterhouse"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Tannery, L("Tannery"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Inventory, L("Inventory"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Production, L("Production"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Production_Admin, L("Production_Admin"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Shoemaker_Step1, L("Shoemaker_Step1"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Shoemaker_Step2, L("Shoemaker_Step2"), multiTenancySides: MultiTenancySides.Host);

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AppConfig.LocalizationSourceName);
        }
    }
}
