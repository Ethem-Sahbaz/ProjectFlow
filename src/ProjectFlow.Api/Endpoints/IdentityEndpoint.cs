using ProjectFlow.Contracts.Identity;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace ProjectFlow.Api.Endpoints;

public static class IdentityEndpoint
{
    private const string TokenSecret = "ThisTokenSecretShouldDefinetlyNotBeHere";
    private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(8);

    public static WebApplication UseIdentityEndpoint(this WebApplication app)
    {
        app.MapPost(ApiEndpoints.Identity.Token, (TokenGenerationRequest request) =>
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(TokenSecret);
            
            var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, request.Email),
            new(JwtRegisteredClaimNames.Email, request.Email),
            new("userid", request.UserId.ToString())
        };

            foreach (var claimPair in request.CustomClaims)
            {
                var jsonElement = (JsonElement)claimPair.Value;
                var valueType = jsonElement.ValueKind switch
                {
                    JsonValueKind.True => ClaimValueTypes.Boolean,
                    JsonValueKind.False => ClaimValueTypes.Boolean,
                    JsonValueKind.Number => ClaimValueTypes.Double,
                    _ => ClaimValueTypes.String
                };

                var claim = new Claim(claimPair.Key, claimPair.Value.ToString()!, valueType);
                claims.Add(claim);
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TokenLifetime),
                Issuer = "https://id.ethemsahbaz.com",
                Audience = "https://projectflow.ethemsahbaz.com",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var jwt = tokenHandler.WriteToken(token);

            return Results.Ok(jwt);

        })
        .WithName("CreateToken")
        .WithOpenApi();


        return app;
    }
}
