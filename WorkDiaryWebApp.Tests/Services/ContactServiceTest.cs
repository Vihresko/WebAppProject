using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using WorkDiaryWebApp.Core.Services;
using WorkDiaryWebApp.Tests.Mocks;
using WorkDiaryWebApp.WorkDiaryDB.Models;
using Xunit;

namespace WorkDiaryWebApp.Tests.Services
{
    public class ContactServiceTest
    {
        [Fact]
        public async Task GetAllContacts_Must_Return_All_User_Contacts()
        {
            using var data = DatabaseMock.Instance;
            var contactService = new ContactService(data);

            var contactUser = new Contact()
            {
                Email = "user@abc.bg",
                PhoneNumber = "0888776655"
            };

            var user = new User()
            {
                UserName = "user",
                ContactId = contactUser.Id,
                FullName = "Test Testov",
                Bank = new Bank()
            };

            IdentityRole roleU = new IdentityRole();
            roleU.Name = "User";

            IdentityUserRole<string> userRole = new IdentityUserRole<string>();
            userRole.UserId = user.Id;
            userRole.RoleId = roleU.Id;

            await data.Roles.AddAsync(roleU);
            await data.Users.AddAsync(user);
            await data.Contacts.AddAsync(contactUser);
            await data.UserRoles.AddAsync(userRole);

            var contactGuest = new Contact()
            {
               Email = "guest@abc.bg",
               PhoneNumber = "0778121314"
            };

            var guest = new User()
            {
                UserName = "guest",
                ContactId = contactGuest.Id,
                FullName = "Test Testov",
                Bank = new Bank()
            };
            IdentityRole roleG = new IdentityRole();
            roleG.Name = "Guest";
            var guestRole = new IdentityUserRole<string>();
            guestRole.UserId = guest.Id;
            guestRole.RoleId = roleG.Id;

            await data.Roles.AddAsync(roleG);
            await data.Users.AddAsync(guest);
            await data.Contacts.AddAsync(contactGuest);
            await data.UserRoles.AddAsync(guestRole);

            await data.SaveChangesAsync();

            var result = await contactService.GetAllContacts();

            int count = result.Count;
            Assert.Equal(1, count);
            Assert.Equal("user@abc.bg", result.First().Email);

        }
    }
}
