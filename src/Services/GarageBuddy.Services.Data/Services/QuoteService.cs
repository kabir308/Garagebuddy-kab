namespace GarageBuddy.Services.Data.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Contracts;
    using GarageBuddy.Data.Common.Repositories;
    using GarageBuddy.Data.Models.Billing;
    using GarageBuddy.Data.Models.Job;
    using Microsoft.EntityFrameworkCore;
    using Models.Billing;

    public class QuoteService : BaseService<Quote, Guid>, IQuoteService
    {
        private readonly IDeletableEntityRepository<Quote, Guid> quoteRepository;
        private readonly IDeletableEntityRepository<Job, Guid> jobRepository;

        public QuoteService(
            IDeletableEntityRepository<Quote, Guid> entityRepository,
            IDeletableEntityRepository<Job, Guid> jobRepository,
            IMapper mapper)
            : base(entityRepository, mapper)
        {
            this.quoteRepository = entityRepository;
            this.jobRepository = jobRepository;
        }

        public async Task<IResult<Guid>> CreateAsync(Guid jobId)
        {
            var job = await this.jobRepository.All()
                .Include(j => j.JobItems)
                .ThenInclude(ji => ji.JobItemParts)
                .FirstOrDefaultAsync(j => j.Id == jobId);

            if (job == null)
            {
                return await Result<Guid>.FailAsync("Job not found.");
            }

            var quote = new Quote
            {
                JobId = jobId,
                TotalPrice = job.JobItems.Sum(ji => ji.TotalPrice),
                Items = job.JobItems.Select(ji => new QuoteItem
                {
                    Description = ji.Description ?? string.Empty,
                    Quantity = ji.Quantity,
                    UnitPrice = ji.Price,
                    TotalPrice = ji.TotalPrice,
                }).ToList(),
            };

            await this.quoteRepository.AddAsync(quote);
            await this.quoteRepository.SaveChangesAsync();

            return await Result<Guid>.SuccessAsync(quote.Id);
        }

        public async Task<IResult<QuoteServiceModel>> GetAsync(Guid id)
        {
            var quote = await this.quoteRepository.All()
                .Include(q => q.Items)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (quote == null)
            {
                return await Result<QuoteServiceModel>.FailAsync("Quote not found.");
            }

            var model = this.Mapper.Map<QuoteServiceModel>(quote);
            return await Result<QuoteServiceModel>.SuccessAsync(model);
        }
    }
}
