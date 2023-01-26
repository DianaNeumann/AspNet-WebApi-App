namespace MPS.API.Services.Interfaces;

public interface IAccountContextService
{
    bool GetMyId(out Guid result);
    string GetMyName();
    string GetMyRole();
}