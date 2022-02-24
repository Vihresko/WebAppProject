using System.ComponentModel.DataAnnotations;

namespace WorkDiaryDB.Models
{
    using static Constraints.Constants;
    public class Contact
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [MaxLength(PHONE_NUMBER_MAX_LENGHT)]
        public string PhoneNumber { get; private set; }

        [MaxLength(TOWN_NAME_MAX_LENGTH)]
        public string? Town { get; private set; }

        [MaxLength(ADDRESS_MAX_LENGTH)]
        public string? Address { get; private set; }
    }
}
