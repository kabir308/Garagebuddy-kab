namespace GarageBuddy.Web.ViewModels.Billing
{
    using System;

    public class QuoteItemViewModel
    {
        public Guid Id { get; set; }

        public string Description { get; set; } = null!;

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
