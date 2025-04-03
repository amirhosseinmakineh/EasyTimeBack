﻿using EasyTime.Model.Models;
using System.Security.Claims;

namespace EasyTime.Application.Contract.IServices
{
    public interface ITokenGenerator
    {
        string GenerateToken(User user);
        ClaimsPrincipal ValidateToken(string token);
    }
}
