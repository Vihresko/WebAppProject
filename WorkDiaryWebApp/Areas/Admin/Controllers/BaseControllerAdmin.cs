using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkDiaryWebApp.Core.Constants;

namespace WorkDiaryWebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = UserConstant.Role.ADMINISTRATOR)]
    [Area("Admin")]
    public class BaseControllerAdmin : Controller
    {
       
    }
}
