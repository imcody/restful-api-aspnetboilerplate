using System.ComponentModel;

namespace ResponsibleSystem.Authorization.Roles
{
    public enum AppUserRole
    {
        [Description("Admin")]
        Admin = 0,

        [Description("Farmer")]
        Farmer = 1,

        [Description("Slaughterhouse")]
        Slaughterhouse = 2,

        [Description("Salt")]
        Salt = 3,

        [Description("Tannery Sweden")]
        TannerySwe = 4,

        [Description("Tannery Italy")]
        TanneryIt = 5,

        [Description("Storage")]
        Storage = 6,

        [Description("Shoemaker Framat")]
        ShoemakerFramat = 7,

        [Description("Shoemaker Aemme")]
        ShoemakerAemme = 8,

        [Description("Shoemaker Italy")]
        ShoemakerItaly = 9,
    }
}