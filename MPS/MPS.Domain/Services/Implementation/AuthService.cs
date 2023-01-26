using Microsoft.EntityFrameworkCore;
using MPS.DAL;
using MPS.DAL.Models;
using MPS.DAL.Models.Tools;
using MPS.Domain.Dto;
using MPS.Domain.Mapping;
using MPS.Domain.Modules.SecurityModules.PasswordModule.Interfaces;
using MPS.Domain.Modules.SecurityModules.TokenModule.Implementation;
using MPS.Domain.Services.Interfaces;

namespace MPS.Domain.Services.Implementation;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IPasswordManager _passwordManager;

    public AuthService(AppDbContext context, IPasswordManager passwordManager)
    {
        _context = context;
        _passwordManager = passwordManager;
    }

    public async Task<AccountDto> RegisterAsync(string login, Role role, string password, CancellationToken cancellationToken)
    {
        string passwordHash = _passwordManager.CreatePasswordHash(password);

        var account = new Account(Guid.NewGuid(), login, role)
        {
            PasswordHash = passwordHash,
        };

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync(cancellationToken);

        return account.AsDto();
    }

    public async Task<bool> IsUserExist(string login)
    {
        return await _context.Accounts.AnyAsync(a => a.Login.Equals(login));
    }

    public async Task<bool> IsPasswordCorrect(string login, string password)
    {
        Account account = await GetAccountByLogin(login);

        bool isPasswordCorrect = _passwordManager.VerifyPasswordHash(password, account.PasswordHash);
        return isPasswordCorrect;
    }

    public Task<Account> GetAccountByLogin(string login)
    {
        return _context.Accounts.FirstAsync(a => a.Login.Equals(login));
    }

    public string GenerateToken(Account account) => JwtTokenManager.CreateToken(account);
}