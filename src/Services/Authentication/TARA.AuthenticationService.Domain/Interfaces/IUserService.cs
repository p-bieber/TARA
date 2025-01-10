﻿using TARA.AuthenticationService.Domain.Entities;
using TARA.AuthenticationService.Domain.ValueObjects;

namespace TARA.AuthenticationService.Domain.Interfaces;
public interface IUserService
{
    Task CreateUser(string username, string password, string email);
    Task<User?> GetUserByIdAsync(string id);
    Task<(bool, UserId?)> ValidateUserAsync(string userName, string password);
}