using System.ComponentModel.DataAnnotations;
using WorkDiaryWebApp.WorkDiaryDB.Constraints;

namespace WorkDiaryWebApp.Models.Procedure
{
    public class ShowProcedureModel
    {
        [MinLength(Constants.PROCEDURE_NAME_MIN_LENGTH)]
        [MaxLength(Constants.PROCEDURE_NAME_MAX_LENGTH)]
        public string Name { get; set; }

        [MaxLength(Constants.PROCEDURE_DESCRIPTION_MAX_LENGTH)]
        public string Description { get; set; }

        [Range((double) Constants.PROCEDURE_MIN_PRICE, (double) Constants.PROCEDURE_MAX_PRICE)]
        public decimal Price { get; set; }
        public string Id { get; set; }
        public bool IsActive { get; set; }

        public DateTime? DateForHistory { get; set; }
    }
}
