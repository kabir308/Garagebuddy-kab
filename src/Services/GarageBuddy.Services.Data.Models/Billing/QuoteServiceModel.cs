namespace GarageBuddy.Services.Data.Models.Billing
{
    using System;
    using System.Collections.Generic;

    public class QuoteServiceModel
    {
        public Guid Id { get; set; }

        public Guid JobId { get; set; }

        public decimal TotalPrice { get; set; }

        public ICollection<QuoteItemServiceModel> Items { get; set; } = new List<QuoteItemServiceModel>();
    }
}
