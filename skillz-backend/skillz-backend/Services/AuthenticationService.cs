using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using skillz_backend.models;

namespace skillz_backend.Services
{
    // Service responsible for authentication-related operations.
    public class AuthenticationService : IAuthenticationService
    {
        private readonly SymmetricSecurityKey _key;

        // Constructor that initializes the service with the provided IConfiguration.
        public AuthenticationService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        // Generates a JWT token for the specified user.
        public string GenerateToken(User user)
        {
            // Creates a list of claims, including the user's username.
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Username)
            };

            // Creates credentials with the key and a specified algorithm.
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            // Creates a token descriptor with subject, expiration, and signing credentials.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            // Creates a JWT security token handler.
            var tokenHandler = new JwtSecurityTokenHandler();

            // Creates a JWT token using the token descriptor.
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Writes the JWT token as a string.
            return tokenHandler.WriteToken(token);
        }
    }
}
