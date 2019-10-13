using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Runtime.Validation;

namespace ResponsibleSystem.Backoffice.Users.Dto
{
    public class CreateUserDto : IShouldNormalize
    {
        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        public bool IsActive { get; set; }
        public string UserRole { get; set; }

        public long? FarmId { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }

        public int? TenantId { get; set; }

        public void Normalize()
        {
            string NormalizeString(string val) => val?.Trim().Length == 0 ? null : val;

            Name = NormalizeString(Name);
            Surname = NormalizeString(Surname);
            EmailAddress = NormalizeString(EmailAddress)?.ToLower();
        }
    }
}
