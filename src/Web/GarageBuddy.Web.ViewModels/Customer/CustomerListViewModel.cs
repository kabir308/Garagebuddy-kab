namespace GarageBuddy.Web.ViewModels.Customer
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CustomerListViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; } = null!;

        [Display(Name = "Address")]
        public string Address { get; set; } = null!;

        [Display(Name = "Phone")]
        public string Phone { get; set; } = null!;

        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; } = null!;
    }
}
