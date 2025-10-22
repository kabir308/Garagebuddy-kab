namespace GarageBuddy.Web.Areas.Admin.Controllers
{
    using Ganss.Xss;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static GarageBuddy.Common.Constants.GlobalConstants;

    [Authorize(Roles = AdministratorRoleName)]
    [Area("Admin")]
    public abstract class AdminController : BaseController
    {
        protected AdminController()
        {
        }
    }
}
