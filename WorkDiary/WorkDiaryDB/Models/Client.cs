using System.ComponentModel.DataAnnotations;

namespace WorkDiaryDB.Models
{
    using static Constraints.Constants;
    public class Client
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MinLength(NAME_MIN_LENGTH)]
        [MaxLength(FIRST_NAME_MAX_LENGTH)]
        public string FirstName { get;  set; }

        [Required]
        [MinLength(NAME_MIN_LENGTH)]
        [MaxLength(LAST_NAME_MAX_LENGTH)]
        public string LastName { get;  set; }

        [Required]
        [MaxLength(EMAIL_MAX_LENGTH)]
        public string Email { get;  set; }

        public DateTime BirthDay { get;  set; }

        public virtual ICollection<ClientProcedure> Procedures { get; set; } = new HashSet<ClientProcedure>();

        public bool IsActive { get; set; } = true;

    }
}
