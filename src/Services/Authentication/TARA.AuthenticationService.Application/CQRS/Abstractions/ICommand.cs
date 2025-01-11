using MediatR;
using TARA.Shared;

namespace TARA.AuthenticationService.Application.CQRS.Abstractions;
public interface ICommand
    : IRequest<Result>
{ }
public interface ICommand<TResponse>
    : IRequest<Result<TResponse>>
{ }
