namespace GarageBuddy.Data.Models.Billing
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;
    using static GarageBuddy.Common.Constants.GlobalValidationConstants;

    public class QuoteItem : BaseDeletableModel<Guid>
    {
        [Required]
        public Guid QuoteId { get; set; }

        [ForeignKey(nameof(QuoteId))]
        public Quote Quote { get; set; } = null!;

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
