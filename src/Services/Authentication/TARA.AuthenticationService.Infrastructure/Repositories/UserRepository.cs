using Microsoft.EntityFrameworkCore;
using TARA.AuthenticationService.Domain.Entities;
using TARA.AuthenticationService.Domain.Events;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Infrastructure.Data;

namespace TARA.AuthenticationService.Infrastructure.Repositories;
internal class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IEventStore _eventStore;

    public UserRepository(ApplicationDbContext context, IEventStore eventStore)
    {
        _context = context;
        _eventStore = eventStore;
    }
    public async Task AddUserAsync(User user)
    {
        foreach (var @event in user.GetUncommittedEvents())
        {
            await _eventStore.SaveEventAsync(@event);
        }
        user.ClearUncommittedEvents();
    }

    public Task DeleteUserAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserByIdAsync(string id)
    {
        throw new NotImplementedException();
    }
    public async Task<User?> GetUserByNameAsync(string username)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName.Value == username);
        if (user == null) return null;

        var userEvents = await _eventStore.GetEventsAsync<UserCreatedEvent>(user.Id.Value);

        foreach (var @event in userEvents)
        {
            if (@event != null)
                user.ApplyEvent(@event);
        }

        return user;
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
