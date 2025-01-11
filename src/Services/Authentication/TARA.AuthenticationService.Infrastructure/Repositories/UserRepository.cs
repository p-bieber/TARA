using TARA.AuthenticationService.Domain.Entities;
using TARA.AuthenticationService.Domain.Interfaces;

namespace TARA.AuthenticationService.Infrastructure.Repositories;
internal class UserRepository : IUserRepository
{
    public Task AddUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetUserIdAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task UpateUserAsync(User updatedUser)
    {
        throw new NotImplementedException();
    }
}
