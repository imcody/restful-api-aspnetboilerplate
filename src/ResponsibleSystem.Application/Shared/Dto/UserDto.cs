using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Navigation;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.Runtime.Validation;

namespace ResponsibleSystem.Shared.Dto
{
    public class UserDto : EntityDto<long>
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

        public long? FarmId { get; set; }

        public bool IsActive { get; set; }

        public string FullName { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public DateTime CreationTime { get; set; }
        
        public string UserRole { get; set; }

        public int? TenantId { get; set; }

        public string AvatarUrl { get; set; }

        public override string ToString()
        {
            return ($"{FullName}").Trim();
        }
    }
}
