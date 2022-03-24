using WorkDiaryWebApp.Models.Admin;
using WorkDiaryWebApp.WorkDiaryDB.Models;

namespace WorkDiaryWebApp.Core.Interfaces
{
    public interface IAdminService
    {
        public List<ShowUserInfoModel> GetUsersInfo();

        public User IsThatFirstRegistration();

        public Task CreateAdminRoleAndMainBank();

    }
}
