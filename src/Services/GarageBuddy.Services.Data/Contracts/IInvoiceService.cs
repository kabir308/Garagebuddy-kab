namespace GarageBuddy.Services.Data.Contracts
{
    using System;
    using System.Threading.Tasks;

    using Models.Billing;

    public interface IInvoiceService
    {
        Task<IResult<Guid>> CreateAsync(Guid jobId);

        Task<IResult<InvoiceServiceModel>> GetAsync(Guid id);
    }
}
