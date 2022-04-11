using System.Text;
using WorkDiaryWebApp.Models.User;

namespace WorkDiaryWebApp.Core.Interfaces
{
    public interface IUserService
    {
        public Task<(bool, StringBuilder, string)> RegisterNewUser(RegisterFormModel model);
        public Task<bool> TryToLogin(LoginFormModel model);
    }
}
