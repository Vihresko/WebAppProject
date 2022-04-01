using Moq;
using WorkDiaryWebApp.Core.Interfaces;

namespace WorkDiaryWebApp.Tests.Mocks
{
    public static class ProcedureServiceMock
    {
        public static IProcedureService Instance
        {
            get
            {
                var procedureServiceMock = new Mock<IProcedureService>();
                return procedureServiceMock.Object;
            }
        }
    }
}
