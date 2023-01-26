using MPS.DAL.Models;
using MPS.DAL.Models.Tools;

namespace MPS.Domain.Dto;

public record AccountDto(Guid Id, string Login, Role Role, IReadOnlyCollection<Message> ProcessMessages, IReadOnlyCollection<Report> Reports);