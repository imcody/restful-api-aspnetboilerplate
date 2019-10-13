using System;
using ResponsibleSystem.Authorization.Roles;
using ResponsibleSystem.Extensions;

namespace ResponsibleSystem.Exceptions
{
    public static class AppExtensions
    {
        public static AppUserRole GetUserRole(string roleName)
        {
            return EnumExtensions.GetValueFromDescription<AppUserRole>(roleName) ?? default(AppUserRole);
        }
    }
}
