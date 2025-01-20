using Microsoft.EntityFrameworkCore;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Domain.Users;
using TARA.AuthenticationService.Domain.Users.Errors;
using TARA.AuthenticationService.Domain.Users.ValueObjects;
using TARA.AuthenticationService.Infrastructure.Data;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Infrastructure.Repositories;

public class UserReadRepository(ApplicationDbContext context) : IUserReadRepository
{
    public async Task<Result<User>> GetUserByIdAsync(Guid id)
    {
        UserId userId = UserId.Create(id).Value;
        var user = await context.Users.FindAsync(userId);
        return user != null
            ? user
            : UserErrors.NotFound;
    }

    public async Task<Result<User>> GetUserByNameAsync(string username)
    {
        var users = await context.Users.ToListAsync();
        var user = users.FirstOrDefault(x => x.Username.Value == username);
        return user != null
            ? user
            : UserErrors.NotFound;
    }

    public async Task<Result<User>> GetUserByEmailAsync(string email)
    {
        var users = await context.Users.ToListAsync();
        var user = users.FirstOrDefault(x => x.Email.Value == email);
        return user != null
            ? user
            : UserErrors.NotFound;
    }
}
