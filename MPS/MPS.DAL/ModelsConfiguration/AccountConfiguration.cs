using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MPS.DAL.Models;
using MPS.DAL.Models.Tools;

namespace MPS.DAL.ModelsConfiguration;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(account => account.Id);

        builder.HasMany(account => account.ProcessedMessages)
            .WithOne(message => message.ProcessedAccount);

        builder.HasMany(account => account.Reports)
            .WithOne(report => report.Account);

        builder.HasData(new Account
        {
            Id = Guid.Parse("9E14545C-FF68-425D-8011-CAD65167A8BC"), 
            Login = "admin",
            PasswordHash = "8C-69-76-E5-B5-41-04-15-BD-E9-08-BD-4D-EE-15-DF-B1-67-A9-C8-73-FC-4B-B8-A8-1F-6F-2A-B4-48-A9-18",
            Role = Role.HighPriority,
            ProcessedMessages = new List<Message>(),
            Reports = new List<Report>(),
        });

        builder.HasData(new Account
        {
            Id = Guid.Parse("480BE48F-33E4-4C83-9B77-AC8BBA555840"),
            Login = "mid-acc-1",
            PasswordHash = "6B-86-B2-73-FF-34-FC-E1-9D-6B-80-4E-FF-5A-3F-57-47-AD-A4-EA-A2-2F-1D-49-C0-1E-52-DD-B7-87-5B-4B",
            Role = Role.MiddlePriority,
            ProcessedMessages = new List<Message>(),
            Reports = new List<Report>(),
        });

        builder.HasData(new Account
        {
            Id = Guid.Parse("2B760726-8AB3-41A2-A2E7-386AF971BDBF"),
            Login = "mid-acc-2",
            PasswordHash = "D4-73-5E-3A-26-5E-16-EE-E0-3F-59-71-8B-9B-5D-03-01-9C-07-D8-B6-C5-1F-90-DA-3A-66-6E-EC-13-AB-35",
            Role = Role.MiddlePriority,
            ProcessedMessages = new List<Message>(),
            Reports = new List<Report>(),
        });

        builder.HasData(new Account
        {
            Id = Guid.Parse("FA47A805-7C5C-4D8A-A07F-135902A561A4"),
            Login = "low-acc-3",
            PasswordHash = "4E-07-40-85-62-BE-DB-8B-60-CE-05-C1-DE-CF-E3-AD-16-B7-22-30-96-7D-E0-1F-64-0B-7E-47-29-B4-9F-CE",
            Role = Role.LowPriority,
            ProcessedMessages = new List<Message>(),
            Reports = new List<Report>(),
        });

        builder.HasData(new Account
        {
            Id = Guid.Parse("48659281-688F-4C4C-B917-F004DD7C3369"),
            Login = "low-acc-4",
            PasswordHash = "4B-22-77-77-D4-DD-1F-C6-1C-6F-88-4F-48-64-1D-02-B4-D1-21-D3-FD-32-8C-B0-8B-55-31-FC-AC-DA-BF-8A",
            Role = Role.LowPriority,
            ProcessedMessages = new List<Message>(),
            Reports = new List<Report>(),
        });

        builder.HasData(new Account
        {
            Id = Guid.Parse("D38719BF-256D-4D5C-9467-C34E2A614870"),
            Login = "low-acc-5",
            PasswordHash = "EF-2D-12-7D-E3-7B-94-2B-AA-D0-61-45-E5-4B-0C-61-9A-1F-22-32-7B-2E-BB-CF-BE-C7-8F-55-64-AF-E3-9D",
            Role = Role.LowPriority,
            ProcessedMessages = new List<Message>(),
            Reports = new List<Report>(),
        });
    }
}