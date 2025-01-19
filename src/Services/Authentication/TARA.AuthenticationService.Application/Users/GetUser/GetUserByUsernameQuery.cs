﻿using TARA.AuthenticationService.Application.Abstractions;

namespace TARA.AuthenticationService.Application.Users.GetUser;

public sealed record GetUserByUsernameQuery(string Username) : IQuery<UserResponse>;
