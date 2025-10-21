namespace GarageBuddy.Data.Models.Billing
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Job;
    using Microsoft.EntityFrameworkCore;
    using static GarageBuddy.Common.Constants.GlobalValidationConstants;

    public class Quote : BaseDeletableModel<Guid>
    {
        [Required]
        public Guid JobId { get; set; }

        [ForeignKey(nameof(JobId))]
        public Job Job { get; set; } = null!;

        [Precision(DefaultDecimalPrecision, DefaultDecimalScale)]
        public decimal TotalPrice { get; set; }

        public ICollection<QuoteItem> Items { get; set; } = new HashSet<QuoteItem>();
    }
}
