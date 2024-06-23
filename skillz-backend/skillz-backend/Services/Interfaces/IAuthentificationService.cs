using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using skillz_backend.models;

namespace skillz_backend.Services
{
    // Interface defining operations related to authentication in the service.
    public interface IAuthenticationService
    {
        // Generates a JWT token based on the provided user information.
        string GenerateToken(User user);
    }
}
