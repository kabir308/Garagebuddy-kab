namespace GarageBuddy.Services.Data.Contracts
{
    using System;
    using System.Threading.Tasks;

    using Models.Billing;

    public interface IQuoteService
    {
        Task<IResult<Guid>> CreateAsync(Guid jobId);

        Task<IResult<QuoteServiceModel>> GetAsync(Guid id);
    }
}
