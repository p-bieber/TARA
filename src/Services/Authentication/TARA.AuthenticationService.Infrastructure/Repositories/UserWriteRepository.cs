using Microsoft.EntityFrameworkCore;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Domain.Users;
using TARA.AuthenticationService.Domain.Users.Errors;
using TARA.AuthenticationService.Infrastructure.Data;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Infrastructure.Repositories;
public class UserWriteRepository(ApplicationDbContext context) : IUserWriteRepository
{
    public async Task<Result> AddUserAsync(User user, CancellationToken cancellationToken)
    {
        try
        {
            await context.Users.AddAsync(user, cancellationToken);
            return Result.Success();
        }
        catch (OperationCanceledException)
        {
            return Error.CancellationRequested;
        }
        catch (Exception ex)
        {
            return new Error("AddUserAsync.Error", ex.Message);
        }
    }

    public async Task<Result> UpateUserAsync(User updatedUser, CancellationToken cancellationToken)
    {
        try
        {
            var users = await context.Users.ToListAsync(cancellationToken);
            var user = users.FirstOrDefault(x => x.Id == updatedUser.Id);
            if (user == null)
                return UserErrors.NotFound;

            context.Entry(user).CurrentValues.SetValues(updatedUser);
            return Result.Success();
        }
        catch (OperationCanceledException)
        {
            return Error.CancellationRequested;
        }
        catch (Exception ex)
        {
            return new Error("UpateUserAsync.Error", ex.Message);
        }
    }

    public async Task<Result> DeleteUserAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var users = await context.Users.ToListAsync(cancellationToken);
            var user = users.FirstOrDefault(x => x.Id == id);
            if (user == null)
                return UserErrors.NotFound;
            context.Users.Remove(user);
        }
        catch (OperationCanceledException)
        {
            return Error.CancellationRequested;
        }
        catch (Exception ex)
        {
            return new Error("DeleteUserAsync.Error", ex.Message);
        }

        return Result.Success();
    }
}
