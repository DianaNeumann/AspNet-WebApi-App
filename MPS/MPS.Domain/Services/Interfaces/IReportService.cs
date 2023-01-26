using MPS.Domain.Dto;

namespace MPS.Domain.Services.Interfaces;

public interface IReportService
{
    Task<ReportMinDto> CreateReportAsync(Guid accountId, CancellationToken cancellationToken);
}