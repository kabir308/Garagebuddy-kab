namespace GarageBuddy.Web.ViewModels.Billing
{
    using System;
    using System.Collections.Generic;

    public class InvoiceViewModel
    {
        public Guid Id { get; set; }

        public Guid JobId { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal TotalPaid { get; set; }

        public bool IsPaid { get; set; }

        public ICollection<InvoiceItemViewModel> Items { get; set; } = new List<InvoiceItemViewModel>();
    }
}
