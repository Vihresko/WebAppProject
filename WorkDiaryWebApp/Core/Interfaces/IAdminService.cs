using WorkDiaryWebApp.Models.Admin;

namespace WorkDiaryWebApp.Core.Interfaces
{
    public interface IAdminService
    {
        public List<ShowUserInfoModel> GetUsersInfo();
    }
}
