using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkDiaryWebApp.Core.Constants;

namespace WorkDiaryWebApp.Controllers
{
    [Authorize(Roles = UserConstant.Role.USER)]
    public class BaseControllerUser : Controller
    {
    }
}
