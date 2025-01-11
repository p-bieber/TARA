using MediatR;
using TARA.Shared;

namespace TARA.AuthenticationService.Application.CQRS.Abstractions;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{ }