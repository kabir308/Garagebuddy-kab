namespace GarageBuddy.Services.Data
{
    using System.Threading.Tasks;

    using Contracts;
    using GarageBuddy.Services.Data.Models.WorkOrder;

    using QuestPDF.Fluent;
    using QuestPDF.Helpers;
    using QuestPDF.Infrastructure;

    public class WorkOrderPdfService : IWorkOrderPdfService
    {
        public async Task<byte[]> GenerateWorkOrderPdfAsync(WorkOrderServiceModel workOrder)
        {
            return await Task.Run(() =>
            {
                return Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.DefaultTextStyle(x => x.FontSize(12));

                        page.Header().Row(row =>
                        {
                            row.RelativeItem().Column(column =>
                            {
                                column.Item().Text("Garage Buddy")
                                    .SemiBold().FontSize(24).FontColor(Colors.Blue.Medium);
                                column.Item().Text("123 Main Street");
                                column.Item().Text("Anytown, USA 12345");
                            });

                            row.ConstantItem(100).Image("wwwroot/images/logo.svg");
                        });

                        page.Content().PaddingVertical(1, Unit.Centimetre).Column(column =>
                        {
                            column.Spacing(20);

                            column.Item().Text($"Work Order #{workOrder.WorkOrderNumber}")
                                .SemiBold().FontSize(20);

                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Column(column =>
                                {
                                    column.Item().Text("Customer Information")
                                        .SemiBold();
                                    column.Item().Text(workOrder.Job.Customer.Name);
                                });

                                row.RelativeItem().Column(column =>
                                {
                                    column.Item().Text("Vehicle Information")
                                        .SemiBold();
                                    column.Item().Text($"{workOrder.Job.Vehicle.Make} {workOrder.Job.Vehicle.Model}");
                                });
                            });

                            column.Item().Text("Description")
                                .SemiBold();
                            column.Item().Text(workOrder.Job.Description);
                        });

                        page.Footer()
                            .AlignCenter()
                            .Text(x =>
                            {
                                x.Span("Page ");
                                x.CurrentPageNumber();
                                x.Span(" | ");
                                x.Span("Phone: 555-555-5555");
                            });
                    });
                })
                .GeneratePdf();
            });
        }
    }
}
