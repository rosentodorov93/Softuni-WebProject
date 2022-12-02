using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FitnessDiary.Areas.Administration.Constants.AdminConstants;

namespace FitnessDiary.Areas.Administration.Controllers
{
    [Area(AdministrationAreaName)]
    [Authorize(Roles = AdminRoleName)]
    public class AdminController : Controller
    {
        
    }
}
