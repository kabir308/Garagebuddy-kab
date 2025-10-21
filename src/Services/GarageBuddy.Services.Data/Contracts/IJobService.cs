namespace GarageBuddy.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models.Job;

    public interface IJobService
    {
        public Task<ICollection<JobListServiceModel>> GetAllAsync(
            ReadOnlyOption asReadOnly = ReadOnlyOption.Normal,
            DeletedFilter includeDeleted = DeletedFilter.Deleted);

        public Task<IResult<JobServiceModel>> GetAsync(Guid id);

        public Task<IResult<Guid>> CreateAsync(JobServiceModel model);

        public Task<IResult> EditAsync(Guid id, JobServiceModel model);

        public Task<IResult> DeleteAsync(Guid id);
    }
}
