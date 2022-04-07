using Moq;
using WorkDiaryWebApp.Core.Interfaces;

namespace WorkDiaryWebApp.Tests.Mocks
{
    public static class ContactServiceMock
    {
        public static IContactService Instance
        {
            get
            {
                var contactServiceMock = new Mock<IContactService>();
                return contactServiceMock.Object;
            }
        }
    }
}
