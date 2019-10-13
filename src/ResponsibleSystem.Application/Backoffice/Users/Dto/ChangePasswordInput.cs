using System.ComponentModel.DataAnnotations;

namespace ResponsibleSystem.Backoffice.Users.Dto
{
    public class ChangePasswordInput
    {
        [Required]
        public int UserId { get; set; }

        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        public bool OldPasswordRequired { get; set; }
    }
}