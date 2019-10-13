using System.ComponentModel.DataAnnotations;

namespace ResponsibleSystem.Backoffice.Users.Dto
{
    public class PasswordRecoveryInput
    {
        [Required]
        public string Email { get; set; }
    }
}