using System.ComponentModel.DataAnnotations;

namespace ResponsibleSystem.Backoffice.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}