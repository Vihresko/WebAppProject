namespace WorkDiaryWebApp.WorkDiaryDB.Constraints
{
    public class Constants
    {
        //Common
        public const int EMAIL_MAX_LENGTH = 254;
        public const string BIRTH_DAY_REGEX = @"^[0-9]{2}/[0-9]{2}/[0-9]{4}$";
        public const string HUMAN_NAMES_REGEX = @"^[A-z]+$";
        public const string PHONE_NUMBER_REGEX = @"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$";

        //Client
        public const int NAME_MIN_LENGTH = 2;
        public const int FIRST_NAME_MAX_LENGTH = 30;
        public const int LAST_NAME_MAX_LENGTH = 30;

        //Procedure
        public const int PROCEDURE_NAME_MIN_LENGTH = 3;
        public const int PROCEDURE_NAME_MAX_LENGTH = 30;
        public const int PROCEDURE_DESCRIPTION_MAX_LENGTH = 10000;
        public const decimal PROCEDURE_MIN_PRICE = 1;
        public const decimal PROCEDURE_MAX_PRICE = 1000000;

        //User
        public const int USERNAME_MIN_LENGTH = 3;
        public const int USERNAME_MAX_LENGTH = 20;
        public const int FULL_NAME_MAX_LENGTH = 61;
        public const int PASSWORD_MIN_VALUE = 6;
        public const int PASSWORD_MAX_LENGTH = 30;

        //Contact
        public const int PHONE_NUMBER_MAX_LENGHT = 20;
        public const int TOWN_NAME_MAX_LENGTH = 85;
        public const int ADDRESS_MAX_LENGTH = 100;

        //Income/Outcome
        public const int IN_OUT_COME_DESCRIPTION_MAX_LENGTH = 1000;

        //MainBank
        public const int MAIN_BANK_NAME_MAX_LENGTH = 50;
       
    }
}
