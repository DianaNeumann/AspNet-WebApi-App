using MPS.DAL;
using MPS.DAL.Models.Tools;
using MPS.Domain.Dto;
using MPS.Domain.Mapping;
using MPS.Domain.Modules.ReportCreationalModules.Interfaces;

namespace MPS.Domain.Modules.ReportCreationalModules.Implementation;

public class PdfReportsFactory
{
    private readonly AccountDto _account;
    private readonly AppDbContext _dbContext;

    public PdfReportsFactory(AccountDto account, AppDbContext dbContext)
    {
        _account = account;
        _dbContext = dbContext;
    }

    public IReportCreator Create()
    {
        return _account.Role switch
        {
            Role.LowPriority => new LowPriorityPdfReportCreator(_account),
            Role.MiddlePriority => new MiddlePriorityPdfReportCreator(_account, GetSubordinates()),
            Role.HighPriority => new HighPriorityPdfReportCreator(_account, GetSubordinates()),
            _ => throw new Exception("[-] RepositoryType doesn't correct"),
        };
    }

    public IReadOnlyCollection<AccountDto> GetSubordinates()
    {
        Role lowerPriorityLevel = _account.Role + 1;
        return _dbContext.Accounts
            .Where(a => a.Role == lowerPriorityLevel)
            .Where(a => a.Reports.Count > 0)
            .Select(account => account.AsDto())
            .ToArray();
    }
}