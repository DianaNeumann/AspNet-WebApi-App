using MPS.DAL.Models;
using MPS.Domain.Dto;

namespace MPS.Domain.Mapping;

public static class AccountMapping
{
    public static AccountDto AsDto(this Account account) =>
        new AccountDto(
            account.Id,
            account.Login,
            account.Role,
            account.ProcessedMessages.ToArray(),
            account.Reports.ToArray());
}