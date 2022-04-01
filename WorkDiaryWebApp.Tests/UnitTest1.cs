using Xunit;

namespace WorkDiaryWebApp.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(1, 1);
        }

        [Theory]
        [InlineData(1,2)]
        [InlineData(2,3)]
        public void Test2(int x, int y)
        {
            Assert.Equal(x + 1, y);
        }
    }
}