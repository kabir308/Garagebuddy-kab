namespace GarageBuddy.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using AutoMapper;

    using GarageBuddy.Services.Data.Contracts;
    using GarageBuddy.Web.ViewModels.Customer;

    using Microsoft.AspNetCore.Mvc;

    public class CustomersController : BaseController
    {
        private readonly ICustomerService customerService;
        private readonly IMapper mapper;

        public CustomersController(ICustomerService customerService, IMapper mapper)
        {
            this.customerService = customerService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await this.customerService.GetAllAsync();
            var model = this.mapper.Map<ICollection<CustomerListViewModel>>(customers);
            return this.View(model);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var customerResult = await this.customerService.GetWithVehiclesAsync(id);
            if (!customerResult.Succeeded)
            {
                return this.NotFound();
            }

            var model = this.mapper.Map<CustomerDetailsViewModel>(customerResult.Data);
            return this.View(model);
        }

        public IActionResult Create()
        {
            var model = new CustomerInputModel();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var serviceModel = this.mapper.Map<Services.Data.Models.Customer.CustomerServiceModel>(model);
            var result = await this.customerService.CreateAsync(serviceModel);

            if (!result.Succeeded)
            {
                this.ModelState.AddModelError(string.Empty, result.Errors.ToString());
                return this.View(model);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var customerResult = await this.customerService.GetAsync(id);
            if (!customerResult.Succeeded)
            {
                return this.NotFound();
            }

            var model = this.mapper.Map<CustomerInputModel>(customerResult.Data);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, CustomerInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var serviceModel = this.mapper.Map<Services.Data.Models.Customer.CustomerServiceModel>(model);
            var result = await this.customerService.EditAsync(id, serviceModel);

            if (!result.Succeeded)
            {
                this.ModelState.AddModelError(string.Empty, result.Errors.ToString());
                return this.View(model);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var customerResult = await this.customerService.GetAsync(id);
            if (!customerResult.Succeeded)
            {
                return this.NotFound();
            }

            var model = this.mapper.Map<CustomerDetailsViewModel>(customerResult.Data);
            return this.View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await this.customerService.DeleteAsync(id);
            if (!result.Succeeded)
            {
                return this.RedirectToAction(nameof(this.Delete), new { id });
            }

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
