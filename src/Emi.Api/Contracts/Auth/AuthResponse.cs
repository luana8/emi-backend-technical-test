namespace Emi.Api.Contracts.Auth;

public sealed class AuthResponse
{
    public string Token     { get; set; } = default!;
    public DateTime Expires { get; set; }
}