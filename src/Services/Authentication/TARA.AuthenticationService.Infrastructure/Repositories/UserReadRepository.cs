using Microsoft.EntityFrameworkCore;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Domain.Users;
using TARA.AuthenticationService.Domain.Users.Errors;
using TARA.AuthenticationService.Infrastructure.Data;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Infrastructure.Repositories;

public class UserReadRepository(ApplicationDbContext context) : IUserReadRepository
{
    public async Task<Result<User>> GetUserByIdAsync(string id)
    {
        var user = await context.Users.SingleOrDefaultAsync(u => u.Id.ToString() == id);
        return user != null
            ? user
            : UserErrors.NotFound;
    }

    public async Task<Result<User>> GetUserByNameAsync(string username)
    {
        var user = await context.Users.SingleOrDefaultAsync(u => u.Username.Value == username);
        return user != null
            ? user
            : UserErrors.NotFound;
    }
}
