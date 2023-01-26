using MPS.DAL.Models;
using MPS.Domain.Dto;
using MPS.Domain.Modules.ReportCreationalModules.Extra;
using MPS.Domain.Modules.ReportCreationalModules.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace MPS.Domain.Modules.ReportCreationalModules.Implementation;

public class HighPriorityPdfReportCreator : IReportCreator
{
    private readonly AccountDto _account;
    private readonly IReadOnlyCollection<AccountDto> _subordinates;

    public HighPriorityPdfReportCreator(AccountDto account, IReadOnlyCollection<AccountDto> subordinates)
    {
        _account = account;
        _subordinates = subordinates;
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
                    .Text($"HighLevel Report from {_account.Login}\n{DateTime.Now}")
                    .SemiBold().FontSize(32).FontColor(Colors.BlueGrey.Darken1);

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(x =>
                    {
                        x.Spacing(5);

                        x.Item().Text("Audited Employees Reports: \n");
                        foreach (AccountDto employee in _subordinates)
                        {
                            x.Item().Text($"  {employee.Login} reports:\n");
                            foreach (Report reportDto in employee.Reports)
                            {
                                x.Item().Hyperlink(reportDto.Link).Text("Link to report with ID=" + reportDto.Id).Underline();
                            }
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