using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}