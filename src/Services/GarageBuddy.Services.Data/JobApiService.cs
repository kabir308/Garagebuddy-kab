namespace GarageBuddy.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using GarageBuddy.Data;
    using GarageBuddy.Services.Data.Models.Job;
    using GarageBuddy.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class JobApiService : IJobApiService
    {
        private readonly ApplicationDbContext context;

        public JobApiService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<JobServiceModel>> GetAllAsync()
        {
            return await this.context.Jobs
                .OrderByDescending(j => j.CreatedOn)
                .To<JobServiceModel>()
                .ToListAsync();
        }

        public async Task<JobServiceModel> GetByIdAsync(Guid id)
        {
            return await this.context.Jobs
                .Where(j => j.Id == id)
                .To<JobServiceModel>()
                .FirstOrDefaultAsync();
        }
    }
}
