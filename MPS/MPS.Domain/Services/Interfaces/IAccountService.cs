using MPS.Domain.Dto;

namespace MPS.Domain.Services.Interfaces;

public interface IAccountService
{
    Task<IReadOnlyCollection<AccountDto>> GetSubordinates(Guid id, CancellationToken cancellationToken);
}