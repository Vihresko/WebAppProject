using System.ComponentModel.DataAnnotations;
using WorkDiaryWebApp.WorkDiaryDB.Constraints;

namespace WorkDiaryWebApp.Models.Procedure
{
    public class AddProcedureModel
    {
        [MaxLength(Constants.PROCEDURE_NAME_MAX_LENGTH)]
        [MinLength(Constants.PROCEDURE_NAME_MIN_LENGTH)]
        [Required]
        public string Name { get; set; }

        [MaxLength(Constants.PROCEDURE_DESCRIPTION_MAX_LENGTH)]
        public string? Description { get; set; }

        [Range((double)Constants.PROCEDURE_MIN_PRICE, (double)Constants.PROCEDURE_MAX_PRICE)]
        public decimal Price { get; set; }
    }
}
