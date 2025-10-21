namespace GarageBuddy.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using AutoMapper;

    using GarageBuddy.Services.Data.Contracts;
    using GarageBuddy.Web.ViewModels.Job;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class JobsController : BaseController
    {
        private readonly IJobService jobService;
        private readonly ICustomerService customerService;
        private readonly IVehicleService vehicleService;
        private readonly IJobStatusService jobStatusService;
        private readonly IJobItemTypeService jobItemTypeService;
        private readonly ITechnicianService technicianService;
        private readonly IMapper mapper;

        public JobsController(
            IJobService jobService,
            ICustomerService customerService,
            IVehicleService vehicleService,
            IJobStatusService jobStatusService,
            IJobItemTypeService jobItemTypeService,
            ITechnicianService technicianService,
            IMapper mapper)
        {
            this.jobService = jobService;
            this.customerService = customerService;
            this.vehicleService = vehicleService;
            this.jobStatusService = jobStatusService;
            this.jobItemTypeService = jobItemTypeService;
            this.technicianService = technicianService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var jobs = await this.jobService.GetAllAsync();
            var model = this.mapper.Map<ICollection<JobListViewModel>>(jobs);
            return this.View(model);
        }

        public IActionResult Calendar()
        {
            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> GetJobs()
        {
            var jobs = await this.jobService.GetAllAsync();
            var events = jobs.Select(j => new
            {
                title = j.CustomerName + " - " + j.VehicleRegistrationNumber,
                start = j.CreatedOn.ToString("yyyy-MM-dd"),
            });
            return this.Json(events);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var jobResult = await this.jobService.GetAsync(id);
            if (!jobResult.Succeeded)
            {
                return this.NotFound();
            }

            var model = this.mapper.Map<JobDetailsViewModel>(jobResult.Data);
            return this.View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = new JobInputModel
            {
                Customers = new SelectList(await this.customerService.GetAllSelectAsync(), "Id", "CustomerName"),
                Vehicles = new SelectList(await this.vehicleService.GetAllSelectAsync(), "Id", "RegistrationNumber"),
                JobStatuses = new SelectList(await this.jobStatusService.GetAllSelectAsync(), "Id", "StatusName"),
                Technicians = new SelectList(await this.technicianService.GetAllAsync(), "Id", "Name"),
            };
            ViewBag.JobItemTypes = new SelectList(await this.jobItemTypeService.GetAllSelectAsync(), "Id", "Name");
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Customers = new SelectList(await this.customerService.GetAllSelectAsync(), "Id", "CustomerName");
                model.Vehicles = new SelectList(await this.vehicleService.GetAllSelectAsync(), "Id", "RegistrationNumber");
                model.JobStatuses = new SelectList(await this.jobStatusService.GetAllSelectAsync(), "Id", "StatusName");
                model.Technicians = new SelectList(await this.technicianService.GetAllAsync(), "Id", "Name");
                ViewBag.JobItemTypes = new SelectList(await this.jobItemTypeService.GetAllSelectAsync(), "Id", "Name");
                return this.View(model);
            }

            var serviceModel = this.mapper.Map<Services.Data.Models.Job.JobServiceModel>(model);
            var result = await this.jobService.CreateAsync(serviceModel);

            if (!result.Succeeded)
            {
                this.ModelState.AddModelError(string.Empty, result.Errors.ToString());
                return this.View(model);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var jobResult = await this.jobService.GetAsync(id);
            if (!jobResult.Succeeded)
            {
                return this.NotFound();
            }

            var model = this.mapper.Map<JobInputModel>(jobResult.Data);
            model.Customers = new SelectList(await this.customerService.GetAllSelectAsync(), "Id", "CustomerName");
            model.Vehicles = new SelectList(await this.vehicleService.GetAllSelectAsync(), "Id", "RegistrationNumber");
            model.JobStatuses = new SelectList(await this.jobStatusService.GetAllSelectAsync(), "Id", "StatusName");
            model.Technicians = new SelectList(await this.technicianService.GetAllAsync(), "Id", "Name");
            ViewBag.JobItemTypes = new SelectList(await this.jobItemTypeService.GetAllSelectAsync(), "Id", "Name");
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, JobInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Customers = new SelectList(await this.customerService.GetAllSelectAsync(), "Id", "CustomerName");
                model.Vehicles = new SelectList(await this.vehicleService.GetAllSelectAsync(), "Id", "RegistrationNumber");
                model.JobStatuses = new SelectList(await this.jobStatusService.GetAllSelectAsync(), "Id", "StatusName");
                model.Technicians = new SelectList(await this.technicianService.GetAllAsync(), "Id", "Name");
                ViewBag.JobItemTypes = new SelectList(await this.jobItemTypeService.GetAllSelectAsync(), "Id", "Name");
                return this.View(model);
            }

            var serviceModel = this.mapper.Map<Services.Data.Models.Job.JobServiceModel>(model);
            var result = await this.jobService.EditAsync(id, serviceModel);

            if (!result.Succeeded)
            {
                this.ModelState.AddModelError(string.Empty, result.Errors.ToString());
                return this.View(model);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var jobResult = await this.jobService.GetAsync(id);
            if (!jobResult.Succeeded)
            {
                return this.NotFound();
            }

            var model = this.mapper.Map<JobListViewModel>(jobResult.Data);
            return this.View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await this.jobService.DeleteAsync(id);
            if (!result.Succeeded)
            {
                return this.RedirectToAction(nameof(this.Delete), new { id });
            }

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
