using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MPS.DAL.Models;
using MPS.DAL.Models.Tools;

namespace MPS.DAL.ModelsConfiguration;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(message => message.Id);

        builder.HasData(new Message
        {
            Id = Guid.NewGuid(),
            Content = "Okay hmmm",
            Source = MessageSource.Sms,
        });

        builder.HasData(new Message
        {
            Id = Guid.NewGuid(),
            Content = "LALALAA hmmm",
            Source = MessageSource.Sms,
        });

        builder.HasData(new Message
        {
            Id = Guid.NewGuid(),
            Content = "test test",
            Source = MessageSource.Email,
        });

        builder.HasData(new Message
        {
            Id = Guid.NewGuid(),
            Content = "tri kota",
            Source = MessageSource.Email,
        });

        builder.HasData(new Message
        {
            Id = Guid.NewGuid(),
            Content = "jopa",
            Source = MessageSource.Cellphone,

        });

        builder.HasData(new Message
        {
            Id = Guid.NewGuid(),
            Content = "jmyaaa",
            Source = MessageSource.Cellphone,

        });
    }
}