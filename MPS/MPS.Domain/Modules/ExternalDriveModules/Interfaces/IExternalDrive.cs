namespace MPS.Domain.Modules.ExternalDriveModules.Interfaces;

public interface IExternalDrive
{
    Task<string> UploadReport(string sourceFile);
    void DownloadReport();
}