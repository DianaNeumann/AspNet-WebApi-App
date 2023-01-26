using MPS.Domain.Dto;

namespace MPS.Domain.Services.Interfaces;

public interface IExternalDriveService
{
    Task<string> UploadToDriveASync(ReportMinDto reportMinDto, CancellationToken cancellationToken);
}