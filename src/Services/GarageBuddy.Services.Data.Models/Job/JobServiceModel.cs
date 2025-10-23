namespace GarageBuddy.Services.Data.Models.Job
{
    using System;
    using Data.Models.Job;
    using Mapping;

    public class JobServiceModel : IMapFrom<Job>
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
