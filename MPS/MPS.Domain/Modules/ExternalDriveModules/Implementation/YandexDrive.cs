using MPS.Domain.Config;
using MPS.Domain.Modules.ExternalDriveModules.Interfaces;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;

namespace MPS.Domain.Modules.ExternalDriveModules.Implementation;

public class YandexDrive : IExternalDrive, IDisposable
{
    private readonly DiskHttpApi _api;

    public YandexDrive()
    {
        _api = new DiskHttpApi(DomainConfig.YandexDiskToken);
    }

    public async Task<string> UploadReport(string sourceFile)
    {
        string reportLocation = await SetReportLocation("MPS", sourceFile);
        Link link = await _api.Files.GetUploadLinkAsync(reportLocation, overwrite: true);

        await using FileStream fs = File.OpenRead(sourceFile);
        await _api.Files.UploadAsync(link, fs);
        return (await _api.Files.GetDownloadLinkAsync(reportLocation)).Href;
    }

    public void DownloadReport()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _api.Dispose();
    }

    private async Task CreateDirAsync(string dirName)
    {
        Resource rootFolderData = await _api.MetaInfo
            .GetInfoAsync(new ResourceRequest { Path = "/", });

        if (!rootFolderData.Embedded.Items
                .Any(item => item.Type == ResourceType.Dir && item.Name.Equals(dirName)))
        {
            await _api.Commands.CreateDictionaryAsync(dirName);
        }
    }

    private async Task<string> SetReportLocation(string dirName, string reportName)
    {
        string reportLocation = "/" + dirName + "/" + reportName; // Path.Combine() doesn't work lmao
        await CreateDirAsync(dirName);
        return reportLocation;
    }
}