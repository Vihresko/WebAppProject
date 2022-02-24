using System.ComponentModel.DataAnnotations;

namespace WorkDiaryDB.Models
{
    using static Constraints.Constants;
    public class Client
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(FIRST_NAME_MAX_LENGTH)]
        public string FirstName { get; private set; }

        [MaxLength(LAST_NAME_MAX_LENGTH)]
        public string? LastName { get; private set; }

        [Required]
        [MaxLength(EMAIL_MAX_LENGTH)]
        public string Email { get; private set; }

        public DateTime BirthDay { get; private set; }

        public virtual ICollection<ClientProcedure> Procedures { get; set; } = new HashSet<ClientProcedure>();
    }
}
