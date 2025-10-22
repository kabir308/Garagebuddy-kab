namespace GarageBuddy.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Ganss.Xss;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Data.Contracts;

    [Authorize]
    public class JobsController : BaseController
    {
        private readonly IJobService jobService;

        public JobsController(
            IHtmlSanitizer sanitizer,
            IJobService jobService)
            : base(sanitizer)
        {
            this.jobService = jobService;
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var model = await this.jobService.GetByIdAsync(id);
            return this.View(model);
        }
    }
}
