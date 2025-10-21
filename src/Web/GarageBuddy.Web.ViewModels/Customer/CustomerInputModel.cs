namespace GarageBuddy.Web.ViewModels.Customer
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static GarageBuddy.Common.Constants.EntityValidationConstants.Customer;

    public class CustomerInputModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(CustomerNameMaxLength, MinimumLength = CustomerNameMinLength)]
        public string Name { get; set; } = null!;

        [StringLength(DefaultAddressMaxLength, MinimumLength = DefaultAddressMinLength)]
        public string? Address { get; set; }

        [EmailAddress]
        [StringLength(DefaultEmailMaxLength, MinimumLength = DefaultEmailMinLength)]
        public string? Email { get; set; }

        [Phone]
        [StringLength(CustomerPhoneMaxLength, MinimumLength = CustomerPhoneMinLength)]
        public string? Phone { get; set; }

        [StringLength(CustomerCompanyNameMaxLength)]
        public string? CompanyName { get; set; }

        public Guid? ApplicationUserId { get; set; }
    }
}
