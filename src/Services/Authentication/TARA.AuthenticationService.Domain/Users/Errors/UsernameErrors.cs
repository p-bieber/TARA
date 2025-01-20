﻿using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Domain.Users.Errors;
public static class UsernameErrors
{
    public static Error Empty =>
        new("Username.Empty", "Username is empty");
    public static Error TooLong =>
        new("Username.TooLong", "Username is too long");
    public static Error TooShort =>
        new("Username.TooShort", "Username is too short");
    public static Error InvalidCharacters =>
        new("Username.InvalidCharacters", "Username contains invalid characters");
    public static Error AlreadyExists =>
        new("Username.AlreadyExists", "Username already exists");
}
