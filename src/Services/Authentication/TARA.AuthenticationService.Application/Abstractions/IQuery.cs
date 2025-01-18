using MediatR;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Application.Abstractions;

public interface IQuery<TResponse>
    : IRequest<Result<TResponse>>
{ }