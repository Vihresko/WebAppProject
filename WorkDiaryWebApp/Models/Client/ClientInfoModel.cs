using System.ComponentModel.DataAnnotations;
using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.WorkDiaryDB.Constraints;

namespace WorkDiaryWebApp.Models.Client
{
    public class ClientInfoModel
    {
        public string Id { get; set; }

        [MaxLength(Constants.FIRST_NAME_MAX_LENGTH)]
        [MinLength(Constants.NAME_MIN_LENGTH)]
        [Required]

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string BirthDay { get; set; }

        public bool IsActive { get; set; }

    }
}
