using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkDiaryWebApp.WorkDiaryDB.Models
{
    public class VisitBag
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

    }
}
