using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Emi.Application.Abstractions.Security;
using Emi.Infrastructure.Options;               // usamos JwtOptions de la API
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Emi.Infrastructure.Security;

public sealed class JwtTokenService : IJwtTokenService
{
    private readonly JwtOptions _opts;
    public JwtTokenService(IOptions<JwtOptions> opts) => _opts = opts.Value;

    public IEnumerable<Claim> BuildClaims(string subject, string name, IEnumerable<string> roles)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, subject),
            new(ClaimTypes.Name, name ?? subject)
        };
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
        return claims;
    }

    public string CreateToken(string subject, string name, IEnumerable<string> roles, DateTime? expires = null)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opts.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _opts.Issuer,
            audience: _opts.Audience,
            claims: BuildClaims(subject, name, roles),
            expires: expires ?? DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}