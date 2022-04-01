using System.Linq;
using System.Threading.Tasks;
using WorkDiaryWebApp.Core.Services;
using WorkDiaryWebApp.Models.Procedure;
using WorkDiaryWebApp.Tests.Mocks;
using Xunit;

namespace WorkDiaryWebApp.Tests.Services
{
    public class ProcedureServiceTest
    {
        private const string CREATE = "Create";
        private const int PRICE = 1;
        private const string UPDATE = "Update";
        [Fact]
        public async Task AddProcedureMustCreateNewProcedureInDb()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var procedureService = new ProcedureService(data);
            //Act
            await procedureService.AddNewProcedure(new AddProcedureModel
            {
                 Name = CREATE,
                 Description = CREATE,
                 Price = PRICE

            });
            //Assert
            Assert.Equal(1, data.Procedures.Count());
            Assert.Equal(CREATE, data.Procedures.First().Name);
            Assert.Equal(CREATE, data.Procedures.First().Description);
            Assert.Equal(PRICE, data.Procedures.First().Price);
        }

        [Fact]
        public async Task EditProcedureMustEditEntityInDb()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var procedureService = new ProcedureService(data);
            //Act
            await procedureService.AddNewProcedure(new AddProcedureModel
            {
                Name = CREATE,
                Description = CREATE,
                Price = PRICE,
            });

            var procedureId = data.Procedures.First().Id;
            await procedureService.EditProcedure(new ShowProcedureModel
            {
                Id = procedureId,
                Name = UPDATE,
                Description = UPDATE,
                Price = 2
            });

            Assert.NotEqual(PRICE, data.Procedures.FirstOrDefault()?.Price);
            Assert.NotEqual(CREATE, data.Procedures.FirstOrDefault()?.Name);
            Assert.NotEqual(CREATE,data.Procedures.FirstOrDefault()?.Description);
        }
    }
}
