using Microsoft.AspNetCore.Identity;
using System.Text;
using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.Models.User;
using WorkDiaryWebApp.WorkDiaryDB;
using WorkDiaryWebApp.WorkDiaryDB.Models;

namespace WorkDiaryWebApp.Core.Services
{
    using static WorkDiaryWebApp.WorkDiaryDB.Constraints.Constants;
    public class UserService : IUserService
    {
        private readonly WorkDiaryDbContext database;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserService(WorkDiaryDbContext _database, UserManager<User> _userManager, SignInManager<User> _signInManager)
        {
            database = _database;
            userManager = _userManager;
            signInManager = _signInManager;
        }
        public async Task<(bool, StringBuilder)> RegisterNewUser(RegisterFormModel model)
        {
            (bool isValidModel, StringBuilder errors) = ValidateRegisterModel(model);
            (bool isEmailExist, string? emailError) = ValidateEmail(model.Email);

            if (isEmailExist) errors.AppendLine(emailError);

            if (isValidModel)
            {
                var newContact = new Contact()
                {
                    PhoneNumber = model.PhoneNumber.Trim(),
                    Address = model.Address,
                    Town = model.Town,
                    Email = model.Email
                };

                var newBank = new Bank();

                var newUser = new User()
                {
                    UserName = model.Username,
                    FullName = model.FirstName + " " + model.LastName,
                    Contact = newContact,
                    Bank = newBank
                };

                var result = await userManager.CreateAsync(newUser, model.Password);
                if (!result.Succeeded)
                {
                    var errorsFromUserManager = result.Errors.Select(e => e.Description);
                    foreach (var errorFromUserManager in errorsFromUserManager)
                    {
                        errors.AppendLine(errorFromUserManager);
                    }
                    isValidModel = false;
                }
                await database.SaveChangesAsync();
            }

            return (isValidModel, errors);
        }

        public async Task<bool> TryToLogin(LoginFormModel model)
        {

            var user = database.Users.FirstOrDefault(u => u.UserName == model.Username);
            bool isValid = false;
            if (user != null)
            {
                isValid = await userManager.CheckPasswordAsync(user, model.Password);
                if (isValid)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                }
            }

            return isValid;
        }

        private (bool, string?) ValidateEmail(string email)
        {
            var isEmailExist = database.Contacts.Where(c => c.Email == email).Any();
            if (isEmailExist)
            {
                return (true, $"{nameof(email)} already exist!");
            }

            return (false, null);
        }


        private (bool, StringBuilder) ValidateRegisterModel(RegisterFormModel model)
        {
            bool isValid = true;
            var errors = new StringBuilder();
            if (model.Username == null ||
                model.Username.Length < USERNAME_MIN_LENGTH ||
                model.Username.Length > USERNAME_MAX_LENGTH)
            {
                isValid = false;
                errors.AppendLine($"{nameof(model.Username)} must be between {USERNAME_MIN_LENGTH} and {USERNAME_MAX_LENGTH} characters!");
            }
            if (model.FirstName == null ||
               model.FirstName.Length < NAME_MIN_LENGTH ||
               model.FirstName.Length > FIRST_NAME_MAX_LENGTH)
            {
                isValid = false;
                errors.AppendLine($"{nameof(model.FirstName)} must be between {NAME_MIN_LENGTH} and {FIRST_NAME_MAX_LENGTH} characters!");
            }
            if (model.LastName == null ||
               model.LastName.Length < NAME_MIN_LENGTH ||
               model.LastName.Length > LAST_NAME_MAX_LENGTH)
            {
                isValid = false;
                errors.AppendLine($"{nameof(model.LastName)} must be between {NAME_MIN_LENGTH} and {LAST_NAME_MAX_LENGTH} characters!");
            }
            if (model.Email == null ||
               model.Email.Length > EMAIL_MAX_LENGTH)
            {
                isValid = false;
                errors.AppendLine($"{nameof(model.Email)} must be maximum from {EMAIL_MAX_LENGTH} symbols!");
            }

            if (model.Password == null ||
              model.Password.Length < PASSWORD_MIN_VALUE ||
              model.Password.Length > PASSWORD_MAX_LENGTH)
            {
                isValid = false;
                errors.AppendLine($"{nameof(model.Password)} must be between {PASSWORD_MIN_VALUE} and {PASSWORD_MAX_LENGTH} symbols!");
            }

            if (model.ConfirmPassword == null || model.ConfirmPassword != model.Password)
            {
                isValid = false;
                errors.AppendLine($"{nameof(model.Password)} and {nameof(model.ConfirmPassword)} must be same");
            }

            if (model.PhoneNumber == null || model.PhoneNumber.Length > PHONE_NUMBER_MAX_LENGHT)
            {
                isValid = false;
                errors.AppendLine($"{nameof(model.PhoneNumber)} must be maximum {PHONE_NUMBER_MAX_LENGHT} symbols!");
            }

            if (model.Town != null)
            {
                if (model.Town.Length > TOWN_NAME_MAX_LENGTH)
                {
                    isValid = false;
                    errors.AppendLine($"{nameof(model.PhoneNumber)} must be maximum from {TOWN_NAME_MAX_LENGTH} characters!");
                }
            }

            if (model.Address != null)
            {
                if (model.Address.Length > ADDRESS_MAX_LENGTH)
                {
                    isValid = false;
                    errors.AppendLine($"{nameof(model.Address)} must be maximum from {ADDRESS_MAX_LENGTH} characters!");
                }
            }

            return (isValid, errors);
        }

       
    }
}
