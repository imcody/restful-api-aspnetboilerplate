using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;

namespace ResponsibleSystem.Models.TokenAuth
{
    public class TokenAuthenticateModel
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        public string Password { get; set; }

        public bool RememberClient { get; set; }
    }
}