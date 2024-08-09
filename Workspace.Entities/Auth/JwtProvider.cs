using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Workspace.Entities;
using Workspace.Entities.Contracts;

namespace Workspace.Auth;

public class JwtProvider (IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly JwtOptions _options = options.Value;
    
    public string GenerateToken(WorkspaceUser workspaceUser)
    {
        Claim[] claims = [new (CustomClaims.UserId, workspaceUser.Id.ToString())];
        
        //алгоритм кодирования
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);
        
        //создаем токен
        var token = new JwtSecurityToken(
            claims:claims,
            signingCredentials : signingCredentials,
            expires : DateTime.UtcNow.AddHours(_options.ExpiresHours));
        
        //получаем строку из JwtSecurityToken
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
    
    
}