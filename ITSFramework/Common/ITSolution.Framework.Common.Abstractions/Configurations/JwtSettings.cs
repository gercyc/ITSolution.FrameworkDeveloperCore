namespace ITSolution.Framework.Core.Server.BaseClasses.Configurators;

public class JwtSettings
{
    public string? Key { get; set; }
    public int TokenExpirationInMinutes { get; set; }

    public int RefreshTokenExpirationInDays { get; set; }
}