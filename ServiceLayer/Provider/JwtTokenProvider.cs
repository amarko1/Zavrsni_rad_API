using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.ServiceModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Provider
{
    internal class JwtTokenProvider : IJwtTokenProvider
    {
        private readonly IConfiguration _configuration;

        public JwtTokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(JwtTokenBodyInfo bodyInfo)
        {
            var jwtKey = _configuration["Jwt:Key"];
            var jwtKeyBytes = Encoding.UTF8.GetBytes(jwtKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new(JwtRegisteredClaimNames.Sub, bodyInfo.Email),
                    new(ClaimTypes.Email, bodyInfo.Email)
                }),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new(new SymmetricSecurityKey(jwtKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[128];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public string Refresh(string expiredToken, string refreshToken)
        {
            var principal = GetPrincipalFromExpiredToken(expiredToken);
            var email = principal.FindFirst(ClaimTypes.Email)!.Value;

            return new JwtTokenProvider(_configuration).GenerateAccessToken(new JwtTokenBodyInfo
            {
                Email = email
            });
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var jwtKey = _configuration["Jwt:Key"]!;
            var jwtKeyBytes = Encoding.UTF8.GetBytes(jwtKey);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(jwtKeyBytes),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = (securityToken as JwtSecurityToken)!;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}
