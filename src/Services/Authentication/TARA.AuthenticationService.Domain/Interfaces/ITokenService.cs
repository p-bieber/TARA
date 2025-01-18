﻿using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Domain.Interfaces;

public interface ITokenService
{
    Result<string> GenerateToken(string userId);
}