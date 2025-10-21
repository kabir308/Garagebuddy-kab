namespace GarageBuddy.Services.Data.Models.Billing
{
    using System;
    using System.Collections.Generic;

    public class InvoiceServiceModel
    {
        public Guid Id { get; set; }

        public Guid JobId { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal TotalPaid { get; set; }

        public bool IsPaid { get; set; }

        public ICollection<InvoiceItemServiceModel> Items { get; set; } = new List<InvoiceItemServiceModel>();
    }
}
