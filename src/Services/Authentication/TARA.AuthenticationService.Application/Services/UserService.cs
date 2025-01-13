using FluentValidation;
using TARA.AuthenticationService.Application.Factories;
using TARA.AuthenticationService.Domain;
using TARA.AuthenticationService.Domain.Entities;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Infrastructure.Services;
using TARA.Shared;

namespace TARA.AuthenticationService.Application.Services;
public class UserService(IUserRepository userRepository, IPasswordHasher passwordHasher) : IUserService
{
    private readonly UserFactory _userFactory = new(passwordHasher);

    public async Task<Result<User>> GetUserByNameAsync(string name)
    {
        return await userRepository.GetUserByNameAsync(name);
    }

    public async Task<Result<User>> VerifyUserPasswordAsync(string username, string password)
    {
        var userResult = await userRepository.GetUserByNameAsync(username);
        if (userResult.IsSuccess)
        {
            var isPasswordVerified = passwordHasher.VerifyPassword(password, userResult.Value.Password.Value);
            if (isPasswordVerified)
                return Result.Success(userResult.Value);
        }

        return Result.Failure<User>(AppErrors.UserError.WrongLoginCredientials);
    }

    public async Task<Result> CreateUser(string username, string password, string email)
    {
        try
        {
            var user = await _userFactory.Create(username, password, email);
            return await userRepository.AddUserAsync(user);

        }
        catch (ValidationException ex)
        {
            return Result.Failure(new AppError("CreateUser.Validation", ex.Message));
        }
        catch (Exception ex)
        {
            return Result.Failure(new AppError("CreateUser.Exception", ex.Message));
        }
    }
}
