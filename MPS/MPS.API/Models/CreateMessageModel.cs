using MPS.DAL.Models.Tools;

namespace MPS.API.Models;

public record CreateMessageModel(string Content, MessageSource Source);