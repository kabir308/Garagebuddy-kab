namespace GarageBuddy.Data.Models.WorkOrder
{
    using Job;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class WorkOrder : BaseDeletableModel<Guid>
    {
        [Required]
        public string WorkOrderNumber { get; set; } = null!;

        public DateTime DateCreated { get; set; }

        [Required]
        public Guid JobId { get; set; }

        [ForeignKey(nameof(JobId))]
        public virtual Job Job { get; set; } = null!;
    }
}
