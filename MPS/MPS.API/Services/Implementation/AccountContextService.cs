using System.Security.Claims;
using MPS.API.Services.Interfaces;

namespace MPS.API.Services.Implementation;

public class AccountContextService : IAccountContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool GetMyId(out Guid result)
    {
        result = Guid.Empty;
        if (_httpContextAccessor.HttpContext == null)
            return false;
        string idStr = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Sid);
        return Guid.TryParse(idStr.ToUpper(), out result);
    }

    public string GetMyName()
    {
        string result = string.Empty;
        if (_httpContextAccessor.HttpContext == null) return result;
        result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);

        return result;
    }

    public string GetMyRole()
    {
        string result = string.Empty;

        if (_httpContextAccessor.HttpContext == null) return result;
        result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

        return result;
    }
}