using System.ComponentModel.DataAnnotations;

namespace ResponsibleSystem.Backoffice.Users.Dto
{
    public class ResetPasswordInput
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}