using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkDiary.Database.Data.Models
{
    public class Procedure
    {
        [Key]
        public int Id{ get; set; }
        [Required]
        public string Name{ get; set; }
        public string Description{ get; set; }
        public DateTime Date { get; set; }

        [ForeignKey(nameof(Client))]
        public string ClientId { get; set; }
        public Client Client { get; set; }


    }
}
