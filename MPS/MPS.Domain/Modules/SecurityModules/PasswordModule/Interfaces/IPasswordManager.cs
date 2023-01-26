namespace MPS.Domain.Modules.SecurityModules.PasswordModule.Interfaces;

public interface IPasswordManager
{
    string CreatePasswordHash(string password);
    bool VerifyPasswordHash(string password, string passwordHash);
}