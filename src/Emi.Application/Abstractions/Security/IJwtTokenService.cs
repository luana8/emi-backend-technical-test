using System.Security.Claims;

namespace Emi.Application.Abstractions.Security;

public interface IJwtTokenService
{
    string CreateToken(string subject, string name, IEnumerable<string> roles, DateTime? expires = null);
    IEnumerable<Claim> BuildClaims(string subject, string name, IEnumerable<string> roles);
}
