namespace GarageBuddy.Web.ViewModels.Vehicle
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using static GarageBuddy.Common.Constants.EntityValidationConstants.Vehicle;

    public class VehicleInputModel
    {
        public Guid Id { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public Guid BrandId { get; set; }

        public Guid? BrandModelId { get; set; }

        [StringLength(VehicleVinNumberMaxLength, MinimumLength = VehicleVinNumberMinLength)]
        public string? VehicleIdentificationNumber { get; set; }

        [Required]
        [StringLength(VehicleRegistrationNumberMaxLength, MinimumLength = VehicleRegistrationNumberMinLength)]
        public string RegistrationNumber { get; set; } = null!;

        public DateTime? DateOfManufacture { get; set; }

        public int? FuelTypeId { get; set; }

        public int? GearboxTypeId { get; set; }

        public int? DriveTypeId { get; set; }

        public int? EngineCapacity { get; set; }

        public int? EngineHorsePower { get; set; }

        [StringLength(DefaultDescriptionMaxLength)]
        public string? Description { get; set; }

        public SelectList? Customers { get; set; }
        public SelectList? Brands { get; set; }
        public SelectList? BrandModels { get; set; }
        public SelectList? FuelTypes { get; set; }
        public SelectList? GearboxTypes { get; set; }
        public SelectList? DriveTypes { get; set; }
    }
}
