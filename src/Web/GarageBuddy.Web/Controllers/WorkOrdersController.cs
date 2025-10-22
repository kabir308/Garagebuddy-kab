namespace GarageBuddy.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Ganss.Xss;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Data.Contracts;

    [Authorize]
    public class WorkOrdersController : BaseController
    {
        private readonly IWorkOrderService workOrderService;
        private readonly IWorkOrderPdfService workOrderPdfService;

        public WorkOrdersController(
            IHtmlSanitizer sanitizer,
            IWorkOrderService workOrderService,
            IWorkOrderPdfService workOrderPdfService)
            : base(sanitizer)
        {
            this.workOrderService = workOrderService;
            this.workOrderPdfService = workOrderPdfService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid jobId)
        {
            var workOrder = await this.workOrderService.CreateAsync(jobId);
            return this.RedirectToAction("Details", "Jobs", new { id = jobId });
        }

        public async Task<IActionResult> Download(Guid id)
        {
            var workOrder = await this.workOrderService.GetByIdAsync(id);
            if (workOrder == null)
            {
                return this.NotFound();
            }

            var pdfBytes = await this.workOrderPdfService.GenerateWorkOrderPdfAsync(workOrder);
            return this.File(pdfBytes, "application/pdf", $"WorkOrder-{workOrder.WorkOrderNumber}.pdf");
        }
    }
}
