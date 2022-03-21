namespace WorkDiaryWebApp.Models.User
{
    using System.ComponentModel.DataAnnotations;
    using WorkDiaryWebApp.Core.Constants;
    using WorkDiaryWebApp.WorkDiaryDB.Constraints;
    public class RegisterFormModel
    {
        [MinLength(Constants.USERNAME_MIN_LENGTH)]
        [MaxLength(Constants.USERNAME_MAX_LENGTH)]
        public string Username { get; set; }

        [MinLength(Constants.NAME_MIN_LENGTH)]
        [MaxLength(Constants.FIRST_NAME_MAX_LENGTH)]
        [RegularExpression(Constants.HUMAN_NAMES_REGEX, ErrorMessage = MessageConstant.INVALID_HUMAN_NAME)]
        public string FirstName { get; set; }

        [MinLength(Constants.NAME_MIN_LENGTH)]
        [MaxLength(Constants.LAST_NAME_MAX_LENGTH)]
        [RegularExpression(Constants.HUMAN_NAMES_REGEX, ErrorMessage = MessageConstant.INVALID_HUMAN_NAME)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MinLength(Constants.PASSWORD_MIN_VALUE)]
        [MaxLength(Constants.PASSWORD_MAX_LENGTH)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = MessageConstant.NOT_EQUAL_PASSOWRDS)]
        public string ConfirmPassword { get; set; }

        [MaxLength(Constants.PHONE_NUMBER_MAX_LENGHT)]
        [RegularExpression(Constants.PHONE_NUMBER_REGEX, ErrorMessage = MessageConstant.INVALID_PHONE_NUMBER)]
        public string PhoneNumber { get; set; }

        [MaxLength(Constants.TOWN_NAME_MAX_LENGTH)]
        public string Town { get; set; }

        [MaxLength(Constants.ADDRESS_MAX_LENGTH)]
        public string Address { get; set; }

    }
}
