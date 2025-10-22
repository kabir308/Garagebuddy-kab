namespace GarageBuddy.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using Models.WorkOrder;

    public interface IWorkOrderPdfService
    {
        Task<byte[]> GenerateWorkOrderPdfAsync(WorkOrderServiceModel workOrder);
    }
}
