using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkDiaryWebApp.Core.Constants;

namespace WorkDiaryWebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = UserConstant.Role.Administrator)]
    [Area("Admin")]
    public class BaseController : Controller
    {
       
    }
}
