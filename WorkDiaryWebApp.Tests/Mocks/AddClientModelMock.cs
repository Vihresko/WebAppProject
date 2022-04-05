using Moq;
using WorkDiaryWebApp.Models.Client;

namespace WorkDiaryWebApp.Tests.Mocks
{
    public static class AddClientModelMock
    {
        public static AddClientModel Instance
        {
            get
            {
                var addClientModelMock = new Mock<AddClientModel>();
                return addClientModelMock.Object;
            }
        }
    }
}
