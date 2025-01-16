using Microsoft.EntityFrameworkCore;
using TARA.AuthenticationService.Domain;
using TARA.AuthenticationService.Domain.Entities;
using TARA.AuthenticationService.Domain.Events;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Infrastructure.Data;
using TARA.Shared;

namespace TARA.AuthenticationService.Infrastructure.Repositories;
internal class UserRepository(ApplicationDbContext context, IEventStore eventStore) : IUserRepository
{
    public async Task<Result> AddUserAsync(User user)
    {
        try
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            var userCreatedEvent = new UserCreatedEvent(user.Id, user.Username.Value, user.Email.Value);
            await eventStore.SaveEventAsync(userCreatedEvent);
        }
        catch (Exception ex)
        {
            return Result.Failure(new AppError("App.Exception", ex.Message));
        }

        return Result.Success();
    }

    public Task<Result> DeleteUserAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<User>> GetUserByIdAsync(string id)
    {
        var user = await context.Users.SingleOrDefaultAsync(u => u.Id.ToString() == id);
        if (user == null)
            return Result.Failure<User>(AppErrors.UserError.NotFound);

        var userEvents = await eventStore.GetEventsAsync<UserCreatedEvent>(user.Id);

        foreach (var @event in userEvents)
        {
            if (@event != null)
                user.ApplyEvent(@event);
        }

        return user;
    }

    public async Task<Result<User>> GetUserByNameAsync(string username)
    {
        var user = await context.Users.SingleOrDefaultAsync(u => u.Username.Value == username);
        if (user == null)
            return Result.Failure<User>(AppErrors.UserError.NotFound);

        var userEvents = await eventStore.GetEventsAsync<UserCreatedEvent>(user.Id);

        foreach (var @event in userEvents)
        {
            if (@event != null)
                user.ApplyEvent(@event);
        }

        return user;
    }

    public Task<Result> UpateUserAsync(User updatedUser)
    {
        throw new NotImplementedException();
    }
}
