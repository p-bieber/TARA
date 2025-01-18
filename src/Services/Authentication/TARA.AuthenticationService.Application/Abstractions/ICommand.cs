using MediatR;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Application.Abstractions;
public interface ICommand
    : IRequest<Result>
{ }
public interface ICommand<TResponse>
    : IRequest<Result<TResponse>>
{ }
