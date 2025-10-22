namespace GarageBuddy.Web.Controllers.Api
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Contracts;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class JobsController : ControllerBase
    {
        private readonly IJobApiService jobApiService;

        public JobsController(IJobApiService jobApiService)
        {
            this.jobApiService = jobApiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jobs = await this.jobApiService.GetAllAsync();
            return Ok(jobs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var job = await this.jobApiService.GetByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return Ok(job);
        }
    }
}
