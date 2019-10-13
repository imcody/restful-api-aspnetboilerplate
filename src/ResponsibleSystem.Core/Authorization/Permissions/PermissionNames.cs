using System.Collections.Generic;
using ResponsibleSystem.Authorization.Roles;

namespace ResponsibleSystem.Authorization.Permissions
{
    public static class PermissionNames
    {
        public const string Pages_Users = "Pages.Users";
        public const string Pages_Farms = "Pages.Farms";

        public const string Pages_Leather = "Pages.Leather";
        public const string Leather_Add = "Leather.Add";
        public const string Leather_Edit = "Leather.Edit";
        public const string Leather_Delete = "Leather.Delete";
        public const string Leather_SelectFarm = "Leather.SelectFarm";
        public const string Leather_Details = "Leather.Details";

        public const string Pages_Slaughterhouse = "Pages.Slaughterhouse";

        public const string Pages_Tannery = "Pages.Tannery";

        public const string Pages_Inventory = "Pages.Inventory";

        public const string Pages_Production = "Pages.Production";
        public const string Production_Admin = "Production.Admin";
        public const string Pages_Shoemaker_Step1 = "Pages.Shoemaker.Step1";
        public const string Pages_Shoemaker_Step2 = "Pages.Shoemaker.Step2";
    }

    public static class StaticRolePermissions
    {
        public static Dictionary<AppUserRole, List<string>> RolePermissions =
            new Dictionary<AppUserRole, List<string>>
            {
                {
                    AppUserRole.Admin,
                    new List<string>()
                    {
                        PermissionNames.Pages_Users,
                        PermissionNames.Pages_Farms,
                        PermissionNames.Pages_Leather,
                        PermissionNames.Leather_Add,
                        PermissionNames.Leather_Edit,
                        PermissionNames.Leather_Delete,
                        PermissionNames.Leather_SelectFarm,
                        PermissionNames.Leather_Details,
                        PermissionNames.Pages_Production,
                        PermissionNames.Production_Admin,
                    }
                },
                {
                    AppUserRole.Farmer,
                    new List<string>()
                    {
                        PermissionNames.Pages_Leather,
                        PermissionNames.Leather_Add,
                        PermissionNames.Leather_Edit,
                    }
                },
                {
                    AppUserRole.Slaughterhouse,
                    new List<string>()
                    {
                        PermissionNames.Pages_Slaughterhouse
                    }
                },
                {
                    AppUserRole.TannerySwe,
                    new List<string>()
                    {
                        PermissionNames.Pages_Tannery
                    }
                },
                {
                    AppUserRole.TanneryIt,
                    new List<string>()
                    {
                        PermissionNames.Pages_Tannery
                    }
                },
                {
                    AppUserRole.ShoemakerAemme,
                    new List<string>()
                    {
                        PermissionNames.Pages_Inventory,
                        PermissionNames.Pages_Production,
                        PermissionNames.Pages_Shoemaker_Step1
                    }
                },
                {
                    AppUserRole.ShoemakerFramat,
                    new List<string>()
                    {
                        PermissionNames.Pages_Inventory,
                        PermissionNames.Pages_Production,
                        PermissionNames.Pages_Shoemaker_Step1
                    }
                },
                {
                    AppUserRole.ShoemakerItaly,
                    new
                    List<string>()
                    {
                        PermissionNames.Pages_Inventory,
                        PermissionNames.Pages_Production,
                        PermissionNames.Pages_Shoemaker_Step2
                    }
                }
            };
    }
}
