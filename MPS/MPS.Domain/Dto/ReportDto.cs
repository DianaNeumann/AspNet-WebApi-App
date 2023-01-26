namespace MPS.Domain.Dto;

public record ReportDto(Guid Id, string Link, DateTime CreationDate, AccountDto Account);