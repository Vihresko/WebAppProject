using Moq;
using WorkDiaryWebApp.Core.Interfaces;

namespace WorkDiaryWebApp.Tests.Mocks
{
    public static class ClientServiceMock
    {
        public static IClientService Instance
        {
            get
            {
                var clientServiceMock = new Mock<IClientService>();
                return clientServiceMock.Object;
            }
        }
    }
}
