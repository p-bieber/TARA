using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Domain.Users;
using TARA.AuthenticationService.Domain.Users.Errors;
using TARA.AuthenticationService.Infrastructure.Data;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Infrastructure.Repositories;
public class UserWriteRepository(ApplicationDbContext context) : IUserWriteRepository
{
    public async Task<Result> AddUserAsync(User user)
    {
        try
        {
            await context.Users.AddAsync(user);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return new Error("AddUserAsync.Error", ex.Message);
        }
    }

    public async Task<Result> UpateUserAsync(User updatedUser)
    {
        try
        {
            var user = await context.Users.FindAsync(updatedUser.Id);
            if (user == null)
                return UserErrors.NotFound;

            context.Entry(user).CurrentValues.SetValues(updatedUser);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return new Error("UpateUserAsync.Error", ex.Message);
        }
    }

    public async Task<Result> DeleteUserAsync(string id)
    {
        try
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
                return UserErrors.NotFound;
            context.Users.Remove(user);
        }
        catch (Exception ex)
        {
            return new Error("DeleteUserAsync.Error", ex.Message);
        }

        return Result.Success();
    }
}
