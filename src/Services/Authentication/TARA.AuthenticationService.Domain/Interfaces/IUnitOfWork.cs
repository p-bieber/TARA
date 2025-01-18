namespace TARA.AuthenticationService.Domain.Interfaces;
public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
