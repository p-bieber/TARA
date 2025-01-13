using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TARA.AuthenticationService.Api.Controllers;

public abstract class BaseController : ControllerBase
{
    protected readonly ILogger _logger;
    protected readonly ISender _sender;

    protected BaseController(ILogger logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }
}
