using Microsoft.EntityFrameworkCore;
using MPS.DAL;
using MPS.DAL.Models;
using MPS.DAL.Models.Tools;
using MPS.Domain.Dto;
using MPS.Domain.Mapping;
using MPS.Domain.Services.Interfaces;

namespace MPS.Domain.Services.Implementation;

public class AccountService : IAccountService
{
    private readonly AppDbContext _context;

    public AccountService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<AccountDto>> GetSubordinates(Guid id, CancellationToken cancellationToken)
    {
        Account chiefAccount = await _context.Accounts.FirstAsync(a => a.Id.Equals(id), cancellationToken: cancellationToken);

        Role lowerPriorityLevel = chiefAccount.Role + 1;

        return await _context.Accounts
            .Where(a => a.Role == lowerPriorityLevel)
            .Select(account => account.AsDto())
            .ToArrayAsync(cancellationToken);
    }
}