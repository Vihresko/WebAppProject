using WorkDiaryWebApp.Models.Admin;
using WorkDiaryWebApp.WorkDiaryDB.Models;

namespace WorkDiaryWebApp.Core.Interfaces
{
    public interface IAdminService
    {
        public Task<List<ShowUserInfoModel>> GetUsersInfo();

        public Task<User> IsThatFirstRegistration();

        public Task CreateRolesAndMainBank();

        public Task<ShowUserInfoModel> GetUserInfo(string userId);

        public Task<(bool, string)> UpdateUser(ShowUserInfoModel user);

        

    }
}
