using MediatR;
using TARA.Shared;

namespace TARA.AuthenticationService.Application.CQRS.Abstractions;

public interface IQuery<TResponse> 
    : IRequest<Result<TResponse>> 
{ }