using Moq;
using WorkDiaryWebApp.Models.Client;

namespace WorkDiaryWebApp.Tests.Mocks
{
    public static class ClientInfoModelMock
    {
        public static ClientInfoModel Instance 
        {
            get
            {
                var model = new Mock<ClientInfoModel>();
                return model.Object;
            }
        }
    }
}
