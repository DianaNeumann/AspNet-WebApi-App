using Microsoft.AspNetCore.Mvc;
using MPS.API.Models.AuthModels;
using MPS.DAL.Models;
using MPS.Domain.Dto;
using MPS.Domain.Services.Interfaces;

namespace MPS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("Register")]
    public async Task<ActionResult<AccountDto>> RegisterAsync([FromBody] RegisterAccountModel model)
    {
        if (await _service.IsUserExist(model.Login))
        {
            return BadRequest("User already exist!");
        }

        AccountDto account = await _service.RegisterAsync(model.Login, model.Role, model.Password, CancellationToken);
        return Ok(account);
    }

    [HttpPost("Login")]
    public async Task<ActionResult<string>> LoginAsync([FromBody] LoginAccountModel model)
    {
        if (!await _service.IsUserExist(model.Login))
        {
            return BadRequest("User doesn't exist.");
        }

        if (!await _service.IsPasswordCorrect(model.Login, model.Password))
        {
            return BadRequest("Incorrect Password");
        }

        Account account = await _service.GetAccountByLogin(model.Login);
        string token = _service.GenerateToken(account);
        return Ok(token);
    }
}