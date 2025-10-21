namespace GarageBuddy.Web.ViewModels.Job
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class JobInputModel
    {
        public Guid Id { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public Guid VehicleId { get; set; }

        [Required]
        public int JobStatusId { get; set; }

        public Guid? TechnicianId { get; set; }

        public string? Description { get; set; }

        public SelectList? Customers { get; set; }
        public SelectList? Vehicles { get; set; }
        public SelectList? JobStatuses { get; set; }
        public SelectList? Technicians { get; set; }

        public ICollection<JobItemInputModel> JobItems { get; set; } = new List<JobItemInputModel>();
    }
}
