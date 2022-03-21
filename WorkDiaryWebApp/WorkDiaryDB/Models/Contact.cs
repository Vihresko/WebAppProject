using System.ComponentModel.DataAnnotations;
using WorkDiaryWebApp.Core.Constants;

namespace WorkDiaryWebApp.WorkDiaryDB.Models
{
    using static WorkDiaryDB.Constraints.Constants;
    public class Contact
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [MaxLength(PHONE_NUMBER_MAX_LENGHT)]
        [RegularExpression(PHONE_NUMBER_REGEX, ErrorMessage = MessageConstant.INVALID_PHONE_NUMBER)]
        public string PhoneNumber { get;  set; }

        [Required]
        [MaxLength(EMAIL_MAX_LENGTH)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(TOWN_NAME_MAX_LENGTH)]
        public string? Town { get;  set; }

        [MaxLength(ADDRESS_MAX_LENGTH)]
        public string? Address { get;  set; }
    }
}
