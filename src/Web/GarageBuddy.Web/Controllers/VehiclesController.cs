namespace GarageBuddy.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using AutoMapper;

    using GarageBuddy.Services.Data.Contracts;
    using GarageBuddy.Web.ViewModels.Vehicle;

    using Microsoft.AspNetCore.Mvc;

    public class VehiclesController : BaseController
    {
        private readonly IVehicleService vehicleService;
        private readonly ICustomerService customerService;
        private readonly IBrandService brandService;
        private readonly IBrandModelService brandModelService;
        private readonly IFuelTypeService fuelTypeService;
        private readonly IGearboxTypeService gearboxTypeService;
        private readonly IDriveTypeService driveTypeService;
        private readonly IMapper mapper;

        public VehiclesController(
            IVehicleService vehicleService,
            ICustomerService customerService,
            IBrandService brandService,
            IBrandModelService brandModelService,
            IFuelTypeService fuelTypeService,
            IGearboxTypeService gearboxTypeService,
            IDriveTypeService driveTypeService,
            IMapper mapper)
        {
            this.vehicleService = vehicleService;
            this.customerService = customerService;
            this.brandService = brandService;
            this.brandModelService = brandModelService;
            this.fuelTypeService = fuelTypeService;
            this.gearboxTypeService = gearboxTypeService;
            this.driveTypeService = driveTypeService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var vehicles = await this.vehicleService.GetAllAsync();
            var model = this.mapper.Map<ICollection<VehicleListViewModel>>(vehicles);
            return this.View(model);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var vehicleResult = await this.vehicleService.GetAsync(id);
            if (!vehicleResult.Succeeded)
            {
                return this.NotFound();
            }

            var model = this.mapper.Map<VehicleDetailsViewModel>(vehicleResult.Data);
            return this.View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = new VehicleInputModel
            {
                Customers = new(await this.customerService.GetAllSelectAsync(), "Id", "CustomerName"),
                Brands = new(await this.brandService.GetAllSelectAsync(), "Id", "Name"),
                FuelTypes = new(await this.fuelTypeService.GetAllSelectAsync(), "Id", "Name"),
                GearboxTypes = new(await this.gearboxTypeService.GetAllSelectAsync(), "Id", "Name"),
                DriveTypes = new(await this.driveTypeService.GetAllSelectAsync(), "Id", "Name"),
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VehicleInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var serviceModel = this.mapper.Map<Services.Data.Models.Vehicle.VehicleServiceModel>(model);
            var result = await this.vehicleService.CreateAsync(serviceModel);

            if (!result.Succeeded)
            {
                this.ModelState.AddModelError(string.Empty, result.Errors.ToString());
                return this.View(model);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var vehicleResult = await this.vehicleService.GetAsync(id);
            if (!vehicleResult.Succeeded)
            {
                return this.NotFound();
            }

            var model = this.mapper.Map<VehicleInputModel>(vehicleResult.Data);
            model.Customers = new(await this.customerService.GetAllSelectAsync(), "Id", "CustomerName");
            model.Brands = new(await this.brandService.GetAllSelectAsync(), "Id", "Name");
            model.BrandModels = new(await this.brandModelService.GetAllByBrandIdSelectAsync(model.BrandId), "Id", "Name");
            model.FuelTypes = new(await this.fuelTypeService.GetAllSelectAsync(), "Id", "Name");
            model.GearboxTypes = new(await this.gearboxTypeService.GetAllSelectAsync(), "Id", "Name");
            model.DriveTypes = new(await this.driveTypeService.GetAllSelectAsync(), "Id", "Name");
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, VehicleInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var serviceModel = this.mapper.Map<Services.Data.Models.Vehicle.VehicleServiceModel>(model);
            var result = await this.vehicleService.EditAsync(id, serviceModel);

            if (!result.Succeeded)
            {
                this.ModelState.AddModelError(string.Empty, result.Errors.ToString());
                return this.View(model);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var vehicleResult = await this.vehicleService.GetAsync(id);
            if (!vehicleResult.Succeeded)
            {
                return this.NotFound();
            }

            var model = this.mapper.Map<VehicleDetailsViewModel>(vehicleResult.Data);
            return this.View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await this.vehicleService.DeleteAsync(id);
            if (!result.Succeeded)
            {
                return this.RedirectToAction(nameof(this.Delete), new { id });
            }

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
