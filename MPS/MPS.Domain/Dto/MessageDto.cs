using MPS.DAL.Models.Tools;

namespace MPS.Domain.Dto;

public record MessageDto(Guid Id, string Content, MessageType Type, MessageSource Source);