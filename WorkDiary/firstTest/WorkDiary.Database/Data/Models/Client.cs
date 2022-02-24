using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkDiary.Database.Common;

namespace WorkDiary.Database.Data.Models
{
    public class Client
    {
        public Client()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Procedures = new HashSet<Procedure>();
        }
        [Key]
        public string Id { get; init; }
        [Required]
        [MaxLength(Constants.CLIENT_NAME_MAX_LENGTH)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(Constants.CLIENT_NAME_MAX_LENGTH)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(Constants.EMAIL_MAX_LENGTH)]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }
        public DateTime BirthDay { get; set; }
        public ICollection<Procedure> Procedures { get; set; }

    }
}
