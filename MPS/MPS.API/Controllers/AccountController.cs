using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MPS.API.Services.Interfaces;
using MPS.Domain.Services.Interfaces;

namespace MPS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly IAccountService _service;
    private readonly IAccountContextService _accountContextService;

    public AccountController(IAccountService service, IAccountContextService accountContextService)
    {
        _service = service;
        _accountContextService = accountContextService;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpGet("GetMyName")]
    [Authorize]
    public ActionResult<string> GetMyName()
    {
        var userName = _accountContextService.GetMyName();
        return Ok(userName);
    }

    [HttpGet("GetMyRole")]
    [Authorize]
    public ActionResult<string> GetMyRole()
    {
        var role = _accountContextService.GetMyRole();
        return Ok(role);
    }

    [HttpGet("GetMyId")]
    [Authorize]
    public ActionResult<Guid> GetMyId()
    {
        bool isExist = _accountContextService.GetMyId(out Guid result);
        return isExist ? Ok(result) : NotFound("Invalid user's Id");
    }

    [HttpGet("GetSubordinates")]
    [Authorize]
    public ActionResult<string> GetSubordinatesAsync()
    {
        bool isExist = _accountContextService.GetMyId(out Guid id);
        if (!isExist)
            return NotFound("Invalid user's Id");

        var accounts = _service.GetSubordinates(id, CancellationToken);
        return Ok(accounts);
    }
}