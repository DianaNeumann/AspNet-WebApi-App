using Microsoft.EntityFrameworkCore;
using MPS.DAL;
using MPS.Domain.Dto;
using MPS.Domain.Mapping;
using MPS.Domain.Modules.ReportCreationalModules.Implementation;
using MPS.Domain.Services.Interfaces;

namespace MPS.Domain.Services.Implementation;

public class ReportService : IReportService
{
    private readonly AppDbContext _context;
    public ReportService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ReportMinDto> CreateReportAsync(Guid accountId, CancellationToken cancellationToken)
    {
        AccountDto account = (await _context.Accounts.FirstAsync(account => account.Id.Equals(accountId), cancellationToken: cancellationToken)).AsDto();

        var reportFactory = new PdfReportsFactory(account, _context);
        var reportCreator = reportFactory.Create();
        var reportMinDto = reportCreator.CreateReport();

        return reportMinDto;
    }
}