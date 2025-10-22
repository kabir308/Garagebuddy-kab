namespace GarageBuddy.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Job;

    public interface IJobApiService
    {
        Task<ICollection<JobServiceModel>> GetAllAsync();
        Task<JobServiceModel> GetByIdAsync(Guid id);
    }
}
