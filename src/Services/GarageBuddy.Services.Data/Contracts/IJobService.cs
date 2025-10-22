namespace GarageBuddy.Services.Data.Contracts
{
    using System;
    using System.Threading.Tasks;

    using Models.Job;

    public interface IJobService
    {
        Task<JobServiceModel> GetByIdAsync(Guid id);
    }
}
