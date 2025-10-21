namespace GarageBuddy.Web.ViewModels.Job
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class JobItemInputModel
    {
        public Guid Id { get; set; }

        [Required]
        public Guid JobItemTypeId { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal? Discount { get; set; }

        public string? Description { get; set; }
    }
}
