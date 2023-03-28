using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace livro_receitas.Application.Services.Token;

public class TokenController
{
    private const string EmailAlias = "eml";
    private readonly double _tempoTokenEmMinutos;
    private readonly string _chaveDeSeguranca;

    public TokenController(double tempoTokenEmMinutos, string chaveDeSeguranca)
    {
        _tempoTokenEmMinutos = tempoTokenEmMinutos;
        _chaveDeSeguranca = chaveDeSeguranca;
    }

    public string GerarToken(string emailUsuario)
    {
        var claims = new List<Claim>
        {
            new Claim(EmailAlias, emailUsuario)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_tempoTokenEmMinutos),
            SigningCredentials = new SigningCredentials(SimetricKey(), SecurityAlgorithms.HmacSha512Signature)
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    private ClaimsPrincipal ValidarToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var paramTokenValidacao = new TokenValidationParameters
        {
            RequireExpirationTime = true,
            IssuerSigningKey = SimetricKey(),
            ClockSkew = new TimeSpan(0),
            ValidateIssuer = false,
            ValidateAudience = false,
        };

        var claims = tokenHandler.ValidateToken(token, paramTokenValidacao, out _);

        return claims;
    }

    public string RecuperarEmail(string token)
    {
        var claims = ValidarToken(token);

        return claims.FindFirst(EmailAlias).Value;
    }

    private SymmetricSecurityKey SimetricKey()
    {
        var symmetric = Convert.FromBase64String(_chaveDeSeguranca);

        return new SymmetricSecurityKey(symmetric);
    }
}