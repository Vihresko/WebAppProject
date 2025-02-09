﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WorkDiaryWebApp.Core.Constants;
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
        private readonly UserManager<User> userManager;
        public AdminService(WorkDiaryDbContext _database, RoleManager<IdentityRole> _roleManager, UserManager<User> _userManager)
        {
            database = _database;
            roleManager = _roleManager;
            userManager = _userManager;
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
                var rolesId = await database.UserRoles.Where(r => r.UserId == user.UserId).Select(r => r.RoleId).ToArrayAsync();
                var rolesNames = await database.Roles.Where(r => rolesId.Contains(r.Id)).Select(r => r.Name).ToArrayAsync();
                
                user.Roles = rolesNames;
            }

            allUsers.AddRange(usersFromDb);
            return allUsers.OrderByDescending(u => u.Roles.Count()).ToList();
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

        public async Task CreateRolesAndMainBank()
        {
            await roleManager.CreateAsync(new IdentityRole()
            {
                Name = UserConstant.Role.ADMINISTRATOR
            });

            await roleManager.CreateAsync(new IdentityRole()
            {
                Name = UserConstant.Role.USER
            });

            await roleManager.CreateAsync(new IdentityRole()
            {
                Name = UserConstant.Role.GUEST
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

        public async Task<(bool, string)> UpdateUser(ShowUserInfoModel model)
        {
            if(model == null)
            {
                return (false, "Corrupted data!");
            }

            var errors = new StringBuilder();
            bool isValid = true;
            var currentUser = await database.Users.FirstOrDefaultAsync(u => u.Id == model.UserId);
            var currentRolesId = await database.UserRoles.Where(ur => ur.UserId == model.UserId).Select(ur => ur.RoleId).ToArrayAsync();
            var currentRolesNames = await database.Roles.Where(r => currentRolesId.Contains(r.Id)).Select(r=> r.Name).ToArrayAsync();
            var currentContact = await database.Contacts.FirstOrDefaultAsync(u => u.Id == currentUser.ContactId);

            if(currentUser == null)
            {
                return (false, "Not existing user!");
            }

            if(currentUser.UserName != model.Username)
            {
                if(await database.Users.AnyAsync(u => u.UserName == model.Username))
                {
                    isValid = false;
                    errors.AppendLine($"This username {model.Username} already exist");
                }
                else
                {
                    currentUser.UserName = model.Username;
                }
            }

            if(currentUser.FullName != model.FullName)
            {
                currentUser.FullName = model.FullName;
            }

            if(currentContact.Email != model.Email)
            {
                currentContact.Email = model.Email;
            }

            var adminRoleId = await database.Roles.Where(r => r.Name == UserConstant.Role.ADMINISTRATOR).Select(r => r.Id).FirstOrDefaultAsync();
            var admins = await database.UserRoles.Where(ur => ur.RoleId == adminRoleId).ToListAsync();

            if(admins.Count < 2 && admins.Any(a => a.UserId == currentUser.Id) && !model.Roles.Contains(UserConstant.Role.ADMINISTRATOR))
            {
                return (false, "This is last Admin, can not remove it!");
            }

            if (currentRolesNames != null)
            {

                var removeUserRoles = await userManager.RemoveFromRolesAsync(currentUser, currentRolesNames);
            }

            var addUserRoles = await userManager.AddToRolesAsync(currentUser, model.Roles);

            if (isValid)
            {
                await database.SaveChangesAsync();
            }

            return (isValid, errors.ToString());
        }
    }
}

