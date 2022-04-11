using System.ComponentModel.DataAnnotations;

namespace WorkDiaryWebApp.Models.Outcome
{
    public class AddOutcomeModel
    {
        [Required]
        [MaxLength(120)]
        public string Description { get; set; }

        public decimal Value { get; set; }

        public decimal? MainBankVault { get; set; }
    }
}
