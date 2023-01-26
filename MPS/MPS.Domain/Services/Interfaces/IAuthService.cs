using MPS.DAL.Models;
using MPS.DAL.Models.Tools;
using MPS.Domain.Dto;

namespace MPS.Domain.Services.Interfaces;

public interface IAuthService
{
    Task<AccountDto> RegisterAsync(string login, Role role, string password, CancellationToken cancellationToken);

    Task<bool> IsUserExist(string login);
    Task<bool> IsPasswordCorrect(string login, string password);

    Task<Account> GetAccountByLogin(string login);
    string GenerateToken(Account account);
}