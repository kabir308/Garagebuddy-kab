namespace GarageBuddy.Web.ViewModels.Vehicle
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class VehicleDetailsViewModel : VehicleListViewModel
    {
        [Display(Name = "Date of Manufacture")]
        public DateTime? DateOfManufacture { get; set; }

        [Display(Name = "Fuel Type")]
        public string? FuelType { get; set; }

        [Display(Name = "Gearbox Type")]
        public string? GearboxType { get; set; }

        [Display(Name = "Drive Type")]
        public string? DriveType { get; set; }

        [Display(Name = "Engine Capacity")]
        public int? EngineCapacity { get; set; }

        [Display(Name = "Engine Horse Power")]
        public int? EngineHorsePower { get; set; }

        public string? Description { get; set; }
    }
}
