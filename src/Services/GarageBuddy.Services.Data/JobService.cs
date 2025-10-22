namespace GarageBuddy.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using GarageBuddy.Data;
    using GarageBuddy.Services.Data.Models.Job;
    using GarageBuddy.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class JobService : IJobService
    {
        private readonly ApplicationDbContext context;

        public JobService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<JobServiceModel> GetByIdAsync(Guid id)
        {
            var job = await this.context.Jobs
                .Include(j => j.Customer)
                .Include(j => j.Vehicle)
                .Include(j => j.WorkOrder)
                .FirstOrDefaultAsync(j => j.Id == id);

            return AutoMapperConfig.MapperInstance.Map<JobServiceModel>(job);
        }
    }
}
