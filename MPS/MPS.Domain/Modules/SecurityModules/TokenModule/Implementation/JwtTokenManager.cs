using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using MPS.DAL.Models;
using MPS.Domain.Config;

namespace MPS.Domain.Modules.SecurityModules.TokenModule.Implementation;

public static class JwtTokenManager
{
    public static string CreateToken(Account account)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, account.Login),
            new Claim(ClaimTypes.Role, account.Role.ToString()),
            new Claim(ClaimTypes.Sid, account.Id.ToString()),
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(DomainConfig.JwtToken));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        string jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}