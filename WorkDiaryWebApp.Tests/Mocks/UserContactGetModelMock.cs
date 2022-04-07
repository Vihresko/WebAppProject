using Moq;
using WorkDiaryWebApp.Models.Contact;

namespace WorkDiaryWebApp.Tests.Mocks
{
    public static class UserContactGetModelMock
    {
        public static UserContactGetModel Instance
        {
            get
            {
                var userContactGetModelMock = new Mock<UserContactGetModel>();
                return userContactGetModelMock.Object;
            }
        }
    }
}
