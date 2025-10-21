namespace GarageBuddy.Web.ViewModels.Vehicle
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class VehicleListViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; } = null!;

        [Display(Name = "VIN")]
        public string? VehicleIdentificationNumber { get; set; }

        [Display(Name = "Brand")]
        public string BrandName { get; set; } = null!;

        [Display(Name = "Model")]
        public string? BrandModelName { get; set; }
    }
}
