using Microsoft.EntityFrameworkCore;
using System;
using WorkDiaryWebApp.WorkDiaryDB;

namespace WorkDiaryWebApp.Tests.Mocks
{
    public static class DatabaseMock
    {
        public static WorkDiaryDbContext Instance
        {
            get
            {
                var options = new DbContextOptionsBuilder<WorkDiaryDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;

                return new WorkDiaryDbContext(options);
            }
        }
    }
}
