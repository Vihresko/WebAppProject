using Moq;
using WorkDiaryWebApp.Models.Procedure;

namespace WorkDiaryWebApp.Tests.Mocks
{
    public static class AddProcedureModelMock
    {
        public static AddProcedureModel Instance
        {
            get
            {
                var addProcedureModelMock = new Mock<AddProcedureModel>();
                return addProcedureModelMock.Object;
            }
        }
    }
}
