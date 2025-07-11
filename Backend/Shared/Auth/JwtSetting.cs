namespace Backend.Shared.Auth;

public record JwtSetting
{
    public required string Secret { get; set; }
    public required string Issuer { get; set; }
    public required int ExpirationTimeInMinute { get; set; }
    public required string Audience { get; set; }
}
