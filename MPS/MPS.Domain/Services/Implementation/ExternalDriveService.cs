using Microsoft.EntityFrameworkCore;
using MPS.DAL;
using MPS.DAL.Models;
using MPS.Domain.Dto;
using MPS.Domain.Modules.ExternalDriveModules.Implementation;
using MPS.Domain.Services.Interfaces;

namespace MPS.Domain.Services.Implementation;

public class ExternalDriveService : IExternalDriveService
{
    private readonly AppDbContext _context;

    public ExternalDriveService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string> UploadToDriveASync(ReportMinDto reportMinDto, CancellationToken cancellationToken)
    {
        var drive = new YandexDrive();

        string uploadLink = await drive.UploadReport(reportMinDto.Name);
        Account account = await _context
                                .Accounts
                                .FirstAsync(account => account.Id.Equals(reportMinDto.Account.Id), cancellationToken: cancellationToken);

        var report = new Report
        {
            Id = reportMinDto.Id,
            Link = uploadLink,
            CreationDate = DateTime.Now,
            Account = account,
        };

        _context.Reports.Add(report);
        await _context.SaveChangesAsync(cancellationToken);

        File.Delete(reportMinDto.Name);
        return uploadLink;
    }
}