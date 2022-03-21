namespace WorkDiaryWebApp.Core.Constants
{
    public static class MessageConstant
    {

        //toabstr
        public const string ErrorMessage = "ErrorMessage";
        public const string SuccessMessage = "SuccessMessage";
        public const string WarningMessage = "WarningMessage";

        //repeat data

        public const string DoubleEntity = "This kind of data, already exist!";

        //attribute errors

        public const string INVALID_HUMAN_NAME = "Not valid name! Must be one word from chars!";
        public const string INVALID_PHONE_NUMBER = "Please enter valid phone number!";
        public const string NOT_EQUAL_PASSOWRDS = "Password and Confirm Password must be same!";
    }
}
