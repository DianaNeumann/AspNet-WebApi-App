using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MPS.API.Services.Interfaces;
using MPS.Domain.Services.Interfaces;

namespace MPS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;
    private readonly IExternalDriveService _externalDriveService;
    private readonly IAccountContextService _accountContextService;

    public ReportController(IReportService reportService, IExternalDriveService externalDriveService, IAccountContextService accountContextService)
    {
        _reportService = reportService;
        _externalDriveService = externalDriveService;
        _accountContextService = accountContextService;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("CreateReport")]
    [Authorize]
    public async Task<ActionResult<string>> CreateReportAsync()
    {
        bool isExist = _accountContextService.GetMyId(out Guid id);
        if (!isExist)
            return NotFound("Invalid user's Id");

        var reportName = await _reportService.CreateReportAsync(id, CancellationToken);
        var result = await _externalDriveService.UploadToDriveASync(reportName, CancellationToken);

        return Ok(result);
    }
}