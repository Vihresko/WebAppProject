using Moq;
using WorkDiaryWebApp.Core.Interfaces;

namespace WorkDiaryWebApp.Tests.Mocks
{
    public static class BankServiceMock
    {
        public static IBankService Instance
        {
            get
            {
                var bankServiceMock = new Mock<IBankService>();
                return bankServiceMock.Object;
            }
        }
    }
}
