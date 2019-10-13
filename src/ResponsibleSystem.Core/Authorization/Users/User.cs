using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Authorization.Users;
using Abp.Extensions;
using Abp.Runtime.Security;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ResponsibleSystem.Authorization.Roles;
using ResponsibleSystem.Entities;

namespace ResponsibleSystem.Authorization.Users
{
    public class User : AbpUser<User>
    {
        public AppUserRole UserRole { get; set; }

        [ForeignKey(nameof(Farm))]
        public long? FarmId { get; set; }
        public Farm Farm { get; set; }

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = emailAddress,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                UserRole = AppUserRole.Admin
            };

            user.SetNormalizedNames();
            return user;
        }
        public static long? GetUserIdFromToken(string userToken)
        {
            try
            {
                var resetToken = new SimpleStringCipher().Decrypt(userToken);
                var data = resetToken.Split('|');
                var userId = data[0];
                return long.Parse(userId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string GetUserEmailFromToken(string userToken)
        {
            try
            {
                var resetToken = new SimpleStringCipher().Decrypt(userToken);
                var data = resetToken.Split('|');
                return data[1];
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public static class UserExtensions
    {
        public static string GetUserToken(this User user)
        {
            return new SimpleStringCipher().Encrypt($"{user.Id}|{user.EmailAddress}|{DateTime.UtcNow.Ticks}");
        }
    }
}
