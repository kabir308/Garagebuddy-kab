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

    public class InvoiceService : BaseService<Invoice, Guid>, IInvoiceService
    {
        private readonly IDeletableEntityRepository<Invoice, Guid> invoiceRepository;
        private readonly IDeletableEntityRepository<Job, Guid> jobRepository;

        public InvoiceService(
            IDeletableEntityRepository<Invoice, Guid> entityRepository,
            IDeletableEntityRepository<Job, Guid> jobRepository,
            IMapper mapper)
            : base(entityRepository, mapper)
        {
            this.invoiceRepository = entityRepository;
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

            var invoice = new Invoice
            {
                JobId = jobId,
                TotalPrice = job.JobItems.Sum(ji => ji.TotalPrice),
                Items = job.JobItems.Select(ji => new InvoiceItem
                {
                    Description = ji.Description ?? string.Empty,
                    Quantity = ji.Quantity,
                    UnitPrice = ji.Price,
                    TotalPrice = ji.TotalPrice,
                }).ToList(),
            };

            await this.invoiceRepository.AddAsync(invoice);
            await this.invoiceRepository.SaveChangesAsync();

            return await Result<Guid>.SuccessAsync(invoice.Id);
        }

        public async Task<IResult<InvoiceServiceModel>> GetAsync(Guid id)
        {
            var invoice = await this.invoiceRepository.All()
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
            {
                return await Result<InvoiceServiceModel>.FailAsync("Invoice not found.");
            }

            var model = this.Mapper.Map<InvoiceServiceModel>(invoice);
            return await Result<InvoiceServiceModel>.SuccessAsync(model);
        }
    }
}
