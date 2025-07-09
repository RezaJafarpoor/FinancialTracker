using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Shared.Auth;
/// <summary>
/// Provide Method for Generating Tokens
/// </summary>
public class TokenProvider(IOptions<JwtSetting> setting)
{
    /// <summary>
    /// Method <c>GenerateJwtToken</c> Generate Jwt Token Based on userId
    /// </summary>
    /// <param name="identifier">a Guid represent UserId</param>
    /// <returns>a string represent the jwt token</returns>
    public string GenerateJwtToken(Guid identifier)
    {

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, identifier.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (JwtRegisteredClaimNames.Aud, setting.Value.Audience)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.Value.Secret));
        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: setting.Value.Issuer,
            audience: setting.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(setting.Value.ExpirationTimeInMinute),
            signingCredentials: credential
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
