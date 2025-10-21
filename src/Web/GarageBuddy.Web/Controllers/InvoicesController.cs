namespace GarageBuddy.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using AutoMapper;
    using GarageBuddy.Services.Data.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Billing;

    public class InvoicesController : BaseController
    {
        private readonly IInvoiceService invoiceService;
        private readonly IMapper mapper;

        public InvoicesController(IInvoiceService invoiceService, IMapper mapper)
        {
            this.invoiceService = invoiceService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid jobId)
        {
            var result = await this.invoiceService.CreateAsync(jobId);
            if (!result.Succeeded)
            {
                return this.BadRequest(result.Errors);
            }

            return this.RedirectToAction(nameof(this.Details), new { id = result.Data });
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var result = await this.invoiceService.GetAsync(id);
            if (!result.Succeeded)
            {
                return this.NotFound();
            }

            var model = this.mapper.Map<InvoiceViewModel>(result.Data);
            return this.View(model);
        }
    }
}
