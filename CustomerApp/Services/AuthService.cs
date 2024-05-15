using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CustomerApp.Services;

public class AuthService
{
    private readonly IConfiguration _configuration;
    private readonly PasswordHasher<object> _passwordHasher = new PasswordHasher<object>();

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public bool validateCreditials(Customer customer, string password)
    {
        return VerifyPassword(customer.password, password);
    }

    public string HashPassword(string password)
    {
        return _passwordHasher.HashPassword(null, password);
    }

    public string generateToken(string email, string role)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var crendentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddHours(1);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: crendentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
        return result == PasswordVerificationResult.Success;
    }
}