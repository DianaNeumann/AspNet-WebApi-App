using Microsoft.Extensions.DependencyInjection;
using MPS.Domain.Modules.ExternalDriveModules.Implementation;
using MPS.Domain.Modules.ExternalDriveModules.Interfaces;
using MPS.Domain.Modules.SecurityModules.PasswordModule.Implementation;
using MPS.Domain.Modules.SecurityModules.PasswordModule.Interfaces;
using MPS.Domain.Services.Implementation;
using MPS.Domain.Services.Interfaces;

namespace MPS.Domain.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IAuthService, AuthService>();
        collection.AddScoped<IAccountService, AccountService>();
        collection.AddScoped<IMessageService, MessageService>();
        collection.AddScoped<IReportService, ReportService>();
        collection.AddScoped<IExternalDriveService, ExternalDriveService>();

        collection.AddScoped<IPasswordManager, Sha256PasswordManager>();
        collection.AddScoped<IExternalDrive, YandexDrive>();


        return collection;
    }
}