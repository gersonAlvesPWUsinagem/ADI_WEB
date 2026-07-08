using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ADI_WEB.Security;

/// <summary>
/// Creates an ASP.NET Core principal from the domain JWT while preserving original claims.
/// </summary>
public sealed class JwtPrincipalFactory
{
    private readonly JwtAuthenticationOptions _options;
    private readonly ILogger<JwtPrincipalFactory> _logger;
    private readonly JwtTokenNormalizer _tokenNormalizer;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtPrincipalFactory"/> class.
    /// </summary>
    public JwtPrincipalFactory(
        IOptions<JwtAuthenticationOptions> options,
        ILogger<JwtPrincipalFactory> logger,
        JwtTokenNormalizer tokenNormalizer)
    {
        _options = options.Value;
        _logger = logger;
        _tokenNormalizer = tokenNormalizer;
    }

    /// <summary>
    /// Validates a JWT and creates a <see cref="ClaimsPrincipal"/>.
    /// </summary>
    public ClaimsPrincipal CreatePrincipal(string token)
    {
        var normalizedToken = _tokenNormalizer.Normalize(token);
        var handler = new JwtSecurityTokenHandler
        {
            MapInboundClaims = false
        };

        var parameters = CreateValidationParameters();
        var principal = handler.ValidateToken(normalizedToken, parameters, out _);
        var originalClaims = principal.Claims.ToList();
        var claims = new List<Claim>(originalClaims);

        AddClaimIfMissing(claims, ClaimTypes.Name, originalClaims.FirstOrDefault(x => x.Type == "Name")?.Value);
        AddClaimIfMissing(claims, ClaimTypes.NameIdentifier, originalClaims.FirstOrDefault(x => x.Type == "Matricula")?.Value);
        AddClaimIfMissing(claims, ClaimTypes.Role, originalClaims.FirstOrDefault(x => x.Type == "Level")?.Value);

        var identity = new ClaimsIdentity(
            claims,
            JwtAuthenticationDefaults.AuthenticationScheme,
            ClaimTypes.Name,
            ClaimTypes.Role);

        _logger.LogInformation("Claims carregadas: {ClaimCount}", claims.Count);
        return new ClaimsPrincipal(identity);
    }

    private TokenValidationParameters CreateValidationParameters()
    {
        var parameters = new TokenValidationParameters
        {
            ValidateLifetime = _options.ValidateLifetime,
            ValidateIssuer = _options.ValidateIssuer,
            ValidateAudience = _options.ValidateAudience,
            ValidateIssuerSigningKey = _options.ValidateSigningKey,
            RequireExpirationTime = _options.ValidateLifetime,
            RequireSignedTokens = _options.ValidateSigningKey,
            SignatureValidator = _options.ValidateSigningKey ? null : CreateUnsignedToken,
            ClockSkew = _options.ClockSkew,
            NameClaimType = ClaimTypes.Name,
            RoleClaimType = ClaimTypes.Role
        };

        if (!string.IsNullOrWhiteSpace(_options.Issuer))
        {
            parameters.ValidIssuer = _options.Issuer;
        }

        if (!string.IsNullOrWhiteSpace(_options.Audience))
        {
            parameters.ValidAudience = _options.Audience;
        }

        if (_options.ValidateSigningKey && !string.IsNullOrWhiteSpace(_options.SigningKey))
        {
            parameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SigningKey));
        }

        return parameters;
    }

    private static void AddClaimIfMissing(ICollection<Claim> claims, string type, string? value)
    {
        if (string.IsNullOrWhiteSpace(value) || claims.Any(x => x.Type == type))
        {
            return;
        }

        claims.Add(new Claim(type, value));
    }

    private static SecurityToken CreateUnsignedToken(string token, TokenValidationParameters parameters)
    {
        return new JwtSecurityToken(token);
    }
}
