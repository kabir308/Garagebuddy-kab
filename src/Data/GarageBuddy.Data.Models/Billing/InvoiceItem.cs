namespace GarageBuddy.Data.Models.Billing
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;
    using static GarageBuddy.Common.Constants.GlobalValidationConstants;

    public class InvoiceItem : BaseDeletableModel<Guid>
    {
        [Required]
        public Guid InvoiceId { get; set; }

        [ForeignKey(nameof(InvoiceId))]
        public Invoice Invoice { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        [Precision(DefaultDecimalPrecision, DefaultDecimalScale)]
        public decimal Quantity { get; set; }

        [Required]
        [Precision(DefaultDecimalPrecision, DefaultDecimalScale)]
        public decimal UnitPrice { get; set; }

        [Required]
        [Precision(DefaultDecimalPrecision, DefaultDecimalScale)]
        public decimal TotalPrice { get; set; }
    }
}
