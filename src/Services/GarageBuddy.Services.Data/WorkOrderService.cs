namespace GarageBuddy.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using GarageBuddy.Data;
    using GarageBuddy.Data.Models.WorkOrder;

    using GarageBuddy.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class WorkOrderService : IWorkOrderService
    {
        private readonly ApplicationDbContext context;

        public WorkOrderService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<WorkOrderServiceModel> CreateAsync(Guid jobId)
        {
            var lastWorkOrderNumber = await this.context.WorkOrders
                .OrderByDescending(wo => wo.WorkOrderNumber)
                .Select(wo => wo.WorkOrderNumber)
                .FirstOrDefaultAsync();

            var newWorkOrderNumber = "WO-000001";
            if (lastWorkOrderNumber != null)
            {
                var lastNumber = int.Parse(lastWorkOrderNumber.Substring(3));
                newWorkOrderNumber = $"WO-{(lastNumber + 1):D6}";
            }

            var workOrder = new WorkOrder
            {
                JobId = jobId,
                DateCreated = DateTime.UtcNow,
                WorkOrderNumber = newWorkOrderNumber,
            };

            await this.context.WorkOrders.AddAsync(workOrder);
            await this.context.SaveChangesAsync();

            return AutoMapperConfig.MapperInstance.Map<WorkOrderServiceModel>(workOrder);
        }

        public async Task<WorkOrderServiceModel> GetByIdAsync(Guid id)
        {
            var workOrder = await this.context.WorkOrders
                .Include(wo => wo.Job)
                    .ThenInclude(j => j.Customer)
                .Include(wo => wo.Job)
                    .ThenInclude(j => j.Vehicle)
                .FirstOrDefaultAsync(wo => wo.Id == id);

            return AutoMapperConfig.MapperInstance.Map<WorkOrderServiceModel>(workOrder);
        }
    }
}
