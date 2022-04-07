using Moq;
using WorkDiaryWebApp.Models.Procedure;

namespace WorkDiaryWebApp.Tests.Mocks
{
    public static class ShowProcedureModelMock
    {
        public static ShowProcedureModel Instance
        {
            get
            {
                var showProcedureMock = new Mock<ShowProcedureModel>();
                return showProcedureMock.Object;
            }
        }
    }
}
