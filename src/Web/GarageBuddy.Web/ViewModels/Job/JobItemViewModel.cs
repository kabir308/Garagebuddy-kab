namespace GarageBuddy.Web.ViewModels.Job
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class JobItemViewModel
    {
        public Guid Id { get; set; }

        public string JobItemType { get; set; } = null!;

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice { get; set; }

        public string? Description { get; set; }
    }
}
