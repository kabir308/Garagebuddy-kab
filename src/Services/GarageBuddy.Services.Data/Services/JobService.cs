namespace GarageBuddy.Services.Data.Services
{
    using AutoMapper;
    using Contracts;

    using GarageBuddy.Data.Common.Repositories;
    using GarageBuddy.Data.Models.Job;

    public class JobService : BaseService<Job, Guid>, IJobService
    {
        private readonly IDeletableEntityRepository<Job, Guid> jobRepository;

        public JobService(
            IDeletableEntityRepository<Job, Guid> entityRepository,
            IMapper mapper)
            : base(entityRepository, mapper)
        {
            this.jobRepository = entityRepository;
        }

        public async Task<ICollection<JobListServiceModel>> GetAllAsync(
            ReadOnlyOption asReadOnly = ReadOnlyOption.Normal,
            DeletedFilter includeDeleted = DeletedFilter.Deleted)
        {
            var jobs = await this.jobRepository.All(asReadOnly, includeDeleted)
                .Include(j => j.Customer)
                .Include(j => j.Vehicle)
                .Include(j => j.JobStatus)
                .OrderBy(j => j.CreatedOn)
                .ToListAsync();

            return this.Mapper.Map<ICollection<JobListServiceModel>>(jobs);
        }

        public async Task<IResult<JobServiceModel>> GetAsync(Guid id)
        {
            var job = await this.jobRepository.All(ReadOnlyOption.Normal, DeletedFilter.NotDeleted)
                .Include(j => j.JobItems)
                .FirstOrDefaultAsync(j => j.Id == id);

            if (job == null)
            {
                return await Result<JobServiceModel>.FailAsync(string.Format(Errors.EntityNotFound, nameof(Job)));
            }

            var model = this.Mapper.Map<JobServiceModel>(job);
            return await Result<JobServiceModel>.SuccessAsync(model);
        }

        public async Task<IResult<Guid>> CreateAsync(JobServiceModel model)
        {
            var entity = this.Mapper.Map<Job>(model);
            await this.jobRepository.AddAsync(entity);
            await this.jobRepository.SaveChangesAsync();
            return await Result<Guid>.SuccessAsync(entity.Id);
        }

        public async Task<IResult> EditAsync(Guid id, JobServiceModel model)
        {
            var entity = await this.jobRepository.GetAsync(id);
            if (entity == null)
            {
                return await Result.FailAsync(string.Format(Errors.EntityNotFound, nameof(Job)));
            }

            this.Mapper.Map(model, entity);
            await this.jobRepository.SaveChangesAsync();
            return await Result.SuccessAsync();
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            await this.jobRepository.DeleteAsync(id);
            await this.jobRepository.SaveChangesAsync();
            return await Result.SuccessAsync();
        }
    }
}
