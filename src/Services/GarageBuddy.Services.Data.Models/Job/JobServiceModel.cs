namespace GarageBuddy.Services.Data.Models.Job
{
    using GarageBuddy.Data.Models.Job;
    using GarageBuddy.Services.Data.Models.Customer;
    using GarageBuddy.Services.Data.Models.Vehicle;
    using GarageBuddy.Services.Data.Models.WorkOrder;
    using GarageBuddy.Services.Mapping;

    public class JobServiceModel : IMapFrom<Job>
    {
        public Guid Id { get; set; }

        public CustomerServiceModel Customer { get; set; } = null!;

        public VehicleServiceModel Vehicle { get; set; } = null!;

        public string Description { get; set; } = null!;

        public WorkOrderServiceModel? WorkOrder { get; set; }
    }
}
