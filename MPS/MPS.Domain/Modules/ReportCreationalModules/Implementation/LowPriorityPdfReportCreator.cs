using MPS.DAL.Models;
using MPS.Domain.Dto;
using MPS.Domain.Modules.ReportCreationalModules.Extra;
using MPS.Domain.Modules.ReportCreationalModules.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Document = QuestPDF.Fluent.Document;

namespace MPS.Domain.Modules.ReportCreationalModules.Implementation;

public class LowPriorityPdfReportCreator : IReportCreator
{
    private readonly AccountDto _account;

    public LowPriorityPdfReportCreator(AccountDto account)
    {
        _account = account;
    }

    public ReportMinDto CreateReport()
    {
        var reportId = Guid.NewGuid();
        string reportName = reportId + ".pdf";

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(20));

                page.Header()
                    .Text($"Report from {_account.Login}\n{DateTime.Now}")
                    .SemiBold().FontSize(32).FontColor(Colors.BlueGrey.Darken1);

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(x =>
                    {
                        x.Spacing(5);

                        x.Item().Text("Processed messages: \n");
                        foreach (Message messageForReport in _account.ProcessMessages)
                        {
                            x.Item().Text($"{messageForReport.Content} [from {messageForReport.Source}]");
                        }

                        x.Item().Image(ReportFooter.GetFooterImage());
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                    });
            });
        }).GeneratePdf(reportName);

        return new ReportMinDto(reportId, reportName, _account);
    }
}