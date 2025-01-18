using MediatR;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Application.Abstractions;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{ }