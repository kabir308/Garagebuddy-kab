namespace GarageBuddy.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using AutoMapper;
    using GarageBuddy.Services.Data.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Billing;

    public class QuotesController : BaseController
    {
        private readonly IQuoteService quoteService;
        private readonly IMapper mapper;

        public QuotesController(IQuoteService quoteService, IMapper mapper)
        {
            this.quoteService = quoteService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid jobId)
        {
            var result = await this.quoteService.CreateAsync(jobId);
            if (!result.Succeeded)
            {
                return this.BadRequest(result.Errors);
            }

            return this.RedirectToAction(nameof(this.Details), new { id = result.Data });
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var result = await this.quoteService.GetAsync(id);
            if (!result.Succeeded)
            {
                return this.NotFound();
            }

            var model = this.mapper.Map<QuoteViewModel>(result.Data);
            return this.View(model);
        }
    }
}
