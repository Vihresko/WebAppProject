using WorkDiary.Database.Data;
using WorkDiary.Database.Data.Models;
using WorkDiary.Database.DataProcess;

WorkDiaryDbContext dbContext = new WorkDiaryDbContext();
dbContext.Database.EnsureCreated();

Client client = new Client()
{
    BirthDay = DateTime.Now,
    Email = "furi@abv.bg",
    FirstName = "Testo",
    LastName = "Testov",
    PhoneNumber = "0778867",

};

ClientEditor Adding = new ClientEditor();
Adding.AddClient(client, dbContext);