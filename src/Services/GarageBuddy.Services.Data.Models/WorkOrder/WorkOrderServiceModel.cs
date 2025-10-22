namespace GarageBuddy.Services.Data.Models.WorkOrder
{
    using GarageBuddy.Data.Models.WorkOrder;
    using GarageBuddy.Services.Data.Models.Job;
    using GarageBuddy.Services.Mapping;

    public class WorkOrderServiceModel : IMapFrom<WorkOrder>
    {
        public Guid Id { get; set; }

        public string WorkOrderNumber { get; set; } = null!;

        public DateTime DateCreated { get; set; }

        public JobServiceModel Job { get; set; } = null!;
    }
}
