using MediatR;
using Microsoft.AspNetCore.Mvc;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Api.Controllers;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected readonly ILogger _logger;
    protected readonly ISender _sender;

    protected ApiController(ILogger logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    protected IActionResult HandleFailure(Result result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult => BadRequest(CreateProblemDetails("Validation Error",
                                                                                  StatusCodes.Status400BadRequest,
                                                                                  result.Error,
                                                                                  validationResult.Errors)),
            _ => BadRequest(CreateProblemDetails("Bad Request",
                                                 StatusCodes.Status400BadRequest,
                                                 result.Error))
        };

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null) =>
        new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };

}
