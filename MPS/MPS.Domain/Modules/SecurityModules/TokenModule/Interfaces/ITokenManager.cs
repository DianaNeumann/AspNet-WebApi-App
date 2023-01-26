using MPS.DAL.Models;

namespace MPS.Domain.Modules.SecurityModules.TokenModule.Interfaces;

public interface ITokenManager
{
    string CreateToken(Account account);
}