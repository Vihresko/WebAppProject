using WorkDiaryWebApp.Models.Admin;
using WorkDiaryWebApp.WorkDiaryDB.Models;

namespace WorkDiaryWebApp.Core.Interfaces
{
    public interface IAdminService
    {
        public Task<List<ShowUserInfoModel>> GetUsersInfo();

        public Task<User> IsThatFirstRegistration();

        public Task CreateAdminRoleAndMainBank();

    }
}
