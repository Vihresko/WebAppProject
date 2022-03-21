using System.ComponentModel.DataAnnotations;
using WorkDiaryWebApp.WorkDiaryDB.Constraints;

namespace WorkDiaryWebApp.Models.Client
{
    public class AddClientModel
    {
       
        [MinLength(Constants.NAME_MIN_LENGTH)]
        [MaxLength(Constants.FIRST_NAME_MAX_LENGTH)]
        
        public string FirstName { get;  set; }

        [MinLength(Constants.NAME_MIN_LENGTH)]
        [MaxLength(Constants.LAST_NAME_MAX_LENGTH)]
        public string? LastName { get;  set; }

        [EmailAddress]
        [MaxLength(Constants.EMAIL_MAX_LENGTH)]
        public string Email { get;  set; }

        public string BirthDay { get;  set; }
    }
}
