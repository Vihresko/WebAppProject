using Microsoft.EntityFrameworkCore;
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
        public async Task Add_Procedure_Must_Create_New_Procedure_In_Db()
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
            var createdProcedure = data.Procedures.FirstOrDefault();
            Assert.Equal(CREATE, createdProcedure?.Name);
            Assert.Equal(CREATE, createdProcedure?.Description);
            Assert.Equal(PRICE, createdProcedure?.Price);
        }

        [Fact]
        public async Task Edit_Procedure_Must_Edit_Entity_In_Db()
        {
            using var data = DatabaseMock.Instance;
            var procedureService = new ProcedureService(data);

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

            var procedure = data.Procedures.FirstOrDefault();
            Assert.NotEqual(PRICE, procedure?.Price);
            Assert.NotEqual(CREATE, procedure?.Name);
            Assert.NotEqual(CREATE, procedure?.Description);

            Assert.Equal(UPDATE, procedure?.Name);
        }

        [Fact]

        public async Task Get_All_Procedures_Must_Return_All_Active_Procedures()
        {
            using var data = DatabaseMock.Instance;
            var procedureService = new ProcedureService(data);
           
            await CreateTwoProcedures(procedureService);

            var nonActiveProcedure = await data.Procedures.Where(p => p.Name == "NonActive").FirstAsync();
            nonActiveProcedure.IsActive = false;
            await data.SaveChangesAsync();

            var result = await procedureService.GetAllProcedures();
            var returnedProceduresCount = result.Procedures.Count();
            Assert.Equal(1, returnedProceduresCount);
            Assert.Equal(CREATE, result.Procedures.First().Name);
        }

        [Fact]
        public async Task Get_All_Procedures_Admin_Must_Return_All_Procedures()
        {
            using var data = DatabaseMock.Instance;
            var procedureService = new ProcedureService(data);

            await CreateTwoProcedures(procedureService);

            var nonActiveProcedure = await data.Procedures.Where(p => p.Name == "NonActive").FirstAsync();
            nonActiveProcedure.IsActive = false;
            await data.SaveChangesAsync();

            var result = await procedureService.GetAllProceduresAdmin();
            var returnedProceduresCount = result.Procedures.Count();
            Assert.Equal(2, returnedProceduresCount);
        }

        [Fact]
        public async Task Procedure_Info_Must_Return_Info_For_Procedure_With_Given_Id()
        {
            using var data = DatabaseMock.Instance;
            var procedureService = new ProcedureService(data);

            await CreateTwoProcedures(procedureService);
            var procedureId = await data.Procedures.Where(p => p.Name == CREATE).Select(p => p.Id).FirstAsync();
            var returnedProcedure = await procedureService.ProcedureInfo(procedureId);
            Assert.Equal(procedureId, returnedProcedure.Id);
        }

        private async Task CreateTwoProcedures(ProcedureService procedureService)
        {
            await procedureService.AddNewProcedure(new AddProcedureModel
            {
                Name = CREATE,
                Description = CREATE,
                Price = PRICE,
            });
            await procedureService.AddNewProcedure(new AddProcedureModel
            {
                Name = "NonActive",
                Description = "NonActive",
                Price = PRICE,
            });
        }
    }
}
