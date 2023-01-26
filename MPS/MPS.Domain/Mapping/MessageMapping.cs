using MPS.DAL.Models;
using MPS.Domain.Dto;

namespace MPS.Domain.Mapping;

public static class MessageMapping
{
    public static MessageDto AsDto(this Message message) =>
        new MessageDto(
            message.Id,
            message.Content,
            message.Type,
            message.Source);
}