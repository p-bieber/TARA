using Microsoft.EntityFrameworkCore;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Domain.Users;
using TARA.AuthenticationService.Domain.Users.Errors;
using TARA.AuthenticationService.Infrastructure.Data;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Infrastructure.Repositories;

public class UserReadRepository(ApplicationDbContext context) : IUserReadRepository
{
    public async Task<Result<User>> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var users = await context.Users.ToListAsync(cancellationToken);
            var user = users.FirstOrDefault(x => x.Id == id);

            return user != null
                ? user
                : UserErrors.NotFound;
        }
        catch (OperationCanceledException)
        {
            return Error.CancellationRequested;
        }
    }

    public async Task<Result<User>> GetUserByNameAsync(string username, CancellationToken cancellationToken)
    {
        try
        {
            var users = await context.Users.ToListAsync(cancellationToken);
            var user = users.FirstOrDefault(x => x.Username.Value == username);
            return user != null
                ? user
                : UserErrors.NotFound;
        }
        catch (OperationCanceledException)
        {
            return Error.CancellationRequested;
        }
    }

    public async Task<Result<User>> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        try
        {
            var users = await context.Users.ToListAsync(cancellationToken);
            var user = users.FirstOrDefault(x => x.Email.Value == email);
            return user != null
                ? user
                : UserErrors.NotFound;
        }
        catch (OperationCanceledException)
        {
            return Error.CancellationRequested;
        }
    }
}
