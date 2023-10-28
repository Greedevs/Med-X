using MedX.Data.IRepositories;
using MedX.Domain.Entities.Administrators;
using MedX.Service.Exceptions;
using MedX.Service.Helpers;
using MedX.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MedX.Service.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration configuration;
    private readonly IRepository<Administrator> repository;

    public AuthService(IConfiguration configuration, IRepository<Administrator> repository)
    {
        this.repository = repository;
        this.configuration = configuration;
    }
    public async Task<string> GenerateTokenAsync(string phone, string password)
    {
        Administrator administrator = await this.repository.GetAsync(a => a.Phone.Equals(phone));
        if (administrator is null)
            throw new NotFoundException($"This phone is not found {phone}");

        bool verifiedPassword = PasswordHash.Verify(administrator.Password, password);
        if (!verifiedPassword)
            throw new CustomException(400, "Phone or password is invalid");

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                 new Claim("Phone", administrator.Phone),
                 new Claim("Id", administrator.Id.ToString()),
                 new Claim(ClaimTypes.Role, administrator.Role.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
