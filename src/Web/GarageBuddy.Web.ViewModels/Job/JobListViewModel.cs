namespace GarageBuddy.Web.ViewModels.Job
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class JobListViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Customer")]
        public string CustomerName { get; set; } = null!;

        [Display(Name = "Vehicle")]
        public string VehicleRegistrationNumber { get; set; } = null!;

        [Display(Name = "Status")]
        public string JobStatus { get; set; } = null!;

        [Display(Name = "Date")]
        public DateTime CreatedOn { get; set; }
    }
}
