using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkDiaryWebApp.WorkDiaryDB.Models
{
    using static WorkDiaryDB.Constraints.Constants;
    public class User : IdentityUser
    {

        [Required]
        [MaxLength(FULL_NAME_MAX_LENGTH)]
        public string FullName { get; set; }

        public virtual ICollection<ClientProcedure> UserPlayers { get; set; } = new HashSet<ClientProcedure>();

        [ForeignKey(nameof(Contact))]
        public string  ContactId { get; set; }
        public virtual Contact Contact { get; set; }

        [ForeignKey(nameof(Bank))]
        public string BankId { get; private set; }
        public virtual Bank Bank { get; set; }

        //TODO:IsAdmin?
    }
}