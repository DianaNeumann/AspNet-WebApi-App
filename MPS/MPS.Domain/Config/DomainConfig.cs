using Microsoft.Extensions.Configuration;

namespace MPS.Domain.Config;

public static class DomainConfig
{
    private static readonly IConfigurationRoot Config = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("domainsettings.json").Build();

    public static readonly string JwtToken = Config
        .GetSection("ReallySecret")
        .GetSection("JwtSecToken")
        .Get<string>();

    public static readonly string YandexDiskToken = Config
        .GetSection("ReallySecret")
        .GetSection("YandexDiskToken")
        .Get<string>();
}