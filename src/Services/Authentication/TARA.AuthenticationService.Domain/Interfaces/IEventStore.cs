using TARA.Shared.Primitives;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Domain.Interfaces;
public interface IEventStore
{
    Task<Result> SaveUncommitedEventsAsync(AggregateRoot aggregate);
    Task<Result> SaveEventAsync<T>(Guid streamId, T @event) where T : IDomainEvent;
    Task<Result<IEnumerable<T?>>> GetEventsAsync<T>(Guid aggregateId) where T : IDomainEvent;
}
