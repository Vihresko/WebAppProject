using System.ComponentModel.DataAnnotations;
using WorkDiaryWebApp.WorkDiaryDB.Constraints;

namespace WorkDiaryWebApp.Models.Admin
{
    public class ShowUserInfoModel
    {
        public string UserId { get; set; }

        [Required]
        [MinLength(Constants.USERNAME_MIN_LENGTH)]
        [MaxLength(Constants.USERNAME_MAX_LENGTH)]
        public string Username { get; set; }
        public string? UserBankId { get; set; }

        [Required]
        [MaxLength(Constants.FULL_NAME_MAX_LENGTH)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string[]? Roles { get; set; } = null;

    }
}
