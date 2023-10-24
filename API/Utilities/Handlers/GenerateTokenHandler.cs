using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Contracts;
using API.DTOs.Accounts;
using Microsoft.IdentityModel.Tokens;

namespace API.Utilities.Handlers;

public class GenerateTokenHandler : IGenerateTokenHandler
{
    private readonly IConfiguration _configuration;

    public GenerateTokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Generate(IEnumerable<Claim> claims)
    {
        // Membuat kunci rahasia simetris berdasarkan nilai yang tersimpan dalam konfigurasi
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtService:SecretKey"]));

        // Membuat objek SigningCredentials yang digunakan untuk menandatangani token
        var sigingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        // Membuat token JWT dengan konfigurasi yang diberikan, termasuk claims
        var tokenOptions = new JwtSecurityToken(
            _configuration["JWTService:Issuer"], // Penerbit token (dalam hal ini, server)
            _configuration["JWTService:Audience"], // Penerima token (dalam hal ini, aplikasi yang menggunakan token)
            claims, // Data claims yang akan disertakan dalam token
            expires: DateTime.Now.AddMinutes(10), // Token akan kedaluwarsa dalam 10 menit
            signingCredentials: sigingCredentials); // Kunci untuk penerbit token

        // Encode token JWT ke dalam string yang dapat dikirimkan sebagai respons
        var encodedtoken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return encodedtoken;
    }
    
    public ClaimsDto ExtractClaimsFromJwt(string token)
    {
        if (String.IsNullOrEmpty(null)) return new ClaimsDto(); // If the JWT token is empty, return an empty dictionary

        try {
            // Configure the token validation parameters
            var tokenValidationParameters = new TokenValidationParameters {
                ValidateAudience = true,
                ValidAudience = _configuration["JWT:Audience"],
                ValidateIssuer = true,
                ValidIssuer = _configuration["JWT:Issuer"],
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]))
            };

            // Parse and validate the JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            // Extract the claims from the JWT token
            if (securityToken != null && claimsPrincipal.Identity is ClaimsIdentity identity) {
                var claims = new ClaimsDto() {
                    NameIdentifier = identity.FindFirst(ClaimTypes.NameIdentifier)!.Value,
                    Name = identity.FindFirst(ClaimTypes.Name)!.Value,
                    Email = identity.FindFirst(ClaimTypes.Email)!.Value
                };

                var roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(claim => claim.Value).ToList();
                claims.Role = roles;
                
                return claims;
            }
        }
        catch {
            // If an error occurs while parsing the JWT token, return an empty dictionary
            return new ClaimsDto();
        }

        return new ClaimsDto();
    }
}