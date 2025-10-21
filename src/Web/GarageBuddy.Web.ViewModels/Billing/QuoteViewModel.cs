namespace GarageBuddy.Web.ViewModels.Billing
{
    using System;
    using System.Collections.Generic;

    public class QuoteViewModel
    {
        public Guid Id { get; set; }

        public Guid JobId { get; set; }

        public decimal TotalPrice { get; set; }

        public ICollection<QuoteItemViewModel> Items { get; set; } = new List<QuoteItemViewModel>();
    }
}
