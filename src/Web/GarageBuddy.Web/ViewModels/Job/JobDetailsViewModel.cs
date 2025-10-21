namespace GarageBuddy.Web.ViewModels.Job
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class JobDetailsViewModel : JobListViewModel
    {
        public string? Description { get; set; }

        [Display(Name = "Estimated Price")]
        public decimal? EstimatedPrice { get; set; }

        [Display(Name = "Total Discount")]
        public decimal? TotalDiscount { get; set; }

        [Display(Name = "Total Price")]
        public decimal? TotalPrice { get; set; }

        [Display(Name = "Total Paid")]
        public decimal? TotalPaid { get; set; }

        public ICollection<JobItemViewModel> JobItems { get; set; } = new List<JobItemViewModel>();
    }
}
