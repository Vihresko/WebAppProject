using Microsoft.AspNetCore.Mvc;
using WorkDiaryWebApp.Areas.Admin.Controllers;
using Xunit;

namespace WorkDiaryWebApp.Tests.Controllers
{
    public class HomeContollerTest
    {
        [Fact]
        public void Index_Must_Return_Correct_Result()
        {
            var controller = new HomeController();
            var result = controller.Index();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
      
    }
}
