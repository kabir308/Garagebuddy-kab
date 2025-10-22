namespace GarageBuddy.Services.Data.Contracts
{
    using System;
    using System.Threading.Tasks;

    using Models.WorkOrder;

    public interface IWorkOrderService
    {
        Task<WorkOrderServiceModel> CreateAsync(Guid jobId);

        Task<WorkOrderServiceModel> GetByIdAsync(Guid id);
    }
}
