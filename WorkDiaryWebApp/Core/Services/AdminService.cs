using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.Models.Admin;
using WorkDiaryWebApp.WorkDiaryDB;

namespace WorkDiaryWebApp.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly WorkDiaryDbContext database;

        public AdminService(WorkDiaryDbContext _database)
        {
            database = _database;
        }
        
        public List<ShowUserInfoModel> GetUsersInfo()
        {
           var allUsers = new List<ShowUserInfoModel>();

            var usersFromDb = database.Users.Select(u => new ShowUserInfoModel()
            {
                Username = u.UserName,
                FullName = u.FullName,
                UserId = u.Id,
                UserBankId = u.BankId,
                Email = u.Email,
            }).ToList();

            foreach (var user in usersFromDb)
            {
                var roleId = database.UserRoles.Where(r => r.UserId == user.UserId).Select(r => r.RoleId).FirstOrDefault();
                var role = database.Roles.Where(r => r.Id == roleId).Select(r => r.Name).FirstOrDefault();
                
                user.Role = role;
            }

            allUsers.AddRange(usersFromDb);
            return allUsers;
        }
    }
}

