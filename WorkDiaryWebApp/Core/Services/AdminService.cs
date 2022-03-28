using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.Models.Admin;
using WorkDiaryWebApp.WorkDiaryDB;
using WorkDiaryWebApp.WorkDiaryDB.Models;

namespace WorkDiaryWebApp.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly WorkDiaryDbContext database;
        private readonly RoleManager<IdentityRole> roleManager;


        public AdminService(WorkDiaryDbContext _database, RoleManager<IdentityRole> _roleManager)
        {
            database = _database;
            roleManager = _roleManager;
        }

        public async Task<List<ShowUserInfoModel>> GetUsersInfo()
        {
           var allUsers = new List<ShowUserInfoModel>();

            var usersFromDb = await database.Users.Select(u => new ShowUserInfoModel()
            {
                Username = u.UserName,
                FullName = u.FullName,
                UserId = u.Id,
                UserBankId = u.BankId,
            }).ToListAsync();

            foreach (var user in usersFromDb)
            {
                var roleId = await database.UserRoles.Where(r => r.UserId == user.UserId).Select(r => r.RoleId).FirstOrDefaultAsync();
                var role = await database.Roles.Where(r => r.Id == roleId).Select(r => r.Name).ToArrayAsync();
                
                user.Roles = role;
            }

            allUsers.AddRange(usersFromDb);
            return allUsers;
        }

        public async Task<User> IsThatFirstRegistration()
        {
            var numberOfUsers = await database.Users.CountAsync();
            User user = null;
            if (numberOfUsers == 1)
            {
                user = await database.Users.FirstAsync();
            }
            return user;
        }

        public async Task CreateAdminRoleAndMainBank()
        {
            await roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Admin"
            });

            var mainBank = new MainBank();
            await database.MainBanks.AddAsync(mainBank);
            await database.SaveChangesAsync();
        }

        public async Task<ShowUserInfoModel> GetUserInfo(string userId)
        {
            var user = await database.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var contact = await database.Contacts.Where(c => c.Id == user.ContactId).FirstOrDefaultAsync();
            var userModel = new ShowUserInfoModel()
            {
                UserId = userId,
                FullName = user.FullName,
                Email = contact.Email,
                UserBankId = user.BankId,
                Username = user.UserName
            };

            var rolesId = await database.UserRoles.Where(r => r.UserId == userId).Select(r => r.RoleId).ToArrayAsync();
            var roles = await database.Roles.Where(r => rolesId.Contains(r.Id)).Select(r => r.Name).ToArrayAsync();
            userModel.Roles = roles;
            return userModel;
        }
    }
}

