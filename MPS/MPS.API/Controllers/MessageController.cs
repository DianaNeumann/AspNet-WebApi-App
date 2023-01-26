using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MPS.API.Models;
using MPS.API.Services.Interfaces;
using MPS.Domain.Dto;
using MPS.Domain.Services.Interfaces;

namespace MPS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MessageController : ControllerBase
{
    private readonly IMessageService _service;
    private readonly IAccountContextService _accountContextService;

    public MessageController(IMessageService service, IAccountContextService accountContextService)
    {
        _service = service;
        _accountContextService = accountContextService;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("CreateMessage")]
    [AllowAnonymous]
    public async Task<ActionResult<MessageDto>> CreateMessageAsync([FromBody] CreateMessageModel model)
    {
        var message = await _service.CreateMessageAsync(model.Content, model.Source, CancellationToken);
        return Ok(message);
    }

    [HttpPost("ProcessMessage")]
    [Authorize(Roles = "LowPriority")]
    public async Task<ActionResult<MessageDto>> ProcessMessageAsync([FromBody] ProcessMessageModel model)
    {
        var accountName = _accountContextService.GetMyName();
        var message = await _service.ProcessMessageAsync(model.MessageId, accountName, CancellationToken);
        return Ok(message);
    }

    [HttpGet("GetProcessedMessages")]
    [Authorize(Roles = "LowPriority")]
    public async Task<ActionResult<string>> GetProcessedMessagesAsync()
    {
        bool isExist = _accountContextService.GetMyId(out Guid accountId);
        if (!isExist)
            return NotFound("Invalid user's Id");

        var messages = await _service.GetProcessedMessagesAsync(accountId, CancellationToken);

        return Ok(messages);
    }

    [HttpGet("GetUnprocessedMessages")]
    [Authorize(Roles = "LowPriority")]
    public async Task<ActionResult<string>> GetUnprocessedMessagesAsync()
    {
        var messages = await _service.GetUnprocessedMessagesAsync(CancellationToken);
        return Ok(messages);
    }

    [HttpGet("GetSmsUnprocessedMessages")]
    [Authorize(Roles = "LowPriority")]
    public async Task<ActionResult<string>> GetSmsUnprocessedMessagesAsync()
    {
        var messages = await _service.GetSmsUnprocessedMessagesAsync(CancellationToken);
        return Ok(messages);
    }

    [HttpGet("GetEmailUnprocessedMessages")]
    [Authorize(Roles = "LowPriority")]
    public async Task<ActionResult<string>> GetEmailUnprocessedMessagesAsync()
    {
        var messages = await _service.GetEmailUnprocessedMessagesAsync(CancellationToken);
        return Ok(messages);
    }

    [HttpGet("GetCellphoneUnprocessedMessages")]
    [Authorize(Roles = "LowPriority")]
    public async Task<ActionResult<string>> GetCellphoneUnprocessedMessagesAsync()
    {
        var messages = await _service.GetCellphoneUnprocessedMessagesAsync(CancellationToken);
        return Ok(messages);
    }
}