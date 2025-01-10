﻿namespace TARA.AuthenticationService.Infrastructure.Services;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}