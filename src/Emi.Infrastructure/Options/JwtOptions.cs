namespace Emi.Infrastructure.Options;

public sealed class JwtOptions
{
    public string Issuer  { get; set; } = "emi";
    public string Audience{ get; set; } = "emi-clients";
    public string Key     { get; set; } = "change-this-in-prod-super-secret";
}